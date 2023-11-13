#include "LinearInterpolator.h"

void LinearInterpolator::Setup(IDeviceDriver *driver, float origin, float delta)
{
  this->driver = driver;

  this->driver->Begin(origin, delta);
}


bool LinearInterpolator::Run(float phase)
{
  this->driver->Drive(phase);

  return true;
}

