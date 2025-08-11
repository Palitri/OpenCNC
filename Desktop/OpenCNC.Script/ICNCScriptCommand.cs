using Palitri.OpenCNC.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.OpenCNC.Script
{
    public interface ICNCScriptCommand
    {
        string Name { get; }
        List<string> Parameters { get; }
        bool InfiniteParameters { get; }
        CNCScriptCommandResult Execute(ICNC cnc, string inputCommand);
    }
}
