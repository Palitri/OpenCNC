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

        void Polyline(Vector[] vertices);
        void Arc(Vector origin, Vector semiMajorAxis, Vector semiMinorAxis, float startAngle, float endAngle);
        void Bezier(Vector[] vectors);
    }
}
