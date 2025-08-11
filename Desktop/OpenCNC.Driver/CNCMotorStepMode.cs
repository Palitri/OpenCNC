using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.OpenCNC.Driver
{
    public enum CNCMotorStepMode
    {
        Full = 1,
        Half = 2,
        Quarter = 4,
        Eighth = 8,
        Sixteenth = 16
    };
}
