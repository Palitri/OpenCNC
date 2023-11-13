#ifndef SerialTransmission_h
#define SerialTransmission_h

#include <Arduino.h>

class SerialTransmission
{
private:
	static const int chunkIdSize = 2;
	static const int chunkFooterSize = 5;
	static const int chunkMaxDataSize = 255;
	static const int chunkMaxTotalSize = chunkMaxDataSize + chunkFooterSize;
	static const unsigned char chunkId[chunkIdSize];

	int readBufferPosition, detectedFooterOffset;
	unsigned char readBuffer[chunkMaxTotalSize];
	unsigned char chunkData[chunkFooterSize - chunkIdSize];

  HardwareSerial *serial;

public:
	SerialTransmission(HardwareSerial *serial);
	virtual ~SerialTransmission(void);

  void Read();

	virtual int SendChunk(void *data, int dataSize);

	virtual void OnReceiveChunk(void *data, int dataSize);
};

#endif
// SerialTransmission_h
