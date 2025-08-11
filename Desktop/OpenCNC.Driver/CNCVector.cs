using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.OpenCNC.Driver
{
    public class CNCVector
    {
        public float[] values;

        public float X { get { return this.values[0]; } set { this.values[0] = value; } }
        public float Y { get { return this.values[1]; } set { this.values[1] = value; } }
        public float Z { get { return this.values[2]; } set { this.values[2] = value; } }
        public float W { get { return this.values[3]; } set { this.values[3] = value; } }
        public float V { get { return this.values[4]; } set { this.values[4] = value; } }
        public float U { get { return this.values[5]; } set { this.values[5] = value; } }

        public CNCVector(int dimensions)
        {
            this.values = new float[dimensions];
            Array.Clear(this.values, 0, dimensions);
        }

        public CNCVector(float x, float y)
        {
            this.values = new float[2];
            this.X = x;
            this.Y = y;
        }
        public CNCVector(float x, float y, float z)
        {
            this.values = new float[3];
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}
