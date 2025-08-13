using OpenIoT.Lib.Tools.Protocol;
using Palitri.OpenCNC.Driver.Settings;
using Palitri.OpenCNC.Driver.Utils;
using Palitri.OpenIoT.Board.Api;
using Palitri.OpenIoT.Board.Protocol.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using static Palitri.OpenCNC.Driver.Settings.OpenIoTBoardSettings;

namespace Palitri.OpenCNC.Driver
{
    public class CNCOpenIoTDevice : ICNC
    {
        // OpenIOT
        const int CommandCode_SetPropertiesValues = 0x23;
        const int CommandCode_SendCommand = 0x48;

        // Asynch peripheral
        const int CommandCode_SetNumberOfChannels = 1;
        const int CommandCode_SetChannelDevice = 2;
        const int CommandCode_SetChannelMapper = 3;
        const int CommandCode_SetVector = 4;
        const int CommandCode_Drive = 5;

        // CNC peripheral
        const int CommandCode_SetAsyncDevice = 1;
        const int CommandCode_Polyline = 11;
        const int CommandCode_Bezier = 12;
        const int CommandCode_Arc = 13;
        const int CommandCode_Wait = 14;

        // Pin Peripheral
        const int CommandCode_SetPWM = 1;

        // Shift register Peripheral
        const int CommandCode_SetBits = 1;



        private OpenIoTBoard board;

        public float speed;

        private OpenIoTCommandsWriter commandWriter;

        internal AutoResetEvent idleEvent;

        private OpenIoTBoardSettings boardSettings;

        private float stepMultiplicationFactor;

        private byte[] configurationBits;


        private CNCState _state;
        public CNCState State 
        {
            get
            {
                return this._state;
            }
            set
            {
                var oldState = this._state;

                this._state = value;

                this.StateChanged?.Invoke(this, this._state, oldState);
            }
        }
        public event StateChangedDelegate StateChanged;

        public CNCOpenIoTDevice(OpenIoTBoard board, OpenIoTBoardSettings settings)
        {
            this.board = board;
            this.board.EventHandlers.Add(new CNCOpenIoTDeviceEventsHandler(this));

            this.commandWriter = new OpenIoTCommandsWriter();

            this.idleEvent = new AutoResetEvent(true);

            this.boardSettings = settings;

            this.stepMultiplicationFactor = 1.0f;

            this.configurationBits = [0, 0, 0, 0];


            this.Begin();
            this.InitializePeripheralConfiguration();
            this.End();
            this.Execute();
        }

        private void InitializePeripheralConfiguration()
        {
            this.commandWriter.BeginCommand(this.boardSettings.AsyncDriverPeripheralID, CommandCode_SetNumberOfChannels);
            this.commandWriter.WriteUInt8((byte)this.boardSettings.AxesSettings.Count);
            this.commandWriter.EndCommand();

            for (int i = 0; i < this.boardSettings.AxesSettings.Count; i++)
                this.MapDevice(i, this.boardSettings.AxesSettings[i].PeripheralId);

            this.commandWriter.BeginCommand(this.boardSettings.CNCPeripheralID, CommandCode_SetAsyncDevice);
            this.commandWriter.WriteUInt8((byte)this.boardSettings.AsyncDriverPeripheralID);
            this.commandWriter.EndCommand();
        }

        public void Polyline(CNCVector[] vectors)
        {
            int numDimensions = this.boardSettings.AxesSettings.Count;
            float speed = this.speed * this.stepMultiplicationFactor * 200.0f / 8.0f;

            this.commandWriter.BeginCommand(this.boardSettings.CNCPeripheralID, CommandCode_Polyline);

            this.commandWriter.WriteFloat32(speed);

            foreach (CNCVector vector in vectors)
                for (int d = 0; d < numDimensions; d++)
                    this.commandWriter.WriteFloat32(vector.values[d] * this.stepMultiplicationFactor * this.boardSettings.AxesSettings[d].StepsPerUnit);

            this.commandWriter.EndCommand();
        }

