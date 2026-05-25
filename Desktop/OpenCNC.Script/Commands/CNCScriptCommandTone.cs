using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandTone : CNCScriptCommandBase
    {
        public CNCScriptCommandTone()
        {
            this.Name = "Tone";
            this.InfiniteParameters = false;
            this.Params = new List<CNCScriptCommandParameter>()
            {
                new CNCScriptCommandParameter("Axis", typeof(int)),
                new CNCScriptCommandParameter("Frequency", typeof(float)),
                new CNCScriptCommandParameter("Duration", typeof(float)),
            };
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            cnc.Tone([(int)values["Axis"]], (float)values["Frequency"], (float)values["Duration"]);
        }
    }
}
