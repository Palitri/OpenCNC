using OpenIoT.Lib.Tools.Protocol;
using OpenIoT.Lib.Tools.Protocol.Commands;
using Palitri.OpenCNC.Driver.Settings;
using Palitri.OpenCNC.Driver.Utils;
using Palitri.OpenIoT.Board.Api;
using Palitri.OpenIoT.Board.Protocol.Events;
using Palitri.OpenIoT.Tools.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using static Palitri.OpenCNC.Driver.Settings.OpenIoTBoardConfiguration;

namespace Palitri.OpenCNC.Driver
{
    public class CNCOpenIoTDevice : ICNC
    {
        // PropertyTransmissionProtocol
        const int CommandCode_Wait = 0x12;
        const int CommandCode_SetPropertiesValues = 0x23;

        // OpenIoTTransmissionProtocol
        const int CommandCode_ExecutePeripheralCommand = 0x48;

        // Asynch peripheral
        const int CommandCode_AsyncPeripheral_SetNumberOfChannels = 1;
        const int CommandCode_AsyncPeripheral_SetChannelDevice = 2;
        const int CommandCode_AsyncPeripheral_SetChannelMapper = 3;
        const int CommandCode_AsyncPeripheral_SetVector = 4;
        const int CommandCode_AsyncPeripheral_Drive = 5;

        // CNC peripheral
        const int CommandCode_CNCPeripheral_SetAsyncDevice = 1;
        const int CommandCode_CNCPeripheral_Polyline = 11;
        const int CommandCode_CNCPeripheral_Bezier = 12;
        const int CommandCode_CNCPeripheral_Arc = 13;

        // Pin Peripheral
        const int CommandCode_PinPeripheral_SetPWM = 1;

        // Shift register Peripheral
        const int CommandCode_ShiftRegPeripheral_SetBits = 1;



        private OpenIoTBoard board;

        public float speed;

        private OpenIoTCommandsWriter commandWriter;

        internal AutoResetEvent idleEvent;

        private OpenIoTBoardConfiguration boardConfiguration;

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

        public CNCOpenIoTDevice(OpenIoTBoard board, OpenIoTBoardConfiguration configuration)
        {
            this.board = board;
            this.board.EventHandlers.Add(new CNCOpenIoTDeviceEventsHandler(this));

            this.commandWriter = new OpenIoTCommandsWriter();

            this.idleEvent = new AutoResetEvent(true);

            this.boardConfiguration = configuration;

            this.stepMultiplicationFactor = 1.0f;

            int configurationBitsByteCount = (this.boardConfiguration.ControlBitsCount / 8) + ((this.boardConfiguration.ControlBitsCount % 8) == 0 ? 0 : 1);
            this.configurationBits = new byte[configurationBitsByteCount];


            this.Begin();
            this.InitializePeripheralConfiguration();
            this.End();
            this.Execute();

            // Workaround, so that script command Execute works (bacuse is calls End(), Send(), Begin())
            //this.Begin();
        }

        private void InitializePeripheralConfiguration()
        {
            this.commandWriter.BeginCommand(this.boardConfiguration.AsyncDriverPeripheralID, CommandCode_AsyncPeripheral_SetNumberOfChannels);
            this.commandWriter.WriteUInt8((byte)this.boardConfiguration.Axes.Count);
            this.commandWriter.EndCommand();

            for (int i = 0; i < this.boardConfiguration.Axes.Count; i++)
                this.MapDevice(i, this.boardConfiguration.Axes[i].PeripheralId);

            this.commandWriter.BeginCommand(this.boardConfiguration.CNCPeripheralID, CommandCode_CNCPeripheral_SetAsyncDevice);
            this.commandWriter.WriteUInt8((byte)this.boardConfiguration.AsyncDriverPeripheralID);
            this.commandWriter.EndCommand();
        }

