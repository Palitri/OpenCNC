using Palitri.OpenCNC.Script.Utils;
using Palitri.OpenCNC.Driver;
using Palitri.OpenCNC.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNCScript.Commands
{
    class CNCScriptCommandDriveSine : ICNCScriptCommand
    {
        public string Name { get; private set; }
        public List<string> Parameters { get; private set; }
        public bool InfiniteParameters { get; private set; }

        public CNCScriptCommandDriveSine()
        {
            this.Name = "DriveSine";
            this.Parameters = new List<string>() { "Offset", "Span", "Amplitude", "PhaseStart", "PhaseEnd" };
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

            float offset, span, amplitude, phaseStart, phaseEnd;
            string message;
            if (!ScriptUtils.TryParse<float>(parameters[1], out offset, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            if (!ScriptUtils.TryParse<float>(parameters[2], out span, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            if (!ScriptUtils.TryParse<float>(parameters[3], out amplitude, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            if (!ScriptUtils.TryParse<float>(parameters[4], out phaseStart, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            if (!ScriptUtils.TryParse<float>(parameters[5], out phaseEnd, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);

            if (cnc != null)
                cnc.DriveSine(offset, span, amplitude, phaseStart, phaseEnd);

            return result;
        }
   }
}