        public void Bezier(CNCVector[] vectors)
        {
            int numDimensions = this.boardSettings.AxesSettings.Count;
            float speed = this.speed * this.stepMultiplicationFactor * 200.0f / 8.0f;

            this.commandWriter.BeginCommand(this.boardSettings.CNCPeripheralID, CommandCode_Bezier);

            this.commandWriter.WriteFloat32(speed);

            foreach (CNCVector vector in vectors)
                for (int d = 0; d < numDimensions; d++)
                    this.commandWriter.WriteFloat32(vector.values[d] * this.stepMultiplicationFactor * this.boardSettings.AxesSettings[d].StepsPerUnit);

            this.commandWriter.EndCommand();
        }

        public void Arc(CNCVector semiMajorAxis, CNCVector semiMinorAxis, float startAngle, float endAngle)
        {
            int numDimensions = this.boardSettings.AxesSettings.Count;
            float speed = this.speed * this.stepMultiplicationFactor * 200.0f / 8.0f;

            this.commandWriter.BeginCommand(this.boardSettings.CNCPeripheralID, CommandCode_Arc);

            this.commandWriter.WriteFloat32(speed);

            this.commandWriter.WriteFloat32(startAngle);
            this.commandWriter.WriteFloat32(endAngle);

            for (int d = 0; d < numDimensions; d++)
                this.commandWriter.WriteFloat32(semiMajorAxis.values[d] * this.stepMultiplicationFactor * this.boardSettings.AxesSettings[d].StepsPerUnit);

            for (int d = 0; d < numDimensions; d++)
                this.commandWriter.WriteFloat32(semiMinorAxis.values[d] * this.stepMultiplicationFactor * this.boardSettings.AxesSettings[d].StepsPerUnit);

            this.commandWriter.EndCommand();
        }

        public void Begin()
        {
            this.commandWriter.Reset();
        }

        public void End()
        {
        }

        public void Execute()
        {
            this.idleEvent.Reset();
            
            this.State = CNCState.Busy;
            
            this.board.SendCommand(CommandCode_SendCommand, this.commandWriter.Data, this.commandWriter.Size);

            this.commandWriter.Reset();

            this.idleEvent.WaitOne();
        }

        public void MapDevice(int channel, int peripheralId)
        {
            this.commandWriter.BeginCommand(this.boardSettings.AsyncDriverPeripheralID, CommandCode_SetChannelDevice);
            this.commandWriter.WriteUInt8((byte)channel);
            this.commandWriter.WriteUInt8((byte)peripheralId);
            this.commandWriter.EndCommand();
        }

        public void SetDriveVector(int channel, float driveVector)
        {
            this.commandWriter.BeginCommand(this.boardSettings.AsyncDriverPeripheralID, CommandCode_SetVector);
            this.commandWriter.WriteUInt8((byte)channel);
            this.commandWriter.WriteFloat32(driveVector);
            this.commandWriter.EndCommand();
        }

        public void Drive(float time)
        {
            this.commandWriter.BeginCommand(this.boardSettings.AsyncDriverPeripheralID, CommandCode_Drive);
            this.commandWriter.WriteFloat32(time);
            this.commandWriter.EndCommand();
        }

        public void DriveLinear(float origin, float vector)
        {
            //Remove
        }

        public void DriveSine(float offset, float span, float amplitude, float phaseStart, float phaseEnd)
        {
            //Remove?
        }

        public void SetPropertyValue(int property, float value)
        {
            byte[] valueBytes = BitConverter.GetBytes(value);

            byte[] data = new byte[6];
            data[0] = 1;
            data[1] = (byte)property;
            Array.Copy(valueBytes, 0, data, 2, 4);
            this.board.SendCommand(CommandCode_SetPropertiesValues, data);
        }
        public void SetPropertyValue(int property, bool value)
        {
            byte[] data = new byte[3];
            data[0] = 1;
            data[1] = (byte)property;
            data[2] = (byte)(value ? 1 : 0);
            this.board.SendCommand(CommandCode_SetPropertiesValues, data);
        }

        public void SetMotorsPowerMode(bool powerOn)
        {
            foreach (AsyncChannelSetting axisSetting in this.boardSettings.AxesSettings)
                BitUtils.SetBits(this.configurationBits, axisSetting.EnableBitmask, powerOn ? axisSetting.EnableValueOn : axisSetting.EnableValueOff);

            this.WriteCommandSetBits();
        }

