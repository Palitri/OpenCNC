#ifndef HierarchyStreamWriter_h
#define HierarchyStreamWriter_h

#include "BaseStream.h"
#include "Stack.h"

class HierarchyStreamWriter
{
private:
  Stack<unsigned short> chunkStack;
  
public:
  BaseStream *stream;
  
  HierarchyStreamWriter(BaseStream &stream);
  ~HierarchyStreamWriter(void);
  
  void BeginChunk(unsigned short chunkId);
  void EndChunk();
};

#endif
// HierarchyStreamWriter_h