        public void Polyline(CNCVector[] vectors)
        {
            // TODO: Problematic - disregards settings - what if motors have different fullStepsPerTurn or axes have different unitsPerTurn?
            const float fullStepsPerTurn = 200.0f;
            const float unitsPerTurn = 8.0f;
            
            int numDimensions = this.boardConfiguration.Axes.Count;
            float speed = this.speed * this.stepMultiplicationFactor * fullStepsPerTurn / unitsPerTurn;

            this.commandWriter.BeginCommand(this.boardConfiguration.CNCPeripheralID, CommandCode_CNCPeripheral_Polyline);

            this.commandWriter.WriteFloat32(speed);

            foreach (CNCVector vector in vectors)
                for (int d = 0; d < numDimensions; d++)
                    this.commandWriter.WriteFloat32(vector.values[d] * this.stepMultiplicationFactor * this.boardConfiguration.Axes[d].StepsPerUnit);

            this.commandWriter.EndCommand();
        }

        public void Bezier(CNCVector[] vectors)
        {
            // TODO: Problematic - disregards settings - what if motors have different fullStepsPerTurn or axes have different unitsPerTurn?
            const float fullStepsPerTurn = 200.0f;
            const float unitsPerTurn = 8.0f;

            int numDimensions = this.boardConfiguration.Axes.Count;
            float speed = this.speed * this.stepMultiplicationFactor * fullStepsPerTurn / unitsPerTurn;

            this.commandWriter.BeginCommand(this.boardConfiguration.CNCPeripheralID, CommandCode_CNCPeripheral_Bezier);

            this.commandWriter.WriteFloat32(speed);

            foreach (CNCVector vector in vectors)
                for (int d = 0; d < numDimensions; d++)
                    this.commandWriter.WriteFloat32(vector.values[d] * this.stepMultiplicationFactor * this.boardConfiguration.Axes[d].StepsPerUnit);

            this.commandWriter.EndCommand();
        }

        public void Arc(CNCVector semiMajorAxis, CNCVector semiMinorAxis, float startAngle, float endAngle)
        {
            // TODO: Problematic - disregards settings - what if motors have different fullStepsPerTurn or axes have different unitsPerTurn?
            const float fullStepsPerTurn = 200.0f;
            const float unitsPerTurn = 8.0f;

            int numDimensions = this.boardConfiguration.Axes.Count;
            float speed = this.speed * this.stepMultiplicationFactor * fullStepsPerTurn / unitsPerTurn;

            this.commandWriter.BeginCommand(this.boardConfiguration.CNCPeripheralID, CommandCode_CNCPeripheral_Arc);

            this.commandWriter.WriteFloat32(speed);

            this.commandWriter.WriteFloat32(startAngle);
            this.commandWriter.WriteFloat32(endAngle);

            for (int d = 0; d < numDimensions; d++)
                this.commandWriter.WriteFloat32(semiMajorAxis.values[d] * this.stepMultiplicationFactor * this.boardConfiguration.Axes[d].StepsPerUnit);

            for (int d = 0; d < numDimensions; d++)
                this.commandWriter.WriteFloat32(semiMinorAxis.values[d] * this.stepMultiplicationFactor * this.boardConfiguration.Axes[d].StepsPerUnit);

            this.commandWriter.EndCommand();
        }

        public void Begin()
        {
            this.commandWriter.Reset();
            this.commandWriter.BeginCommand((int)CommandIdOpenIoTTransmission.ExecutePeripheralCommand);
        }

        public void End()
        {
            this.commandWriter.EndCommands();
        }

        public void Execute()
        {
            this.idleEvent.Reset();
            
            this.State = CNCState.Busy;

            this.board.SendChunk(this.commandWriter.Data, this.commandWriter.Size);
            this.Begin();

            this.idleEvent.WaitOne();
        }

        public void MapDevice(int channel, int peripheralId)
        {
            this.commandWriter.BeginCommand(this.boardConfiguration.AsyncDriverPeripheralID, CommandCode_AsyncPeripheral_SetChannelDevice);
            this.commandWriter.WriteUInt8((byte)channel);
            this.commandWriter.WriteUInt8((byte)peripheralId);
            this.commandWriter.EndCommand();
        }

