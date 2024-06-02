using Palitri.Graphics;
using static Palitri.SVG.Elements.SVGPath;

namespace SVG.Elements.PathElements
{
    public interface IPathElement
    {
        void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams);
    }
}
