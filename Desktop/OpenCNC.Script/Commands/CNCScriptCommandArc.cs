using Palitri.OpenCNC.Script.Utils;
using Palitri.OpenCNC.Driver;
using Palitri.OpenCNC.Script.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandArc : ICNCScriptCommand
    {
        public string Name { get; private set; }
        public List<string> Parameters { get; private set; }
        public bool InfiniteParameters { get; }
        public int Dimensions { get; private set; }

        public CNCScriptCommandArc(int dimensions)
        {
            this.Name = "Arc";
            this.InfiniteParameters = false;
            this.Parameters = CNCScriptCommandArc.CreateParametersList(dimensions);
            this.Dimensions = dimensions;
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

            int paramIndex = 1;
            string message;
            CNCVector semiMajor = new CNCVector(this.Dimensions);
            for (int d = 0; d < this.Dimensions; d++)
            {
                if (!ScriptUtils.TryParse<float>(parameters[paramIndex], out float coordValue, out message))
                    return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);

                paramIndex++;
                semiMajor.values[d] = coordValue;
            }
            CNCVector semiMinor = new CNCVector(this.Dimensions);
            for (int d = 0; d < this.Dimensions; d++)
            {
                if (!ScriptUtils.TryParse<float>(parameters[paramIndex], out float coordValue, out message))
                    return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);

                paramIndex++;
                semiMinor.values[d] = coordValue;
            }

            if (!ScriptUtils.TryParse<float>(parameters[paramIndex], out float startAngle, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            paramIndex++;
            if (!ScriptUtils.TryParse<float>(parameters[paramIndex], out float endAngle, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            paramIndex++;

            if (cnc != null)
                cnc.Arc(semiMajor, semiMinor, (float)Math.PI * startAngle / 180.0f, (float)Math.PI * endAngle / 180.0f);

            return result;
        }

        static private List<string> CreateParametersList(int dimensions)
        {
            List<string> result = new List<string>();
            for (int d = 0; d < dimensions; d++)
                result.Add("X" + DimensionUtils.GetName(d).ToLower());
            for (int d = 0; d < dimensions; d++)
                result.Add("Y" + DimensionUtils.GetName(d).ToLower());
            result.Add("StartAngle");
            result.Add("EndAngle");

            return result;
        }
    }
}
