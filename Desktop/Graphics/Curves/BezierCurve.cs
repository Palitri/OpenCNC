using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.Graphics.Curves
{
    public class BezierCurve
    {
        private Vector[] controlPoints;
        private Vector[] points;
        private int controlPointsCount;

        public BezierCurve(Vector[] controlPoints)
        {
            this.controlPoints = controlPoints;

            this.controlPointsCount = controlPoints.Length;

            if ((this.points == null) || (this.points.Length < controlPointsCount - 1))
            {
                this.points = new Vector[controlPointsCount - 1];
                for (int i = 0; i < controlPointsCount - 1; i++)
                    this.points[i] = new Vector();
            }
        }

        public Vector Get(float t)
        {
            int initialStage = controlPointsCount - 1;
            if (initialStage <= 0)
                return new Vector(controlPoints[0].x, controlPoints[0].y);

            for (int i = 0; i < initialStage; i++)
            {
                this.points[i].x = controlPoints[i].x + (controlPoints[i + 1].x - controlPoints[i].x) * t;
                this.points[i].y = controlPoints[i].y + (controlPoints[i + 1].y - controlPoints[i].y) * t;
            }

            for (int stage = initialStage - 1; stage >= 0; stage--)
            {
                for (int i = 0; i < stage; i++)
                {
                    this.points[i].x = this.points[i].x + (this.points[i + 1].x - this.points[i].x) * t;
                    this.points[i].y = this.points[i].y + (this.points[i + 1].y - this.points[i].y) * t;
                }
            }

            return new Vector(this.points[0].x, this.points[0].y);
        }
    }
}
