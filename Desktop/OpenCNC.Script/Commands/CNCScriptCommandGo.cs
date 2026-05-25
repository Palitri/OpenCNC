using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandGo : CNCScriptCommandBase
    {
        public CNCScriptCommandGo()
        {
            this.Name = "Go";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>();
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            new Thread(() =>
            {
                cnc.End();
                cnc.Execute();
                cnc.Begin();
            }).Start();
        }
    }
}
