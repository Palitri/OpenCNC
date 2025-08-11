using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.Graphics
{
    public interface IGraphicsDevice
    {
        void Begin();
        void End();

        void Polyline(Vector2[] vertices);
        void Arc(Vector2 origin, Vector2 semiMajorAxis, Vector2 semiMinorAxis, float startAngle, float endAngle);
        void Bezier(Vector2[] vectors);
    }
}
