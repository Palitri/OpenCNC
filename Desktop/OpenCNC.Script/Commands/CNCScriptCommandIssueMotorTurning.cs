using Palitri.OpenCNC.Script.Utils;
using Palitri.OpenCNC.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandIssueMotorTurning : ICNCScriptCommand
    {
        public string Name { get; private set; }
        public List<string> Parameters { get; private set; }
        public bool InfiniteParameters { get; private set; }

        public CNCScriptCommandIssueMotorTurning()
        {
            this.Name = "IssueMotorTurning";
            this.Parameters = new List<string>() { "Motor", "Speed" };
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

            int motor;
            float speed;
            string message;
            if (!ScriptUtils.TryParse<int>(parameters[1], out motor, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            if (!ScriptUtils.TryParse<float>(parameters[2], out speed, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);

            if (cnc != null)
                cnc.IssueMotorTurning(motor, speed);

            return result;
        }
    }
}
