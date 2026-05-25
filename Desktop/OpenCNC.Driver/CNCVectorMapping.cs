using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.OpenCNC.Driver
{
    public class CNCVectorMapping
    {
        public static int NonIndexed = -1;

        public int[] indices;

        public CNCVectorMapping(int[] indices)
        {
            if (indices == null)
            {
                this.indices = Array.Empty<int>();
                return;
            }

            this.indices = new int[indices.Length];
            Array.Copy(indices, this.indices, indices.Length);
        }
    }
}
