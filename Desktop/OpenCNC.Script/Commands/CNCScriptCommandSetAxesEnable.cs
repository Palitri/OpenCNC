using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandSetAxesEnable : CNCScriptCommandBase
    {
        public CNCScriptCommandSetAxesEnable()
        {
            this.Name = "SetAxesEnable";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>() 
            { 
                new CNCScriptCommandParameter("GroupName", typeof(string)),
                new CNCScriptCommandParameter("Enabled", typeof(bool))
            };
        }
        
        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            cnc.SetAxesEnabled((string)values["GroupName"], (bool)values["Enabled"]);
        }
    }
}
