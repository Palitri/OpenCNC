#include "SerialTransmission.h"

#include "Board.h"
#include "Math.h"
#include "ShiftRegister.h"
#include "BinaryShiftInput.h"

#include "IDeviceDriver.h"
#include "StepMotorDriver.h"
#include "LaserDriver.h"

#include "IInterpolator.h"
#include "LinearInterpolator.h"
#include "SineInterpolator.h"

#include "EEPROMStream.h"


//#include <Arduino.h>

struct CNCMotorSettings
{
  int fullStepsPerTurn;
  float distanceUnitsPerTurn;
};

struct CNCMotorControllerSteppingMode
{
  int stepMultiplier;
  int controllerBits;
};

class CNCMotorControllerSettings
{
private:
  int stepModesBitMask;

public:
  int bitsAwake;
  int bitsDisabled;

  int stepModesCount;
  CNCMotorControllerSteppingMode *stepModes;

  bool isSleepMode, isEnabled;
  int mode;

  CNCMotorControllerSettings(int stepModesCount, int bitsAwake, int bitsDisabled)
  {
    this->stepModesCount = stepModesCount;
    this->stepModes = new CNCMotorControllerSteppingMode[this->stepModesCount];

    this->bitsAwake = bitsAwake;
    this->bitsDisabled = bitsDisabled;

    this->stepModesBitMask = 0;

    this->isSleepMode = true;
    this->isEnabled = false;
    this->mode = 0;
  }

  ~CNCMotorControllerSettings()
  {
    delete[] this->stepModes;
  }

  void SetStepMode(int index, int stepMultiplier, int controllerBits)
  {
    this->stepModes[index].stepMultiplier = stepMultiplier;
    this->stepModes[index].controllerBits = controllerBits;

    this->stepModesBitMask |= controllerBits;
  }

  int GetControllerBits()
  {
    int result = this->stepModes[this->mode].controllerBits;

    if (!this->isSleepMode)
      result |= this->bitsAwake;

    if (!this->isEnabled)
      result |= this->bitsDisabled;
    
    return result;
  }
};

class CNCSettings
{
public:
  unsigned char motorsCount;
  CNCMotorSettings *motorSettings;

  CNCSettings()
  {
    this->motorsCount = 0;
    
    this->SetMotorsCount(4);
    this->motorSettings[0].fullStepsPerTurn = 200;
    this->motorSettings[0].distanceUnitsPerTurn = 8.0f;
    this->motorSettings[1].fullStepsPerTurn = 200;
    this->motorSettings[1].distanceUnitsPerTurn = 8.0f;
    this->motorSettings[2].fullStepsPerTurn = 200;
    this->motorSettings[2].distanceUnitsPerTurn = 8.0f;
    this->motorSettings[3].fullStepsPerTurn = 200;
    this->motorSettings[3].distanceUnitsPerTurn = 8.0f;
  }

  ~CNCSettings()
  {
    this->SetMotorsCount(0);
  }

  void SetMotorsCount(int motorsCount)
  {
    if (motorsCount == this->motorsCount)
      return;
      
    if (this->motorsCount != 0)
      delete[] this->motorSettings;

    if (motorsCount > 0)
      this->motorSettings = new CNCMotorSettings[motorsCount];
    this->motorsCount = this->motorsCount;
  }
  
  void SetAddress(unsigned short address)
  {
    EEPROMStream *stream = new EEPROMStream();
    stream->WriteUInt16(address);
  }
  
  bool Load()
  {
    EEPROMStream *stream = new EEPROMStream();
    int address = stream->ReadUInt16();
    stream->Seek(address);

    unsigned char motorsCount = stream->ReadUInt8();
    unsigned short crc = stream->ReadUInt16();
    if (Math::CRC16(&motorsCount, 0, 1) != crc)
    {
      delete stream;
      return false;
    }
    
    this->SetMotorsCount(motorsCount);
    stream->ReadData(this->motorSettings, 0, sizeof(CNCMotorSettings) * this->motorsCount);
    crc = stream->ReadUInt16();
    
    delete stream;

    return Math::CRC16(this->motorSettings, sizeof(CNCMotorSettings) * this->motorsCount) == crc;
  };

