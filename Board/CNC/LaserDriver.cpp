#include "LaserDriver.h"

#include <Arduino.h>

#include "Board.h"
#include "Math.h"

LaserDriver::LaserDriver(int pinPwm)
{
  this->pinPwm = pinPwm;

  pinMode(this->pinPwm, PWM);
  //pinMode(this->pinPwm, OUTPUT);

  this->SetPower(0.0f);
}

void LaserDriver::SetPower(float power)
{
    pwmWrite(this->pinPwm, (int)Math::Round(Math::Trim(power, 0.0f, 1.0f) * (float)Board::pwmMaxValue));
    //analogWrite(this->pinPwm, (int)(power * (float)Board::pwmMaxValue));
}

void LaserDriver::Begin(float origin, float vector)
{
  this->origin = origin;
  this->vector = vector;
  this->phase = 0.0f;

  this->SetPower(origin);
}

void LaserDriver::Drive(float phase)
{
  this->phase = phase;
  this->SetPower(this->origin + phase * this->vector);
}

