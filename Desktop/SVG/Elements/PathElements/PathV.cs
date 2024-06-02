using Palitri.Graphics;
using static Palitri.SVG.Elements.SVGPath;

namespace SVG.Elements.PathElements
{
    /// <summary>
    /// Vertical line
    /// </summary>
    public class PathV : IPathElement
    {
        public bool relative;
        public float y;

        public void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
        {
            if (this.relative)
            {
                g.Polyline(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x, renderingParams.pos.y + this.y) });

                renderingParams.pos.y += this.y;
            }
            else
            {
                g.Polyline(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x, this.y) });

                renderingParams.pos.y = this.y;
            }
        }
    }
}
