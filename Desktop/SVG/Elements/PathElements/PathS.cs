using Palitri.Graphics;
using static Palitri.SVG.Elements.SVGPath;

namespace SVG.Elements.PathElements
{
    /// <summary>
    /// Series of Cubic Bezier curves
    /// </summary>
    public class PathS : IPathElement
    {
        public bool relative;
        public float x2, y2, x, y;

        public void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
        {
            if (this.relative)
            {
                g.Bezier(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x - renderingParams.lastControlPointVector.x, renderingParams.pos.y - renderingParams.lastControlPointVector.y), transform.Transform(renderingParams.pos.x + x2, renderingParams.pos.y + y2), transform.Transform(renderingParams.pos.x + x, renderingParams.pos.y + y) });

                renderingParams.pos.x += x;
                renderingParams.pos.y += y;
            }
            else
            {
                g.Bezier(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x - renderingParams.lastControlPointVector.x, renderingParams.pos.y - renderingParams.lastControlPointVector.y), transform.Transform(x2, y2), transform.Transform(x, y) });

                renderingParams.pos.x = x;
                renderingParams.pos.y = y;
            }

            renderingParams.lastControlPointVector.x = x2 - x;
            renderingParams.lastControlPointVector.y = y2 - y;
        }
    }
}
