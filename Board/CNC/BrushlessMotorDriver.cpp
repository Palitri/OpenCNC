#include "BrushlessMotorDriver.h"

#include <Arduino.h>

#include "Math.h"

BrushlessMotorDriver::BrushlessMotorDriver(int pinPwm, int pinPower)
{
    this->pinPwm = pinPwm;
    this->pinPower = pinPower;

    this->pwmMinMilliseconds = 800;
    this->pwmRangeMilliseconds = 1200;

    this->servo = new Servo();
    this->servo->attach(this->pinPwm);
    this->servo->writeMicroseconds(0);
}

BrushlessMotorDriver::~BrushlessMotorDriver()
{
  delete this->servo;
}

void BrushlessMotorDriver::Begin(float vector)
{
}

void BrushlessMotorDriver::Drive(float delta)
{
  this->servo->writeMicroseconds(this->pwmMinMilliseconds + (int)(delta * (float)this->pwmRangeMilliseconds));
}


