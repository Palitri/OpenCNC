using Palitri.CNCDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.CNCScript.Commands
{
    public class CNCScriptCommandArc : ICNCScriptCommand
    {
        public string Name { get; private set; }
        public List<string> Parameters { get; private set; }
        public bool InfiniteParameters { get; private set; }

        public CNCScriptCommandArc()
        {
            this.Name = "Arc";
            this.Parameters = new List<string>() { "Xx", "Xy", "Xz", "Yx", "Yy", "Yz", "StartAngle", "EndAngle" };
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

            int paramIndex = 1;
            float xx, xy, xz, yx, yy, yz, startAngle, endAngle;
            string message;
            if (!CNCScriptUtils.TryParse<float>(parameters[paramIndex], out xx, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            paramIndex++;
            if (!CNCScriptUtils.TryParse<float>(parameters[paramIndex], out xy, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            paramIndex++;
            if (!CNCScriptUtils.TryParse<float>(parameters[paramIndex], out xz, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            paramIndex++;
            if (!CNCScriptUtils.TryParse<float>(parameters[paramIndex], out yx, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            paramIndex++;
            if (!CNCScriptUtils.TryParse<float>(parameters[paramIndex], out yy, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            paramIndex++;
            if (!CNCScriptUtils.TryParse<float>(parameters[paramIndex], out yz, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            paramIndex++;
            if (!CNCScriptUtils.TryParse<float>(parameters[paramIndex], out startAngle, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            paramIndex++;
            if (!CNCScriptUtils.TryParse<float>(parameters[paramIndex], out endAngle, out message))
                return new CNCScriptCommandResult(CNCScriptCommandResultType.Error, message);
            paramIndex++;

            if (cnc != null)
                cnc.Arc(new CNCVector(xx, xy, xz), new CNCVector(yx, yy, yz), (float)Math.PI * startAngle / 180.0f, (float)Math.PI * endAngle / 180.0f);

            return result;
        }
    }
}
