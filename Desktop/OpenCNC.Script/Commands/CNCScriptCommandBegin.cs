using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandBegin : CNCScriptCommandBase
    {
        public CNCScriptCommandBegin()
        {
            this.Name = "Begin";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>();
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            cnc.Begin();
        }
    }
}
