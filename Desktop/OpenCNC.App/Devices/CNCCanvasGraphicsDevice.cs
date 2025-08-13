using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Palitri.Graphics;
using Palitri.Graphics.Devices;

namespace Palitri.OpenCNC.App.Devices
{
    public class CNCCanvasGraphicsDevice : CanvasGraphicsDevice
    {
        private Pen defaultPen, undrawnPen, drawnPen, drawingPen;
        private Pen ActivePen { get { return this.isHighlighted ? this.renderPrimitiveIndex < this.highlightPrimitiveIndex ? this.drawnPen : this.renderPrimitiveIndex == this.highlightPrimitiveIndex ? this.drawingPen : this.undrawnPen : this.defaultPen; } }

        private int renderPrimitiveIndex, highlightPrimitiveIndex;
        private bool isHighlighted;

        public CNCCanvasGraphicsDevice(System.Drawing.Graphics canvasGraphics)
            : base(canvasGraphics, new Pen(Color.Blue))
        {
            this.defaultPen = new Pen(Color.Blue);
            this.undrawnPen = new Pen(Color.LightBlue);
            this.drawnPen = new Pen(Color.Green);
            this.drawingPen = new Pen(Color.Red, 2.0f);

            this.renderPrimitiveIndex = 0;
            this.highlightPrimitiveIndex = -1;
            this.isHighlighted = false;
        }

        public void SetHighlight(int highlightPrimitiveIndex = -1)
        {
            this.isHighlighted = highlightPrimitiveIndex >= 0;
            this.highlightPrimitiveIndex = highlightPrimitiveIndex;
        }

        public override void Begin()
        {
            this.renderPrimitiveIndex = 0;
        }

        public override void Polyline(Vector2[] vertices)
        {
            this.renderPrimitiveIndex++;
            this.canvasPen = this.ActivePen;

            base.Polyline(vertices);
        }

        public override void Arc(Vector2 origin, Vector2 semiMajorAxis, Vector2 semiMinorAxis, float startAngle, float endAngle)
        {
            this.renderPrimitiveIndex++;
            this.canvasPen = this.ActivePen;

            base.Arc(origin, semiMajorAxis, semiMinorAxis, startAngle, endAngle);
        }

        public override void Bezier(Vector2[] vectors)
        {
            this.renderPrimitiveIndex++;
            this.canvasPen = this.ActivePen;

            base.Bezier(vectors);
        }
    }
}
