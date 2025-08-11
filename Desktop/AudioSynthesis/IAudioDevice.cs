using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.AudioSynthesis
{
    public interface IAudioDevice
    {
        void SynthesizeBegin();
        void SynthesizeEnd();

        void Synthesize(float frequency, float duration);
        void Silence(float duration);
    }
}
