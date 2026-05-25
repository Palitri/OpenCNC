using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandSetAxesSleepMode : CNCScriptCommandBase
    {
        public CNCScriptCommandSetAxesSleepMode()
        {
            this.Name = "SetAxesSleepMode";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>()
            {
                new CNCScriptCommandParameter("GroupName", typeof(string)),
                new CNCScriptCommandParameter("Sleep", typeof(bool))
            };
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            cnc.SetMotorsSleepMode((string)values["GroupName"], !(bool)values["Sleep"]);
        }
    }
}
