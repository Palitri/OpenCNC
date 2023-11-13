using Palitri.CNCDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.CNCScript.Commands
{
    public class CNCScriptCommandBezier : ICNCScriptCommand
    {
        public string Name { get; private set; }
        public List<string> Parameters { get; private set; }
        public bool InfiniteParameters { get; private set; }

        public CNCScriptCommandBezier()
        {
            this.Name = "Bezier";
            this.Parameters = new List<string>() { "X", "Y", "Z" };
            this.InfiniteParameters = true;
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

            CNCVector[] vectors = new CNCVector[(parameters.Length - 1) / 3];
            int paramIndex = 1;
            for (int i = 0; i < vectors.Length; i++)
            {
                float x, y, z;
                string message;
                if (!CNCScriptUtils.TryParse<float>(parameters[paramIndex], out x, out message))
                    return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
                paramIndex++;
                if (!CNCScriptUtils.TryParse<float>(parameters[paramIndex], out y, out message))
                    return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
                paramIndex++;
                if (!CNCScriptUtils.TryParse<float>(parameters[paramIndex], out z, out message))
                    return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
                paramIndex++;

                vectors[i] = new CNCVector(x, y, z);
            }

            if (cnc != null)
                cnc.Bezier(vectors);

            return result;
        }
    }
}
