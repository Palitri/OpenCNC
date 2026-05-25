using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandSetSpeed : CNCScriptCommandBase
    {
        public CNCScriptCommandSetSpeed()
        {
            this.Name = "SetSpeed";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>()
            {
                new CNCScriptCommandParameter("Speed", typeof(float)),
            };
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            cnc.SetSpeed((float)values["Speed"]);
        }
    }
}
