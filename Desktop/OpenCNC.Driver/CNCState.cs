using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.OpenCNC.Driver
{
    public enum CNCState
    {
        None        = 0,
        Ready       = 1,
        Busy        = 2,
        Unavailable = 3
    }
}
