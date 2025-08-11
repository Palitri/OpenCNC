using Palitri.Graphics;
using static Palitri.SVG.Elements.SVGPath;

namespace Palitri.SVG.Elements.PathElements
{
    /// <summary>
    /// Series of Quadratic Bezier curves
    /// </summary>
    public class PathT : IPathElement
    {
        public bool relative;
        public float x, y;

        public void Render(Matrix3 transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
        {
            if (this.relative)
            {
                g.Bezier(new Vector2[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x - renderingParams.lastControlPointVector.x, renderingParams.pos.y - renderingParams.lastControlPointVector.y), transform.Transform(renderingParams.pos.x + x, renderingParams.pos.y + y) });

                renderingParams.lastControlPointVector.x = -renderingParams.lastControlPointVector.x - x;
                renderingParams.lastControlPointVector.y = -renderingParams.lastControlPointVector.y - y;

                renderingParams.pos.x += x;
                renderingParams.pos.y += y;
            }
            else
            {
                g.Bezier(new Vector2[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x - renderingParams.lastControlPointVector.x, renderingParams.pos.y - renderingParams.lastControlPointVector.y), transform.Transform(x, y) });

                renderingParams.lastControlPointVector.x = renderingParams.pos.x - renderingParams.lastControlPointVector.x - x;
                renderingParams.lastControlPointVector.y = renderingParams.pos.y - renderingParams.lastControlPointVector.y - y;

                renderingParams.pos.x = x;
                renderingParams.pos.y = y;
            }
        }
    }
}
