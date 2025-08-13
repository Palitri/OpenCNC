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
            comboArbitraryAxis = new ComboBox();
            thumbwheelArbitrary = new Palitri.OpenCNC.App.Components.Thumbwheel();
            thumbwheelZ = new Palitri.OpenCNC.App.Components.Thumbwheel();
            thumbwheelY = new Palitri.OpenCNC.App.Components.Thumbwheel();
            thumbwheelX = new Palitri.OpenCNC.App.Components.Thumbwheel();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)thumbwheelArbitrary).BeginInit();
            ((System.ComponentModel.ISupportInitialize)thumbwheelZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)thumbwheelY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)thumbwheelX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(comboArbitraryAxis);
            panel1.Controls.Add(thumbwheelArbitrary);
            panel1.Controls.Add(thumbwheelZ);
            panel1.Controls.Add(thumbwheelY);
            panel1.Controls.Add(thumbwheelX);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(6, 0);
            panel1.Margin = new Padding(6, 7, 6, 7);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 800);
            panel1.TabIndex = 7;
            // 
            // comboArbitraryAxis
            // 
            comboArbitraryAxis.DropDownStyle = ComboBoxStyle.DropDownList;
            comboArbitraryAxis.Font = new Font("Segoe UI", 19.875F, FontStyle.Regular, GraphicsUnit.Point, 204);
            comboArbitraryAxis.FormattingEnabled = true;
            comboArbitraryAxis.Location = new Point(423, 31);
            comboArbitraryAxis.Name = "comboArbitraryAxis";
            comboArbitraryAxis.Size = new Size(100, 79);
            comboArbitraryAxis.TabIndex = 18;
            comboArbitraryAxis.TextChanged += comboArbitraryAxis_TextChanged;
            // 
            // thumbwheelArbitrary
            // 
            thumbwheelArbitrary.BackgroundImage = Resources.btn_manual_any;
            thumbwheelArbitrary.BackgroundImageLayout = ImageLayout.Center;
            thumbwheelArbitrary.Location = new Point(529, 12);
            thumbwheelArbitrary.Name = "thumbwheelArbitrary";
            thumbwheelArbitrary.Size = new Size(263, 113);
            thumbwheelArbitrary.TabIndex = 17;
            thumbwheelArbitrary.TabStop = false;
            thumbwheelArbitrary.Tag = "0";
            thumbwheelArbitrary.Value = 0;
            thumbwheelArbitrary.ValueVector = new Point(400, 0);
            thumbwheelArbitrary.ValueChanged += thumbwheelAxis_ValueChanged;
            thumbwheelArbitrary.MouseDown += thumbwheelAxis_MouseDown;
            thumbwheelArbitrary.MouseUp += thumbwheelAxis_MouseUp;
            // 
            // thumbwheelZ
            // 
            thumbwheelZ.BackgroundImage = Resources.btn_manual_z;
            thumbwheelZ.BackgroundImageLayout = ImageLayout.Center;
            thumbwheelZ.Location = new Point(290, 169);
            thumbwheelZ.Name = "thumbwheelZ";
            thumbwheelZ.Size = new Size(117, 261);
            thumbwheelZ.TabIndex = 16;
            thumbwheelZ.TabStop = false;
            thumbwheelZ.Tag = "2";
            thumbwheelZ.Value = 0;
            thumbwheelZ.ValueVector = new Point(0, -400);
            thumbwheelZ.ValueChanged += thumbwheelAxis_ValueChanged;
            thumbwheelZ.MouseDown += thumbwheelAxis_MouseDown;
            thumbwheelZ.MouseUp += thumbwheelAxis_MouseUp;
            // 
            // thumbwheelY
            // 
            thumbwheelY.BackgroundImage = Resources.btn_manual_y;
            thumbwheelY.BackgroundImageLayout = ImageLayout.Center;
            thumbwheelY.Location = new Point(23, 374);
            thumbwheelY.Name = "thumbwheelY";
            thumbwheelY.Size = new Size(116, 262);
            thumbwheelY.TabIndex = 15;
            thumbwheelY.TabStop = false;
            thumbwheelY.Tag = "1";
            thumbwheelY.Value = 0;
            thumbwheelY.ValueVector = new Point(0, -400);
            thumbwheelY.ValueChanged += thumbwheelAxis_ValueChanged;
            thumbwheelY.MouseDown += thumbwheelAxis_MouseDown;
            thumbwheelY.MouseUp += thumbwheelAxis_MouseUp;
            // 
            // thumbwheelX
            // 
            thumbwheelX.BackgroundImage = Resources.btn_manual_x;
            thumbwheelX.BackgroundImageLayout = ImageLayout.Center;
            thumbwheelX.Location = new Point(352, 572);
            thumbwheelX.Name = "thumbwheelX";
            thumbwheelX.Size = new Size(263, 113);
            thumbwheelX.TabIndex = 13;
            thumbwheelX.TabStop = false;
            thumbwheelX.Tag = "0";
            thumbwheelX.Value = 0;
            thumbwheelX.ValueVector = new Point(400, 0);
            thumbwheelX.ValueChanged += thumbwheelAxis_ValueChanged;
            thumbwheelX.MouseDown += thumbwheelAxis_MouseDown;
            thumbwheelX.MouseUp += thumbwheelAxis_MouseUp;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Resources.axes3d;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(1, 0);
            pictureBox1.Margin = new Padding(6, 7, 6, 7);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(800, 800);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // ManualControlsForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(810, 805);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6, 7, 6, 7);
            Name = "ManualControlsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Manual Controls";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)thumbwheelArbitrary).EndInit();
            ((System.ComponentModel.ISupportInitialize)thumbwheelZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)thumbwheelY).EndInit();
            ((System.ComponentModel.ISupportInitialize)thumbwheelX).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
    }
}