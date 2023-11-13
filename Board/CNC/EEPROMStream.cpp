#include "EEPROMStream.h"

#include <Arduino.h>
#include <EEPROM.h>

#include "Math.h"

EEPROMStream::EEPROMStream()
  : BaseStream()
{
  this->data = 0;
}

EEPROMStream::~EEPROMStream()
{
}


void EEPROMStream::SetCapacity(int newCapacity)
{
}

void EEPROMStream::WriteData(const void *source, int offset, int length)
{
  char *src = (char*)((int)source + offset);
  for (int i = length; i > 0; i--)
  {
    EEPROM.write(this->position, *src);
    src++;
    this->position++;
  }
}

void EEPROMStream::ReadData(void *destination, int offset, int length)
{
  char *dest = (char*)((int)destination + offset);
  for (int i = length; i > 0; i--)
  {
    *dest = EEPROM.read(this->position);
    dest++;
    this->position++;
  }
}

void EEPROMStream::Seek(int position)
{
  this->position = position;
}

