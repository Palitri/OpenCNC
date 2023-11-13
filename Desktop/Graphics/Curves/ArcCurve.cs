using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.Graphics.Curves
{
    public class ArcCurve
    {
        Vector origin;
        Vector semiMajorAxis;
        Vector semiMinorAxis;
        float startAngle;
        float endAngle;
        float deltaAngle;

        public ArcCurve(Vector origin, Vector semiMajorAxis, Vector semiMinorAxis, float startAngle, float endAngle)
        {
            this.origin = new Vector(origin);
            this.semiMajorAxis = new Vector(semiMajorAxis);
            this.semiMinorAxis = new Vector(semiMinorAxis);
            this.startAngle = startAngle;
            this.endAngle = endAngle;
            this.deltaAngle = endAngle - startAngle;
        }

        public Vector Get(float t)
        {
            float angle = this.startAngle + t * this.deltaAngle;
            float x = (float)Math.Cos(angle);
            float y = (float)Math.Sin(angle);

            return new Vector(this.origin.x + this.semiMajorAxis.x * x + this.semiMinorAxis.x * y, this.origin.y + this.semiMajorAxis.y * x + this.semiMinorAxis.y * y);
        }
    }
}
