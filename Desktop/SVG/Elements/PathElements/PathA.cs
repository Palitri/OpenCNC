using Palitri.Graphics;
using System;
using static Palitri.SVG.Elements.SVGPath;

namespace SVG.Elements.PathElements
{
    public class PathA : IPathElement
    {
        public bool relative;
        public float rx, ry, angle, x, y;
        public bool sweep, largeSweep;

        public void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
        {
            float endX = this.relative ? renderingParams.pos.x + this.x : this.x;
            float endY = this.relative ? renderingParams.pos.x + this.y : this.y;

            float cx, cy, startAngle, endAngle;
            float angle = (float)Math.PI * this.angle / 180.0f;
            SVGArcToCenterParam(renderingParams.pos.x, renderingParams.pos.y, this.rx, this.ry, angle, this.largeSweep, this.sweep, endX, endY, out cx, out cy, out startAngle, out endAngle);
            Vector semiMajor = new Vector((float)Math.Cos(angle) * this.rx, (float)Math.Sin(angle) * this.rx);
            Vector semiMinor = new Vector(-(float)Math.Sin(angle) * this.ry, (float)Math.Cos(angle) * this.ry);

            g.Arc(transform.Transform(cx, cy), transform.TransformDimensions(semiMajor), transform.TransformDimensions(semiMinor), startAngle, endAngle);

            renderingParams.pos.x = endX;
            renderingParams.pos.y = endY;
        }

        // svg : [A | a] (rx ry x-axis-rotation large-arc-flag sweep-flag x y)+
        private static float Radian(float ux, float uy, float vx, float vy)
        {
            float dot = ux * vx + uy * vy;
            float mod = (float)Math.Sqrt((ux * ux + uy * uy) * (vx * vx + vy * vy));
            float rad = (float)Math.Acos(dot / mod);
            if (ux * vy - uy * vx < 0.0)
            {
                rad = -rad;
            }

            return rad;
        }

        //conversion_from_endpoint_to_center_parameterization
        //sample :  svgArcToCenterParam(200,200,50,50,0,1,1,300,200)
        // x1 y1 rx ry φ fA fS x2 y2
        private static bool SVGArcToCenterParam(float x1, float y1, float rx, float ry, float phi, bool fA, bool fS, float x2, float y2, out float cx, out float cy, out float startAngle, out float endAngle)
        {
            cx = 0.0f;
            cy = 0.0f;
            startAngle = 0.0f;
            endAngle = 0.0f;

            float PIx2 = (float)Math.PI * 2.0f;

            if (rx < 0)
                rx = -rx;

            if (ry < 0)
                ry = -ry;

            // Radii must not be zero
            if (rx == 0.0 || ry == 0.0)
                return false;

            float s_phi = (float)Math.Sin(phi);
            float c_phi = (float)Math.Cos(phi);
            float hd_x = (x1 - x2) / 2.0f; // half diff of x
            float hd_y = (y1 - y2) / 2.0f; // half diff of y
            float hs_x = (x1 + x2) / 2.0f; // half sum of x
            float hs_y = (y1 + y2) / 2.0f; // half sum of y

            // F6.5.1
            float x1_ = c_phi * hd_x + s_phi * hd_y;
            float y1_ = c_phi * hd_y - s_phi * hd_x;

            // F.6.6 Correction of out-of-range radii
            //   Step 3: Ensure radii are large enough
            float lambda = (x1_ * x1_) / (rx * rx) + (y1_ * y1_) / (ry * ry);
            if (lambda > 1.0f)
            {
                rx = rx * (float)Math.Sqrt(lambda);
                ry = ry * (float)Math.Sqrt(lambda);
            }

            var rxry = rx * ry;
            var rxy1_ = rx * y1_;
            var ryx1_ = ry * x1_;
            var sum_of_sq = rxy1_ * rxy1_ + ryx1_ * ryx1_; // sum of square
            // Start point and end point can not be the same
            if (sum_of_sq == 0.0f)
                return false;

            float coe = (float)Math.Sqrt(Math.Abs((rxry * rxry - sum_of_sq) / sum_of_sq));
            if (fA == fS)
                coe = -coe;

            // F6.5.2
            float cx_ = coe * rxy1_ / ry;
            float cy_ = -coe * ryx1_ / rx;

            // F6.5.3
            cx = c_phi * cx_ - s_phi * cy_ + hs_x;
            cy = s_phi * cx_ + c_phi * cy_ + hs_y;

            float xcr1 = (x1_ - cx_) / rx;
            float xcr2 = (x1_ + cx_) / rx;
            float ycr1 = (y1_ - cy_) / ry;
            float ycr2 = (y1_ + cy_) / ry;

            // F6.5.5
            startAngle = Radian(1.0f, 0.0f, xcr1, ycr1);

            // F6.5.6
            float deltaAngle = Radian(xcr1, ycr1, -xcr2, -ycr2);
            while (deltaAngle > PIx2) deltaAngle -= PIx2;
            while (deltaAngle < 0.0) deltaAngle += PIx2;

            if (!fS)
                deltaAngle -= PIx2;

            endAngle = startAngle + deltaAngle;
            //while (endAngle > PIx2) { endAngle -= PIx2; }
            //while (endAngle < 0.0) { endAngle += PIx2; }

            return true;
        }
    }
}
