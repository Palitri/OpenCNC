#include "HierarchyStreamReader.h"


HierarchyStreamReader::HierarchyStreamReader(BaseStream &stream)
{
    this->stream = &stream;
}


HierarchyStreamReader::~HierarchyStreamReader(void)
{
}

bool HierarchyStreamReader::ReadChunkHeader()
{
  while (this->chunkStack.count > 0)
    if (this->chunkStack.Peek() <= this->stream->position)
      this->stream->Seek(this->chunkStack.Pop());
    else
      break;
  
  if (this->stream->length - this->stream->position < 2 + 2)
    return false;
  
  this->chunkId = this->stream->ReadUInt16();
  this->chunkSize = this->stream->ReadUInt16();
  this->chunkStack.Push(this->stream->position + this->chunkSize);
  
  return true;
}

void HierarchyStreamReader::SkipChunk()
{
  while (this->chunkStack.count > 0)
    if (this->chunkStack.Peek() < this->stream->position)
      this->stream->Seek(this->chunkStack.Pop());
    else
      break;
  
  if (this->chunkStack.count > 0)
    this->stream->Seek(this->chunkStack.Pop());
}
