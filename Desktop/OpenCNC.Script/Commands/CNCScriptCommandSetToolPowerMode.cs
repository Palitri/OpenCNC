using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandSetToolPowerMode : CNCScriptCommandBase
    {
        public CNCScriptCommandSetToolPowerMode()
        {
            this.Name = "SetToolPowerMode";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>()
            {
                new CNCScriptCommandParameter("PowerOn", typeof(bool)),
            };
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            cnc.SetToolPowerMode((bool)values["PowerOn"]);
        }
    }
}
