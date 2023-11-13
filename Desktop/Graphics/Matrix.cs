using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.Graphics
{
    public class Matrix
    {
        public float
            m11, m12, m13,
            m21, m22, m23,
            m31, m32, m33;

        public Matrix()
        {
            this.m11 = 1.0f;
            this.m12 = 0.0f;
            this.m13 = 0.0f;
            this.m21 = 0.0f;
            this.m22 = 1.0f;
            this.m23 = 0.0f;
            this.m31 = 0.0f;
            this.m32 = 0.0f;
            this.m33 = 1.0f;
        }

        public Vector Transform(Vector point)
        {
            return this.Transform(point.x, point.y);
        }

        public Vector Transform(float x, float y)
        {
            Vector result = new Vector();

            result.x = x * this.m11 +
                       y * this.m21 +
                           this.m31;
            result.y = x * this.m12 +
                       y * this.m22 +
                           this.m32;

            return result;
        }

        public Vector TransformDimensions(Vector point)
        {
            return this.TransformDimensions(point.x, point.y);
        }

        public Vector TransformDimensions(float x, float y)
        {
            Vector result = new Vector();

            result.x = x * this.m11 +
                       y * this.m21;
            result.y = x * this.m12 +
                       y * this.m22;

            return result;
        }

        public static void Copy(Matrix result, Matrix source)
        {
            result.m11 = source.m11;
            result.m12 = source.m12;
            result.m13 = source.m13;
            result.m21 = source.m21;
            result.m22 = source.m22;
            result.m23 = source.m23;
            result.m31 = source.m31;
            result.m32 = source.m32;
            result.m33 = source.m33;
        }

        public static void Multiply(Matrix result, Matrix matrix1, Matrix matrix2)
        {
            float m11 = matrix1.m11 * matrix2.m11 +
                        matrix1.m12 * matrix2.m21 +
                        matrix1.m13 * matrix2.m31;
            float m12 = matrix1.m11 * matrix2.m12 +
                        matrix1.m12 * matrix2.m22 +
                        matrix1.m13 * matrix2.m32;
            float m13 = matrix1.m11 * matrix2.m13 +
                        matrix1.m12 * matrix2.m23 +
                        matrix1.m13 * matrix2.m33;
            float m21 = matrix1.m21 * matrix2.m11 +
                        matrix1.m22 * matrix2.m21 +
                        matrix1.m23 * matrix2.m31;
            float m22 = matrix1.m21 * matrix2.m12 +
                        matrix1.m22 * matrix2.m22 +
                        matrix1.m23 * matrix2.m32;
            float m23 = matrix1.m21 * matrix2.m13 +
                        matrix1.m22 * matrix2.m23 +
                        matrix1.m23 * matrix2.m33;
            float m31 = matrix1.m31 * matrix2.m11 +
                        matrix1.m32 * matrix2.m21 +
                        matrix1.m33 * matrix2.m31;
            float m32 = matrix1.m31 * matrix2.m12 +
                        matrix1.m32 * matrix2.m22 +
                        matrix1.m33 * matrix2.m32;
            float m33 = matrix1.m31 * matrix2.m13 +
                        matrix1.m32 * matrix2.m23 +
                        matrix1.m33 * matrix2.m33;

            result.m11 = m11;
            result.m12 = m12;
            result.m13 = m13;
            result.m21 = m21;
            result.m22 = m22;
            result.m23 = m23;
            result.m31 = m31;
            result.m32 = m32;
            result.m33 = m33;
        }

        public static void CreateViewport(Matrix result, float x, float y, float width, float height)
        {
            result.m11 = 1.0f / width;
            result.m12 = 0.0f;
            result.m13 = 0.0f;
            result.m21 = 0.0f;
            result.m22 = 1.0f / height;
            result.m23 = 0.0f;
            result.m31 = -x / width;
            result.m32 = -y / height;
            result.m33 = 1.0f;
        }

        public static void CreatePlot(Matrix result, float x, float y, float width, float height)
        {
            result.m11 = width;
            result.m12 = 0.0f;
            result.m13 = 0.0f;
            result.m21 = 0.0f;
            result.m22 = height;
            result.m23 = 0.0f;
            result.m31 = x;
            result.m32 = y;
            result.m33 = 1.0f;
        }

        public static void CreatePlot(Matrix result, float x, float y, float width, float height, float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            result.m11 = cos * width;
            result.m12 = sin * width;
            result.m13 = 0.0f;
            result.m21 = -sin * height;
            result.m22 = cos * height;
            result.m23 = 0.0f;
            result.m31 = x;
            result.m32 = y;
            result.m33 = 1.0f;
        }

        public static void CreateTranslation(Matrix result, float x, float y)
        {
            result.m11 = 1.0f;
            result.m12 = 0.0f;
            result.m13 = 0.0f;
            result.m21 = 0.0f;
            result.m22 = 1.0f;
            result.m23 = 0.0f;
            result.m31 = x;
            result.m32 = y;
            result.m33 = 1.0f;
        }

        public static void CreateSkewX(Matrix result, float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            float tg = cos != 0 ? sin / cos : 0.0f;
            result.m11 = 1.0f;
            result.m12 = 0.0f;
            result.m13 = 0.0f;
            result.m21 = tg;
            result.m22 = 1.0f;
            result.m23 = 0.0f;
            result.m31 = 0.0f;
            result.m32 = 0.0f;
            result.m33 = 1.0f;
        }

        public static void CreateSkewY(Matrix result, float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            float tg = cos != 0 ? sin / cos : 0.0f;
            result.m11 = 1.0f;
            result.m12 = tg;
            result.m13 = 0.0f;
            result.m21 = 0.0f;
            result.m22 = 1.0f;
            result.m23 = 0.0f;
            result.m31 = 0.0f;
            result.m32 = 0.0f;
            result.m33 = 1.0f;
        }

        public static void CreateScale(Matrix result, float x, float y)
        {
            result.m11 = x;
            result.m12 = 0.0f;
            result.m13 = 0.0f;
            result.m21 = 0.0f;
            result.m22 = y;
            result.m23 = 0.0f;
            result.m31 = x;
            result.m32 = y;
            result.m33 = 1.0f;
        }

        public static void CreateRotation(Matrix result, float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            result.m11 = cos;
            result.m12 = sin;
            result.m13 = 0.0f;
            result.m21 = -sin;
            result.m22 = cos;
            result.m23 = 0.0f;
            result.m31 = 0;
            result.m32 = 0;
            result.m33 = 1.0f;
        }
    }
}
