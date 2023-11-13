using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNCPlotter.Rendering
{
    public class ViewerRendererTopLeft
    {
        public Bitmap backBuffer;
        private Bitmap mainScreen, horizontalStrip, verticalStrip;

        public Color backgroundColor, gridColor, stripsColor, textColor, coordLineColorBig, coordLineColorMed, coordLineColorSmall;

        private Graphics backBufferGraphics, horizontalStripGraphics, verticalStripGraphics;
        public Graphics mainGraphics;

        public int width, height;
        public int mainScreenWidth, mainScreenHeight;
        public int stripsSize;
        public float coordLineBig, coordLineMed, coordLineSmall;
        public int textSize;
        public int[] rulerDivisionFactors = new int[] { 10, 20, 100 };
        public float[] rulerDivisionLines = new float[] { 0.1f, 0.15f, 0.3f };
        public Color[] rulerDivisionColors = new Color[] { Color.FromArgb(170, 170, 170), Color.FromArgb(170, 170, 170), Color.FromArgb(150, 150, 150) };
        public Font[] rulerDivisionFonts = new Font[] { null, new Font("Arial", 6.0f), new Font("Arial", 8.0f) };

        public float zoom;
        public PointF offset;
        public PointF mouse, pointer;

        public ViewerRendererTopLeft(int width, int height)
        {
            this.backgroundColor = Color.White;
            this.gridColor = Color.FromArgb(220, 220, 220);
            this.stripsColor = Color.FromArgb(230, 230, 230);
            this.textColor = Color.FromArgb(127, 127, 127);
            this.coordLineColorBig = Color.FromArgb(150, 150, 150);
            this.coordLineColorSmall = Color.FromArgb(170, 170, 170);

            this.coordLineBig = 0.3f;
            this.coordLineMed = 0.15f;
            this.coordLineSmall = 0.1f;
            this.textSize = 20;

            this.stripsSize = 25;
            this.zoom = 1.0f;
            this.offset = new PointF(0.0f, 0.0f);
            this.mouse = new PointF(0.0f, 0.0f);
            this.pointer = new PointF(0.0f, 0.0f);

            this.SetSize(width, height);
        }

        public void SetSize(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.mainScreenWidth = this.width - this.stripsSize;
            this.mainScreenHeight = this.height - this.stripsSize;

            this.backBuffer = new Bitmap(this.width, this.height);
            this.horizontalStrip = new Bitmap(this.width, this.stripsSize);
            this.verticalStrip = new Bitmap(this.stripsSize, this.height);
            this.mainScreen = new Bitmap(this.mainScreenWidth, this.mainScreenHeight);

            this.backBufferGraphics = Graphics.FromImage(this.backBuffer);
            this.mainGraphics = Graphics.FromImage(this.mainScreen);
            this.horizontalStripGraphics = Graphics.FromImage(this.horizontalStrip);
            this.verticalStripGraphics = Graphics.FromImage(this.verticalStrip);

            this.RenderStrips();
            this.RenderMain();
        }

        public int GetPowerOf2(int v)
        {
            int x = 1;
            while (x < v)
                x *= 2;
            return v == x ? x : x / 2;
        }

        public PointF ScreenToGraphic(float x, float y)
        {
            return new PointF((x - this.offset.X) / this.zoom, (y - this.offset.Y) / this.zoom);
        }
        public PointF GraphicToScreen(float x, float y)
        {
            return new PointF((this.offset.X + x) * this.zoom, (this.offset.Y + y) * this.zoom);
        }
        public bool IsMultipleOf(float value, float divisor)
        {
            float quotient = value / divisor;
            float remainder = quotient - (float)Math.Round(quotient);
            return Math.Abs(remainder) < 0.00001;
        }

        public void RenderStrips()
        {
            float divisionsZoomFactor = this.zoom >= 1.0f ? this.GetPowerOf2((int)this.zoom) : 1.0f / this.GetPowerOf2((int)(1.0f / this.zoom));
            float divisionStep = this.rulerDivisionFactors[0] / divisionsZoomFactor;

            
            this.FillBitmap(this.horizontalStrip, this.horizontalStripGraphics, this.stripsColor, true);

            float divisionX = -(int)(this.offset.X / divisionStep) * divisionStep;
            float rulerX = 0.0f;
            do
            {
                rulerX = this.GraphicToScreen(divisionX, 0.0f).X;

                for (int d = this.rulerDivisionFactors.Length - 1; d >= 0; d--)
                {
                    if (this.IsMultipleOf(divisionX, this.rulerDivisionFactors[d] / divisionsZoomFactor))
                    {
                        int y = (int)(this.stripsSize * (1.0f - this.rulerDivisionLines[d]));
                        this.horizontalStripGraphics.DrawLine(new Pen(this.rulerDivisionColors[d]), rulerX, y, rulerX, this.stripsSize);

                        if (this.rulerDivisionFonts[d] != null)
                        {
                            StringFormat sf = new StringFormat();
                            sf.LineAlignment = StringAlignment.Far;
                            sf.Alignment = rulerX < divisionStep ? StringAlignment.Near : StringAlignment.Center;
                            string text = (d == this.rulerDivisionFactors.Length - 1 ? divisionX : (divisionX % 100) / 10).ToString();
                            this.horizontalStripGraphics.DrawString(text, this.rulerDivisionFonts[d], new SolidBrush(this.textColor), new PointF(rulerX, y), sf);
                        }

                        break;
                    }
                }

                divisionX += divisionStep;
            }
            while (rulerX < this.mainScreen.Width);

            this.horizontalStripGraphics.DrawLine(new Pen(this.coordLineColorBig) { DashPattern = new float[] { 2, 2 } }, this.mouse.X, 0, this.mouse.X, this.stripsSize);



            this.FillBitmap(this.verticalStrip, this.verticalStripGraphics, this.stripsColor, false);

            float divisionY = -(int)(this.offset.Y / divisionStep) * divisionStep;
            float rulerY = 0.0f;
            do
            {
                rulerY = this.GraphicToScreen(0.0f, divisionY).Y;

                for (int d = this.rulerDivisionFactors.Length - 1; d >= 0; d--)
                {
                    if (this.IsMultipleOf(divisionY, this.rulerDivisionFactors[d] / divisionsZoomFactor))
                    {
                        int x = (int)(this.stripsSize * (1.0f - this.rulerDivisionLines[d]));
                        this.verticalStripGraphics.DrawLine(new Pen(this.rulerDivisionColors[d]), x, rulerY, this.stripsSize, rulerY);

                        if (this.rulerDivisionFonts[d] != null)
                        {
                            StringFormat sf = new StringFormat();
                            sf.LineAlignment = StringAlignment.Far;
                            sf.Alignment = rulerY < divisionStep ? StringAlignment.Near : StringAlignment.Center;
                            sf.FormatFlags = StringFormatFlags.DirectionVertical;
                            string text = (d == this.rulerDivisionFactors.Length - 1 ? divisionY : (divisionY % 100) / 10).ToString();
                            this.verticalStripGraphics.DrawString(text, this.rulerDivisionFonts[d], new SolidBrush(this.textColor), new PointF(x, rulerY), sf);
                        }

                        break;
                    }
                }

                divisionY += divisionStep;
            }
            while (rulerY < this.mainScreen.Width);

            this.verticalStripGraphics.DrawLine(new Pen(this.coordLineColorBig) { DashPattern = new float[] { 2, 2 } }, 0, this.mouse.Y, this.stripsSize, this.mouse.Y);


            // Left top square between rulers
            this.backBufferGraphics.FillPolygon(this.BrushFromColor(this.stripsColor, true, this.stripsSize), new PointF[] { new PointF(0.0f, 0.0f), new PointF(this.stripsSize, 0.0f), new PointF(this.stripsSize, this.stripsSize) });
            this.backBufferGraphics.FillPolygon(this.BrushFromColor(this.stripsColor, false, this.stripsSize), new PointF[] { new PointF(0.0f, 0.0f), new PointF(this.stripsSize, this.stripsSize), new PointF(0.0f, this.stripsSize) });

            // Rulers
            this.backBufferGraphics.DrawImage(this.horizontalStrip, this.stripsSize, 0);
            this.backBufferGraphics.DrawImage(this.verticalStrip, 0, this.stripsSize);
        }

        public void RenderMain()
        {
            this.FillBitmap(this.mainScreen, this.mainGraphics, this.backgroundColor, true);

            for (int x = 0; x < this.mainScreenWidth; x += 50)
                this.mainGraphics.DrawLine(new Pen(this.gridColor) { DashPattern = new float[] { 9, 19, 3, 19 } }, x, -4, x, this.mainScreenHeight);

            for (int y = 0; y < this.mainScreenHeight; y += 50)
                this.mainGraphics.DrawLine(new Pen(this.gridColor) { DashPattern = new float[] { 9, 19, 3, 19 } }, -4, y, this.mainScreenWidth, y);
        }

        public void PrensetMain()
        {
            this.backBufferGraphics.DrawImage(this.mainScreen, this.stripsSize, this.stripsSize);
        }

        public void Centralize()
        {
        }

        public void Zoom(float zoomFactor, PointF pivot)
        {
        }

        public void SetMouseCoords(int x, int y)
        {
            this.mouse.X = (x - this.stripsSize);
            this.mouse.Y = (y - this.stripsSize);
            this.pointer.X = this.mouse.X / this.zoom - this.offset.X;
            this.pointer.Y = this.mouse.Y / this.zoom - this.offset.Y;
        }

        private Brush BrushFromColor(Color color, bool vertical, int size, float shade = 0.1f)
        {
            shade = 1.0f - shade;

            if (vertical)
                return new LinearGradientBrush(new Point(0, 0), new Point(0, size), color, Color.FromArgb((int)(color.R * shade), (int)(color.G * shade), (int)(color.B * shade)));
            else
                return new LinearGradientBrush(new Point(0, 0), new Point(size, 0), color, Color.FromArgb((int)(color.R * shade), (int)(color.G * shade), (int)(color.B * shade)));
        }

        private void FillBitmap(Bitmap b, Graphics g, Color c, bool vertical)
        {
            g.FillRectangle(this.BrushFromColor(c, vertical, vertical ? b.Height : b.Width), new Rectangle(0, 0, b.Width, b.Height));
        }
    }
}
