using Palitri.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.SVG.Elements
{
    public class SVGPath : SVGElement
    {
        public override string NodeName { get { return "path"; } }

        public string d;

        public List<IPathElement> elements = new List<IPathElement>();

        public override void UpdateProperties()
        {
            this.d = this.attributes["d"];

            this.elements.Clear();

            char[] signatures = { 'M', 'L', 'H', 'V', 'Z', 'C', 'S', 'Q', 'T', 'A' , 'm', 'l', 'h', 'v', 'z', 'c', 's', 'q', 't', 'a' };
            char[] dataSplitters = { ' ', ',', '\r', '\n', '\t' };

            int pos = 0, lastPos = 0;
            while (pos <= this.d.Length)
            {
                if ((pos == this.d.Length) || signatures.Contains(this.d[pos]))
                {
                    int elementLength = pos - lastPos;
                    if (elementLength != 0)
                    {
                        string[] data = this.d.Substring(lastPos + 1, elementLength - 1).Split(dataSplitters, StringSplitOptions.RemoveEmptyEntries);
                        char signature = this.d[lastPos];
                        bool isRelative = char.IsLower(signature);
                        if (isRelative)
                            signature = char.ToUpper(signature);

                        switch (signature)
                        {
                            case 'M':
                                for (int i = 0; i <= data.Length - 2; i += 2)
                                    if (i == 0)
                                        this.elements.Add(new PathM() { relative = isRelative, x = float.Parse(data[i + 0]), y = float.Parse(data[i + 1]) });
                                            else
                                        this.elements.Add(new PathL() { relative = isRelative, x = float.Parse(data[i + 0]), y = float.Parse(data[i + 1]) });

                                break;

                            case 'L':
                                for (int i = 0; i <= data.Length - 2; i += 2)
                                    this.elements.Add(new PathL() { relative = isRelative, x = float.Parse(data[i + 0]), y = float.Parse(data[i + 1]) });

                                break;

                            case 'H':
                                for (int i = 0; i < data.Length; i++)
                                    this.elements.Add(new PathH() { relative = isRelative, x = float.Parse(data[i]) });

                                break;

                            case 'V':
                                for (int i = 0; i < data.Length; i++)
                                    this.elements.Add(new PathV() { relative = isRelative, y = float.Parse(data[i]) });

                                break;

                            case 'Z':
                                this.elements.Add(new PathZ());

                                break;

                            case 'C':
                                for (int i = 0; i <= data.Length - 6; i += 6)
                                    this.elements.Add(new PathC() { relative = isRelative, x1 = float.Parse(data[i + 0]), y1 = float.Parse(data[i + 1]), x2 = float.Parse(data[i + 2]), y2 = float.Parse(data[i + 3]), x = float.Parse(data[i + 4]), y = float.Parse(data[i + 5]) });

                                break;

                            case 'S':
                                for (int i = 0; i <= data.Length - 4; i += 4)
                                    this.elements.Add(new PathS() { relative = isRelative, x2 = float.Parse(data[i + 0]), y2 = float.Parse(data[i + 1]), x = float.Parse(data[i + 2]), y = float.Parse(data[i + 3]) });

                                break;

                            case 'Q':
                                for (int i = 0; i <= data.Length - 4; i += 4)
                                    this.elements.Add(new PathQ() { relative = isRelative, x1 = float.Parse(data[i + 0]), y1 = float.Parse(data[i + 1]), x = float.Parse(data[i + 2]), y = float.Parse(data[i + 3]) });

                                break;

                            case 'T':
                                for (int i = 0; i <= data.Length - 2; i += 2)
                                    this.elements.Add(new PathT() { relative = isRelative, x = float.Parse(data[i + 0]), y = float.Parse(data[i + 1]) });

                                break;

                            case 'A':
                                for (int i = 0; i <= data.Length - 7; i += 7)
                                    this.elements.Add(new PathA() { relative = isRelative, rx = float.Parse(data[i + 0]), ry = float.Parse(data[i + 1]), angle = float.Parse(data[i + 2]), largeSweep = int.Parse(data[i + 3]) != 0, sweep = int.Parse(data[i + 4]) != 0, x = float.Parse(data[i + 5]), y = float.Parse(data[i + 6]) });

                                break;

                            default:
                            {
                                break;
                            }

                        }

                    }

                    lastPos = pos;
                }

                pos++;
            }
        }

        public override void Render(Matrix transform, IGraphicsDevice g)
        {
            SVGPathRenderingParameters renderParams = new SVGPathRenderingParameters();

            foreach (IPathElement element in this.elements)
                element.Render(transform, g, renderParams);
        }

        public class SVGPathRenderingParameters
        {
            internal bool hasOrigin = false;
            internal Vector origin = new Vector(0.0f, 0.0f);
            internal Vector pos = new Vector(0.0f, 0.0f);
            internal Vector lastControlPointVector = new Vector();
        }


        public interface IPathElement
        {
            void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams);
        }

        public class PathM : IPathElement
        {
            public bool relative;
            public float x, y;

            public void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
            {
                if (this.relative)
                {
                    renderingParams.pos.x += this.x;
                    renderingParams.pos.y += this.y;
                }
                else
                {
                    renderingParams.pos.x = this.x;
                    renderingParams.pos.y = this.y;
                }

//                if (!renderingParams.hasOrigin || !this.relative)
                {
                    renderingParams.origin.x = renderingParams.pos.x;
                    renderingParams.origin.y = renderingParams.pos.y;
                    renderingParams.hasOrigin = true;
                }
            }
        }

        public class PathL : IPathElement
        {
            public bool relative;
            public float x, y;

            public void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
            {
                if (this.relative)
                {
                    g.Polyline(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x + this.x, renderingParams.pos.y + this.y) });

                    renderingParams.pos.x += this.x;
                    renderingParams.pos.y += this.y;
                }
                else
                {
                    g.Polyline(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(this.x, this.y) });

                    renderingParams.pos.x = this.x;
                    renderingParams.pos.y = this.y;
                }
            }
        }

        public class PathH : IPathElement
        {
            public bool relative;
            public float x;

            public void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
            {
                if (this.relative)
                {
                    g.Polyline(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x + this.x, renderingParams.pos.y) });

                    renderingParams.pos.x += this.x;
                }
                else
                {
                    g.Polyline(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(this.x, renderingParams.pos.y) });

                    renderingParams.pos.x = this.x;
                }
            }
        }

        public class PathV : IPathElement
        {
            public bool relative;
            public float y;

            public void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
            {
                if (this.relative)
                {
                    g.Polyline(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x, renderingParams.pos.y + this.y) });

                    renderingParams.pos.y += this.y;
                }
                else
                {
                    g.Polyline(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x, this.y) });

                    renderingParams.pos.y = this.y;
                }
            }
        }

        public class PathZ : IPathElement
        {
            public void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
            {
                g.Polyline(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.origin) });

                renderingParams.pos.x = renderingParams.origin.x;
                renderingParams.pos.y = renderingParams.origin.y;

                renderingParams.hasOrigin = false;
            }
        }

        public class PathC : IPathElement
        {
            public bool relative;
            public float x1, y1, x2, y2, x, y;

            public void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
            {
                if (this.relative)
                {
                    g.Bezier(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x + x1, renderingParams.pos.y + y1), transform.Transform(renderingParams.pos.x + x2, renderingParams.pos.y + y2), transform.Transform(renderingParams.pos.x + x, renderingParams.pos.y + y) });

                    renderingParams.pos.x += x;
                    renderingParams.pos.y += y;
                }
                else
                {
                    g.Bezier(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(x1, y1), transform.Transform(x2, y2), transform.Transform(x, y) });

                    renderingParams.pos.x = x;
                    renderingParams.pos.y = y;
                }

                renderingParams.lastControlPointVector.x = x2 - x;
                renderingParams.lastControlPointVector.y = y2 - y;
            }
        }

        public class PathS : IPathElement
        {
            public bool relative;
            public float x2, y2, x, y;

            public void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
            {
                if (this.relative)
                {
                    g.Bezier(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x - renderingParams.lastControlPointVector.x, renderingParams.pos.y - renderingParams.lastControlPointVector.y), transform.Transform(renderingParams.pos.x + x2, renderingParams.pos.y + y2), transform.Transform(renderingParams.pos.x + x, renderingParams.pos.y + y) });

                    renderingParams.pos.x += x;
                    renderingParams.pos.y += y;
                }
                else
                {
                    g.Bezier(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x - renderingParams.lastControlPointVector.x, renderingParams.pos.y - renderingParams.lastControlPointVector.y), transform.Transform(x2, y2), transform.Transform(x, y) });

                    renderingParams.pos.x = x;
                    renderingParams.pos.y = y;
                }

                renderingParams.lastControlPointVector.x = x2 - x;
                renderingParams.lastControlPointVector.y = y2 - y;
            }
        }

        public class PathQ : IPathElement
        {
            public bool relative;
            public float x1, y1, x, y;

            public void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
            {
                if (this.relative)
                {
                    g.Bezier(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x + x1, renderingParams.pos.y + y1), transform.Transform(renderingParams.pos.x + x, renderingParams.pos.y + y) });

                    renderingParams.pos.x += x;
                    renderingParams.pos.y += y;
                }
                else
                {
                    g.Bezier(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(x1, y1), transform.Transform(x, y) });

                    renderingParams.pos.x = x;
                    renderingParams.pos.y = y;
                }

                renderingParams.lastControlPointVector.x = x1 - x;
                renderingParams.lastControlPointVector.y = y1 - y;
            }
        }

        public class PathT : IPathElement
        {
            public bool relative;
            public float x, y;

            public void Render(Matrix transform, IGraphicsDevice g, SVGPathRenderingParameters renderingParams)
            {
                if (this.relative)
                {
                    g.Bezier(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x - renderingParams.lastControlPointVector.x, renderingParams.pos.y - renderingParams.lastControlPointVector.y), transform.Transform(renderingParams.pos.x + x, renderingParams.pos.y + y) });

                    renderingParams.lastControlPointVector.x = -renderingParams.lastControlPointVector.x - x;
                    renderingParams.lastControlPointVector.y = -renderingParams.lastControlPointVector.y - y;

                    renderingParams.pos.x += x;
                    renderingParams.pos.y += y;
                }
                else
                {
                    g.Bezier(new Vector[] { transform.Transform(renderingParams.pos), transform.Transform(renderingParams.pos.x - renderingParams.lastControlPointVector.x, renderingParams.pos.y - renderingParams.lastControlPointVector.y), transform.Transform(x, y) });

                    renderingParams.lastControlPointVector.x = renderingParams.pos.x - renderingParams.lastControlPointVector.x - x;
                    renderingParams.lastControlPointVector.y = renderingParams.pos.y - renderingParams.lastControlPointVector.y - y;

                    renderingParams.pos.x = x;
                    renderingParams.pos.y = y;
                }
            }
        }

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
        }

        // svg : [A | a] (rx ry x-axis-rotation large-arc-flag sweep-flag x y)+
        private static float Radian(float ux, float uy, float vx, float vy ) 
        {
            float dot = ux * vx + uy * vy;
            float mod = (float)Math.Sqrt(( ux * ux + uy * uy ) * ( vx * vx + vy * vy ));
            float rad = (float)Math.Acos(dot / mod);
            if( ux * vy - uy * vx < 0.0 )
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
