using Palitri.OpenCNC.Script.Utils;
using Palitri.OpenCNC.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandSetPowerMode : ICNCScriptCommand
    {
        public string Name { get; private set; }
        public List<string> Parameters { get; private set; }
        public bool InfiniteParameters { get; private set; }

        public CNCScriptCommandSetPowerMode()
        {
            this.Name = "SetPowerMode";
            this.Parameters = new List<string>() { "PowerOn" };
            this.InfiniteParameters = false;
        }
        
        public CNCScriptCommandResult Execute(ICNC cnc, string inputCommand)
        {
            string[] parameters = ScriptUtils.SplitParams(inputCommand);

            if (parameters.Length == 0)
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error);

            if (!parameters[0].Equals(this.Name, StringComparison.OrdinalIgnoreCase))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error);

            CNCScriptCommandResult result = ScriptUtils.GetResultByParameterCount(parameters.Length - 1, this.Parameters.Count(), this.InfiniteParameters);
            if (result.ResultType == CNCScriptCommandResultType.Error)
                return result;

            bool powerOn;
            string message;
            if (!ScriptUtils.TryParse(parameters[1], out powerOn, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);

            if (cnc != null)
                cnc.SetMotorsSleepMode(!powerOn);

            return result;
        }
    }
}
