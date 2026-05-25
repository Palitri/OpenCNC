using Palitri.OpenCNC.Script.Utils;
using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandArc : CNCScriptCommandBase
    {
        public int Dimensions { get; private set; }

        public CNCScriptCommandArc(int dimensions)
        {
            this.Name = "Arc";
            this.InfiniteParameters = false;
            this.Dimensions = dimensions;
            this.Params = CNCScriptCommandArc.CreateParametersList(dimensions);
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            CNCVector semiMajorAxis = ScriptUtils.VectorFromValues(0, this.Dimensions, values);
            CNCVector semiMinorAxis = ScriptUtils.VectorFromValues(this.Dimensions, this.Dimensions, values);
            float startAngle = (float)Math.PI * 2.0f * (float)values["TurnsStartAngle"];
            float endAngle = (float)Math.PI * 2.0f * (float)values["TurnsEndAngle"];

            cnc.Arc(semiMajorAxis, semiMinorAxis, startAngle, endAngle);
        }

        static private List<CNCScriptCommandParameter> CreateParametersList(int dimensions)
        {
            List<CNCScriptCommandParameter> result = new List<CNCScriptCommandParameter>();
            for (int d = 0; d < dimensions; d++)
                result.Add(new CNCScriptCommandParameter("X" + DimensionUtils.GetName(d).ToLower(), typeof(float)));
            for (int d = 0; d < dimensions; d++)
                result.Add(new CNCScriptCommandParameter("Y" + DimensionUtils.GetName(d).ToLower(), typeof(float)));
            result.Add(new CNCScriptCommandParameter("TurnsStartAngle", typeof(float)));
            result.Add(new CNCScriptCommandParameter("TurnsEndAngle", typeof(float)));

            return result;
        }
    }
}
