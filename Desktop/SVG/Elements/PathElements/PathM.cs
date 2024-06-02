using Palitri.Graphics;
using static Palitri.SVG.Elements.SVGPath;

namespace SVG.Elements.PathElements
{
    /// <summary>
    /// Move to position
    /// </summary>
    public class PathM : IPathElement
    {
        public bool relative;
        public float x, y;

        public void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
        {
            if (this.relative)
            {
                renderingParams.pos.x += this.x;
                renderingParams.pos.y += this.y;
            }
            else
            {
                renderingParams.pos.x = this.x;
                renderingParams.pos.y = this.y;
            }

            //                if (!renderingParams.hasOrigin || !this.relative)
            {
                renderingParams.origin.x = renderingParams.pos.x;
                renderingParams.origin.y = renderingParams.pos.y;
                renderingParams.hasOrigin = true;
            }
        }
    }
}
