using Palitri.Graphics;
using static Palitri.SVG.Elements.SVGPath;

namespace SVG.Elements.PathElements
{
    /// <summary>
    /// Line
    /// </summary>
    public class PathL : IPathElement
    {
        public bool relative;
        public float x, y;

        public void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
        {
            if (this.relative)
            {
                g.Polyline(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x + this.x, renderingParams.pos.y + this.y) });

                renderingParams.pos.x += this.x;
                renderingParams.pos.y += this.y;
            }
            else
            {
                g.Polyline(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(this.x, this.y) });

                renderingParams.pos.x = this.x;
                renderingParams.pos.y = this.y;
            }
        }
    }
}
