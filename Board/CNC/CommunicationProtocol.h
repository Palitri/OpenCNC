#ifndef CommunicationProtocol_h
#define CommunicationProtocol_h

#include "MemoryStream.h"
#include "CommunicationChannel.h"
#include "HierarchyStreamWriter.h"
#include "HierarchyStreamReader.h"

class CommunicationProtocol
{
private:

  static const int SizeOfChunkId = 2;
  static const int OffsetOfChunkId = 0;
  static const int SizeOfChunkSize = 2;
  static const int OffsetOfChunkSize = CommunicationProtocol::OffsetOfChunkId + CommunicationProtocol::SizeOfChunkId;
  
  static const int SizeOfChunkHeader = CommunicationProtocol::SizeOfChunkId + CommunicationProtocol::SizeOfChunkSize;
  static const int OffsetOfChunkData = CommunicationProtocol::OffsetOfChunkSize + CommunicationProtocol::SizeOfChunkSize;

  bool ReadInputChunk();
  void SkipInputChunk();

  bool ContainsFullChunk();
  void ClearProcessedInputData();

public:
  MemoryStream inputStream, outputStream;
  CommunicationChannel *commChannel;
  HierarchyStreamWriter *outputStreamWriter;
  HierarchyStreamReader *inputStreamReader;
  
  
  CommunicationProtocol();
  virtual ~CommunicationProtocol();

  virtual bool OnReceive(int chunkId, int chunkSize) = 0;
  void Init(CommunicationChannel *commChannel);
  int ReadInput();
  void BeginChunk(int chunkId);
  void EndChunk();
  int Write();
};

#endif
// CommunicationProtocol_h
