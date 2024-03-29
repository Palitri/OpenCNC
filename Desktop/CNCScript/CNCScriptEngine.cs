﻿using CNCScript.Commands;
using Palitri.CNCDriver;
using Palitri.CNCScript.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.CNCScript
{
    public class CNCScriptEngine
    {
        public List<ICNCScriptCommand> commands;

        public CNCScriptEngine()
        {
            this.commands = new List<ICNCScriptCommand>()
            {
                new CNCScriptCommandBegin(),
                new CNCScriptCommandEnd(),
                new CNCScriptCommandExecute(),
                new CNCScriptCommandGo(),
                new CNCScriptCommandPolyline(),
                new CNCScriptCommandArc(),
                new CNCScriptCommandBezier(),
                new CNCScriptCommandSetMotorsPowerMode(),
                new CNCScriptCommandSetMotorsStepMode(),
                new CNCScriptCommandSetPowerMode(),
                new CNCScriptCommandSetToolPowerMode(),
                new CNCScriptCommandMapDevice(),
                new CNCScriptCommandSetPower(),
                new CNCScriptCommandSetSpeed(),
                new CNCScriptCommandTone(),
                new CNCScriptCommandTurn(),
                new CNCScriptCommandWait(),
                new CNCScriptCommandIssueMotorTurning(),
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
