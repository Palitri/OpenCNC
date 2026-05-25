using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandDriveReset : CNCScriptCommandBase
    {
        public CNCScriptCommandDriveReset()
        {
            this.Name = "DriveReset";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>()
            {
            };
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            cnc.ResetDriveVectors();
        }
    }
}
