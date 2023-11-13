#ifndef Stack_h
#define Stack_h

#include <Arduino.h>

template <typename T> class Stack
{
private:
  void SetCapacity(int newCapacity)
  {
    T *newValues = newCapacity > 0 ? new T[newCapacity] : 0;
    if ((newValues != 0) && (this->values != 0))
      memcpy(newValues, this->values, (this->capacity < newCapacity ? this->capacity : newCapacity) * sizeof(T));

    if (this->values!= 0)
      delete this->values;

    if (this->count > this->capacity)
      this->count = this->capacity;

    this->values = newValues;
    this->capacity = newCapacity;
  };

public:
  T *values;
  int capacity, count;

  Stack(int capacity = 8)
  {
    this->values= 0;
    this->capacity = 0;
    this->count = 0;
    
    this->SetCapacity(capacity);
  };

  ~Stack()
  {
    this->SetCapacity(0);
  }
  
  T& Push(T value)
  {
    if (this->count == this->capacity)
      this->SetCapacity(this->capacity * 2);

    this->values[this->count] = value;
    
    return this->values[this->count++];
  };

  T& Pop()
  {
    return this->values[--this->count];
  };

  T& Peek()
  {
    return this->values[this->count- 1];
  };
};


#endif
// Stack_h
