#ifndef LinearInterpolator_h
#define LinearInterpolator_h

#include "IInterpolator.h"

#include "IDeviceDriver.h"

class LinearInterpolator
  : public IInterpolator
{
private:
  IDeviceDriver *driver;
  
public:
  void Setup(IDeviceDriver *driver, float origin, float delta);

  virtual bool Run(float phase);
};

#endif
// LinearInterpolator_h

