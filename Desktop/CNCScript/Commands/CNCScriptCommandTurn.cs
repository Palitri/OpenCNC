using Palitri.CNCDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.CNCScript.Commands
{
    public class CNCScriptCommandTurn : ICNCScriptCommand
    {
        public string Name { get; private set; }
        public List<string> Parameters { get; private set; }
        public bool InfiniteParameters { get; private set; }

        public CNCScriptCommandTurn()
        {
            this.Name = "Turn";
            this.Parameters = new List<string>() { "Motors", "Direction", "Interval", "Steps" };
            this.InfiniteParameters = false;
        }
        
        public CNCScriptCommandResult Execute(ICNC cnc, string inputCommand)
        {
            string[] parameters = CNCScriptUtils.SplitParams(inputCommand);

            if (parameters.Length == 0)
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error);

            if (!parameters[0].Equals(this.Name, StringComparison.OrdinalIgnoreCase))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error);

            CNCScriptCommandResult result = CNCScriptUtils.GetResultByParameterCount(parameters.Length - 1, this.Parameters.Count(), this.InfiniteParameters);
            if (result.ResultType == CNCScriptCommandResultType.Error)
                return result;

            int motors, interval, steps;
            string message;
            CNCRotationDirection direction;
            if (!CNCScriptUtils.TryParse<int>(parameters[1], out motors, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            if (!CNCScriptUtils.TryParse<CNCRotationDirection>(parameters[2], out direction, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            if (!CNCScriptUtils.TryParse<int>(parameters[3], out interval, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            if (!CNCScriptUtils.TryParse<int>(parameters[4], out steps, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);

            if (cnc != null)
                cnc.Turn(motors, direction, interval, steps);

            return result;
        }
    }
}
