using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandMapAxesDevices : CNCScriptCommandBase
    {
        public CNCScriptCommandMapAxesDevices()
        {
            this.Name = "MapAxesDevices";
            this.InfiniteParameters = true;
            this.Params = new List<CNCScriptCommandParameter>()
            {
                new CNCScriptCommandParameter("PeripheralId", typeof(int)),
            };
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            cnc.SetChannelsDevices(values.Select(v => (byte)v.Value).ToArray());
        }
    }
}
