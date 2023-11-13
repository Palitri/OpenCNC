#include "ShiftRegister.h"

#include <Arduino.h>

ShiftRegister::ShiftRegister(int shiftRegSerialPin, int shiftRegStoragePin, int shiftRegClockPin, int bitCount)
{
  this->shiftRegSerialPin = shiftRegSerialPin;
  this->shiftRegStoragePin = shiftRegStoragePin;
  this->shiftRegClockPin = shiftRegClockPin;
  this->bitCount = bitCount;

  pinMode(this->shiftRegSerialPin, OUTPUT);
  pinMode(this->shiftRegStoragePin, OUTPUT);
  pinMode(this->shiftRegClockPin, OUTPUT);

  this->bitMask = 1 << (this->bitCount - 1);
  
  this->SetBits(0);
}

ShiftRegister::~ShiftRegister()
{
  
}

void ShiftRegister::SetBits(int bits)
{
  digitalWrite(this->shiftRegStoragePin, LOW);

  for (int i = this->bitCount - 1; i >= 0; i--)
  {
    digitalWrite(shiftRegClockPin, LOW);
    digitalWrite(this->shiftRegSerialPin, (bits & this->bitMask) == 0 ? LOW : HIGH);
    digitalWrite(shiftRegClockPin, HIGH);

    bits <<= 1;
  }
  
  digitalWrite(this->shiftRegStoragePin, HIGH);  

  this->bits = bits;
}

