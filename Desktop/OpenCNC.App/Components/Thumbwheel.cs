using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palitri.OpenCNC.App.Components
{
    [ToolboxItem(true)]
    public class Thumbwheel : PictureBox
    {
        //public enum Orientation
        //{
        //    Horizontal,
        //    Vertical
        //}

        public event EventHandler ValueChanged;

        private bool _isMouseDown;
        private Point _mouseDownPos;
        private Point mouseVector;

        public Point ValueVector { get; set; }

        private int _value;
        public int Value
        {
            get => _value;
            set
            {
                int newVal = value;
                if (_value != newVal)
                {
                    _value = newVal;
                    Invalidate();
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public Thumbwheel()
            : base()
        {
            this.MouseDown += Thumbwheel_MouseDown;
            this.MouseUp += Thumbwheel_MouseUp;
            this.MouseMove += Thumbwheel_MouseMove;

            this._isMouseDown = false;
        }

        private void Thumbwheel_MouseMove(object? sender, MouseEventArgs e)
        {
            if (!this._isMouseDown)
                return;

            int step = 25;

            this.mouseVector.X += Cursor.Position.X - this._mouseDownPos.X;
            this.mouseVector.Y += Cursor.Position.Y - this._mouseDownPos.Y;
            //this.mouseVector.X = Cursor.Position.X - this._mouseDownPos.X;
            //this.mouseVector.Y = Cursor.Position.Y - this._mouseDownPos.Y;

            PointF delta = new PointF(this.mouseVector.X, this.mouseVector.Y);

            delta.X = delta.X * (this.ValueVector.X == 0 ? 0 : 1.0f / (float)this.ValueVector.X);
            delta.X = delta.X * Math.Abs(this.ValueVector.X);

            delta.Y = delta.Y * (this.ValueVector.Y == 0 ? 0 : 1.0f / (float)this.ValueVector.Y);
            delta.Y = delta.Y * Math.Abs(this.ValueVector.Y);

            this.Value = (((int)(delta.X + delta.Y)) / step) * step;

            Cursor.Position = this._mouseDownPos;
        }

        private void Thumbwheel_MouseUp(object? sender, MouseEventArgs e)
        {
            this._isMouseDown = false;
            this.Value = 0;

            Cursor.Position = this._mouseDownPos;
            Cursor.Show();
        }

        private void Thumbwheel_MouseDown(object? sender, MouseEventArgs e)
        {
            Cursor.Hide();

            this.mouseVector.X = 0;
            this.mouseVector.Y = 0;
            this._isMouseDown = true;
            this._mouseDownPos = Cursor.Position;
        }
    }
}
