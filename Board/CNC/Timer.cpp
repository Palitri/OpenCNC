#include "Timer.h"

#include <Arduino.h>

void Timer::Init()
{
  this->speed = 1.0f;
  this->time = 0.0f;
  this->totalTime = 0.0f;
  this->actualTime = 0.0f;
  this->totalActualTime = 0.0f;
  this->ticksLastSecond = 0;
  this->totalTicks = 0;

  this->lastSecondTicks = 0;
  this->lastSecondTime = 0.0f;

  this->timeAnchor = micros();
}

void Timer::Tick()
{
  unsigned long timeAnchor = micros();
  unsigned long elapsed = timeAnchor - this->timeAnchor;

  if (elapsed > 0)
    this->actualTime = (float)elapsed / 1000000.0f;
  this->time = this->actualTime * this->speed;
  this->totalTime += this->time;
  this->totalActualTime += this->actualTime;
  this->totalTicks++;

  this->lastSecondTicks++;
  this->lastSecondTime += this->actualTime;
  if (this->lastSecondTime >= 1.0f)
  {
      this->ticksLastSecond = (float)this->lastSecondTicks / this->lastSecondTime;
      this->lastSecondTicks = 0;
      this->lastSecondTime = 0.0f;
  }

  this->timeAnchor = timeAnchor;
}

