using Palitri.Graphics;
using static Palitri.SVG.Elements.SVGPath;

namespace Palitri.SVG.Elements.PathElements
{
    public interface IPathElement
    {
        void Render(Matrix3 transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams);
    }
}
