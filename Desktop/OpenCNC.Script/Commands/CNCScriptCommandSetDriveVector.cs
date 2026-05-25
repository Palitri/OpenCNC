using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandSetDriveVector : CNCScriptCommandBase
    {
        public CNCScriptCommandSetDriveVector()
        {
            this.Name = "SetDriveVector";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>()
            {
                new CNCScriptCommandParameter("Channel", typeof(int)),
                new CNCScriptCommandParameter("Origin", typeof(float)),
                new CNCScriptCommandParameter("Vector", typeof(float)),
            };
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            cnc.SetDriveVector((int)values["Channel"], (float)values["Origin"], (float)values["Vector"]);
        }
    }
}
