using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioSynthesis
{
    public class Pause : INote
    {
        public float Duration { get; set; }

        public Pause(float duration)
        {
            this.Duration = duration;
        }

        public void Play(AudioSynthesizer synth)
        {
            synth.Pause(this.Duration);
        }
    }
}
