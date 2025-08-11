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

        bool RequestInfo();
        
        void Wait(float time);
        void Turn(int motors, CNCRotationDirection direction, int interval, int steps);
        void Tone(int motors, float frequency, float duration);
        void SetToolPowerMode(bool awake);
        void SetMotorsSleepMode(bool awake);
        void SetMotorsPowerMode(bool powerOn);
        void SetMotorsStepMode(int motorGroup, CNCMotorStepMode stepMode);
        void IssueMotorTurning(int motor, float speed);
        void MapDevice();
        void Drive(float time);
        void DriveLinear(float origin, float vector);
        void DriveSine(float offset, float span, float amplitude, float phaseStart, float phaseEnd);

        void SetPower(float power);
        void SetSpeed(float speed);
        void Polyline(CNCVector[] vectors);
        void Arc(CNCVector semiMajorAxis, CNCVector semiMinorAxis, float startAngle, float endAngle);
        void Bezier(CNCVector[] vectors);
    }
}
