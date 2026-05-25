using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandDrive : CNCScriptCommandBase
    {
        public CNCScriptCommandDrive()
        {
            this.Name = "Drive";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>()
            {
                new CNCScriptCommandParameter("Time", typeof(float))
            };
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            cnc.Drive((float)values["Time"]);
        }
    }
}
