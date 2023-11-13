#ifndef HierarchyStreamReader_h
#define HierarchyStreamReader_h

#include "BaseStream.h"
#include "Stack.h"

class HierarchyStreamReader
{
public:
  Stack <unsigned short> chunkStack;

	BaseStream *stream;

	unsigned short chunkId;
	unsigned short chunkSize;

  HierarchyStreamReader(BaseStream &stream);
	~HierarchyStreamReader(void);

  bool ReadChunkHeader();
  void SkipChunk();
};

#endif
// HierarchyStreamReader_h
