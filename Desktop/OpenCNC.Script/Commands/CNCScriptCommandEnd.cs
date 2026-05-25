using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandEnd : CNCScriptCommandBase
    {
        public CNCScriptCommandEnd()
        {
            this.Name = "End";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>();
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            cnc.End();
        }
    }
}
