namespace Palitri.OpenCNC.App
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.tpDeviceSettings = new System.Windows.Forms.TabPage();
            this.tpDisplay = new System.Windows.Forms.TabPage();
            this.tpAppSettings = new System.Windows.Forms.TabPage();
            this.pgAppSettings = new System.Windows.Forms.PropertyGrid();
            this.cbPreset = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tcSettings = new System.Windows.Forms.TabControl();
            this.tpAppSettings.SuspendLayout();
            this.tcSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpDeviceSettings
            // 
            this.tpDeviceSettings.Location = new System.Drawing.Point(4, 22);
            this.tpDeviceSettings.Name = "tpDeviceSettings";
            this.tpDeviceSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpDeviceSettings.Size = new System.Drawing.Size(394, 506);
            this.tpDeviceSettings.TabIndex = 1;
            this.tpDeviceSettings.Text = "Device";
            this.tpDeviceSettings.UseVisualStyleBackColor = true;
            // 
            // tpDisplay
            // 
            this.tpDisplay.Location = new System.Drawing.Point(4, 22);
            this.tpDisplay.Name = "tpDisplay";
            this.tpDisplay.Padding = new System.Windows.Forms.Padding(3);
            this.tpDisplay.Size = new System.Drawing.Size(394, 506);
            this.tpDisplay.TabIndex = 2;
            this.tpDisplay.Text = "Display";
            this.tpDisplay.UseVisualStyleBackColor = true;
            // 
            // tpAppSettings
            // 
            this.tpAppSettings.Controls.Add(this.pgAppSettings);
            this.tpAppSettings.Controls.Add(this.cbPreset);
            this.tpAppSettings.Controls.Add(this.label1);
            this.tpAppSettings.Location = new System.Drawing.Point(4, 22);
            this.tpAppSettings.Name = "tpAppSettings";
            this.tpAppSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpAppSettings.Size = new System.Drawing.Size(394, 529);
            this.tpAppSettings.TabIndex = 0;
            this.tpAppSettings.Text = "Application";
            this.tpAppSettings.UseVisualStyleBackColor = true;
            // 
            // pgAppSettings
            // 
            this.pgAppSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgAppSettings.Location = new System.Drawing.Point(8, 38);
            this.pgAppSettings.Name = "pgAppSettings";
            this.pgAppSettings.Size = new System.Drawing.Size(378, 484);
            this.pgAppSettings.TabIndex = 2;
            // 
            // cbPreset
            // 
            this.cbPreset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPreset.FormattingEnabled = true;
            this.cbPreset.Location = new System.Drawing.Point(76, 11);
            this.cbPreset.Name = "cbPreset";
            this.cbPreset.Size = new System.Drawing.Size(310, 21);
            this.cbPreset.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Preset";
            // 
            // tcSettings
            // 
            this.tcSettings.Controls.Add(this.tpAppSettings);
            this.tcSettings.Controls.Add(this.tpDisplay);
            this.tcSettings.Controls.Add(this.tpDeviceSettings);
            this.tcSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSettings.Location = new System.Drawing.Point(0, 0);
            this.tcSettings.Multiline = true;
            this.tcSettings.Name = "tcSettings";
            this.tcSettings.SelectedIndex = 0;
            this.tcSettings.Size = new System.Drawing.Size(402, 555);
            this.tcSettings.TabIndex = 0;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 555);
            this.Controls.Add(this.tcSettings);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.tpAppSettings.ResumeLayout(false);
            this.tpAppSettings.PerformLayout();
            this.tcSettings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tpDeviceSettings;
        private System.Windows.Forms.TabPage tpDisplay;
        private System.Windows.Forms.TabPage tpAppSettings;
        private System.Windows.Forms.PropertyGrid pgAppSettings;
        private System.Windows.Forms.ComboBox cbPreset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tcSettings;

    }
}