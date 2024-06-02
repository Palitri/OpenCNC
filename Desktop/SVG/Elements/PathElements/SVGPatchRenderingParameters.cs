using Palitri.Graphics;

namespace SVG.Elements.PathElements
{
    public class SVGPathRenderingParameters
    {
        internal bool hasOrigin = false;
        internal Vector origin = new Vector(0.0f, 0.0f);
        internal Vector pos = new Vector(0.0f, 0.0f);
        internal Vector lastControlPointVector = new Vector();
    }
}
