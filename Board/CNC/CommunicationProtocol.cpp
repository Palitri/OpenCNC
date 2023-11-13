#include "CommunicationProtocol.h"

#include <Arduino.h>

CommunicationProtocol::CommunicationProtocol()
{
  this->outputStreamWriter = new HierarchyStreamWriter(this->outputStream);
  this->inputStreamReader = new HierarchyStreamReader(this->inputStream);
}

CommunicationProtocol::~CommunicationProtocol()
{
  delete this->inputStreamReader;
  delete this->outputStreamWriter;
}

void CommunicationProtocol::Init(CommunicationChannel *commChannel)
{
  this->commChannel = commChannel;
}

int CommunicationProtocol::ReadInput()
{
  int oldLength = this->inputStream.length;

  while (true)
  {
    int bytesAvailable = this->commChannel->AvailableInputBytes();

    if (bytesAvailable <= 0)
      break;

    int originalPosition = this->inputStream.position;
    this->inputStream.position = this->inputStream.length;
    this->inputStream.EnsureOffsetLength(bytesAvailable);
    this->commChannel->Read(this->inputStream.GetPositionAddress(), bytesAvailable);
    this->inputStream.position = originalPosition;
  }

  int result = this->inputStream.length - oldLength;;
  
  while (this->ContainsFullChunk())
  {
    this->inputStreamReader->ReadChunkHeader();
  
    this->OnReceive(this->inputStreamReader->chunkId, this->inputStreamReader->chunkSize);

    this->SkipInputChunk();
  }
  
  this->ClearProcessedInputData();

  return result; 
}

void CommunicationProtocol::BeginChunk(int chunkId)
{
  this->outputStreamWriter->BeginChunk(chunkId);
}

void CommunicationProtocol::EndChunk()
{
  this->outputStreamWriter->EndChunk();
}

int CommunicationProtocol::Write()
{
  int result = this->commChannel->Write(this->outputStream.data, this->outputStream.length);

  this->outputStream.Clear();

  return result;
}

void CommunicationProtocol::SkipInputChunk()
{
  this->inputStreamReader->SkipChunk();
}

bool CommunicationProtocol::ContainsFullChunk()
{
  int originalPosition = this->inputStream.position;

  if (this->inputStream.length - this->inputStream.position < 2 + 2)
    return false;

  int chunkId = this->inputStream.ReadUInt16();
  int chunkSize = this->inputStream.ReadUInt16();

  bool result = this->inputStream.length - this->inputStream.position >= chunkSize;

  this->inputStream.position = originalPosition;

  return result;
}

void CommunicationProtocol::ClearProcessedInputData()
{
  this->inputStream.DeleteData(0, this->inputStream.position);
}
