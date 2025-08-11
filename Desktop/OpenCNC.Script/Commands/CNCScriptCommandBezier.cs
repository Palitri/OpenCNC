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
    public class CNCScriptCommandBezier : ICNCScriptCommand
    {
        public string Name { get; private set; }
        public List<string> Parameters { get; private set; }
        public bool InfiniteParameters { get; private set; }
        public int Dimensions { get; private set; }

        public CNCScriptCommandBezier(int dimensions)
        {
            this.Name = "Bezier";
            this.InfiniteParameters = true;
            this.Parameters = CNCScriptCommandBezier.CreateParametersList(dimensions);
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

            CNCVector[] vectors = new CNCVector[(parameters.Length - 1) / this.Dimensions];
            int paramIndex = 1;
            for (int i = 0; i < vectors.Length; i++)
            {
                CNCVector vector = new CNCVector(this.Dimensions);
                for (int d = 0; d < this.Dimensions; d++)
                {
                    if (!ScriptUtils.TryParse<float>(parameters[paramIndex], out float dimensionValue, out string message))
                        return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);

                    vector.values[d] = dimensionValue;
                    paramIndex++;
                }

                vectors[i] = vector;
            }

            if (cnc != null)
                cnc.Bezier(vectors);

            return result;
        }
        static private List<string> CreateParametersList(int dimensions)
        {
            List<string> result = new List<string>();
            for (int d = 0; d < dimensions; d++)
                result.Add(DimensionUtils.GetName(d));

            return result;
        }
    }
}
