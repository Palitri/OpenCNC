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
        private Vector2[] controlPoints;
        private Vector2[] points;
        private int controlPointsCount;

        public BezierCurve(Vector2[] controlPoints)
        {
            this.controlPoints = controlPoints;

            this.controlPointsCount = controlPoints.Length;

            if ((this.points == null) || (this.points.Length < controlPointsCount - 1))
            {
                this.points = new Vector2[controlPointsCount - 1];
                for (int i = 0; i < controlPointsCount - 1; i++)
                    this.points[i] = new Vector2();
            }
        }

        public Vector2 Get(float t)
        {
            int initialStage = controlPointsCount - 1;
            if (initialStage <= 0)
                return new Vector2(controlPoints[0].x, controlPoints[0].y);

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

            return new Vector2(this.points[0].x, this.points[0].y);
        }
    }
}
