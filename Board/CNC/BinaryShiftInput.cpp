#include "BinaryShiftInput.h"

#include <Arduino.h>

#include "Board.h"

BinaryShiftInput::BinaryShiftInput(int shiftRegSerialPin, int shiftRegStoragePin, int shiftRegClockPin, int readPin, int bitCount)
  : ShiftRegister(shiftRegSerialPin, shiftRegStoragePin, shiftRegClockPin, bitCount)
{
  this->readPin = readPin;

  this->values = new bool[this->bitCount];
}

BinaryShiftInput::~BinaryShiftInput()
{
  delete[] this->values;
}

bool BinaryShiftInput::Update()
{
    bool result = false;
    
    digitalWrite(this->shiftRegStoragePin, LOW);
    
    for (int i = this->bitCount - 2; i >=0; i--)
    {
      digitalWrite(this->shiftRegClockPin, LOW);
      digitalWrite(this->shiftRegSerialPin, LOW);
      digitalWrite(this->shiftRegClockPin, HIGH);
    }

    digitalWrite(this->shiftRegClockPin, LOW);
    digitalWrite(this->shiftRegSerialPin, HIGH);
    digitalWrite(this->shiftRegClockPin, HIGH);
  
    digitalWrite(this->shiftRegStoragePin, HIGH);

    bool pinValue = analogRead(this->readPin) > Board::analogReadHalfValue;
    result |= this->values[this->bitCount - 1] != pinValue;
    this->values[this->bitCount - 1] = pinValue;
    for (int i = this->bitCount - 2; i >= 0; i--)
    {
      digitalWrite(this->shiftRegStoragePin, LOW);
  
      digitalWrite(this->shiftRegClockPin, LOW);
      digitalWrite(this->shiftRegSerialPin, LOW);
      digitalWrite(this->shiftRegClockPin, HIGH);

      digitalWrite(this->shiftRegStoragePin, HIGH);

      pinValue = analogRead(this->readPin) > Board::analogReadHalfValue;
      result |= this->values[i] != pinValue;
      this->values[i] = pinValue;
    }
    
    digitalWrite(this->shiftRegStoragePin, LOW);
    for (int i = this->bitCount - 1; i >=0; i--)
    {
      digitalWrite(this->shiftRegClockPin, LOW);
      digitalWrite(this->shiftRegSerialPin, HIGH);
      digitalWrite(this->shiftRegClockPin, HIGH);
    }
    digitalWrite(this->shiftRegStoragePin, HIGH);

    return result;
}

