#include "StepMotorDriver.h"

#include <Arduino.h>

#include "Math.h"

StepMotorDriver::StepMotorDriver(int pinStep, int pinDir, float distanceUnitsPerTurn, int fullStepsPerTurn)
{
    this->pinStep = pinStep;
    this->pinDir = pinDir;

    this->distanceUnitsPerTurn = distanceUnitsPerTurn;
    this->fullStepsPerTurn = fullStepsPerTurn;

    this->stepState = false;
    this->forward = true;

    this->vectorStepsTotal = 0.0f;
    this->currentStep = 0;

    pinMode(this->pinStep, OUTPUT);
    pinMode(this->pinDir, OUTPUT);

    digitalWrite(this->pinStep, this->stepState ? HIGH : LOW);
    digitalWrite(this->pinDir, this->forward ? LOW : HIGH);

    this->SetStepMultiplier(1);
}

StepMotorDriver::~StepMotorDriver()
{
}

void StepMotorDriver::Begin(float origin, float vector)
{
  float remainder = this->vectorStepsTotal - this->currentStep;
  this->vectorStepsTotal = 2.0f * vector * this->stepsPerUnit + remainder;
  this->currentStep = 0;
}

void StepMotorDriver::Drive(float phase)
{
  float targetStep = phase * this->vectorStepsTotal;
  int targetStepDiscreet = (int)Math::Round(targetStep);
  int motorSteps = targetStepDiscreet - this->currentStep;
  this->currentStep = targetStepDiscreet;

  bool forward = motorSteps >= 0;
  if (this->forward != forward)
  {
    this->forward = forward;
    digitalWrite(this->pinDir, forward ? LOW : HIGH);
  }

  if (!forward)
    motorSteps = -motorSteps;

  while (motorSteps > 0)
  {
    this->stepState = !this->stepState;
    digitalWrite(this->pinStep, this->stepState ? HIGH : LOW);
    
    motorSteps--;
  }
}

//void StepMotorDriver::Begin(float origin, float vector)
//{
//  this->vectorStepsTotal = 2.0f * Math::Abs(vector) * this->stepsPerUnit + this->vectorStepsRemainder;
//  this->vectorStepsRemainder = 0.0f;
//
//  this->forward = vector >= 0.0f;
//  digitalWrite(this->pinDir, this->forward ? LOW : HIGH);
//}
//
//void StepMotorDriver::Drive(float delta)
//{
//  this->vectorStepsRemainder += this->vectorStepsTotal * delta;
//  int motorSteps = (int)Math::Round(this->vectorStepsRemainder);
//  if (motorSteps > 0)
//  {
//    this->vectorStepsRemainder -= motorSteps;
//
//    do
//    {
//      this->stepState = !this->stepState;
//      digitalWrite(this->pinStep, this->stepState ? HIGH : LOW);
//      
//      motorSteps--;
//    }
//    while (motorSteps > 0);
//  }
//}


float StepMotorDriver::CalculateStepsPerUnit()
{
  return (float)(this->stepMultiplier * this->fullStepsPerTurn) / this->distanceUnitsPerTurn;
};

float StepMotorDriver::CalculateStepIntervalForSpeed(float speedUnitsPerSecond)
{
  return 1000000.0f / (this->CalculateStepsPerUnit() * speedUnitsPerSecond);
}

void StepMotorDriver::SetStepMultiplier(int stepMultiplier)
{
  this->stepMultiplier = stepMultiplier;
  this->stepsPerUnit = this->CalculateStepsPerUnit();  
}

void StepMotorDriver::SetDirection(bool forward)
{
  this->forward = forward;
  digitalWrite(this->pinDir, this->forward ? LOW : HIGH);
}

void StepMotorDriver::Pulse(bool forward, bool pulseState)
{
  this->forward = forward;
  digitalWrite(this->pinDir, this->forward ? LOW : HIGH);

  this->stepState = pulseState;
  digitalWrite(this->pinStep, this->stepState ? HIGH : LOW);
}

void StepMotorDriver::Pulse(bool forward)
{
  this->forward = forward;
  digitalWrite(this->pinDir, this->forward ? LOW : HIGH);

  this->stepState = !this->stepState;
  digitalWrite(this->pinStep, this->stepState ? HIGH : LOW);
}

void StepMotorDriver::Pulse()
{
  this->stepState = !this->stepState;
  digitalWrite(this->pinStep, this->stepState ? HIGH : LOW);
}

