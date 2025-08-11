using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.AudioSynthesis
{
    public interface INote
    {
        void Play(AudioSynthesizer synth);
    }
}
