namespace CNCPlotter
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.roundButton6 = new CNCPlotter.Components.RoundButton();
            this.roundButton5 = new CNCPlotter.Components.RoundButton();
            this.roundButton4 = new CNCPlotter.Components.RoundButton();
            this.roundButton3 = new CNCPlotter.Components.RoundButton();
            this.roundButton2 = new CNCPlotter.Components.RoundButton();
            this.roundButton1 = new CNCPlotter.Components.RoundButton();
            this.btnAxisZPos = new CNCPlotter.Components.RoundButton();
            this.btnAxisXNeg = new CNCPlotter.Components.RoundButton();
            this.btnAxisZNeg = new CNCPlotter.Components.RoundButton();
            this.btnAxisXPos = new CNCPlotter.Components.RoundButton();
            this.btnAxisYPos = new CNCPlotter.Components.RoundButton();
            this.btnAxisYNeg = new CNCPlotter.Components.RoundButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.roundButton6);
            this.panel1.Controls.Add(this.roundButton5);
            this.panel1.Controls.Add(this.roundButton4);
            this.panel1.Controls.Add(this.roundButton3);
            this.panel1.Controls.Add(this.roundButton2);
            this.panel1.Controls.Add(this.roundButton1);
            this.panel1.Controls.Add(this.btnAxisZPos);
            this.panel1.Controls.Add(this.btnAxisXNeg);
            this.panel1.Controls.Add(this.btnAxisZNeg);
            this.panel1.Controls.Add(this.btnAxisXPos);
            this.panel1.Controls.Add(this.btnAxisYPos);
            this.panel1.Controls.Add(this.btnAxisYNeg);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(378, 352);
            this.panel1.TabIndex = 7;
            // 
            // roundButton6
            // 
            this.roundButton6.FlatAppearance.BorderSize = 0;
            this.roundButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundButton6.Image = global::CNCPlotter.Properties.Resources.btn_axis_x_pos_small;
            this.roundButton6.Location = new System.Drawing.Point(265, 316);
            this.roundButton6.Name = "roundButton6";
            this.roundButton6.Size = new System.Drawing.Size(24, 24);
            this.roundButton6.TabIndex = 12;
            this.roundButton6.Tag = "3";
            this.roundButton6.UseVisualStyleBackColor = true;
            this.roundButton6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseDown);
            this.roundButton6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseUp);
            // 
            // roundButton5
            // 
            this.roundButton5.FlatAppearance.BorderSize = 0;
            this.roundButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundButton5.Image = global::CNCPlotter.Properties.Resources.btn_axis_x_neg_small;
            this.roundButton5.Location = new System.Drawing.Point(197, 316);
            this.roundButton5.Name = "roundButton5";
            this.roundButton5.Size = new System.Drawing.Size(24, 24);
            this.roundButton5.TabIndex = 11;
            this.roundButton5.Tag = "2";
            this.roundButton5.UseVisualStyleBackColor = true;
            this.roundButton5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseDown);
            this.roundButton5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseUp);
            // 
            // roundButton4
            // 
            this.roundButton4.FlatAppearance.BorderSize = 0;
            this.roundButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundButton4.Image = global::CNCPlotter.Properties.Resources.btn_axis_y_neg_small;
            this.roundButton4.Location = new System.Drawing.Point(223, 146);
            this.roundButton4.Name = "roundButton4";
            this.roundButton4.Size = new System.Drawing.Size(24, 24);
            this.roundButton4.TabIndex = 10;
            this.roundButton4.Tag = "6";
            this.roundButton4.UseVisualStyleBackColor = true;
            this.roundButton4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseDown);
            this.roundButton4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseUp);
            // 
            // roundButton3
            // 
            this.roundButton3.FlatAppearance.BorderSize = 0;
            this.roundButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundButton3.Image = global::CNCPlotter.Properties.Resources.btn_axis_y_pos_small;
            this.roundButton3.Location = new System.Drawing.Point(223, 84);
            this.roundButton3.Name = "roundButton3";
            this.roundButton3.Size = new System.Drawing.Size(24, 24);
            this.roundButton3.TabIndex = 7;
            this.roundButton3.Tag = "7";
            this.roundButton3.UseVisualStyleBackColor = true;
            this.roundButton3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseDown);
            this.roundButton3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseUp);
            // 
            // roundButton2
            // 
            this.roundButton2.FlatAppearance.BorderSize = 0;
            this.roundButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundButton2.Image = global::CNCPlotter.Properties.Resources.btn_axis_z_pos_small;
            this.roundButton2.Location = new System.Drawing.Point(-2, 211);
            this.roundButton2.Name = "roundButton2";
            this.roundButton2.Size = new System.Drawing.Size(24, 24);
            this.roundButton2.TabIndex = 8;
            this.roundButton2.Tag = "11";
            this.roundButton2.UseVisualStyleBackColor = true;
            this.roundButton2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseDown);
            this.roundButton2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseUp);
            // 
            // roundButton1
            // 
            this.roundButton1.FlatAppearance.BorderSize = 0;
            this.roundButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundButton1.Image = global::CNCPlotter.Properties.Resources.btn_axis_z_neg_small;
            this.roundButton1.Location = new System.Drawing.Point(41, 166);
            this.roundButton1.Name = "roundButton1";
            this.roundButton1.Size = new System.Drawing.Size(24, 24);
            this.roundButton1.TabIndex = 7;
            this.roundButton1.Tag = "10";
            this.roundButton1.UseVisualStyleBackColor = true;
            this.roundButton1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseDown);
            this.roundButton1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseUp);
            // 
            // btnAxisZPos
            // 
            this.btnAxisZPos.FlatAppearance.BorderSize = 0;
            this.btnAxisZPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAxisZPos.Image = global::CNCPlotter.Properties.Resources.btn_axis_z_pos;
            this.btnAxisZPos.Location = new System.Drawing.Point(13, 228);
            this.btnAxisZPos.Name = "btnAxisZPos";
            this.btnAxisZPos.Size = new System.Drawing.Size(60, 60);
            this.btnAxisZPos.TabIndex = 6;
            this.btnAxisZPos.Tag = "9";
            this.btnAxisZPos.UseVisualStyleBackColor = true;
            this.btnAxisZPos.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseDown);
            this.btnAxisZPos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseUp);
            // 
            // btnAxisXNeg
            // 
            this.btnAxisXNeg.FlatAppearance.BorderSize = 0;
            this.btnAxisXNeg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAxisXNeg.Image = global::CNCPlotter.Properties.Resources.btn_axis_x_neg;
            this.btnAxisXNeg.Location = new System.Drawing.Point(181, 250);
            this.btnAxisXNeg.Name = "btnAxisXNeg";
            this.btnAxisXNeg.Size = new System.Drawing.Size(60, 60);
            this.btnAxisXNeg.TabIndex = 1;
            this.btnAxisXNeg.Tag = "0";
            this.btnAxisXNeg.UseVisualStyleBackColor = true;
            this.btnAxisXNeg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseDown);
            this.btnAxisXNeg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseUp);
            // 
            // btnAxisZNeg
            // 
            this.btnAxisZNeg.FlatAppearance.BorderSize = 0;
            this.btnAxisZNeg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAxisZNeg.Image = global::CNCPlotter.Properties.Resources.btn_axis_z_neg;
            this.btnAxisZNeg.Location = new System.Drawing.Point(57, 184);
            this.btnAxisZNeg.Name = "btnAxisZNeg";
            this.btnAxisZNeg.Size = new System.Drawing.Size(60, 60);
            this.btnAxisZNeg.TabIndex = 5;
            this.btnAxisZNeg.Tag = "8";
            this.btnAxisZNeg.UseVisualStyleBackColor = true;
            this.btnAxisZNeg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseDown);
            this.btnAxisZNeg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseUp);
            // 
            // btnAxisXPos
            // 
            this.btnAxisXPos.FlatAppearance.BorderSize = 0;
            this.btnAxisXPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAxisXPos.Image = global::CNCPlotter.Properties.Resources.btn_axis_x_pos;
            this.btnAxisXPos.Location = new System.Drawing.Point(246, 250);
            this.btnAxisXPos.Name = "btnAxisXPos";
            this.btnAxisXPos.Size = new System.Drawing.Size(60, 60);
            this.btnAxisXPos.TabIndex = 2;
            this.btnAxisXPos.Tag = "1";
            this.btnAxisXPos.UseVisualStyleBackColor = true;
            this.btnAxisXPos.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseDown);
            this.btnAxisXPos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseUp);
            // 
            // btnAxisYPos
            // 
            this.btnAxisYPos.FlatAppearance.BorderSize = 0;
            this.btnAxisYPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAxisYPos.Image = global::CNCPlotter.Properties.Resources.btn_axis_y_pos;
            this.btnAxisYPos.Location = new System.Drawing.Point(157, 66);
            this.btnAxisYPos.Name = "btnAxisYPos";
            this.btnAxisYPos.Size = new System.Drawing.Size(60, 60);
            this.btnAxisYPos.TabIndex = 5;
            this.btnAxisYPos.Tag = "5";
            this.btnAxisYPos.UseVisualStyleBackColor = true;
            this.btnAxisYPos.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseDown);
            this.btnAxisYPos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseUp);
            // 
            // btnAxisYNeg
            // 
            this.btnAxisYNeg.FlatAppearance.BorderSize = 0;
            this.btnAxisYNeg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAxisYNeg.Image = global::CNCPlotter.Properties.Resources.btn_axis_y_neg;
            this.btnAxisYNeg.Location = new System.Drawing.Point(157, 128);
            this.btnAxisYNeg.Name = "btnAxisYNeg";
            this.btnAxisYNeg.Size = new System.Drawing.Size(60, 60);
            this.btnAxisYNeg.TabIndex = 3;
            this.btnAxisYNeg.Tag = "4";
            this.btnAxisYNeg.UseVisualStyleBackColor = true;
            this.btnAxisYNeg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseDown);
            this.btnAxisYNeg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAxis_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CNCPlotter.Properties.Resources.axes3d;
            this.pictureBox1.Location = new System.Drawing.Point(34, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(340, 343);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ManualControlsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 352);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManualControlsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manual Controls";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Components.RoundButton btnAxisZPos;
        private Components.RoundButton btnAxisXNeg;
        private Components.RoundButton btnAxisZNeg;
        private Components.RoundButton btnAxisXPos;
        private Components.RoundButton btnAxisYPos;
        private Components.RoundButton btnAxisYNeg;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Components.RoundButton roundButton1;
        private Components.RoundButton roundButton6;
        private Components.RoundButton roundButton5;
        private Components.RoundButton roundButton4;
        private Components.RoundButton roundButton3;
        private Components.RoundButton roundButton2;



    }
}