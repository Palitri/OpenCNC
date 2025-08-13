using CNCScript.Commands;
using Palitri.OpenCNC.Driver;
using Palitri.OpenCNC.Script.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.OpenCNC.Script
{
    public class CNCScriptEngine
    {
        public List<ICNCScriptCommand> commands;

        public CNCScriptEngine(int dimensions)
        {
            this.commands = new List<ICNCScriptCommand>()
            {
                new CNCScriptCommandBegin(),
                new CNCScriptCommandEnd(),
                new CNCScriptCommandExecute(),
                new CNCScriptCommandGo(),
                new CNCScriptCommandPolyline(dimensions),
                new CNCScriptCommandArc(dimensions),
                new CNCScriptCommandBezier(dimensions),
                new CNCScriptCommandSetMotorsPowerMode(),
                new CNCScriptCommandSetMotorsStepMode(),
                new CNCScriptCommandSetPowerMode(),
                new CNCScriptCommandSetToolPowerMode(),
                new CNCScriptCommandMapDevice(),
                new CNCScriptCommandSetPower(),
                new CNCScriptCommandSetSpeed(),
                new CNCScriptCommandSetRelay(),
                new CNCScriptCommandSetDriveVector(),
                new CNCScriptCommandTone(),
                new CNCScriptCommandWait(),
                new CNCScriptCommandDrive(),
                new CNCScriptCommandDriveLinear(),
                new CNCScriptCommandDriveSine()
            };
        }

        public CNCScriptCommandResult Execute(ICNC cnc, string inputCommand)
        {
            foreach (ICNCScriptCommand command in this.commands)
            {
                CNCScriptCommandResult result = command.Execute(cnc, inputCommand);
                if (result.ResultType != CNCScriptCommandResultType.Error)
                    return result;
            }

            return new CNCScriptCommandResult(CNCScriptCommandResultType.Error);
        }
    }
}
