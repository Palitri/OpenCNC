#include "HierarchyStreamWriter.h"


HierarchyStreamWriter::HierarchyStreamWriter(BaseStream &stream)
{
    this->stream = &stream;
}


HierarchyStreamWriter::~HierarchyStreamWriter(void)
{
}

void HierarchyStreamWriter::BeginChunk(unsigned short chunkId)
{
	this->stream->WriteUInt16(chunkId);
	this->chunkStack.Push(this->stream->position);
	this->stream->WriteUInt16(0);
}

void HierarchyStreamWriter::EndChunk()
{
	unsigned int currentPos = this->stream->position;
	this->stream->Seek(this->chunkStack.Pop());
	this->stream->WriteUInt16(currentPos - this->stream->position - 2);
	this->stream->Seek(currentPos);
}
