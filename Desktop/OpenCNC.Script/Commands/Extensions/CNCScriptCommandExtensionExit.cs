using Palitri.OpenCNC.Driver;
using Palitri.OpenCNC.Script;
using Palitri.OpenCNC.Script.Utils;

namespace OpenCNC.Script.Commands.Extensions
{
    public class CNCScriptCommandExtensionExit : CNCScriptCommandBase
    {
        private ICNCScriptExtensionsHandler extensions;

        public CNCScriptCommandExtensionExit(ICNCScriptExtensionsHandler extensions)
        {
            this.Name = "Exit";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>();

            this.extensions = extensions;
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            this.extensions.Exit();
        }
    }
}
