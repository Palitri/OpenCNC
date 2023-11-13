#ifndef BinaryShiftInput_h
#define BinaryShiftInput_h

#include "ShiftRegister.h"

class BinaryShiftInput
  : public ShiftRegister
{
private:
  int readPin;
  
public:
  bool *values;

  BinaryShiftInput(int shiftRegSerialPin, int shiftRegStoragePin, int shiftRegClockPin, int readPin, int bitCount);
  ~BinaryShiftInput();

  bool Update();
};

#endif
// BinaryShiftInput_h

