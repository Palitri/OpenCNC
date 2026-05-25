using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandSetToolPower : CNCScriptCommandBase
    {
        public CNCScriptCommandSetToolPower()
        {
            this.Name = "SetToolPower";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>()
            {
                new CNCScriptCommandParameter("Power", typeof(float)),
            };
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            cnc.SetPower((float)values["Power"]);
        }
    }
}
