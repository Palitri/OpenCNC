using Palitri.AudioSynthesis;
using Palitri.OpenCNC.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCNC.App.Devices
{
    public class CNCAudioDevice : IAudioDevice
    {
        private ICNC cnc;
        private int motors;

        public CNCAudioDevice(ICNC cnc, int motors)
        {
            this.cnc = cnc;
            this.motors = motors;
        }

        public void SynthesizeBegin()
        {
            this.cnc.Begin();
            this.cnc.SetMotorsPowerMode(true);
        }

        public void SynthesizeEnd()
        {
            this.cnc.SetMotorsPowerMode(false);
            this.cnc.End();
            this.cnc.Execute();
        }

        public void Synthesize(float frequency, float duration)
        {
            this.cnc.Tone(this.motors, frequency, duration);
        }

        public void Silence(float duration)
        {
            this.cnc.Wait(duration);
        }
    }
}
