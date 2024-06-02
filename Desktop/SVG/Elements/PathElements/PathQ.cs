using Palitri.Graphics;
using static Palitri.SVG.Elements.SVGPath;

namespace SVG.Elements.PathElements
{
    /// <summary>
    /// Quadratic Bezier curve
    /// </summary>
    public class PathQ : IPathElement
    {
        public bool relative;
        public float x1, y1, x, y;

        public void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
        {
            if (this.relative)
            {
                g.Bezier(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x + x1, renderingParams.pos.y + y1), transform.Transform(renderingParams.pos.x + x, renderingParams.pos.y + y) });

                renderingParams.pos.x += x;
                renderingParams.pos.y += y;
            }
            else
            {
                g.Bezier(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(x1, y1), transform.Transform(x, y) });

                renderingParams.pos.x = x;
                renderingParams.pos.y = y;
            }

            renderingParams.lastControlPointVector.x = x1 - x;
            renderingParams.lastControlPointVector.y = y1 - y;
        }
    }
}
