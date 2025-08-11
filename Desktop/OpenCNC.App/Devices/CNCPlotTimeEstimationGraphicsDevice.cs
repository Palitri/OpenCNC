using OpenCNC.App.Settings;
using Palitri.Graphics;
using Palitri.Graphics.Curves;
using Palitri.OpenCNC.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenCNC.App.Devices
{
    public class CNCPlotTimeEstimationGraphicsDevice : IGraphicsDevice
    {
        private const float commTime = 0.03f;
        private const float extraTimeFactor = 1.0f;

        public float ProjectedTime { get { return this.projectedTime * extraTimeFactor; } }
        
        private OpenCNCAppSettings settings;
        
        private float projectedTime;
        private CNCVector lastPos = new CNCVector(0.0f, 0.0f, 0.0f);
        public CNCVector SVGToCNCVector(Vector2 vector, float z = 0.0f)
        {
            CNCVector result = new CNCVector(vector.x - lastPos.X, vector.y - lastPos.Y, z - lastPos.Z);
            this.lastPos.X = vector.x;
            this.lastPos.Y = vector.y;
            this.lastPos.Z = z;
            return result;
        }

        public CNCPlotTimeEstimationGraphicsDevice(OpenCNCAppSettings settings)
        {
            this.settings = settings;
        }

        public void SetOrigin(float x, float y)
        {
            this.lastPos.X = x;
            this.lastPos.Y = y;
        }

        public void IssueMove(Vector2 vector)
        {
            CNCVector move = SVGToCNCVector(vector);

            if ((move.X == 0.0f) && (move.Y == 0.0f) && (move.Z == 0.0f))
                return;

            this.projectedTime += vector.Length / this.settings.MoveSpeed;

            if (this.settings.DisengagementDistance != 0.0f)
                this.projectedTime += this.settings.DisengagementDistance * 2 / this.settings.MoveSpeed;
        }

        public void Begin()
        {
            this.projectedTime = commTime;
        }

        public void End()
        {
        }

        public void Polyline(Vector2[] vertices)
        {
            int length = vertices.Length;
            if (length == 0)
                return;

            this.IssueMove(vertices[0]);

            float totalLength = 0.0f;
            for (int i = 0; i < length - 1; i++)
                totalLength += vertices[i + 1].Subtract(vertices[i]).Length;

            SVGToCNCVector(vertices[length - 1]);

            this.projectedTime += totalLength / this.settings.WorkSpeed + commTime;
        }

        public void Arc(Vector2 origin, Vector2 semiMajorAxis, Vector2 semiMinorAxis, float startAngle, float endAngle)
        {
            ArcCurve arc = new ArcCurve(origin, semiMajorAxis, semiMinorAxis, startAngle, endAngle);

            if (startAngle == endAngle)
                return;

            float deltaAngle = endAngle - startAngle;
            int discreteSteps = Math.Max((int)Math.Abs(180.0 * deltaAngle / Math.PI), 1);

            Vector2 lastPoint = arc.Get(0.0f);
            this.IssueMove(lastPoint);

            float totalLength = 0.0f;
            for (int i = 1; i <= discreteSteps; i++)
            {
                Vector2 point = arc.Get((float)i / (float)discreteSteps);

                totalLength += lastPoint.Subtract(point).Length;

                lastPoint = point;
            }

            SVGToCNCVector(arc.Get(1.0f));

            this.projectedTime += totalLength / this.settings.WorkSpeed + commTime;
        }

        public void Bezier(Vector2[] vectors)
        {
            const int steps = 100;

            Vector2 lastPoint = vectors[0];
            this.IssueMove(lastPoint);

            BezierCurve bezier = new BezierCurve(vectors);
            float totalLength = 0.0f;
            for (int step = 1; step <= steps; step++)
            {
                Vector2 point = bezier.Get((float)step / (float)steps);

                totalLength += lastPoint.Subtract(point).Length;

                lastPoint = point;
            }

            SVGToCNCVector(vectors[vectors.Length - 1]);

            this.projectedTime += totalLength / this.settings.WorkSpeed + commTime;
        }
    }

}
