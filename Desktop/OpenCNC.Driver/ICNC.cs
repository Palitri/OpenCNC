using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.OpenCNC.Driver
{
    public delegate void StateChangedDelegate(ICNC cnc, CNCState newState, CNCState oldState);

    public interface ICNC
    {
        event StateChangedDelegate StateChanged;

        void Begin();
        void End();
        void Execute();

        void Wait(float time);
        void Tone(int[] channels, float frequency, float duration);
        void SetToolPowerMode(bool awake);
        void SetAxesEnabled(string groupName, bool enabled);
        void SetMotorsPowerMode(bool enabled);
        void SetMotorsSleepMode(string groupName, bool awake);
        void SetMotorsSleepMode(bool awake);
        void SetMotorsStepMode(string groupName, CNCMotorStepMode stepMode);
        void SetMotorsStepMode(CNCMotorStepMode stepMode);
        void SetPropertyValue(int property, float value);
        void SetPropertyValue(int property, bool value);
        void SetChannelsDevices(byte[] peripheralId);
        void SetAxesChannels(byte[] channels);
        void SetDriveVector(int channel, float origin, float vector);
        void ResetDriveVectors();
        void Drive(float time);
        void SetRelay(int relayIndex, bool enabled);

        void SetPower(float power);
        void SetSpeed(float speed);
        void Polyline(CNCVector[] vectors);
        void Arc(CNCVector semiMajorAxis, CNCVector semiMinorAxis, float startAngle, float endAngle);
        void Bezier(CNCVector[] vectors);
    }
}
