using Palitri.Graphics;

namespace Palitri.SVG.Elements.PathElements
{
    public class SVGPathRenderingParameters
    {
        internal bool hasOrigin = false;
        internal Vector2 origin = new Vector2(0.0f, 0.0f);
        internal Vector2 pos = new Vector2(0.0f, 0.0f);
        internal Vector2 lastControlPointVector = new Vector2();
    }
}
