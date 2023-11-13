using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.Graphics
{
    public interface IGraphicsObject
    {
        void Render(IGraphicsDevice g);
        void Render(float x, float y, IGraphicsDevice g);
        void Render(float x, float y, float scale, IGraphicsDevice g);
        void Render(float x, float y, float width, float height, IGraphicsDevice g);
        void Render(float x, float y, float width, float height, float angle, IGraphicsDevice g);
        void Render(Matrix transform, IGraphicsDevice g);
    }
}