        public void SetDriveVector(int channel, float driveVector)
        {
            this.commandWriter.BeginCommand(this.boardConfiguration.AsyncDriverPeripheralID, CommandCode_AsyncPeripheral_SetVector);
            this.commandWriter.WriteUInt8((byte)channel);
            this.commandWriter.WriteFloat32(driveVector);
            this.commandWriter.EndCommand();
        }
        public void ResetDriveVectors()
        {
            this.commandWriter.BeginCommand(this.boardConfiguration.AsyncDriverPeripheralID, (int)CommandIdPeripheralAsyncDriver.ResetVectors);
            this.commandWriter.EndCommand();
        }

        public void Drive(float time)
        {
            this.commandWriter.BeginCommand(this.boardConfiguration.AsyncDriverPeripheralID, CommandCode_AsyncPeripheral_Drive);
            this.commandWriter.WriteFloat32(time);
            this.commandWriter.EndCommand();
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
            foreach (AsyncChannelConfiguration axisSetting in this.boardConfiguration.Axes)
                BitUtils.SetBits(this.configurationBits, axisSetting.Enable.Bitmask, powerOn ? axisSetting.Enable.On : axisSetting.Enable.Off);

            this.WriteCommandSetBits();
        }

        public void SetMotorsStepMode(int motorGroup, CNCMotorStepMode stepMode)
        {
            this.stepMultiplicationFactor = (float)((int)stepMode);

            foreach (AsyncChannelConfiguration axisSetting in this.boardConfiguration.Axes)
            {
                byte[] stepModeValue;
                switch (stepMode)
                {
                    case CNCMotorStepMode.Full: stepModeValue = axisSetting.StepMode.Full; break;
                    case CNCMotorStepMode.Half: stepModeValue = axisSetting.StepMode.Half; break;
                    case CNCMotorStepMode.Quarter: stepModeValue = axisSetting.StepMode.Quarter; break;
                    case CNCMotorStepMode.Eighth: stepModeValue = axisSetting.StepMode.Eighth; break;
                    case CNCMotorStepMode.Sixteenth: stepModeValue = axisSetting.StepMode.Sixteenth; break;
                    default: stepModeValue = axisSetting.StepMode.Full; break;
                }

                BitUtils.SetBits(this.configurationBits, axisSetting.StepMode.Bitmask, stepModeValue);
            }

            this.WriteCommandSetBits();
        }

        public void SetPower(float power)
        {
            this.commandWriter.BeginCommand(this.boardConfiguration.ToolPeripheralID, CommandCode_PinPeripheral_SetPWM);
            this.commandWriter.WriteFloat32(power);
            this.commandWriter.EndCommand();
        }

        public void SetMotorsSleepMode(bool sleep)
        {
            foreach (AsyncChannelConfiguration axisSetting in this.boardConfiguration.Axes)
                BitUtils.SetBits(this.configurationBits, axisSetting.Sleep.Bitmask, sleep ? axisSetting.Sleep.On : axisSetting.Sleep.Off);

            this.WriteCommandSetBits();
        }

        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }

        public void SetToolPowerMode(bool awake)
        {
            BitUtils.SetBits(this.configurationBits, this.boardConfiguration.ToolEnable.Bitmask, awake ? this.boardConfiguration.ToolEnable.On : this.boardConfiguration.ToolEnable.Off);

            this.WriteCommandSetBits();
        }

        public void SetRelay(int relayIndex, bool enable)
        {
            if ((this.boardConfiguration.SwitchesSettings == null) || (this.boardConfiguration.SwitchesSettings.Count <= relayIndex))
                return;

            SwitchConfiguration relaySettings = this.boardConfiguration.SwitchesSettings[relayIndex];

            BitUtils.SetBits(this.configurationBits, relaySettings.Bitmask, enable ? relaySettings.On: relaySettings.Off);

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
            this.commandWriter.EndCommands();

            this.commandWriter.BeginCommand(CommandCode_Wait);
            this.commandWriter.WriteFloat32(time);
            this.commandWriter.EndCommand();

            this.commandWriter.BeginCommand(CommandCode_ExecutePeripheralCommand);
        }

        private void WriteCommandSetBits()
        {
            this.commandWriter.BeginCommand(this.boardConfiguration.ShiftRegPeripheralID, CommandCode_ShiftRegPeripheral_SetBits);
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
