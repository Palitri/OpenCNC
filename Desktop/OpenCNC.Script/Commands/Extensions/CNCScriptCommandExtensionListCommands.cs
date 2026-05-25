using Palitri.OpenCNC.Driver;
using Palitri.OpenCNC.Script;

namespace OpenCNC.Script.Commands.Extensions
{
    public class CNCScriptCommandExtensionListCommands : CNCScriptCommandBase
    {
        private ICNCScriptExtensionsHandler extensions;

        public CNCScriptCommandExtensionListCommands(ICNCScriptExtensionsHandler extensions)
        {
            this.Name = "?";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>();

            this.extensions = extensions;
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            foreach (ICNCScriptCommand command in this.extensions.ScriptEngine.commands)
                this.extensions.Write(string.Format("@{0} {1}", command.Name, string.Join(", ", command.Params.Select(p => p.Name).ToArray())));
        }
    }
}
