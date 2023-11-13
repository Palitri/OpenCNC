#include "SineInterpolator.h"

#include "Math.h"

void SineInterpolator::Setup(IDeviceDriver *driver, float offset, float span, float amplitude, float phaseStart, float phaseEnd)
{
  this->driver = driver;
  this->offset = offset;
  this->span = span;
  this->amplitude = amplitude / 2.0f;
  this->phaseStart = phaseStart * Math::Pi2;
  this->phaseDelta = (phaseEnd - phaseStart) * Math::Pi2;

  this->driver->Begin(offset, span);
}


bool SineInterpolator::Run(float phase)
{
  this->driver->Drive(this->offset + Math::Sin((this->phaseStart + this->phaseDelta * phase)) * this->amplitude);
  
  return true;
}

