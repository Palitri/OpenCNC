using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandMapSpacialAxes : CNCScriptCommandBase
    {
        public CNCScriptCommandMapSpacialAxes()
        {
            this.Name = "MapSpacialAxes";
            this.InfiniteParameters = true;
            this.Params = new List<CNCScriptCommandParameter>()
            {
                new CNCScriptCommandParameter("AxisIndex", typeof(int)),
            };
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            cnc.SetAxesChannels(values.Select(v => (byte)v.Value).ToArray());
        }
    }
}
