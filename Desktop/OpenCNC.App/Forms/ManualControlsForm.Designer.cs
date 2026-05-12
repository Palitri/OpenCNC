namespace Palitri.OpenCNC.App
{
    partial class ManualControlsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManualControlsForm));
            panel1 = new Panel();
            thumbwheelXY = new Palitri.OpenCNC.App.Components.Thumbwheel();
            comboArbitraryAxis = new ComboBox();
            thumbwheelArbitrary = new Palitri.OpenCNC.App.Components.Thumbwheel();
            thumbwheelZ = new Palitri.OpenCNC.App.Components.Thumbwheel();
            thumbwheelY = new Palitri.OpenCNC.App.Components.Thumbwheel();
            thumbwheelX = new Palitri.OpenCNC.App.Components.Thumbwheel();
            pictureBox1 = new PictureBox();
            thumbwheelYZ = new Palitri.OpenCNC.App.Components.Thumbwheel();
            thumbwheelXZ = new Palitri.OpenCNC.App.Components.Thumbwheel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)thumbwheelXY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)thumbwheelArbitrary).BeginInit();
            ((System.ComponentModel.ISupportInitialize)thumbwheelZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)thumbwheelY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)thumbwheelX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)thumbwheelYZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)thumbwheelXZ).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(thumbwheelXZ);
            panel1.Controls.Add(thumbwheelYZ);
            panel1.Controls.Add(thumbwheelXY);
            panel1.Controls.Add(comboArbitraryAxis);
            panel1.Controls.Add(thumbwheelArbitrary);
            panel1.Controls.Add(thumbwheelZ);
            panel1.Controls.Add(thumbwheelY);
            panel1.Controls.Add(thumbwheelX);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(6, 0);
            panel1.Margin = new Padding(6);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 800);
            panel1.TabIndex = 7;
            // 
            // thumbwheelXY
            // 
            thumbwheelXY.BackgroundImage = Resources.icon_manual_xy;
            thumbwheelXY.BackgroundImageLayout = ImageLayout.Zoom;
            thumbwheelXY.Location = new Point(225, 588);
            thumbwheelXY.Margin = new Padding(4, 2, 4, 2);
            thumbwheelXY.Name = "thumbwheelXY";
            thumbwheelXY.Size = new Size(168, 65);
            thumbwheelXY.TabIndex = 19;
            thumbwheelXY.TabStop = false;
            thumbwheelXY.Tag = "0,1";
            thumbwheelXY.Value = 0;
            thumbwheelXY.ValueComponents = (PointF)resources.GetObject("thumbwheelXY.ValueComponents");
            thumbwheelXY.ValueVector = new Point(-400, -400);
            thumbwheelXY.ValueChanged += thumbwheelAxis_ValueChanged;
            thumbwheelXY.MouseDown += thumbwheelAxis_MouseDown;
            thumbwheelXY.MouseUp += thumbwheelAxis_MouseUp;
            // 
            // comboArbitraryAxis
            // 
            comboArbitraryAxis.DropDownStyle = ComboBoxStyle.DropDownList;
            comboArbitraryAxis.Font = new Font("Segoe UI", 19.875F, FontStyle.Regular, GraphicsUnit.Point, 204);
            comboArbitraryAxis.FormattingEnabled = true;
            comboArbitraryAxis.Location = new Point(690, 130);
            comboArbitraryAxis.Margin = new Padding(4, 2, 4, 2);
            comboArbitraryAxis.Name = "comboArbitraryAxis";
            comboArbitraryAxis.Size = new Size(101, 79);
            comboArbitraryAxis.TabIndex = 18;
            comboArbitraryAxis.TextChanged += comboArbitraryAxis_TextChanged;
            // 
            // thumbwheelArbitrary
            // 
            thumbwheelArbitrary.BackgroundImage = Resources.btn_manual_any;
            thumbwheelArbitrary.BackgroundImageLayout = ImageLayout.Zoom;
            thumbwheelArbitrary.Location = new Point(529, 13);
            thumbwheelArbitrary.Margin = new Padding(4, 2, 4, 2);
            thumbwheelArbitrary.Name = "thumbwheelArbitrary";
            thumbwheelArbitrary.Size = new Size(264, 113);
            thumbwheelArbitrary.TabIndex = 17;
            thumbwheelArbitrary.TabStop = false;
            thumbwheelArbitrary.Tag = "0";
            thumbwheelArbitrary.Value = 0;
            thumbwheelArbitrary.ValueComponents = (PointF)resources.GetObject("thumbwheelArbitrary.ValueComponents");
            thumbwheelArbitrary.ValueVector = new Point(400, 0);
            thumbwheelArbitrary.ValueChanged += thumbwheelAxis_ValueChanged;
            thumbwheelArbitrary.MouseDown += thumbwheelAxis_MouseDown;
            thumbwheelArbitrary.MouseUp += thumbwheelAxis_MouseUp;
            // 
            // thumbwheelZ
            // 
            thumbwheelZ.BackgroundImage = Resources.btn_manual_z;
            thumbwheelZ.BackgroundImageLayout = ImageLayout.Zoom;
            thumbwheelZ.Location = new Point(298, 2);
            thumbwheelZ.Margin = new Padding(4, 2, 4, 2);
            thumbwheelZ.Name = "thumbwheelZ";
            thumbwheelZ.Size = new Size(117, 260);
            thumbwheelZ.TabIndex = 16;
            thumbwheelZ.TabStop = false;
            thumbwheelZ.Tag = "2";
            thumbwheelZ.Value = 0;
            thumbwheelZ.ValueComponents = (PointF)resources.GetObject("thumbwheelZ.ValueComponents");
            thumbwheelZ.ValueVector = new Point(0, -400);
            thumbwheelZ.ValueChanged += thumbwheelAxis_ValueChanged;
            thumbwheelZ.MouseDown += thumbwheelAxis_MouseDown;
            thumbwheelZ.MouseUp += thumbwheelAxis_MouseUp;
            // 
            // thumbwheelY
            // 
            thumbwheelY.BackgroundImage = Resources.btn_manual_y;
            thumbwheelY.BackgroundImageLayout = ImageLayout.Zoom;
            thumbwheelY.Location = new Point(22, 373);
            thumbwheelY.Margin = new Padding(4, 2, 4, 2);
            thumbwheelY.Name = "thumbwheelY";
            thumbwheelY.Size = new Size(115, 262);
            thumbwheelY.TabIndex = 15;
            thumbwheelY.TabStop = false;
            thumbwheelY.Tag = "1";
            thumbwheelY.Value = 0;
            thumbwheelY.ValueComponents = (PointF)resources.GetObject("thumbwheelY.ValueComponents");
            thumbwheelY.ValueVector = new Point(0, -400);
            thumbwheelY.ValueChanged += thumbwheelAxis_ValueChanged;
            thumbwheelY.MouseDown += thumbwheelAxis_MouseDown;
            thumbwheelY.MouseUp += thumbwheelAxis_MouseUp;
            // 
            // thumbwheelX
            // 
            thumbwheelX.BackgroundImage = Resources.btn_manual_x;
            thumbwheelX.BackgroundImageLayout = ImageLayout.Zoom;
            thumbwheelX.Location = new Point(529, 576);
            thumbwheelX.Margin = new Padding(4, 2, 4, 2);
            thumbwheelX.Name = "thumbwheelX";
            thumbwheelX.Size = new Size(264, 113);
            thumbwheelX.TabIndex = 13;
            thumbwheelX.TabStop = false;
            thumbwheelX.Tag = "0";
            thumbwheelX.Value = 0;
            thumbwheelX.ValueComponents = (PointF)resources.GetObject("thumbwheelX.ValueComponents");
            thumbwheelX.ValueVector = new Point(400, 0);
            thumbwheelX.ValueChanged += thumbwheelAxis_ValueChanged;
            thumbwheelX.MouseDown += thumbwheelAxis_MouseDown;
            thumbwheelX.MouseUp += thumbwheelAxis_MouseUp;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Resources.axes3d;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(2, 0);
            pictureBox1.Margin = new Padding(6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(800, 800);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // thumbwheelYZ
            // 
            thumbwheelYZ.BackgroundImage = Resources.icon_manual_yz;
            thumbwheelYZ.BackgroundImageLayout = ImageLayout.Zoom;
            thumbwheelYZ.Location = new Point(156, 408);
            thumbwheelYZ.Margin = new Padding(4, 2, 4, 2);
            thumbwheelYZ.Name = "thumbwheelYZ";
            thumbwheelYZ.Size = new Size(69, 159);
            thumbwheelYZ.TabIndex = 20;
            thumbwheelYZ.TabStop = false;
            thumbwheelYZ.Tag = "1,2";
            thumbwheelYZ.Value = 0;
            thumbwheelYZ.ValueComponents = (PointF)resources.GetObject("thumbwheelYZ.ValueComponents");
            thumbwheelYZ.ValueVector = new Point(-400, -400);
            thumbwheelYZ.ValueChanged += thumbwheelAxis_ValueChanged;
            thumbwheelYZ.MouseDown += thumbwheelAxis_MouseDown;
            thumbwheelYZ.MouseUp += thumbwheelAxis_MouseUp;
            // 
            // thumbwheelXZ
            // 
            thumbwheelXZ.BackgroundImage = Resources.icon_manual_xz;
            thumbwheelXZ.BackgroundImageLayout = ImageLayout.Zoom;
            thumbwheelXZ.Location = new Point(294, 408);
            thumbwheelXZ.Margin = new Padding(4, 2, 4, 2);
            thumbwheelXZ.Name = "thumbwheelXZ";
            thumbwheelXZ.Size = new Size(99, 97);
            thumbwheelXZ.TabIndex = 21;
            thumbwheelXZ.TabStop = false;
            thumbwheelXZ.Tag = "0,2";
            thumbwheelXZ.Value = 0;
            thumbwheelXZ.ValueComponents = (PointF)resources.GetObject("thumbwheelXZ.ValueComponents");
            thumbwheelXZ.ValueVector = new Point(-400, -400);
            thumbwheelXZ.ValueChanged += thumbwheelAxis_ValueChanged;
            thumbwheelXZ.MouseDown += thumbwheelAxis_MouseDown;
            thumbwheelXZ.MouseUp += thumbwheelAxis_MouseUp;
            // 
            // ManualControlsForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(810, 804);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6);
            Name = "ManualControlsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Manual Controls";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)thumbwheelXY).EndInit();
            ((System.ComponentModel.ISupportInitialize)thumbwheelArbitrary).EndInit();
            ((System.ComponentModel.ISupportInitialize)thumbwheelZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)thumbwheelY).EndInit();
            ((System.ComponentModel.ISupportInitialize)thumbwheelX).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)thumbwheelYZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)thumbwheelXZ).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Components.Thumbwheel thumbwheelX;
        private Components.Thumbwheel thumbwheelZ;
        private Components.Thumbwheel thumbwheelY;
        private Components.Thumbwheel thumbwheelArbitrary;
        private ComboBox comboArbitraryAxis;
        private Components.Thumbwheel thumbwheelXY;
        private Components.Thumbwheel thumbwheelXZ;
        private Components.Thumbwheel thumbwheelYZ;
    }
}