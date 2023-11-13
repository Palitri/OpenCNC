#ifndef IDeviceDriver_h
#define IDeviceDriver_h

class IDeviceDriver
{
public:
  virtual void Begin(float origin, float vector) = 0;
//  void End() = 0;
  virtual void Drive(float phase) = 0;
};

#endif
// IDeviceDriver_h
