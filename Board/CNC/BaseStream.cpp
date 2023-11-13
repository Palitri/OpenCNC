#include "BaseStream.h"

#include <Arduino.h>

#include "Math.h"

BaseStream::BaseStream()
{
  this->capacity = 0;
  this->length = 0;
  this->position = 0;

  this->stringLength = 0;
}

BaseStream::~BaseStream()
{
  if (this->stringLength != 0)
    delete[] this->string;
}

void BaseStream::AllocateStringLength(unsigned short length)
{
  if (this->stringLength >= length)
    return;

  if (this->stringLength != 0)
    delete[] this->string;

  this->stringLength = length;

  if (this->stringLength > 0)
    this->string = new char[this->stringLength + 1];
}

void BaseStream::EnsureCapacity(int capacity)
{
  if (this->capacity < capacity)
  {
    int newCapacity = this->capacity;
    do
    {
      newCapacity += Math::Trim(this->capacity, 16, 64);
    }
    while (newCapacity < capacity);

    this->SetCapacity(newCapacity);
  }
}

void BaseStream::EnsureOffsetLength(int offsetLength)
{
  int newLength = this->position + offsetLength;
  this->EnsureCapacity(newLength);
  this->length = Math::Max(newLength, this->length);
}

void BaseStream::Clear()
{
  this->position = 0;
  this->length = 0;
}

void BaseStream::AppendData(void *source, int offset, int length)
{
    int originalPosition = this->position;

    this->position = this->length;

    this->WriteData(source, offset, length);

    this->position = originalPosition;
}

void BaseStream::WriteInt8(char value)
{
  this->WriteData(&value, 0, 1);
}

char BaseStream::ReadInt8()
{
  char result;
  this->ReadData(&result, 0, 1);
  return result;
}

void BaseStream::WriteInt16(short value)
{
  this->WriteData(&value, 0, 2);
};

short BaseStream::ReadInt16()
{
  short result;
  this->ReadData(&result, 0, 2);
  return result;
}

void BaseStream::WriteInt32(long value)
{
  this->WriteData(&value, 0, 4);
};

long BaseStream::ReadInt32()
{
  long result;
  this->ReadData(&result, 0, 4);
  return result;
}

void BaseStream::WriteUInt8(unsigned char value)
{
  this->WriteData(&value, 0, 1);
}

unsigned char BaseStream::ReadUInt8()
{
  unsigned char result;
  this->ReadData(&result, 0, 1);
  return result;
}

void BaseStream::WriteUInt16(unsigned short value)
{
  this->WriteData(&value, 0, 2);
};

unsigned short BaseStream::ReadUInt16()
{
  unsigned short result;
  this->ReadData(&result, 0, 2);
  return result;
}

void BaseStream::WriteUInt32(unsigned long value)
{
  this->WriteData(&value, 0, 4);
};

unsigned long BaseStream::ReadUInt32()
{
  unsigned long result;
  this->ReadData(&result, 0, 4);
  return result;
}

void BaseStream::WriteFloat32(float value)
{
  this->WriteData(&value, 0, 4);
}

float BaseStream::ReadFloat32()
{
  float result;
  this->ReadData(&result, 0, 4);
  return result;
}

void BaseStream::WriteBool(bool value)
{
  this->WriteUInt8(value ? 1 : 0);
}


bool BaseStream::ReadBool()
{
  return this->ReadUInt8() == 0 ? false : true;
}

void BaseStream::WriteString(const char *value)
{
  int length = 0;
  while (value[length] != 0)
    length++;
    
  this->WriteUInt16(length);
  this->WriteData(value, 0, length);
}

char *BaseStream::ReadString()
{
  unsigned short length = this->ReadUInt16();
  this->AllocateStringLength(length);
  this->ReadData(this->string, 0, length);
 this->string[length] = 0;
 return this->string;
}
