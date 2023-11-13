#include "SerialTransmission.h"

#include "Math.h"

#include <Arduino.h>

const unsigned char SerialTransmission::chunkId[2] = { 0xE6, 0x71 };

SerialTransmission::SerialTransmission(HardwareSerial *serial)
{
  this->serial = serial;
  
	this->readBufferPosition = 0;
	this->detectedFooterOffset = 0;
}


SerialTransmission::~SerialTransmission(void)
{
}

int SerialTransmission::SendChunk(void *data, int dataSize)
{
  unsigned short crc = Math::CRC16(data, dataSize);
  
  this->serial->write(data, dataSize);
  this->serial->write(SerialTransmission::chunkId, SerialTransmission::chunkIdSize);
  this->serial->write(&dataSize, 1);
  this->serial->write(&crc, 2);

	return dataSize;
}

void SerialTransmission::Read()
{
  int dataSize = this->serial->available();

  for (int i = 0; i < dataSize; i++)
  {
    unsigned char dataByte;
    this->serial->readBytes(&dataByte, 1);

    this->readBuffer[this->readBufferPosition] = dataByte;
    this->readBufferPosition++;

    if (this->detectedFooterOffset < SerialTransmission::chunkIdSize)
    {
      if (dataByte == SerialTransmission::chunkId[this->detectedFooterOffset])
        this->detectedFooterOffset++;
      else
        this->detectedFooterOffset = 0;
    }
    else if (this->detectedFooterOffset < SerialTransmission::chunkFooterSize)
    {
      this->chunkData[this->detectedFooterOffset - SerialTransmission::chunkIdSize] = dataByte;
      this->detectedFooterOffset++;

      if (this->detectedFooterOffset == SerialTransmission::chunkFooterSize)
      {
        this->detectedFooterOffset = 0;

        unsigned char payloadSize = this->chunkData[0];
        unsigned short chunkCrc16 = *(unsigned short*)(&this->chunkData[1]);

        int payloadPosition = this->readBufferPosition - SerialTransmission::chunkFooterSize - payloadSize;
        if (payloadPosition >= 0)
        {
          unsigned short actualCrc16 = Math::CRC16(&this->readBuffer[payloadPosition], payloadSize);
          if (actualCrc16 == chunkCrc16)
          {
            this->OnReceiveChunk(&this->readBuffer[payloadPosition], payloadSize);

            this->readBufferPosition = 0;
          }
        }
        else
        {
          unsigned char chunkData[SerialTransmission::chunkMaxDataSize];
          memcpy(chunkData, this->readBuffer + SerialTransmission::chunkMaxTotalSize + payloadPosition, -payloadPosition);
          memcpy(chunkData - payloadPosition, this->readBuffer, payloadSize + payloadPosition);
          unsigned short actualCrc16 = Math::CRC16(chunkData, payloadSize);
          if (actualCrc16 = chunkCrc16)
          {
            this->OnReceiveChunk(chunkData, payloadSize);

            this->readBufferPosition = 0;
          }
        }
      }
    }

     this->readBufferPosition %= SerialTransmission::chunkMaxTotalSize;
  }
}


void SerialTransmission::OnReceiveChunk(void *data, int dataSize)
{
}
