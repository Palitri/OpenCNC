using Palitri.OpenCNC.Driver;
using Palitri.OpenCNC.Script;
using Palitri.OpenCNC.Script.Utils;

namespace OpenCNC.Script.Commands.Extensions
{
    public class CNCScriptCommandExtensionListAxisGroups : CNCScriptCommandBase
    {
        private ICNCScriptExtensionsHandler extensions;

        public CNCScriptCommandExtensionListAxisGroups(ICNCScriptExtensionsHandler extensions)
        {
            this.Name = "ListAxisGroups";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>();

            this.extensions = extensions;
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            foreach (string groupName in this.extensions.BoardConfig.Axes.Select(a => a.Group).Distinct())
            {
                int[] groupIndices = this.extensions.BoardConfig.Axes.Where(a => ScriptUtils.AxisGroupsEqual(a.Group, groupName)).Select(a => this.extensions.BoardConfig.Axes.IndexOf(a)).ToArray();
                this.extensions.Write(string.Format("@{0} [{1}]", groupName, string.Join(", ", groupIndices)));
            }
        }
    }
}
