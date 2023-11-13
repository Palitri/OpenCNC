#ifndef ShiftRegister_h
#define ShiftRegister_h

class ShiftRegister
{
private:
  int bitMask;
  
public:
  int shiftRegSerialPin, shiftRegStoragePin, shiftRegClockPin;
  int bitCount;

  int bits;

  ShiftRegister(int shiftRegSerialPin, int shiftRegStoragePin, int shiftRegClockPin, int bitCount);
  ~ShiftRegister();

  void SetBits(int bits);
};

#endif
// ShiftRegister_h

