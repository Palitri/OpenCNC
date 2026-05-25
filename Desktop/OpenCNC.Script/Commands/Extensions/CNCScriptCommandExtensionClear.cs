using Palitri.OpenCNC.Driver;
using Palitri.OpenCNC.Script;
using Palitri.OpenCNC.Script.Utils;

namespace OpenCNC.Script.Commands.Extensions
{
    public class CNCScriptCommandExtensionClear : CNCScriptCommandBase
    {
        private ICNCScriptExtensionsHandler extensions;

        public CNCScriptCommandExtensionClear(ICNCScriptExtensionsHandler extensions)
        {
            this.Name = "Clear";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>();

            this.extensions = extensions;
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            this.extensions.Clear();
        }
    }
}
