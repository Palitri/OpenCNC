#ifndef CommunicationChannel_h
#define CommunicationChannel_h

class CommunicationChannel
{
  public:
  
  virtual int AvailableInputBytes() = 0;
  virtual int Read(void *destination, int count) = 0;
  virtual int Write(void *source, int count) = 0;
};

#endif
//CommunicationChannel_h
