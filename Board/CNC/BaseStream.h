#ifndef BaseStream_h
#define BaseStream_h

class BaseStream
{
private:
  char *string;
  unsigned short stringLength;
  
  void AllocateStringLength(unsigned short length);
  
public:
  int capacity, length, position;

  BaseStream();
  virtual ~BaseStream();

  virtual void SetCapacity(int newCapacity) = 0;
  virtual void WriteData(const void *source, int offset, int length) = 0;
  virtual void ReadData(void *destination, int offset, int length) = 0;
  virtual void Seek(int position) = 0;

  void EnsureCapacity(int capacity);
  void EnsureOffsetLength(int offsetLength);
  void Clear();
  void AppendData(void *source, int offset, int length);

  void WriteInt8(char value);
  char ReadInt8();
  void WriteInt16(short value);
  short ReadInt16();
  void WriteInt32(long value);
  long ReadInt32();

  void WriteUInt8(unsigned char value);
  unsigned char ReadUInt8();
  void WriteUInt16(unsigned short value);
  unsigned short ReadUInt16();
  void WriteUInt32(unsigned long value);
  unsigned long ReadUInt32();

  void WriteFloat32(float value);
  float ReadFloat32();

  void WriteBool(bool value);
  bool ReadBool();

  void WriteString(const char *value);
  char *ReadString();
};

#endif
// BaseStream_h
