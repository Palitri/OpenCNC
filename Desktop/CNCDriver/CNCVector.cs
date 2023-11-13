using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.CNCDriver
{
    public class CNCVector
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public CNCVector()
        {
            this.X = 0.0f;
            this.Y = 0.0f;
            this.Z = 0.0f;
        }

        public CNCVector(float value)
        {
            this.X = value;
            this.Y = value;
            this.Z = value;
        }

        public CNCVector(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

    }
}
