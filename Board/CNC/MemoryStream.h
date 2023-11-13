#ifndef MemoryStream_h
#define MemoryStream_h

#include "BaseStream.h"

class MemoryStream
  : public BaseStream
{
public:

  char *data;

  MemoryStream();
  virtual ~MemoryStream();

  virtual void SetCapacity(int newCapacity);
  virtual void WriteData(const void *source, int offset, int length);
  virtual void ReadData(void *destination, int offset, int length);
  virtual void Seek(int position);

  void *GetPositionAddress();
  void *GetAddress(int position);
  
  void DeleteData(int position, int size);
};

#endif
// MemoryStream_h
