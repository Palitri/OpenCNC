#ifndef EEPROMStream_h
#define EEPROMStream_h

#include "BaseStream.h"

class EEPROMStream
  : public BaseStream
{
public:

  char *data;

  EEPROMStream();
  virtual ~EEPROMStream();

  virtual void SetCapacity(int newCapacity);
  virtual void WriteData(const void *source, int offset, int length);
  virtual void ReadData(void *destination, int offset, int length);
  virtual void Seek(int position);
};

#endif
// EEPROMStream_h
