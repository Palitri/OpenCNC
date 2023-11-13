#ifndef SineInterpolator_h
#define SineInterpolator_h

#include "IInterpolator.h"

#include "IDeviceDriver.h"

class SineInterpolator
  : public IInterpolator
{
private:
  IDeviceDriver *driver;
  float offset, span, amplitude, phaseStart, phaseDelta;
  
public:
  void Setup(IDeviceDriver *driver, float offset, float span, float amplitude, float phaseStart, float phaseEnd);

  virtual bool Run(float phase);
};

#endif
// SineInterpolator_h

