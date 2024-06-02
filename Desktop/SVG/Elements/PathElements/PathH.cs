using Palitri.Graphics;
using static Palitri.SVG.Elements.SVGPath;

namespace SVG.Elements.PathElements
{
    /// <summary>
    /// Horizontal line
    /// </summary>
    public class PathH : IPathElement
    {
        public bool relative;
        public float x;

        public void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
        {
            if (this.relative)
            {
                g.Polyline(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x + this.x, renderingParams.pos.y) });

                renderingParams.pos.x += this.x;
            }
            else
            {
                g.Polyline(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(this.x, renderingParams.pos.y) });

                renderingParams.pos.x = this.x;
            }
        }
    }
}
