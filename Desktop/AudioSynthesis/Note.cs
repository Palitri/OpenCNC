using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.AudioSynthesis
{
    public class Note : INote
    {
        public Tone Tone { get; set; }
        public int Octave { get; set; }
        public float Duration { get; set; }

        public Note(Tone tone, int octave, float duration)
        {
            this.Tone = tone;
            this.Octave = octave;
            this.Duration = duration;
        }

        public void Play(AudioSynthesizer synth)
        {
            synth.Play(this.Tone, this.Octave, this.Duration);
        }
    }
}
