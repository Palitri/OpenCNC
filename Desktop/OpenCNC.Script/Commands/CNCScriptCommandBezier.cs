using Palitri.OpenCNC.Script.Utils;
using Palitri.OpenCNC.Driver;

namespace Palitri.OpenCNC.Script.Commands
{
    public class CNCScriptCommandBezier : CNCScriptCommandBase
    {
        public int Dimensions { get; private set; }

        public CNCScriptCommandBezier(int dimensions)
        {
            this.Name = "Bezier";
            this.InfiniteParameters = true;
            this.Dimensions = dimensions;
            this.Params = CNCScriptCommandBezier.CreateParametersList(dimensions);
        }

        public override void ExecuteCNCCommand(ICNC cnc, Dictionary<string, object> values)
        {
            int numPoints = values.Count / this.Dimensions;
            CNCVector[] points = new CNCVector[numPoints];
            for (int i = 0; i < numPoints; i++)
                points[i] = ScriptUtils.VectorFromValues(i * this.Dimensions, this.Dimensions, values);

            cnc.Bezier(points);
        }

        static private List<CNCScriptCommandParameter> CreateParametersList(int dimensions)
        {
            List<CNCScriptCommandParameter> result = new List<CNCScriptCommandParameter>();
            for (int d = 0; d < dimensions; d++)
                result.Add(new CNCScriptCommandParameter(DimensionUtils.GetName(d), typeof(float)));

            return result;
        }
    }
}
