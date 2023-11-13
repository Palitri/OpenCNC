using Palitri.Graphics;
using Palitri.Graphics.Curves;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.Graphics.Devices
{
    public class CanvasGraphicsDevice : IGraphicsDevice
    {
        public System.Drawing.Graphics canvasGraphics;
        public Pen canvasPen;

        public CanvasGraphicsDevice(System.Drawing.Graphics canvasGraphics, Pen canvasPen)
        {
            this.canvasGraphics = canvasGraphics;
            this.canvasPen = canvasPen;

        }

        public virtual void Begin()
        {
        }

        public virtual void End()
        {
        }

        public virtual void Polyline(Vector[] vertices)
        {
            this.canvasGraphics.DrawLines(this.canvasPen, vertices.Select(p => new PointF(p.x, p.y)).ToArray());
        }

        public virtual void Arc(Vector origin, Vector semiMajorAxis, Vector semiMinorAxis, float startAngle, float endAngle)
        {
            ArcCurve arc = new ArcCurve(origin, semiMajorAxis, semiMinorAxis, startAngle, endAngle);

            if (startAngle == endAngle)
                return;

            float deltaAngle = endAngle - startAngle;
            int discreteSteps = Math.Max((int)Math.Abs(180.0 * deltaAngle / Math.PI), 1);

            PointF[] points = new PointF[discreteSteps + 1];
            for (int i = 0; i <= discreteSteps; i++)
                points[i] = arc.Get((float)i / (float)discreteSteps).ToPointF();

            this.canvasGraphics.DrawLines(this.canvasPen, points);
        }

        public virtual void Bezier(Vector[] vectors)
        {
            const int steps = 100;

            PointF[] drawPoints = new PointF[steps + 1];

            BezierCurve bezier = new BezierCurve(vectors);
            for (int step = 0; step <= steps; step++)
                drawPoints[step] = bezier.Get((float)step / (float)steps).ToPointF();

            this.canvasGraphics.DrawLines(this.canvasPen, drawPoints);
        }
    }
}
