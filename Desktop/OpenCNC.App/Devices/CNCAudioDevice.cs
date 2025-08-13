using Palitri.AudioSynthesis;
using Palitri.OpenCNC.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.OpenCNC.App.Devices
{
    public class CNCAudioDevice : IAudioDevice
    {
        private ICNC cnc;
        private int[] channels;

        public CNCAudioDevice(ICNC cnc, int[] channels)
        {
            this.cnc = cnc;
            this.channels = channels;
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
            this.cnc.Tone(this.channels, frequency, duration);
        }

        public void Silence(float duration)
        {
            this.cnc.Wait(duration);
        }
    }
}
