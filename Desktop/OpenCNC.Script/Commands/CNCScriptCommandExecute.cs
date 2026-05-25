using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandExecute : CNCScriptCommandBase
    {
        public CNCScriptCommandExecute()
        {
            this.Name = "Execute";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>();
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            new Thread(() =>
                {
                    cnc.Execute();
                }
            ).Start();
        }
    }
}