  void Save()
  {
    EEPROMStream *stream = new EEPROMStream();
    int address = stream->ReadUInt16();
    stream->Seek(address);

    stream->WriteUInt8(this->motorsCount);
    stream->WriteUInt16(Math::CRC16(&this->motorsCount, 1));
    stream->WriteData(this->motorSettings, 0, sizeof(CNCMotorSettings) * this->motorsCount);
    stream->WriteUInt16(Math::CRC16(this->motorSettings, sizeof(CNCMotorSettings) * this->motorsCount));
  };
};

enum CNCState
{
  CNCState_None    = 0,
  CNCState_Ready   = 1,
  CNCState_Busy    = 2
};

class CNC 
  : public SerialTransmission
{
private:

  static const byte CommandCode_Info = 0xB2;
  static const byte CommandCode_Wait = 0x01;
  static const byte CommandCode_Turn = 0x02;
  static const byte CommandCode_Tone = 0x03;
  static const byte CommandCode_SleepMode = 0x04;
  static const byte CommandCode_ToolPowerMode = 0x05;
  static const byte CommandCode_MotorPowerMode = 0x06;
  static const byte CommandCode_MotorStepMode = 0x07;
  static const byte CommandCode_IssueTurning = 0x08;
  static const byte CommandCode_MapDriver = 0x09;
  static const byte CommandCode_SetPower = 0x11;
  static const byte CommandCode_SetSpeed = 0x12;
  static const byte CommandCode_Polyline = 0x13;
  static const byte CommandCode_Arc = 0x14;
  static const byte CommandCode_Bezier = 0x15;
  static const byte CommandCode_Drive = 0x20;
  static const byte CommandCode_DriveLinear = 0x21;
  static const byte CommandCode_DriveSine = 0x22;  
  
  static const byte ResponseCode_Info = 0xAC;
  static const byte ResponseCode_State = 0x01;
  
  static const int motorsCount = 4;
  static const int motorGroupsCount = 2;

  static const int devicesCount = motorsCount + 1;

  static const int binaryInputsCount = 8;

  static const byte toolPowerModeBitEnable  = 0b00000001;

  int analogInputReadPin;

  long issuedIntervals[motorsCount] = { 0, 0, 0, 0 };
  unsigned long issuedTimes[motorsCount] = { 0, 0, 0, 0 };
  long issuedTargetIntervals[motorsCount] = {0, 0, 0, 0 };
  unsigned long issuedTransitionTime[motorsCount] = { 0, 0, 0, 0 };
  unsigned long routinesTime, routinesInterval;

  float *bezierCP, *bezierSubP;
  int bezierCPCount;
  
  float spd = 1.0f;

  CNCSettings settings;
  bool toolPowered;

  StepMotorDriver *motorDrivers[motorsCount];
  LaserDriver *laserDriver;
  ShiftRegister *motorGroup1Bits, *motorGroup2Bits;
  BinaryShiftInput *binaryInput;
  CNCMotorControllerSettings *motorGroupSettings[motorGroupsCount];

  IDeviceDriver *driversListing[devicesCount];
  IDeviceDriver *driversMapping[devicesCount];

  IInterpolator *interpolators[devicesCount];
  int interpolatorsCount;

  
public:

  CNC(HardwareSerial *serial, int m1, int d1, int m2, int d2, int m3, int d3, int m4, int d4, int tool, int shiftSerialPin, int shiftStoragePin, int shift1ClockPin, int shift2ClockPin, int shift3ClockPin, int binaryInputReadPin) 
    : SerialTransmission(serial) 
  { 
    this->motorDrivers[0] = new StepMotorDriver(m1, d1, this->settings.motorSettings[0].distanceUnitsPerTurn, this->settings.motorSettings[0].fullStepsPerTurn);
    this->motorDrivers[1] = new StepMotorDriver(m2, d2, this->settings.motorSettings[1].distanceUnitsPerTurn, this->settings.motorSettings[1].fullStepsPerTurn);
    this->motorDrivers[2] = new StepMotorDriver(m3, d3, this->settings.motorSettings[2].distanceUnitsPerTurn, this->settings.motorSettings[2].fullStepsPerTurn);
    this->motorDrivers[3] = new StepMotorDriver(m4, d4, this->settings.motorSettings[3].distanceUnitsPerTurn, this->settings.motorSettings[3].fullStepsPerTurn);

    this->laserDriver = new LaserDriver(tool);

    this->motorGroup1Bits = new ShiftRegister(shiftSerialPin, shiftStoragePin, shift1ClockPin, 8);
    this->motorGroup2Bits = new ShiftRegister(shiftSerialPin, shiftStoragePin, shift2ClockPin, 8);
    
    this->binaryInput = new BinaryShiftInput(shiftSerialPin, shiftStoragePin, shift3ClockPin, binaryInputReadPin, 8);

    this->motorGroupSettings[0] = new CNCMotorControllerSettings(5, 0b00100000, 0b00000010);
    this->motorGroupSettings[0]->SetStepMode(0, 1,  0b00000000);
    this->motorGroupSettings[0]->SetStepMode(1, 2,  0b00000100);
    this->motorGroupSettings[0]->SetStepMode(2, 4,  0b00001000);
    this->motorGroupSettings[0]->SetStepMode(3, 8,  0b00001100);
    this->motorGroupSettings[0]->SetStepMode(4, 16, 0b00011100);
    this->motorGroupSettings[0]->mode = 2;

    this->motorGroupSettings[1] = new CNCMotorControllerSettings(5, 0b00001000, 0b00010000);
    this->motorGroupSettings[1]->SetStepMode(0, 1,  0b00000000);
    this->motorGroupSettings[1]->SetStepMode(1, 2,  0b10000000);
    this->motorGroupSettings[1]->SetStepMode(2, 4,  0b01000000);
    this->motorGroupSettings[1]->SetStepMode(3, 8,  0b11000000);
    this->motorGroupSettings[1]->SetStepMode(4, 16, 0b11100000);
    this->motorGroupSettings[1]->mode = 2;
      
    this->driversListing[0] = this->motorDrivers[0];
    this->driversListing[1] = this->motorDrivers[1];
    this->driversListing[2] = this->motorDrivers[2];
    this->driversListing[3] = this->motorDrivers[3];
    this->driversListing[4] = this->laserDriver;

    this->driversMapping[0] = this->driversListing[0];
    this->driversMapping[1] = this->driversListing[1];
    this->driversMapping[2] = this->driversListing[2];

    this->interpolatorsCount = 0;

    this->toolPowered = false;

    this->SetPower(0.0f);
    this->SetModeControlBits();

    this->UpdateBinaryInput();
  
    this->bezierCPCount = 0;

    this->routinesTime = micros();
    this->routinesInterval = 50000;
  };

  void Plot(float time)
  {
    unsigned long timeMicroseconds = time * 1000000;
    unsigned long now = micros();
    unsigned long start = now;
    unsigned long end = now + timeMicroseconds;
  
    while (now < end)
    {
      float u = (float)(now - start) / (float)timeMicroseconds;
      for (int i = 0; i < this->interpolatorsCount; i++)
        this->interpolators[i]->Run(u);

      now = micros();
    }
  }

  void ClearInterpolators()
  {
    for (int i = 0; i < this->interpolatorsCount; i++)
      delete this->interpolators[i];

    this->interpolatorsCount = 0;
  }

  float ReadAnalogInputPin()
  {
    return analogRead(this->analogInputReadPin) / (float)Board::analogReadMaxValue;
  }
  
  void SetPower(float power)
  {
    this->laserDriver->SetPower(power);
  }
  
  void SetSpeed(float speed)
  {
    this->spd = speed;
  }

  void SetModeControlBits()
  {
    int toolBits = this->toolPowered ? CNC::toolPowerModeBitEnable : 0;
   
    this->motorGroup1Bits->SetBits(this->motorGroupSettings[0]->GetControllerBits() | toolBits);

    this->motorGroup2Bits->SetBits(this->motorGroupSettings[1]->GetControllerBits());
  }

  bool UpdateBinaryInput()
  {
    return this->binaryInput->Update();
  }

  virtual void OnReceiveChunk(void *data, int dataSize)
  {
    float x, y, z, newX, newY, newZ, dx, dy, dz, d;
    int driverMappingIndex = 0;
    
    this->SendState(CNCState_Busy);
    
    unsigned int offset = 0;
    while (offset < dataSize)
    {
      unsigned char opcode = *(unsigned char*)((unsigned int)data + offset);
      offset += 1;
      switch (opcode)
      {
 
        case CommandCode_Info:
        {
          char info[] = "\1\1Axis CNC Plotter\r\nv0.9\r\nUID: 6F067A465D7140E7";
          info[0] = ResponseCode_Info;
          info[1] = strlen(&info[2]);
          this->SendChunk(info, info[1] + 2);
          
          break;
        }
        
        case CommandCode_Wait:
        {
          float time = *(float*)((unsigned int)data + offset);
          offset += 4;

          if (time > 0.0f)
            delayMicroseconds((unsigned int)time * 1000000.0f);

          break;
        }
        
        case CommandCode_Turn:
        {
          unsigned char motors = *(unsigned char*)((unsigned int)data + offset);
          offset += 1;
          unsigned char dir = *(unsigned char*)((unsigned int)data + offset);
          offset += 1;
          unsigned short interval = *(unsigned short*)((unsigned int)data + offset);
          offset += 2;
          unsigned short steps = *(unsigned short*)((unsigned int)data + offset);
          offset += 2;

          int intervalOn = interval / 2;
          int intervalOff = intervalOn + interval % 2;
  
          for (int motor = 0; motor < CNC::motorsCount; motor++)
            if ((motors & (1 << motor)) != 0)
              this->motorDrivers[motor]->SetDirection(dir == 0);
          
          for (int i = 0; i < steps; i++)
          {
            for (int motor = 0; motor < CNC::motorsCount; motor++)
              if ((motors & (1 << motor)) != 0)
                this->motorDrivers[motor]->Pulse();
            
            delayMicroseconds(intervalOn);
            
            for (int motor = 0; motor < CNC::motorsCount; motor++)
              if ((motors & (1 << motor)) != 0)
                this->motorDrivers[motor]->Pulse();
              
            delayMicroseconds(intervalOff);
          }
          
          break;
        }

        case CommandCode_Tone:
        {
          unsigned char motors = *(unsigned char*)((unsigned int)data + offset);
          offset += 1;
          float frequency = *(float*)((unsigned int)data + offset);
          offset += 4;
          float duration = *(float*)((unsigned int)data + offset);
          offset += 4;

          float interval = 1000000.0f / frequency;
          int oscillations = (int)(duration * 1000000.0f / interval);
          int delayIntervalOn = ((int)interval) / 2;
          int delayIntervalOff = delayIntervalOn + ((int)interval) % 2;
          bool dir = true;
          for (int i = 0; i < oscillations; i++)
          {
            dir = !dir;
            for (int motor = 0; motor < CNC::motorsCount; motor++)
              if ((motors & (1 << motor)) != 0)
              {
                this->motorDrivers[motor]->Pulse(dir, false);
              }

            delayMicroseconds(delayIntervalOn);
            
            for (int motor = 0; motor < CNC::motorsCount; motor++)
              if ((motors & (1 << motor)) != 0)
                this->motorDrivers[motor]->Pulse();
               
            delayMicroseconds(delayIntervalOff);
          }

          break;
        }
        
        case CommandCode_SleepMode:
        {
          bool wasSleeping = this->motorGroupSettings[0]->isSleepMode;
          this->motorGroupSettings[0]->isSleepMode = *(bool*)((unsigned int)data + offset);
          offset += 1;

          this->motorGroupSettings[1]->isSleepMode = this->motorGroupSettings[0]->isSleepMode;

          this->SetModeControlBits();

          // A4988 specs require 1ms delay after emerging from Sleep mode, in order to allow its charge pump to stabilize
          if (wasSleeping && !this->motorGroupSettings[0]->isSleepMode)
            delay(1);

          break;
        }

        case CommandCode_ToolPowerMode:
        {
          this->toolPowered = *(bool*)((unsigned int)data + offset);
          offset += 1;

          this->SetModeControlBits();

          break;
        }

        case CommandCode_MotorPowerMode:
        {
          this->motorGroupSettings[0]->isEnabled = *(bool*)((unsigned int)data + offset);
          offset += 1;

          this->motorGroupSettings[1]->isEnabled = this->motorGroupSettings[0]->isEnabled;

          this->SetModeControlBits();

          break;
        }

        case CommandCode_MotorStepMode:
        {
          unsigned char motorGroupIndex = *(unsigned char*)((unsigned int)data + offset);
          offset += 1;
          unsigned char modeIndex = *(unsigned char*)((unsigned int)data + offset);
          offset += 1;

          CNCMotorControllerSettings *motorsGroup = this->motorGroupSettings[motorGroupIndex];
          motorsGroup->mode = modeIndex;

          for (int i = 0; i < CNC::motorsCount; i++)
            this->motorDrivers[i]->SetStepMultiplier(motorsGroup->stepModes[modeIndex].stepMultiplier);

          this->SetModeControlBits();

          break;
        }

        case CommandCode_IssueTurning:
        {
          unsigned char issuedMotor = *(unsigned char*)((unsigned int)data + offset);
          offset += 1;
          float vector = *(float*)((unsigned int)data + offset);
          offset += 4;

          if (issuedMotor < CNC::motorsCount)
          {
            if (vector != 0.0f)
            {
              long interval = this->motorDrivers[issuedMotor]->CalculateStepIntervalForSpeed(vector) / 2.0f;
              this->motorDrivers[issuedMotor]->SetDirection(interval >= 0.0f);
              this->issuedTargetIntervals[issuedMotor] = interval < 0.0f ? -interval : interval;
              this->issuedIntervals[issuedMotor] = this->issuedTargetIntervals[issuedMotor] * 5;
              this->issuedTransitionTime[issuedMotor] = 0;
            }
            else
              this->issuedIntervals[issuedMotor] = 0;
          }          

          break;
        }

        case CommandCode_MapDriver:
        {
          unsigned char driverIndex = *(unsigned char*)((unsigned int)data + offset);
          offset += 1;
          
          this->driversMapping[driverMappingIndex] = this->driversListing[driverIndex];
          driverMappingIndex++;

          break;
        }

        case CommandCode_SetPower:
        {
          float power = *(float*)((unsigned int)data + offset);
          offset += 4;
          this->SetPower(power);
          
          break;
        }
  
        case CommandCode_SetSpeed:
        {
          float speed = *(float*)((unsigned int)data + offset);
          offset += 4;
          this->SetSpeed(speed);
          
          break;
        }
  
        case CommandCode_Polyline:
        {
          int zero = this->interpolatorsCount;
          LinearInterpolator *ix = new LinearInterpolator();
          LinearInterpolator *iy = new LinearInterpolator();
          LinearInterpolator *iz = new LinearInterpolator();

          this->interpolators[this->interpolatorsCount++] = ix;
          this->interpolators[this->interpolatorsCount++] = iy;
          this->interpolators[this->interpolatorsCount++] = iz;
          
          unsigned char numVectors = *(unsigned char*)((unsigned int)data + offset);
          offset += 1;
          for (int i = 0; i < numVectors; i++)
          {
            x = *(float*)((unsigned int)data + offset);
            offset += 4;
            y = *(float*)((unsigned int)data + offset);
            offset += 4;
            z = *(float*)((unsigned int)data + offset);
            offset += 4;
  
            d = Math::Sqrt(x * x + y * y + z * z);

            ix->Setup(this->driversMapping[zero], 0.0f, x);
            iy->Setup(this->driversMapping[zero + 1], 0.0f, y);
            iz->Setup(this->driversMapping[zero + 2], 0.0f, z);
  
            this->Plot(d / this->spd);
          }

          this->ClearInterpolators();
          
          break;
        }
  
        case CommandCode_Arc:
        {
          int zero = this->interpolatorsCount;
          LinearInterpolator *ix = new LinearInterpolator();
          LinearInterpolator *iy = new LinearInterpolator();
          LinearInterpolator *iz = new LinearInterpolator();

          this->interpolators[this->interpolatorsCount++] = ix;
          this->interpolators[this->interpolatorsCount++] = iy;
          this->interpolators[this->interpolatorsCount++] = iz;

          float xx = *(float*)((unsigned int)data + offset);
          offset += 4;
          float xy = *(float*)((unsigned int)data + offset);
          offset += 4;
          float xz = *(float*)((unsigned int)data + offset);
          offset += 4;
          
          float yx = *(float*)((unsigned int)data + offset);
          offset += 4;
          float yy = *(float*)((unsigned int)data + offset);
          offset += 4;
          float yz = *(float*)((unsigned int)data + offset);
          offset += 4;

          float startAngle = *(float*)((unsigned int)data + offset);
          offset += 4;
          float endAngle = *(float*)((unsigned int)data + offset);
          offset += 4;

          // !! Delta angle with -10, 10 range
          float deltaAngle = endAngle - startAngle;
          int steps = Math::Max((int)Math::Abs(180.0f * deltaAngle / Math::Pi), 1);
          for (int i = 0; i <= steps; i++)
          {
            float a = startAngle + deltaAngle * (float)i / (float)steps;
            float rx = Math::Cos(a);
            float ry = Math::Sin(a);

            newX = xx * rx + yx * ry;
            newY = xy * rx + yy * ry;
            newZ = xz * rx + yz * ry;

            if (i != 0)
            {
              dx = newX - x;
              dy = newY - y;
              dz = newZ - z;

              d = Math::Sqrt(dx * dx + dy * dy + dz * dz);
    
              ix->Setup(this->driversMapping[zero], 0.0f, dx);
              iy->Setup(this->driversMapping[zero + 1], 0.0f, dy);
              iz->Setup(this->driversMapping[zero + 2], 0.0f, dz);

              this->Plot(d / spd);
            }

            x = newX;
            y = newY;
            z = newZ;
          }

          this->ClearInterpolators();

          break;
        }
  
        case CommandCode_Bezier:
        {
          int zero = this->interpolatorsCount;
          LinearInterpolator *ix = new LinearInterpolator();
          LinearInterpolator *iy = new LinearInterpolator();
          LinearInterpolator *iz = new LinearInterpolator();

          this->interpolators[this->interpolatorsCount++] = ix;
          this->interpolators[this->interpolatorsCount++] = iy;
          this->interpolators[this->interpolatorsCount++] = iz;

          unsigned char controlPointsCount = *(unsigned char*)((unsigned int)data + offset);
          offset += 1;

          controlPointsCount++;

          if (controlPointsCount > this->bezierCPCount)
          {
            if (this->bezierCPCount > 0)
            {
              delete[] this->bezierCP;
              delete[] this->bezierSubP;
            }

            this->bezierCPCount = controlPointsCount;
            this->bezierCP = new float[bezierCPCount * 3];
            this->bezierSubP = new float[(bezierCPCount - 1) * 3];
          }

          this->bezierCP[0] = 0.0f;
          this->bezierCP[1] = 0.0f;
          this->bezierCP[2] = 0.0f;
          int c = controlPointsCount * 3;
          for (int i = 3; i < c; i++)
          {
            this->bezierCP[i] = *(float*)((unsigned int)data + offset);
            offset += 4;
          }

          int initialStage = controlPointsCount - 1;
          if (initialStage <= 0)
              break;
              
          const int steps = 100;
          for (int s = 0; s <= steps; s++)
          {
            float t = (float)s / (float)steps;
            
            c = initialStage * 3;
            for (int i = 0; i < c; i++)
                this->bezierSubP[i] = this->bezierCP[i] + (this->bezierCP[i + 3] - this->bezierCP[i]) * t;
  
            for (int stage = initialStage - 1; stage >= 0; stage--)
            {
                c = stage * 3;
                for (int i = 0; i < c; i++)
                  this->bezierSubP[i] += (this->bezierSubP[i + 3] - this->bezierSubP[i]) * t;
            }

            newX = this->bezierSubP[0];
            newY = this->bezierSubP[1];
            newZ = this->bezierSubP[2];
            
            if (s != 0)
            {
              dx = newX - x;
              dy = newY - y;
              dz = newZ - z;

              d = Math::Sqrt(dx * dx + dy * dy + dz * dz);
    
              ix->Setup(this->driversMapping[zero], 0.0f, dx);
              iy->Setup(this->driversMapping[zero + 1], 0.0f, dy);
              iz->Setup(this->driversMapping[zero + 2], 0.0f, dz);

              Plot(d / spd);
            }

            x = newX;
            y = newY;
            z = newZ;
          }

          this->ClearInterpolators();

          break;
        }

        case CommandCode_Drive:
        {
          float time = *(float*)((unsigned int)data + offset);
          offset += 4;
          
          Plot(time);
          
          this->ClearInterpolators();

          break;
        }

        case CommandCode_DriveLinear:
        {
          float origin = *(float*)((unsigned int)data + offset);
          offset += 4;
          float vector = *(float*)((unsigned int)data + offset);
          offset += 4;

          LinearInterpolator *interpolator = new LinearInterpolator();
          interpolator->Setup(this->driversMapping[this->interpolatorsCount], origin, vector);
          
          this->interpolators[this->interpolatorsCount++] = interpolator;
          
          break;
        }
        
        case CommandCode_DriveSine:
        {
          float phaseOffset = *(float*)((unsigned int)data + offset);
          offset += 4;
          float span = *(float*)((unsigned int)data + offset);
          offset += 4;
          float amplitude = *(float*)((unsigned int)data + offset);
          offset += 4;
          float phaseStart = *(float*)((unsigned int)data + offset);
          offset += 4;
          float phaseEnd = *(float*)((unsigned int)data + offset);
          offset += 4;

          SineInterpolator *interpolator = new SineInterpolator();
          interpolator->Setup(this->driversMapping[this->interpolatorsCount], phaseOffset, span, amplitude, phaseStart, phaseEnd);
          
          this->interpolators[this->interpolatorsCount++] = interpolator;
          
          break;
        }

      }
    }
    
    this->SendState(CNCState_Ready);
  };

  void SendState(CNCState state)
  {
    unsigned char stateData[2] = { ResponseCode_State, (unsigned char)state };
    this->SendChunk(stateData, 2);
  }

  bool BackgroundRoutines()
  {
//    if (this->UpdateBinaryInput())
//    {
//      for (int i = 0; i < 8; i++)
//        if (this->binaryInput->values[i])
//          this->SetPower((float)i / (float)8);
//    }

    
    unsigned long now = micros();
    long deltaTime = now - this->routinesTime;
    
    bool routinesActive = false;
    
    for (int motorIndex = 0; motorIndex < CNC::motorsCount; motorIndex++)
    {
      if (this->issuedIntervals[motorIndex] != 0)
      {
        routinesActive = true;

        float t = (float)Math::Min(this->issuedTransitionTime[motorIndex], 500000) / 500000.0f;
        unsigned int interval = this->issuedIntervals[motorIndex] + (long)((float)(this->issuedTargetIntervals[motorIndex] - this->issuedIntervals[motorIndex]) * t);

        this->issuedTimes[motorIndex] += deltaTime;
        if (this->issuedTimes[motorIndex] >= interval)
        {
          this->issuedTimes[motorIndex] -= interval;

          this->motorDrivers[motorIndex]->Pulse();
        }

        this->issuedTransitionTime[motorIndex] += deltaTime;

      }
    }

    this->routinesTime = now;
  
    return routinesActive;  
  }

};



CNC *cnc;

void setup() 
{
  Serial.begin(9600);

  cnc = new CNC(&Serial, PB13, PB12, PB15, PB14, PB6, PB5, PB1, PB0, PB8, PA4, PA6, PA5, PA1, PA8, PA0);
}


void loop() 
{
  cnc->Read();

  if(!cnc->BackgroundRoutines())
    delay(50);
}

