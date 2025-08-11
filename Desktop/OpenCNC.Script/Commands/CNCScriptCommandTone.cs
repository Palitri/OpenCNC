using Palitri.OpenCNC.Script.Utils;
using Palitri.OpenCNC.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandTone : ICNCScriptCommand
    {
        public string Name { get; private set; }
        public List<string> Parameters { get; private set; }
        public bool InfiniteParameters { get; private set; }

        public CNCScriptCommandTone()
        {
            this.Name = "Tone";
            this.Parameters = new List<string>() { "Motors", "Frequency", "Duration" };
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

            int motors;
            float frequency, duration;
            string message;
            if (!ScriptUtils.TryParse<int>(parameters[1], out motors, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            if (!ScriptUtils.TryParse<float>(parameters[2], out frequency, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            if (!ScriptUtils.TryParse<float>(parameters[3], out duration, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);

            if (cnc != null)
                cnc.Tone(motors, frequency, duration);

            return result;
        }
    }
}
