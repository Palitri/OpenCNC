using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioSynthesis
{
    public class AudioSynthesizer
    {
        private static readonly Dictionary<Tone, int> toneKeys = new Dictionary<Tone, int>()
        {
            { Tone.Ab  , 0 },
            { Tone.A   , 1 },
            { Tone.As  , 2 },
            { Tone.Bb  , 2 },
            { Tone.B   , 3 },
            { Tone.C   , 4 },
            { Tone.Cs  , 5 },
            { Tone.Db  , 5 },
            { Tone.D   , 6 },
            { Tone.Ds  , 7 },
            { Tone.Eb  , 7 },
            { Tone.E   , 8 },
            { Tone.F   , 9 },
            { Tone.Fs  , 10 },
            { Tone.Gb  , 10 },
            { Tone.G   , 11 },
            { Tone.Gs  , 12 },
        };

        private IAudioDevice audioDevice;

        public float A4Frequency { get; set; }

        public float Tempo { get; set; }

        public AudioSynthesizer(IAudioDevice audioDevice)
        {
            this.A4Frequency = 440.0f;
            this.Tempo = 1.0f;

            this.audioDevice = audioDevice;
        }

        public static int GetToneKey(Tone tone, int octave)
        {
            return (octave - 1) * 12 + toneKeys[tone];
        }

        public static float GetToneFrequency(Tone tone, int octave, float a4Frequency = 440.0f)
        {
            int toneKey = GetToneKey(tone, octave);

            return (float)Math.Pow(2.0, ((double)toneKey - 49.0) / 12.0) * a4Frequency;
        }

        public float GetToneFrequency(Tone tone, int octave)
        {
            return AudioSynthesizer.GetToneFrequency(tone, octave, this.A4Frequency);
        }

        public void Pause(float duration)
        {
            this.audioDevice.Silence(duration);
        }

        public void Play(Tone tone, int octave, float duration)
        {
            float frequency = this.GetToneFrequency(tone, octave);

            this.audioDevice.Synthesize(frequency, duration);
        }

        public void Play(INote note)
        {
            this.audioDevice.SynthesizeBegin();

            note.Play(this);

            this.audioDevice.SynthesizeEnd();
        }

        public void Play(IEnumerable<INote> notes)
        {
            this.audioDevice.SynthesizeBegin();

            foreach (INote note in notes)
                note.Play(this);

            this.audioDevice.SynthesizeEnd();
        }
    }
}
