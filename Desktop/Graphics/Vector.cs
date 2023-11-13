using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.Graphics
{
    public class Vector
    {
        public float x, y;

        public float Length { get { return (float)Math.Sqrt(this.x * this.x + this.y * this.y); } }

        public static float Dot(Vector v1, Vector v2)
        {
            return v1.x * v2.x + v1.y * v2.y;
        }

        public Vector()
        {
            this.x = 0.0f;
            this.y = 0.0f;
        }

        public Vector(float value)
        {
            this.x = value;
            this.y = value;
        }

        public Vector(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector(Vector v)
        {
            this.x = v.x;
            this.y = v.y;
        }

        public Vector Add(Vector v)
        {
            return new Vector(this.x + v.x, this.y + v.y);
        }

        public Vector Subtract(Vector v)
        {
            return new Vector(this.x - v.x, this.y - v.y);
        }

        public Vector Scale(float factor)
        {
            return new Vector(this.x * factor, this.y * factor);
        }

        public Point ToPoint()
        {
            return new Point((int)this.x, (int)this.y);
        }

        public PointF ToPointF()
        {
            return new PointF(this.x, this.y);
        }

        public Size ToSize()
        {
            return new Size((int)this.x, (int)this.y);
        }

        public SizeF ToSizeF()
        {
            return new SizeF(this.x, this.y);
        }

        public Vector Normal()
        {
            float k = 1.0f / this.Length;
            return new Vector(this.x * k, this.y * k);
        }
    }
}
