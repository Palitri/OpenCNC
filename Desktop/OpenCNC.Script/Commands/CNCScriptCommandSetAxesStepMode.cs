using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandSetAxesStepMode : CNCScriptCommandBase
    {
        public CNCScriptCommandSetAxesStepMode()
        {
            this.Name = "SetAxesStepMode";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>()
            {
                new CNCScriptCommandParameter("GroupName", typeof(string)),
                new CNCScriptCommandParameter("StepMode", typeof(CNCMotorStepMode)),
            };
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            cnc.SetMotorsStepMode((string)values["GroupName"], (CNCMotorStepMode)values["StepMode"]);
        }
    }
}
