using Palitri.Graphics.Curves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.Graphics.Devices
{
    public class PickerGraphicsDevice : IGraphicsDevice
    {
        public float MaxPickingDistance { get; set; }
        public int PickedIndex { get; private set; }
        public bool IsPicked { get { return this.PickedIndex != -1; } }
        public Vector2 PickPoint { get; set; }

        public float d;

        private int index;

        public PickerGraphicsDevice()
        {
            this.MaxPickingDistance = 2.0f;
            this.PickedIndex = -1;
            this.PickPoint = new Vector2();
        }

        public virtual void Begin()
        {
            this.PickedIndex = -1;
            this.index = 0;
        }

        public virtual void End()
        {
        }

        public virtual void Polyline(Vector2[] vertices)
        {
            if (this.IsPicked)
                return;

            int length = vertices.Length;
            for (int i = 1; i < length; i++)
            {
                if (PickerGraphicsDevice.Distance(vertices[i - 1], vertices[i], this.PickPoint) <= this.MaxPickingDistance)
                {
                    this.PickedIndex = index;
                    break;
                }
            }

            this.index++;
        }

        public virtual void Arc(Vector2 origin, Vector2 semiMajorAxis, Vector2 semiMinorAxis, float startAngle, float endAngle)
        {
            if (this.IsPicked)
                return;

            ArcCurve arc = new ArcCurve(origin, semiMajorAxis, semiMinorAxis, startAngle, endAngle);

            if (startAngle == endAngle)
                return;

            float deltaAngle = endAngle - startAngle;
            int discreteSteps = Math.Max((int)Math.Abs(180.0 * deltaAngle / Math.PI), 1);

            Vector2 lastPoint = null;
            for (int i = 0; i <= discreteSteps; i++)
            {
                Vector2 point = arc.Get((float)i / (float)discreteSteps);

                if (i > 0)
                {
                    if (PickerGraphicsDevice.Distance(lastPoint, point, this.PickPoint) <= this.MaxPickingDistance)
                    {
                        this.PickedIndex = index;
                        break;
                    }
                }

                lastPoint = point;
            }

            this.index++;
        }

        public virtual void Bezier(Vector2[] vectors)
        {
            const int steps = 100;

            if (this.IsPicked)
                return;

            BezierCurve bezier = new BezierCurve(vectors);
            Vector2 lastPoint = null;
            for (int step = 0; step <= steps; step++)
            {
                Vector2 point = bezier.Get((float)step / (float)steps);

                if (step > 0)
                {
                    if (PickerGraphicsDevice.Distance(lastPoint, point, this.PickPoint) <= this.MaxPickingDistance)
                    {
                        this.PickedIndex = index;
                        break;
                    }
                }

                lastPoint = point;
            }

            this.index++;
        }

        public static float Distance(Vector2 linePoint1, Vector2 linePoint2, Vector2 pickPoint)
        {
            Vector2 lineVector = linePoint2.Subtract(linePoint1);
            float lineLength = lineVector.Length;
            if (lineLength == 0.0f)
                return pickPoint.Subtract(linePoint1).Length;

            lineVector = lineVector.Scale(1.0f / lineLength);
            float projectionDistance = Vector2.Dot(lineVector, pickPoint.Subtract(linePoint1));
            
            if (projectionDistance <= 0)
                return pickPoint.Subtract(linePoint1).Length;
            if (projectionDistance >= lineLength)
                return pickPoint.Subtract(linePoint2).Length;

            Vector2 projection = lineVector.Scale(projectionDistance).Add(linePoint1);
            return pickPoint.Subtract(projection).Length;
        }
    }
}
