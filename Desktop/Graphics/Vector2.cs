using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.Graphics
{
    public class Vector2
    {
        public float x, y;

        public float Length { get { return (float)Math.Sqrt(this.x * this.x + this.y * this.y); } }

        public static float Dot(Vector2 v1, Vector2 v2)
        {
            return v1.x * v2.x + v1.y * v2.y;
        }

        public Vector2()
        {
            this.x = 0.0f;
            this.y = 0.0f;
        }

        public Vector2(float value)
        {
            this.x = value;
            this.y = value;
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(Vector2 v)
        {
            this.x = v.x;
            this.y = v.y;
        }

        public Vector2 Add(Vector2 v)
        {
            return new Vector2(this.x + v.x, this.y + v.y);
        }

        public Vector2 Subtract(Vector2 v)
        {
            return new Vector2(this.x - v.x, this.y - v.y);
        }

        public Vector2 Scale(float factor)
        {
            return new Vector2(this.x * factor, this.y * factor);
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

        public Vector2 Normal()
        {
            float k = 1.0f / this.Length;
            return new Vector2(this.x * k, this.y * k);
        }
    }
}
