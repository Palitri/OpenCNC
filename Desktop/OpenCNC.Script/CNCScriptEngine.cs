using OpenCNC.Script;
using OpenCNC.Script.Commands.Extensions;
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

        public CNCScriptEngine(int dimensions, ICNCScriptExtensionsHandler extensionsHandler)
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
                new CNCScriptCommandMapAxesDevices(),
                new CNCScriptCommandMapSpacialAxes(),
                new CNCScriptCommandSetAxesEnable(),
                new CNCScriptCommandSetAxesStepMode(),
                new CNCScriptCommandSetAxesSleepMode(),
                new CNCScriptCommandSetToolPower(),
                new CNCScriptCommandSetToolPowerMode(),
                new CNCScriptCommandSetSpeed(),
                new CNCScriptCommandSetDriveVector(),
                new CNCScriptCommandSetRelay(),
                new CNCScriptCommandDrive(),
                new CNCScriptCommandDriveReset(),
                new CNCScriptCommandTone(),
                new CNCScriptCommandWait(),

                new CNCScriptCommandExtensionListCommands(extensionsHandler),
                new CNCScriptCommandExtensionListAxisGroups(extensionsHandler),
                new CNCScriptCommandExtensionClear(extensionsHandler),
                new CNCScriptCommandExtensionExit(extensionsHandler),
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