        public void SetMotorsStepMode(int motorGroup, CNCMotorStepMode stepMode)
        {
            this.stepMultiplicationFactor = (float)((int)stepMode);

            foreach (AsyncChannelSetting axisSetting in this.boardSettings.AxesSettings)
            {
                byte[] stepModeValue;
                switch (stepMode)
                {
                    case CNCMotorStepMode.Full: stepModeValue = axisSetting.StepModeValueFull; break;
                    case CNCMotorStepMode.Half: stepModeValue = axisSetting.StepModeValueHalf; break;
                    case CNCMotorStepMode.Quarter: stepModeValue = axisSetting.StepModeValueQuarter; break;
                    case CNCMotorStepMode.Eighth: stepModeValue = axisSetting.StepModeValueEighth; break;
                    case CNCMotorStepMode.Sixteenth: stepModeValue = axisSetting.StepModeValueSixteenth; break;
                    default: stepModeValue = axisSetting.StepModeValueFull; break;
                }

                BitUtils.SetBits(this.configurationBits, axisSetting.StepModeBitmask, stepModeValue);
            }

            this.WriteCommandSetBits();
        }

        public void SetPower(float power)
        {
            this.commandWriter.BeginCommand(this.boardSettings.ToolPeripheralID, CommandCode_SetPWM);
            this.commandWriter.WriteFloat32(power);
            this.commandWriter.EndCommand();
        }

        public void SetMotorsSleepMode(bool sleep)
        {
            foreach (AsyncChannelSetting axisSetting in this.boardSettings.AxesSettings)
                BitUtils.SetBits(this.configurationBits, axisSetting.SleepBitmask, sleep ? axisSetting.SleepValueOn : axisSetting.SleepValueOff);

            this.WriteCommandSetBits();
        }

        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }

        public void SetToolPowerMode(bool awake)
        {
            BitUtils.SetBits(this.configurationBits, this.boardSettings.ToolEnableBitmask, awake ? this.boardSettings.ToolEnableValueOn : this.boardSettings.ToolEnableValueOff);

            this.WriteCommandSetBits();
        }

        public void SetRelay(int relayIndex, bool enable)
        {
            BitUtils.SetBits(this.configurationBits, this.boardSettings.RelayBitmask, enable ? this.boardSettings.RelayValueOn: this.boardSettings.RelayValueOff);

            this.WriteCommandSetBits();
        }

        public void Tone(int[] channels, float frequency, float duration)
        {
            // Kinda. Think about repeat/oscillate command. Or simply tone command. Or playing by rotating back and forth, channel after channel - better spectacle
            float vector = frequency * duration;
            foreach (int channel in channels)
            {
                this.SetDriveVector(channel, vector);
            }

            this.Drive(duration);
        }

        public void Wait(float time)
        {
            this.commandWriter.BeginCommand(this.boardSettings.CNCPeripheralID, CommandCode_Wait);
            this.commandWriter.WriteFloat32(time);
            this.commandWriter.EndCommand();
        }

        private void WriteCommandSetBits()
        {
            this.commandWriter.BeginCommand(this.boardSettings.ShiftRegPeripheralID, CommandCode_SetBits);
            this.commandWriter.WriteUInt8(this.configurationBits[0]);
            this.commandWriter.WriteUInt8(this.configurationBits[1]);
            this.commandWriter.WriteUInt8(this.configurationBits[2]);
            this.commandWriter.WriteUInt8(this.configurationBits[3]);
            this.commandWriter.EndCommand();
        }
    }

    internal class CNCOpenIoTDeviceEventsHandler : OpenIoTProtocolEventsHandler
    {
        private CNCOpenIoTDevice device;

        private IntPtr handle;

        public CNCOpenIoTDeviceEventsHandler(CNCOpenIoTDevice device)
        {
            this.device = device;
        }

        public override void OnCommandExecuted(object sender)
        {
            this.device.State = CNCState.Ready;
            this.device.idleEvent.Set();
        }
    }
}
