using Palitri.Graphics;
using static Palitri.SVG.Elements.SVGPath;

namespace Palitri.SVG.Elements.PathElements
{
    /// <summary>
    /// Cubic Bezier curve
    /// </summary>
    public class PathC : IPathElement
    {
        public bool relative;
        public float x1, y1, x2, y2, x, y;

        public void Render(Matrix3 transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
        {
            if (this.relative)
            {
                g.Bezier(new Vector2[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x + x1, renderingParams.pos.y + y1), transform.Transform(renderingParams.pos.x + x2, renderingParams.pos.y + y2), transform.Transform(renderingParams.pos.x + x, renderingParams.pos.y + y) });

                renderingParams.pos.x += x;
                renderingParams.pos.y += y;
            }
            else
            {
                g.Bezier(new Vector2[] { transform.Transform(renderingParams.pos), transform.Transform(x1, y1), transform.Transform(x2, y2), transform.Transform(x, y) });

                renderingParams.pos.x = x;
                renderingParams.pos.y = y;
            }

            renderingParams.lastControlPointVector.x = x2 - x;
            renderingParams.lastControlPointVector.y = y2 - y;
        }
    }
}
