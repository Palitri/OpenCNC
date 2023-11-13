#ifndef BrushlessMotorDriver_h
#define BrushlessMotorDriver_h

#include "IDeviceDriver.h"

#include <Servo.h>

class BrushlessMotorDriver
  : public IDeviceDriver
{
private:
  int pinPwm, pinPower;

  Servo *servo;

public:
  int pwmMinMilliseconds, pwmRangeMilliseconds;


  BrushlessMotorDriver(int pinPwm, int pinPower);
  ~BrushlessMotorDriver(void);

  void Begin(float vector);
  void Drive(float delta);
};

#endif
// BrushlessMotorDriver_h
