#ifndef MotorDriver_h
#define MotorDriver_h

#include "IDeviceDriver.h"

class MotorDriver
  : public IDeviceDriver
{
private:
  int pinPwm;
    
public:
  MotorDriver(int pinPwm);
  ~MotorDriver();

  void Begin(float vector);
  void Drive(float delta);
};

#endif
// MotorDriver_h
