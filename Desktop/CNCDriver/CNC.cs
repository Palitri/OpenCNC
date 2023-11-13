using Palitri.CNCDriver.Serial;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Palitri.CNCDriver
{
    public delegate void CNCConnectedDelegate(object sender);

    public class CNC : ICNC
    {
        private const byte CommandCode_Info = 0xB2;
        private const byte CommandCode_Wait = 0x01;
        private const byte CommandCode_Turn = 0x02;
        private const byte CommandCode_Tone = 0x03;
        private const byte CommandCode_SleepMode = 0x04;
        private const byte CommandCode_ToolPowerMode = 0x05;
        private const byte CommandCode_MotorPowerMode = 0x06;
        private const byte CommandCode_MotorStepMode = 0x07;
        private const byte CommandCode_IssueTurning = 0x08;
        private const byte CommandCode_MapDevice = 0x09;
        private const byte CommandCode_SetPower = 0x11;
        private const byte CommandCode_SetSpeed = 0x12;
        private const byte CommandCode_Polyline = 0x13;
        private const byte CommandCode_Arc = 0x14;
        private const byte CommandCode_Bezier = 0x15;
        private const byte CommandCode_Drive = 0x20;
        private const byte CommandCode_DriveLinear = 0x21;
        private const byte CommandCode_DriveSine = 0x22;

        private const byte ResponseCode_Info = 0xAC;
        private const byte ResponseCode_State = 0x01;
        private const byte ResponseCode_Setting = 0x02;

        //private const ushort SettingId_Speed = 0x01;
        //private const ushort SettingId_PowerMin = 0x02;
        //private const ushort SettingId_PowerMax = 0x03;


        SerialTransmission transmission;

        MemoryStream plotData;
        BinaryWriter plotDataWriter;

        private AutoResetEvent infoEvent, idleEvent;


        public event StateChangedDelegate StateChanged;
        public event CNCConnectedDelegate Connected;

        public CNCState State { get; private set; }
        public string Port { get; private set; }
        public string Info { get; private set; }
        public int ConnectionTimeoutMilliseconds { get; set; }

        public Dictionary<int, CNCProperty> Properties { get; private set; }

        public CNC()
        {
            this.plotData = new MemoryStream();
            this.plotDataWriter = new BinaryWriter(this.plotData);

            this.State = CNCState.Unavailable;
            this.ConnectionTimeoutMilliseconds = 200;

            this.Properties = new Dictionary<int, CNCProperty>();

            this.infoEvent = new AutoResetEvent(true);
            this.idleEvent = new AutoResetEvent(true);
        }

        public bool Connect(int startPort = 0, int endPort = 255)
        {
            for (int port = startPort; port <= endPort; port++)
                if (this.Connect(string.Format("COM{0}", port)))
                {
                    if (this.Connected != null)
                        this.Connected(this);

                    return true;
                }

            return false;
        }

        public bool Connect(string comPort)
        {
            try
            {
                this.transmission = new SerialTransmission(comPort);
                this.transmission.ReceiveChunk += transmission_ReceiveChunk;

                if (this.RequestInfo())
                {
                    this.State = CNCState.Ready;
                    this.Port = comPort;

                    return true;
                }

                this.transmission.ReceiveChunk -= transmission_ReceiveChunk;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private void transmission_ReceiveChunk(object sender, byte[] data, int dataSize)
        {
            BinaryReader responseReader = new BinaryReader(new MemoryStream(data));
            byte responseCode = responseReader.ReadByte();

            switch (responseCode)
            {
                case CNC.ResponseCode_Info:
                {
                    this.Info = new string(responseReader.ReadChars(responseReader.ReadByte()));
                    this.infoEvent.Set();
                    break;
                }

                case CNC.ResponseCode_State:
                {
                    CNCState oldState = this.State;
                    this.State = (CNCState)responseReader.ReadByte();

                    if (this.State == CNCState.Ready)
                        this.idleEvent.Set();

                    if (this.StateChanged != null)
                        this.StateChanged(this, this.State, oldState);

                    break;
                }
            }
        }

        public bool RequestInfo()
        {
            this.plotData.SetLength(0);

            this.infoEvent.Reset();

            this.plotDataWriter.Write((byte)CommandCode_Info);
            this.transmission.SendChunk(this.plotData.GetBuffer(), (int)this.plotData.Length);

            return this.infoEvent.WaitOne(this.ConnectionTimeoutMilliseconds);
        }

        public void Begin()
        {
            this.plotData.SetLength(0);
        }

        public void End()
        {

        }

        public void Execute()
        {
            if (this.plotData.Length > SerialTransmission.chunkMaxDataSize)
                throw new Exception(string.Format("Chunk length too big: {0}. Can be {1} at most.", this.plotData.Length, SerialTransmission.chunkMaxDataSize));

            this.idleEvent.Reset();

            this.transmission.SendChunk(this.plotData.GetBuffer(), (int)this.plotData.Length);

            this.idleEvent.WaitOne();
        }

        public async Task ExecuteAsync()
        {
            await Task.Run(() => this.Execute());
        }

        public void Wait(float time)
        {
            this.plotDataWriter.Write((byte)CommandCode_Wait);
            this.plotDataWriter.Write((float)time);
        }

        public void Turn(int motors, CNCRotationDirection direction, int interval, int steps)
        {
            this.plotDataWriter.Write((byte)CommandCode_Turn);
            this.plotDataWriter.Write((byte)motors);
            this.plotDataWriter.Write((byte)direction);
            this.plotDataWriter.Write((ushort)interval);
            this.plotDataWriter.Write((ushort)steps);
        }

        public void Tone(int motors, float frequency, float duration)
        {
            this.plotDataWriter.Write((byte)CommandCode_Tone);
            this.plotDataWriter.Write((byte)motors);
            this.plotDataWriter.Write((float)frequency);
            this.plotDataWriter.Write((float)duration);
        }

        public void SetToolPowerMode(bool powered)
        {
            this.plotDataWriter.Write((byte)CommandCode_ToolPowerMode);
            this.plotDataWriter.Write(powered);
        }

        public void SetPowerMode(bool awake)
        {
            this.plotDataWriter.Write((byte)CommandCode_SleepMode);
            this.plotDataWriter.Write(!awake);
        }

        public void SetMotorsPowerMode(bool powerOn)
        {
            this.plotDataWriter.Write((byte)CommandCode_MotorPowerMode);
            this.plotDataWriter.Write(powerOn);
        }

        public void SetMotorsStepMode(int motorGroup, CNCMotorStepMode stepMode)
        {
            this.plotDataWriter.Write((byte)CommandCode_MotorStepMode);
            this.plotDataWriter.Write((byte)motorGroup);
            this.plotDataWriter.Write((byte)stepMode);
        }

        public void IssueMotorTurning(int motor, float speed)
        {
            this.plotDataWriter.Write((byte)CommandCode_IssueTurning);
            this.plotDataWriter.Write((byte)motor);
            this.plotDataWriter.Write((float)speed);
        }

        public void MapDevice(CNCDevice device)
        {
            this.plotDataWriter.Write((byte)CommandCode_MapDevice);
            this.plotDataWriter.Write((byte)device);
        }

        public void Drive(float time)
        {
            this.plotDataWriter.Write((byte)CommandCode_Drive);
            this.plotDataWriter.Write((float)time);
        }

        public void DriveLinear(float origin, float vector)
        {
            this.plotDataWriter.Write((byte)CommandCode_DriveLinear);
            this.plotDataWriter.Write((float)origin);
            this.plotDataWriter.Write((float)vector);
        }

        public void DriveSine(float offset, float span, float amplitude, float phaseStart, float phaseEnd)
        {
            this.plotDataWriter.Write((byte)CommandCode_DriveSine);
            this.plotDataWriter.Write((float)offset);
            this.plotDataWriter.Write((float)span);
            this.plotDataWriter.Write((float)amplitude);
            this.plotDataWriter.Write((float)phaseStart);
            this.plotDataWriter.Write((float)phaseEnd);
        }

        public void SetPower(float power)
        {
            this.plotDataWriter.Write((byte)CommandCode_SetPower);
            this.plotDataWriter.Write((float)power);
        }

        public void SetSpeed(float speed)
        {
            this.plotDataWriter.Write((byte)CommandCode_SetSpeed);
            this.plotDataWriter.Write((float)speed);
        }

        public void Polyline(CNCVector[] vectors)
        {
            this.plotDataWriter.Write((byte)CommandCode_Polyline);
            this.plotDataWriter.Write((byte)vectors.Length);
            foreach (CNCVector point in vectors)
                this.WriteVector(point);
        }

        public void Arc(CNCVector semiMajorAxis, CNCVector semiMinorAxis, float startAngle, float endAngle)
        {
            this.plotDataWriter.Write((byte)CommandCode_Arc);
            this.WriteVector(semiMajorAxis);
            this.WriteVector(semiMinorAxis);
            this.plotDataWriter.Write((float)startAngle);
            this.plotDataWriter.Write((float)endAngle);
        }

        public void Bezier(CNCVector[] vectors)
        {
            this.plotDataWriter.Write((byte)CommandCode_Bezier);
            this.plotDataWriter.Write((byte)vectors.Length);
            foreach (CNCVector point in vectors)
                this.WriteVector(point);
        }

        private void WriteVector(CNCVector vector)
        {
            this.plotDataWriter.Write((float)vector.X);
            this.plotDataWriter.Write((float)vector.Y);
            this.plotDataWriter.Write((float)vector.Z);
        }

        //private CNCProperty GetProperty(sbyte id)
        //{
        //    this.Begin();
        //    this.plotDataWriter.Write((byte)CommandCode_GetSetting);
        //    this.plotDataWriter.Write(id);
        //    this.End();
        //    this.Execute();
        //}


        public void Close()
        {
            this.transmission.Close();
        }
    }
}
