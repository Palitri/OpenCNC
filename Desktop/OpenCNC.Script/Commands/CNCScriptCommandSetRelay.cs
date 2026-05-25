using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandSetRelay : CNCScriptCommandBase
    {
        public CNCScriptCommandSetRelay()
        {
            this.Name = "SetRelay";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>()
            {
                new CNCScriptCommandParameter("RelayIndex", typeof(int)),
                new CNCScriptCommandParameter("Enabled", typeof(bool)),
            };
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            cnc.SetRelay((int)values["RelayIndex"], (bool)values["Enabled"]);
        }
    }
}
