using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCNC.App.Components
{
    public class RoundButton : Button
    {
        public RoundButton()
            : base()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;

            this.MouseEnter += RoundButton_MouseEnter;
            this.MouseLeave += RoundButton_MouseLeave;
        }

        void RoundButton_MouseEnter(object sender, EventArgs e)
        {
            if (this.ImageList != null)
                if (this.ImageList.Images.Count > 1)
                    this.ImageIndex = 1;
        }

        void RoundButton_MouseLeave(object sender, EventArgs e)
        {
            if (this.ImageList != null)
                this.ImageIndex = 0;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(grPath);
            base.OnPaint(e);
        }
    }
}
