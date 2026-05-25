using Palitri.OpenCNC.Driver;
using Palitri.OpenCNC.Script.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.OpenCNC.Script
{
    public abstract class CNCScriptCommandBase : ICNCScriptCommand
    {
        public string Name { get; protected set; }
        public List<CNCScriptCommandParameter> Params { get; protected set; }
        public bool InfiniteParameters { get; protected set; }

        public abstract void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values);

        public CNCScriptCommandResult Execute(ICNC cnc, string inputCommand)
        {
            CNCScriptCommandResult result = this.TryParseInputCommand(inputCommand, out Dictionary<string, object> values);

            if (result.ResultType != CNCScriptCommandResultType.Success)
                return result;

            if (cnc != null)
                this.ExecuteCNCCommand(cnc, values);

            return result;
        }

        public CNCScriptCommandResult TryParseInputCommand(string inputCommand, out Dictionary<string, object> values)
        {
            values = new Dictionary<string, object>();

            string[] parameters = ScriptUtils.SplitParams(inputCommand);

            if (parameters.Length == 0)
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error);

            if (!parameters[0].Equals(this.Name, StringComparison.OrdinalIgnoreCase))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error);

            CNCScriptCommandResult result = ScriptUtils.GetResultByParameterCount(parameters.Length - 1, this.Params.Count(), this.InfiniteParameters);
            if (result.ResultType == CNCScriptCommandResultType.Error)
                return result;

            string message;
            int paramIndex = 1;
            foreach (CNCScriptCommandParameter param in this.Params)
            {
                if (!ScriptUtils.TryParse(parameters[paramIndex++], param.ValueType, out object? value, out message))
                    return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);

                values.Add(param.Name, value);
            }

            return result;
        }
    }
}
