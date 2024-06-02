using Palitri.Graphics;
using static Palitri.SVG.Elements.SVGPath;

namespace SVG.Elements.PathElements
{
    /// <summary>
    /// End of path
    /// </summary>
    public class PathZ : IPathElement
    {
        public void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
        {
            g.Polyline(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.origin) });

            renderingParams.pos.x = renderingParams.origin.x;
            renderingParams.pos.y = renderingParams.origin.y;

            renderingParams.hasOrigin = false;
        }
    }
}
