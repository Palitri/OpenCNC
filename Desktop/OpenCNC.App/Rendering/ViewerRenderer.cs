using OpenCNC.App.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCNC.App.Rendering
{
    public class ViewerRenderer
    {
        public Bitmap backBuffer;
        private Bitmap mainScreen, horizontalStrip, verticalStrip, rulersOrigin;

        public Color backgroundColor, gridColor, rulersColor, textColor, warningTextColor, zeroTextColor, zeroBackColor, coordLineColorBig, coordLineColorMed, coordLineColorSmall, workAreaMarkerColor;

        private Graphics backBufferGraphics, horizontalStripGraphics, verticalStripGraphics, rulersOriginGraphics;
        public Graphics mainGraphics;

        public int width, height;
        public int mainScreenWidth, mainScreenHeight;
        public int rulersSize, workAreaMarkerSize;
        public int[] rulerDivisionFactors = new int[] { 10, 20, 100 };
        public float[] rulerDivisionLines = new float[] { 0.1f, 0.15f, 0.3f };
        public Color[] rulerDivisionColors = new Color[] { Color.FromArgb(170, 170, 170), Color.FromArgb(170, 170, 170), Color.FromArgb(150, 150, 150) };
        public Font[] rulerDivisionFonts = new Font[] { null, new Font("Arial", 6.0f), new Font("Arial", 8.0f) };

        public float zoom;
        public PointF offset;
        public PointF mouse, pointer;

        public float scalingFactor;

        public RectangleF safeArea, workArea;

        public ViewerRenderer(int width, int height)
        {
            this.scalingFactor = WinAPI.GetScalingFactor(0);

            this.backgroundColor = Color.White;
            this.gridColor = Color.FromArgb(220, 220, 220);
            this.rulersColor = Color.FromArgb(230, 230, 230);
            this.textColor = Color.FromArgb(128, 128, 128);
            this.warningTextColor = Color.FromArgb(170, 68, 132);
            this.zeroTextColor = Color.FromArgb(255, 255, 255);
            this.zeroBackColor = Color.FromArgb(119, 179, 181);
            this.coordLineColorBig = Color.FromArgb(150, 150, 150);
            this.coordLineColorSmall = Color.FromArgb(170, 170, 170);
            this.workAreaMarkerColor = Color.FromArgb(168, 168, 200);

            this.rulersSize = (int)(25.0f * this.scalingFactor);
            this.workAreaMarkerSize = (int)(3.0f * this.scalingFactor);
            this.zoom = 1.0f;
            this.offset = new PointF(0.0f, 0.0f);
            this.mouse = new PointF(0.0f, 0.0f);
            this.pointer = new PointF(0.0f, 0.0f);

            this.safeArea = new RectangleF(0.0f, 0.0f, 500.0f, 500.0f);
            this.workArea = new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);

            this.SetSize(width, height);
        }

        public void SetSize(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.mainScreenWidth = this.width - this.rulersSize;
            this.mainScreenHeight = this.height - this.rulersSize;

            this.backBuffer = new Bitmap(this.width, this.height);
            this.horizontalStrip = new Bitmap(this.mainScreenWidth, this.rulersSize);
            this.verticalStrip = new Bitmap(this.rulersSize, this.mainScreenHeight);
            this.mainScreen = new Bitmap(this.mainScreenWidth, this.mainScreenHeight);
            this.rulersOrigin = new Bitmap(this.rulersSize, this.rulersSize);

            this.backBufferGraphics = Graphics.FromImage(this.backBuffer);
            this.mainGraphics = Graphics.FromImage(this.mainScreen);
            this.horizontalStripGraphics = Graphics.FromImage(this.horizontalStrip);
            this.verticalStripGraphics = Graphics.FromImage(this.verticalStrip);
            this.rulersOriginGraphics = Graphics.FromImage(this.rulersOrigin);

            this.PrerenderRulers();
            this.PrerenderMainScreen();
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

        public void PrerenderRulers()
        {
            float divisionsZoomFactor = this.zoom >= 1.0f ? this.GetPowerOf2((int)this.zoom) : 1.0f / this.GetPowerOf2((int)(1.0f / this.zoom));
            float divisionStep = this.rulerDivisionFactors[0] / divisionsZoomFactor;


            // Origin
            
            this.rulersOriginGraphics.FillPolygon(this.BrushFromColor(this.rulersColor, false, true, this.rulersSize, this.rulersSize), new PointF[] { new PointF(0.0f, 0.0f), new PointF(this.rulersSize, 0.0f), new PointF(0.0f, this.rulersSize) });
            this.rulersOriginGraphics.FillPolygon(this.BrushFromColor(this.rulersColor, true, false, this.rulersSize, this.rulersSize), new PointF[] { new PointF(0.0f, this.rulersSize), new PointF(this.rulersSize, 0.0f), new PointF(this.rulersSize, this.rulersSize) });

            
            
            // Horizontal strip

            this.FillBitmap(this.horizontalStrip, this.horizontalStripGraphics, this.rulersColor, true, false);

            if (this.workArea.Width > 0)
                this.horizontalStripGraphics.FillRectangle(new SolidBrush(this.workAreaMarkerColor), new RectangleF((this.offset.X + this.workArea.X) * this.zoom, this.rulersSize - this.workAreaMarkerSize, this.workArea.Width * this.zoom, this.rulersSize));

            float divisionX = -(int)(this.offset.X / divisionStep) * divisionStep;
            float rulerX = 0.0f;
            do
            {
                rulerX = this.GraphicToScreen(divisionX, 0.0f).X;

                for (int d = this.rulerDivisionFactors.Length - 1; d >= 0; d--)
                {
                    if (this.IsMultipleOf(divisionX, this.rulerDivisionFactors[d] / divisionsZoomFactor))
                    {
                        int y = (int)(this.rulersSize * this.rulerDivisionLines[d]);
                        this.horizontalStripGraphics.DrawLine(new Pen(this.rulerDivisionColors[d]), rulerX, 0, rulerX, y);

                        if (this.rulerDivisionFonts[d] != null)
                        {
                            StringFormat sf = new StringFormat();
                            sf.LineAlignment = StringAlignment.Near;
                            sf.Alignment = StringAlignment.Near;
                            float rulerValue = (d == this.rulerDivisionFactors.Length - 1 ? divisionX : (divisionX % 100) / 10);
                            string text = rulerValue.ToString();
                            float textLength = (int)this.horizontalStripGraphics.MeasureString(text, this.rulerDivisionFonts[d]).Width;
                            float x = rulerX - textLength / 2.0f;
                            x = Math.Max(0, x);
                            bool isZero = divisionX == 0;
                            if (isZero)
                                this.horizontalStripGraphics.FillRectangle(new SolidBrush(this.zeroBackColor), x - 1, y, textLength + 2, 14 * this.scalingFactor);
                            this.horizontalStripGraphics.DrawString(text, this.rulerDivisionFonts[d], new SolidBrush(isZero ? this.zeroTextColor : divisionX < this.safeArea.X || divisionX > this.safeArea.Right ? this.warningTextColor : this.textColor), new PointF(x, y), sf);
                        }

                        break;
                    }
                }

                divisionX += divisionStep;
            }
            while (rulerX < this.mainScreenWidth);




            // Vertical strip

            this.FillBitmap(this.verticalStrip, this.verticalStripGraphics, this.rulersColor, false, true);

            if (this.workArea.Height > 0)
            {
                float markerHeight = this.workArea.Height * this.zoom;
                this.verticalStripGraphics.FillRectangle(new SolidBrush(Color.FromArgb(168, 168, 200)), new RectangleF(0, this.mainScreenHeight - (this.offset.Y + this.workArea.Y) * this.zoom - markerHeight, this.workAreaMarkerSize, markerHeight));
            }

            float divisionY = -(int)(this.offset.Y / divisionStep) * divisionStep;
            float rulerY = 0.0f;
            do
            {
                rulerY = this.GraphicToScreen(0.0f, divisionY).Y;

                for (int d = this.rulerDivisionFactors.Length - 1; d >= 0; d--)
                {
                    if (this.IsMultipleOf(divisionY, this.rulerDivisionFactors[d] / divisionsZoomFactor))
                    {
                        int x = (int)(this.rulersSize * (1.0f - this.rulerDivisionLines[d]));
                        this.verticalStripGraphics.DrawLine(new Pen(this.rulerDivisionColors[d]), x, this.mainScreenHeight - rulerY, this.rulersSize, this.mainScreenHeight - rulerY);

                        if (this.rulerDivisionFonts[d] != null)
                        {
                            StringFormat sf = new StringFormat();
                            sf.LineAlignment = StringAlignment.Far;
                            sf.Alignment = StringAlignment.Far;
                            sf.FormatFlags = StringFormatFlags.DirectionVertical;
                            float rulerValue = (d == this.rulerDivisionFactors.Length - 1 ? divisionY : (divisionY % 100) / 10);
                            string text = rulerValue.ToString();
                            float textLength = (int)this.verticalStripGraphics.MeasureString(text, this.rulerDivisionFonts[d]).Width;
                            float y = rulerY - textLength / 2.0f;
                            y = Math.Max(0, y);
                            bool isZero = divisionY == 0;
                            if (isZero)
                                this.verticalStripGraphics.FillRectangle(new SolidBrush(this.zeroBackColor), x - 14 * this.scalingFactor, this.mainScreenHeight - y - textLength - 2 * this.scalingFactor, 14 * this.scalingFactor, textLength + 2 * this.scalingFactor);
                            this.verticalStripGraphics.DrawString(text, this.rulerDivisionFonts[d], new SolidBrush(divisionY == 0 ? this.zeroTextColor : divisionY < this.safeArea.Y || divisionY > this.safeArea.Bottom ? this.warningTextColor : this.textColor), new PointF(x, this.mainScreenHeight - y), sf);
                        }

                        break;
                    }
                }

                divisionY += divisionStep;
            }
            while (rulerY < this.mainScreenHeight);


        }

        public void PrerenderMainScreen()
        {
            int gridSize = (int)(50 * this.scalingFactor);

            this.FillBitmap(this.mainScreen, this.mainGraphics, this.backgroundColor, true, true);

            for (int x = 0; x < this.mainScreenWidth; x += gridSize)
                this.mainGraphics.DrawLine(new Pen(this.gridColor) { DashPattern = new float[] { 9 * this.scalingFactor, 19 * this.scalingFactor, 3 * this.scalingFactor, 19 * this.scalingFactor } }, x, -4 * this.scalingFactor, x, this.mainScreenHeight);

            for (int y = 0; y < this.mainScreenHeight; y += gridSize)
                this.mainGraphics.DrawLine(new Pen(this.gridColor) { DashPattern = new float[] { 9 * this.scalingFactor, 19 * this.scalingFactor, 3 * this.scalingFactor, 19 * this.scalingFactor } }, -4 * this.scalingFactor, y, this.mainScreenWidth, y);
        }


        public void Present(bool rulers = true, bool main = true)
        {
            if (rulers)
            {
                //this.PrerenderRulers();

                this.backBufferGraphics.DrawImage(this.rulersOrigin, 0, this.mainScreenHeight);

                this.backBufferGraphics.DrawImage(this.horizontalStrip, this.rulersSize, this.mainScreen.Height);
                this.backBufferGraphics.DrawImage(this.verticalStrip, 0, 0);

                this.backBufferGraphics.DrawLine(new Pen(this.coordLineColorBig) { DashPattern = new float[] { 2, 2 } }, this.rulersSize + this.mouse.X, this.mainScreenHeight, this.rulersSize + this.mouse.X, this.mainScreenHeight + this.rulersSize);
                this.backBufferGraphics.DrawLine(new Pen(this.coordLineColorBig) { DashPattern = new float[] { 2, 2 } }, 0, this.mainScreenHeight - this.mouse.Y, this.rulersSize, this.mainScreenHeight - this.mouse.Y);
            }

            if (main)
            {
                //this.PrerenderMainScreen();
                this.backBufferGraphics.DrawImage(this.mainScreen, this.rulersSize, 0);
            }
        }

        public void Centralize()
        {
        }

        public void Zoom(float zoomFactor, PointF pivot)
        {
        }

        public void SetMouseCoords(int x, int y)
        {
            this.mouse.X = (x - this.rulersSize);
            this.mouse.Y = this.mainScreenHeight - y;
            this.pointer.X = this.mouse.X / this.zoom - this.offset.X;
            this.pointer.Y = this.mouse.Y / this.zoom - this.offset.Y;
        }

        private Brush BrushFromColor(Color color, bool vertical, bool direction, int width, int height, float shade = 0.1f)
        {
            shade = 1.0f - shade;
            Color cShaded = Color.FromArgb((int)(color.R * shade), (int)(color.G * shade), (int)(color.B * shade));
            Color cStart = direction ? color : cShaded;
            Color cEnd = direction ? cShaded : color;
            Point pStart = new Point(0, 0);
            Point pEnd = vertical ? new Point(0, height) : new Point(width, 0);

            return new LinearGradientBrush(pStart, pEnd, cStart, cEnd);
        }

        private void FillBitmap(Bitmap b, Graphics g, Color c, bool vertical, bool direction)
        {
            g.FillRectangle(this.BrushFromColor(c, vertical, direction, b.Width, b.Height), new Rectangle(0, 0, b.Width, b.Height));
        }
    }
}
