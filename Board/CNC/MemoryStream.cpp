#include "MemoryStream.h"

#include <Arduino.h>

#include "Math.h"

MemoryStream::MemoryStream()
  : BaseStream()
{
  this->data = 0;
}

MemoryStream::~MemoryStream()
{
  this->SetCapacity(0);
}

void MemoryStream::SetCapacity(int newCapacity)
{
  char *newData = newCapacity > 0 ? new char[newCapacity] : 0;
  int newLength = Math::Min(this->length, newCapacity);

  if (this->data != 0)
  {
    if (newLength > 0)
      memcpy(newData, this->data, newLength);

    delete[] this->data;
  }

  this->data = newData;
  this->capacity = newCapacity;
  this->length = newLength;
  this->position = Math::Min(this->position, newLength);
}

void MemoryStream::WriteData(const void *source, int offset, int length)
{

  this->EnsureOffsetLength(length);

  memcpy(this->GetPositionAddress(), (void*)((int)source + offset), length);

  this->position += length;
}

void MemoryStream::ReadData(void *destination, int offset, int length)
{
  memcpy((void*)((int)destination + offset), this->GetPositionAddress(), length);

  this->position += length;
}

void MemoryStream::Seek(int position)
{
  this->position = position;
}

void *MemoryStream::GetPositionAddress()
{
  return (void*)((int)this->data + this->position);
}

void *MemoryStream::GetAddress(int position)
{
  return (void*)((int)this->data + position);
}

void MemoryStream::DeleteData(int position, int size)
{
  if (size <= 0)
    return;

  int sourcePosition = position + size;
  int destPosition = position;
  
  while (sourcePosition < this->length)
    this->data[destPosition++] = this->data[sourcePosition++];

  this->length = Math::Max(this->length - size, 0);
  this->position = Math::Max(this->position - size, 0);
}

