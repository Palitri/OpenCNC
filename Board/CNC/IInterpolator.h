#ifndef IInterpolator_h
#define IInterpolator_h

#include "IDeviceDriver.h"

class IInterpolator
{
public:
  virtual bool Run(float phase) = 0;
};

#endif
// IInterpolator_h

