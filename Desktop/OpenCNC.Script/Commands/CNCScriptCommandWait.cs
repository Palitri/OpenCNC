using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandWait : CNCScriptCommandBase
    {
        public CNCScriptCommandWait()
        {
            this.Name = "Wait";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>()
            {
                new CNCScriptCommandParameter("Time", typeof(float)),
            };
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            cnc.Wait((float)values["Time"]);
        }
    }
}
