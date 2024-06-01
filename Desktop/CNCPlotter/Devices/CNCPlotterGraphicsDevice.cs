using Palitri.CNCDriver;
using Palitri.CNCPlotter.Common;
using Palitri.Graphics;
using Palitri.Graphics.Curves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CNCPlotter
{
    public delegate void OnRenderPrimitiveDelegate(object sender, CNCPlotterGraphicsDeviceRenderPrimitiveEventArgs e);

    public class CNCPlotterGraphicsDevice : IGraphicsDevice
    {
        private CNCSettings settings;

        private bool isCancelled = false;
        public bool IsCancelled { get { return this.isCancelled; } }

        private CNCVector lastPos;
        private CNCVector origin;

        public CNCPlotterGraphicsDevice(ICNC cnc, CNCSettings settings)
        {
            this.cnc = cnc;
            this.settings = settings;

            this.isCancelled = false;
            this.lastPos = new CNCVector(0.0f, 0.0f, 0.0f);
            this.origin = new CNCVector(0.0f, 0.0f, 0.0f);
        }

        public CNCVector SVGToCNCVector(Vector vector, float z = 0.0f)
        {
            CNCVector result = new CNCVector(vector.x - lastPos.X, vector.y - lastPos.Y, z - lastPos.Z);
            this.lastPos.X = vector.x;
            this.lastPos.Y = vector.y;
            this.lastPos.Z = z;
            return result;
        }


        public void SetOrigin(float x, float y)
        {
            this.lastPos.X = x;
            this.lastPos.Y = y;

            this.origin.X = x;
            this.origin.Y = y;
        }

        public void ReturnToOrigin()
        {
            this.cnc.Begin();
            this.IssueMove(new Vector(this.origin.X, this.origin.Y));
            this.cnc.End();
            this.cnc.Execute();
        }

        public void CancelPlotting()
        {
            this.isCancelled = true;
        }

        public static CNCVector SVGToCNCDimensions(float x, float y, float z = 0.0f)
        {
            return new CNCVector(x, y, z);
        }

        public static CNCVector SVGToCNCDimensions(Vector dimensions, float z = 0.0f)
        {
            return SVGToCNCDimensions(dimensions.x, dimensions.y, z);
        }

        public void IssueMove(Vector vector)
        {
            this.IssueMoveLinear(vector);
            //this.IssueMoveSine(vector);
            //if (vector.Length > 30.0f)
            //    this.IssueMoveSine(vector);
            //else
            //    this.IssueMoveLinear(vector);
        }

        public void IssueMoveLinear(Vector vector)
        {
            CNCVector move = SVGToCNCVector(vector);

            if ((move.X != 0.0f) || (move.Y != 0.0f) || (move.Z != 0.0f))
            {
                this.cnc.SetSpeed(this.settings.MoveSpeed);
                this.cnc.SetPower(this.settings.OffPower);
                if (this.settings.DisengagementDistance == 0.0f)
                    this.cnc.Polyline(new CNCVector[] { move });
                else
                    this.cnc.Polyline(new CNCVector[] { new CNCVector(0.0f, 0.0f, this.settings.DisengagementDistance), move, new CNCVector(0.0f, 0.0f, -this.settings.DisengagementDistance) });

                this.cnc.SetSpeed(this.settings.WorkSpeed);
                if (this.settings.EngagementDistance == 0.0f)
                {
                    this.cnc.SetPower(this.settings.OnPower);
                }
                else
                {
                    this.cnc.Polyline(new CNCVector[] { new CNCVector(0.0f, 0.0f, -this.settings.EngagementDistance)});
                    this.cnc.SetPower(this.settings.OnPower);
                    this.cnc.Polyline(new CNCVector[] { new CNCVector(0.0f, 0.0f, this.settings.EngagementDistance) });
                }
            }

            this.cnc.End();
            this.cnc.Execute();
            this.cnc.Begin();
        }

        public void IssueMoveSine(Vector vector)
        {
            CNCVector move = SVGToCNCVector(vector);

            if ((move.X == 0.0f) && (move.Y == 0.0f) && (move.Z == 0.0f))
                return;

            this.cnc.SetSpeed(this.settings.MoveSpeed);
            this.cnc.SetPower(this.settings.OffPower);
            if (this.settings.DisengagementDistance != 0.0f)
                this.cnc.Polyline(new CNCVector[] { new CNCVector(0.0f, 0.0f, this.settings.DisengagementDistance) });
            this.cnc.DriveSine(0.5f, move.X, 1.0f, -0.25f, 0.25f);
            this.cnc.DriveSine(0.5f, move.Y, 1.0f, -0.25f, 0.25f);
            this.cnc.DriveSine(0.5f, move.Z, 1.0f, -0.25f, 0.25f);
            this.cnc.Drive(vector.Length / this.settings.MoveSpeed);
            if (this.settings.DisengagementDistance != 0.0f)
                this.cnc.Polyline(new CNCVector[] { new CNCVector(0.0f, 0.0f, -this.settings.DisengagementDistance) });
        }

        private ICNC cnc;

        public int PrimitiveIndex { get; private set; }

        public event OnRenderPrimitiveDelegate RenderPrimitive;

        public void OnRenderPrimitive(int primitiveIndex)
        {
            if (this.RenderPrimitive != null)
                this.RenderPrimitive.Invoke(this, new CNCPlotterGraphicsDeviceRenderPrimitiveEventArgs(primitiveIndex));
        }

        public void Begin()
        {
            this.cnc.Begin();
            this.cnc.SetMotorsPowerMode(true);
            this.cnc.End();
            this.cnc.Execute();

            this.cnc.Begin();

            this.isCancelled = false;
        }

        public void End()
        {
            this.cnc.End();
            this.cnc.Execute();

            this.cnc.Begin();
            this.cnc.SetPower(this.settings.IdlePower);
            if (this.settings.PowerOffMotorsWhenIdle)
                this.cnc.SetMotorsPowerMode(false);
            this.cnc.End();
            this.cnc.Execute();
        }

        public void Polyline(Vector[] vertices)
        {
            if (this.isCancelled)
                return;

            this.PrimitiveIndex++;

            this.OnRenderPrimitive(this.PrimitiveIndex);

            if (vertices.Length <= 0)
                return;

            this.IssueMove(vertices[0]);

            this.cnc.Polyline(vertices.Skip(1).Select(v => SVGToCNCVector(v)).ToArray());
        }

        public void Arc(Vector origin, Vector semiMajorAxis, Vector semiMinorAxis, float startAngle, float endAngle)
        {
            if (this.isCancelled)
                return;

            this.PrimitiveIndex++;

            this.OnRenderPrimitive(this.PrimitiveIndex);

            ArcCurve arc = new ArcCurve(origin, semiMajorAxis, semiMinorAxis, startAngle, endAngle);

            this.IssueMove(arc.Get(0.0f));

            this.cnc.Arc(SVGToCNCDimensions(semiMajorAxis), SVGToCNCDimensions(semiMinorAxis), startAngle, endAngle);

            SVGToCNCVector(arc.Get(1.0f));
        }

        public void Bezier(Vector[] vectors)
        {
            if (this.isCancelled)
                return;

            this.PrimitiveIndex++;

            this.OnRenderPrimitive(this.PrimitiveIndex);

            Vector origin = vectors[0];

            this.IssueMove(origin);

            this.cnc.Bezier(vectors.Select(v => SVGToCNCDimensions(v.x - origin.x, v.y - origin.y, 0.0f)).ToArray());

            SVGToCNCVector(vectors[vectors.Length - 1]);
        }
    }

    public class CNCPlotterGraphicsDeviceRenderPrimitiveEventArgs : EventArgs
    {
        public int PrimitiveIndex { get; private set; }
        public bool Continue { get; set; }

        public CNCPlotterGraphicsDeviceRenderPrimitiveEventArgs(int primitiveIndex)
        {
            this.PrimitiveIndex = primitiveIndex;
            this.Continue = true;
        }
    }
}
