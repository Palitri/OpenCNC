#ifndef LaserDriver_h
#define LaserDriver_h

#include "IDeviceDriver.h"

class LaserDriver
  : public IDeviceDriver
{
private:
  int pinPwm;

  float vector, origin, phase;
public:
  LaserDriver(int pinPwm);
  ~LaserDriver();

  void SetPower(float power);

  void Begin(float origin, float vector);
  void Drive(float phase);
};

#endif
// LaserDriver_h
