using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.CNCDriver.Serial
{
    public delegate void ReceiveChunkDelegate(object sender, byte[] data, int dataSize);
    public delegate void ConnectedDelegate(object sender);
    public delegate void DisconnectedDeleate(object sender);

    public class SerialTransmission : SerialPort
    {
        const int chunkIdSize = 2;
        const int chunkFooterSize = 5;
        public const int chunkMaxDataSize = 255;
        const int chunkMaxTotalSize = chunkMaxDataSize + chunkFooterSize;
        static readonly byte[] chunkId = { 0xE6, 0x71 };

        int readBufferPosition, detectedFooterOffset;
        byte[] readBuffer = new byte[chunkMaxTotalSize];
        byte[] chunkData = new byte[chunkFooterSize - chunkIdSize];

        public event ReceiveChunkDelegate ReceiveChunk;
        public event DisconnectedDeleate Disconnected;

        public SerialTransmission(string comPortName, int baudRate = 9600, int bufferSize = 65536)
            : base(comPortName, baudRate, Parity.None, 8, StopBits.One)
        {
            this.DataReceived += SerialTransmission_DataReceived;
            this.ErrorReceived += SerialTransmission_ErrorReceived;
            this.Open();
        }

        private void SerialTransmission_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            this.OnDisconnected();
        }

        // CRC16-CCITT, polynomial 0x1021 (0b1000000100001), that is x^16 + x^12 + x^5 + 1
        static private ushort CRC16(byte[] source, uint size, uint offset = 0, ushort seed = 0x1D0F)
        {
            ushort result = seed;

            for (uint i = 0; i < size; i++)
            {
                byte x = (byte)(result >> 8 ^ source[offset + i]);
                x ^= (byte)(x >> 4);
                result = (ushort)((ushort)(result << 8) ^ (ushort)(x << 12) ^ (ushort)(x << 5) ^ (ushort)x);
            }

            return result;
        }

        private void SerialTransmission_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int dataSize = this.BytesToRead;
            byte[] data = new byte[dataSize];
            this.Read(data, 0, dataSize);

            this.OnReceive(data, dataSize);
        }

        public virtual int SendChunk(byte[] data, int dataSize)
        {
            byte[] writeBuffer = new byte[SerialTransmission.chunkMaxTotalSize];

            int sendDataSize = Math.Min(dataSize, SerialTransmission.chunkMaxDataSize);

            Array.Copy(data, writeBuffer, sendDataSize);
            Array.Copy(SerialTransmission.chunkId, 0, writeBuffer, sendDataSize, SerialTransmission.chunkIdSize);
            writeBuffer[sendDataSize + SerialTransmission.chunkIdSize] = (byte)sendDataSize;
            Array.Copy(BitConverter.GetBytes(SerialTransmission.CRC16(data, (uint)dataSize)), 0, writeBuffer, sendDataSize + SerialTransmission.chunkIdSize + 1, 2);

            this.Write(writeBuffer, 0, sendDataSize + SerialTransmission.chunkFooterSize);

            return sendDataSize + SerialTransmission.chunkFooterSize;
        }

        public virtual void OnReceive(byte[] data, int dataSize)
        {
            for (int i = 0; i < dataSize; i++)
            {
                byte dataByte = data[i];

                this.readBuffer[this.readBufferPosition] = dataByte;
                this.readBufferPosition++;

                if (this.detectedFooterOffset < SerialTransmission.chunkIdSize)
                {
                    if (dataByte == SerialTransmission.chunkId[this.detectedFooterOffset])
                        this.detectedFooterOffset++;
                    else
                        this.detectedFooterOffset = 0;
                }
                else if (this.detectedFooterOffset < SerialTransmission.chunkFooterSize)
                {
                    this.chunkData[this.detectedFooterOffset - SerialTransmission.chunkIdSize] = dataByte;
                    this.detectedFooterOffset++;

                    if (this.detectedFooterOffset == SerialTransmission.chunkFooterSize)
                    {
                        this.detectedFooterOffset = 0;

                        byte payloadSize = this.chunkData[0];
                        ushort payloadCrc16 = BitConverter.ToUInt16(this.chunkData, 1);

                        int payloadPosition = this.readBufferPosition - SerialTransmission.chunkFooterSize - payloadSize;
                        byte[] chunkData = new byte[SerialTransmission.chunkMaxDataSize];
                        if (payloadPosition >= 0)
                        {
                            Array.Copy(this.readBuffer, payloadPosition, chunkData, 0, payloadSize);
                            ushort actualCrc16 = SerialTransmission.CRC16(chunkData, payloadSize);
                            if (actualCrc16 == payloadCrc16)
                            {
                                this.OnReceiveChunk(chunkData, payloadSize);

                                this.readBufferPosition = 0;
                            }
                        }
                        else
                        {
                            Array.Copy(this.readBuffer, SerialTransmission.chunkMaxTotalSize + payloadPosition, chunkData, 0, -payloadPosition);
                            Array.Copy(this.readBuffer, 0, chunkData, -payloadPosition, payloadSize + payloadPosition);
                            ushort actualCrc16 = SerialTransmission.CRC16(chunkData, payloadSize);
                            if (actualCrc16 == payloadCrc16)
                            {
                                this.OnReceiveChunk(chunkData, payloadSize);

                                this.readBufferPosition = 0;
                            }
                        }
                    }
                }

                this.readBufferPosition %= SerialTransmission.chunkMaxTotalSize;
            }
        }

        public virtual void OnReceiveChunk(byte[] data, int dataSize)
        {
            if (this.ReceiveChunk != null)
                this.ReceiveChunk.Invoke(this, data, dataSize);
        }

        public virtual void OnDisconnected()
        {
            if (this.Disconnected != null)
                this.Disconnected.Invoke(this);
        }

        new public void Close()
        {
            if (this.IsOpen)
                base.Close();
        }
    }
}
