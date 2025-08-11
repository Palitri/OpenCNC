namespace OpenCNC.App
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            pRender = new Panel();
            pbRender = new PictureBox();
            mainMenu = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            centralizeViewToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            deviceToolStripMenuItem = new ToolStripMenuItem();
            plotToolStripMenuItem = new ToolStripMenuItem();
            returnToOriginToolStripMenuItem = new ToolStripMenuItem();
            manualConrolsToolStripMenuItem = new ToolStripMenuItem();
            testToolToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripSeparator();
            consoleToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripSeparator();
            infoToolStripMenuItem = new ToolStripMenuItem();
            pMenu = new Panel();
            button1 = new Button();
            btnSetOrigin = new Button();
            splitter3 = new Splitter();
            btnToolTest = new Button();
            pbZoomIcon = new PictureBox();
            btnManualControls = new Button();
            btnPlot = new Button();
            cbZoom = new ComboBox();
            splitter1 = new Splitter();
            btnSettings = new Button();
            btnCenterView = new Button();
            btnOpen = new Button();
            timerRender = new System.Windows.Forms.Timer(components);
            pBotton = new Panel();
            lblPlottingTimeEstimation = new Label();
            label2 = new Label();
            lPort = new Label();
            lStatus = new Label();
            pbStateLed = new PictureBox();
            lPointer = new Label();
            lOffset = new Label();
            lOrigin = new Label();
            label1 = new Label();
            toolTip = new ToolTip(components);
            pRender.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbRender).BeginInit();
            mainMenu.SuspendLayout();
            pMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbZoomIcon).BeginInit();
            pBotton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbStateLed).BeginInit();
            SuspendLayout();
            // 
            // pRender
            // 
            pRender.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pRender.Controls.Add(pbRender);
            pRender.Location = new Point(0, 128);
            pRender.Margin = new Padding(6, 8, 6, 8);
            pRender.Name = "pRender";
            pRender.Size = new Size(1896, 1371);
            pRender.TabIndex = 1;
            // 
            // pbRender
            // 
            pbRender.Cursor = Cursors.Cross;
            pbRender.Dock = DockStyle.Fill;
            pbRender.Location = new Point(0, 0);
            pbRender.Margin = new Padding(6, 8, 6, 8);
            pbRender.Name = "pbRender";
            pbRender.Size = new Size(1896, 1371);
            pbRender.TabIndex = 0;
            pbRender.TabStop = false;
            pbRender.SizeChanged += pbRender_SizeChanged;
            pbRender.MouseDown += pbRender_MouseDown;
            pbRender.MouseMove += pbSVG_MouseMove;
            pbRender.MouseUp += pbRender_MouseUp;
            // 
            // mainMenu
            // 
            mainMenu.ImageScalingSize = new Size(32, 32);
            mainMenu.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, deviceToolStripMenuItem });
            mainMenu.Location = new Point(0, 0);
            mainMenu.Name = "mainMenu";
            mainMenu.Padding = new Padding(6, 3, 0, 3);
            mainMenu.Size = new Size(1896, 44);
            mainMenu.TabIndex = 2;
            mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, centralizeViewToolStripMenuItem, toolStripMenuItem1, settingsToolStripMenuItem, toolStripMenuItem2, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(71, 38);
            fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Image = Resources.icon_btn_open;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(308, 44);
            openToolStripMenuItem.Text = "&Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // centralizeViewToolStripMenuItem
            // 
            centralizeViewToolStripMenuItem.Image = Resources.icon_btn_center;
            centralizeViewToolStripMenuItem.Name = "centralizeViewToolStripMenuItem";
            centralizeViewToolStripMenuItem.Size = new Size(308, 44);
            centralizeViewToolStripMenuItem.Text = "&Centralize view";
            centralizeViewToolStripMenuItem.Click += centralizeViewToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(305, 6);
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Image = Resources.icon_btn_settings;
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(308, 44);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(305, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Image = Resources.icon_btn_exit;
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(308, 44);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // deviceToolStripMenuItem
            // 
            deviceToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { plotToolStripMenuItem, returnToOriginToolStripMenuItem, manualConrolsToolStripMenuItem, testToolToolStripMenuItem, toolStripMenuItem3, consoleToolStripMenuItem, toolStripMenuItem4, infoToolStripMenuItem });
            deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
            deviceToolStripMenuItem.Size = new Size(106, 38);
            deviceToolStripMenuItem.Text = "&Device";
            // 
            // plotToolStripMenuItem
            // 
            plotToolStripMenuItem.Image = Resources.icon_btn_plot;
            plotToolStripMenuItem.Name = "plotToolStripMenuItem";
            plotToolStripMenuItem.Size = new Size(359, 44);
            plotToolStripMenuItem.Text = "&Plot";
            plotToolStripMenuItem.Click += plotToolStripMenuItem_Click;
            // 
            // returnToOriginToolStripMenuItem
            // 
            returnToOriginToolStripMenuItem.Image = Resources.icon_btn_back_to_origin;
            returnToOriginToolStripMenuItem.Name = "returnToOriginToolStripMenuItem";
            returnToOriginToolStripMenuItem.Size = new Size(359, 44);
            returnToOriginToolStripMenuItem.Text = "Return to &Origin";
            returnToOriginToolStripMenuItem.Click += returnToOriginToolStripMenuItem_Click;
            // 
            // manualConrolsToolStripMenuItem
            // 
            manualConrolsToolStripMenuItem.Image = Resources.icon_btn_axes;
            manualConrolsToolStripMenuItem.Name = "manualConrolsToolStripMenuItem";
            manualConrolsToolStripMenuItem.Size = new Size(359, 44);
            manualConrolsToolStripMenuItem.Text = "&Manual Conrols";
            manualConrolsToolStripMenuItem.Click += manualConrolsToolStripMenuItem_Click;
            // 
            // testToolToolStripMenuItem
            // 
            testToolToolStripMenuItem.Image = Resources.icon_btn_lamp_off;
            testToolToolStripMenuItem.Name = "testToolToolStripMenuItem";
            testToolToolStripMenuItem.Size = new Size(359, 44);
            testToolToolStripMenuItem.Text = "&Test Tool mode";
            testToolToolStripMenuItem.Click += testToolToolStripMenuItem_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(356, 6);
            // 
            // consoleToolStripMenuItem
            // 
            consoleToolStripMenuItem.Image = Resources.icon_btn_console;
            consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
            consoleToolStripMenuItem.Size = new Size(359, 44);
            consoleToolStripMenuItem.Text = "&Console";
            consoleToolStripMenuItem.Click += consoleToolStripMenuItem_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(356, 6);
            // 
            // infoToolStripMenuItem
            // 
            infoToolStripMenuItem.Image = Resources.icon_btn_sysinfo;
            infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            infoToolStripMenuItem.Size = new Size(359, 44);
            infoToolStripMenuItem.Text = "&Info";
            infoToolStripMenuItem.Click += infoToolStripMenuItem_Click;
            // 
            // pMenu
            // 
            pMenu.Controls.Add(button1);
            pMenu.Controls.Add(btnSetOrigin);
            pMenu.Controls.Add(splitter3);
            pMenu.Controls.Add(btnToolTest);
            pMenu.Controls.Add(pbZoomIcon);
            pMenu.Controls.Add(btnManualControls);
            pMenu.Controls.Add(btnPlot);
            pMenu.Controls.Add(cbZoom);
            pMenu.Controls.Add(splitter1);
            pMenu.Controls.Add(btnSettings);
            pMenu.Controls.Add(btnCenterView);
            pMenu.Controls.Add(btnOpen);
            pMenu.Dock = DockStyle.Top;
            pMenu.Location = new Point(0, 44);
            pMenu.Margin = new Padding(6, 8, 6, 8);
            pMenu.Name = "pMenu";
            pMenu.Size = new Size(1896, 72);
            pMenu.TabIndex = 3;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Left;
            button1.Location = new Point(590, 0);
            button1.Margin = new Padding(6, 8, 6, 8);
            button1.Name = "button1";
            button1.Size = new Size(78, 72);
            button1.TabIndex = 24;
            toolTip.SetToolTip(button1, "Press the button and either left-click on the viewport to set an origin point or right-click to cancel.\r\nTo disable the origin point, click the button again.\r\n");
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btnSetOrigin
            // 
            btnSetOrigin.Dock = DockStyle.Left;
            btnSetOrigin.Image = Resources.icon_btn_origin;
            btnSetOrigin.Location = new Point(512, 0);
            btnSetOrigin.Margin = new Padding(6, 8, 6, 8);
            btnSetOrigin.Name = "btnSetOrigin";
            btnSetOrigin.Size = new Size(78, 72);
            btnSetOrigin.TabIndex = 23;
            toolTip.SetToolTip(btnSetOrigin, "Press the button and either left-click on the viewport to set an origin point or right-click to cancel.\r\nTo disable the origin point, click the button again.\r\n");
            btnSetOrigin.UseVisualStyleBackColor = true;
            btnSetOrigin.Click += btnSetOrigin_Click;
            // 
            // splitter3
            // 
            splitter3.Location = new Point(490, 0);
            splitter3.Margin = new Padding(6, 8, 6, 8);
            splitter3.Name = "splitter3";
            splitter3.Size = new Size(22, 72);
            splitter3.TabIndex = 22;
            splitter3.TabStop = false;
            // 
            // btnToolTest
            // 
            btnToolTest.Dock = DockStyle.Left;
            btnToolTest.Image = Resources.icon_btn_lamp_off;
            btnToolTest.Location = new Point(412, 0);
            btnToolTest.Margin = new Padding(6, 8, 6, 8);
            btnToolTest.Name = "btnToolTest";
            btnToolTest.Size = new Size(78, 72);
            btnToolTest.TabIndex = 21;
            toolTip.SetToolTip(btnToolTest, "Enable or disable tool test mode.\r\nIn tool test mode, the tool is powered typically at a minimal value, which is configurable in settings.");
            btnToolTest.UseVisualStyleBackColor = true;
            btnToolTest.Click += btnToolTest_Click;
            // 
            // pbZoomIcon
            // 
            pbZoomIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pbZoomIcon.Image = Resources.icon_btn_zoom;
            pbZoomIcon.ImageLocation = "";
            pbZoomIcon.Location = new Point(1638, 16);
            pbZoomIcon.Margin = new Padding(6, 8, 6, 8);
            pbZoomIcon.Name = "pbZoomIcon";
            pbZoomIcon.Size = new Size(35, 35);
            pbZoomIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            pbZoomIcon.TabIndex = 19;
            pbZoomIcon.TabStop = false;
            pbZoomIcon.Click += pbZoomIcon_Click;
            // 
            // btnManualControls
            // 
            btnManualControls.Dock = DockStyle.Left;
            btnManualControls.Image = Resources.icon_btn_axes;
            btnManualControls.Location = new Point(334, 0);
            btnManualControls.Margin = new Padding(6, 8, 6, 8);
            btnManualControls.Name = "btnManualControls";
            btnManualControls.Size = new Size(78, 72);
            btnManualControls.TabIndex = 4;
            toolTip.SetToolTip(btnManualControls, "Open the manual axes controls");
            btnManualControls.UseVisualStyleBackColor = true;
            btnManualControls.Click += btnManualControls_Click;
            // 
            // btnPlot
            // 
            btnPlot.Dock = DockStyle.Left;
            btnPlot.Image = Resources.icon_btn_plot;
            btnPlot.Location = new Point(256, 0);
            btnPlot.Margin = new Padding(6, 8, 6, 8);
            btnPlot.Name = "btnPlot";
            btnPlot.Size = new Size(78, 72);
            btnPlot.TabIndex = 3;
            toolTip.SetToolTip(btnPlot, "Start plotting sequence");
            btnPlot.UseVisualStyleBackColor = true;
            btnPlot.Click += btnPlot_Click;
            // 
            // cbZoom
            // 
            cbZoom.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbZoom.FlatStyle = FlatStyle.Flat;
            cbZoom.FormattingEnabled = true;
            cbZoom.Items.AddRange(new object[] { "10%", "20%", "50%", "100%", "200%", "500%", "1000%" });
            cbZoom.Location = new Point(1686, 13);
            cbZoom.Margin = new Padding(6, 8, 6, 8);
            cbZoom.Name = "cbZoom";
            cbZoom.Size = new Size(180, 40);
            cbZoom.TabIndex = 18;
            cbZoom.SelectedIndexChanged += cbZoom_SelectedIndexChanged;
            cbZoom.KeyPress += cbZoom_KeyPress;
            // 
            // splitter1
            // 
            splitter1.Location = new Point(234, 0);
            splitter1.Margin = new Padding(6, 8, 6, 8);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(22, 72);
            splitter1.TabIndex = 4;
            splitter1.TabStop = false;
            // 
            // btnSettings
            // 
            btnSettings.Dock = DockStyle.Left;
            btnSettings.Image = Resources.icon_btn_settings;
            btnSettings.Location = new Point(156, 0);
            btnSettings.Margin = new Padding(6, 8, 6, 8);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(78, 72);
            btnSettings.TabIndex = 2;
            toolTip.SetToolTip(btnSettings, "Settings");
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnCenterView
            // 
            btnCenterView.Dock = DockStyle.Left;
            btnCenterView.Image = Resources.icon_btn_center;
            btnCenterView.Location = new Point(78, 0);
            btnCenterView.Margin = new Padding(6, 8, 6, 8);
            btnCenterView.Name = "btnCenterView";
            btnCenterView.Size = new Size(78, 72);
            btnCenterView.TabIndex = 1;
            toolTip.SetToolTip(btnCenterView, "Center the viewport on the current graphic");
            btnCenterView.UseVisualStyleBackColor = true;
            btnCenterView.Click += btnCenterView_Click;
            // 
            // btnOpen
            // 
            btnOpen.Dock = DockStyle.Left;
            btnOpen.Image = Resources.icon_btn_open;
            btnOpen.Location = new Point(0, 0);
            btnOpen.Margin = new Padding(6, 8, 6, 8);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(78, 72);
            btnOpen.TabIndex = 0;
            toolTip.SetToolTip(btnOpen, "Open a graphic");
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // timerRender
            // 
            timerRender.Interval = 1;
            timerRender.Tick += timerRender_Tick;
            // 
            // pBotton
            // 
            pBotton.Controls.Add(lblPlottingTimeEstimation);
            pBotton.Controls.Add(label2);
            pBotton.Controls.Add(lPort);
            pBotton.Controls.Add(lStatus);
            pBotton.Controls.Add(pbStateLed);
            pBotton.Controls.Add(lPointer);
            pBotton.Controls.Add(lOffset);
            pBotton.Controls.Add(lOrigin);
            pBotton.Controls.Add(label1);
            pBotton.Dock = DockStyle.Bottom;
            pBotton.Location = new Point(0, 1499);
            pBotton.Margin = new Padding(6, 8, 6, 8);
            pBotton.Name = "pBotton";
            pBotton.Size = new Size(1896, 61);
            pBotton.TabIndex = 4;
            // 
            // lblPlottingTimeEstimation
            // 
            lblPlottingTimeEstimation.AutoSize = true;
            lblPlottingTimeEstimation.Font = new Font("Segoe UI", 9F);
            lblPlottingTimeEstimation.Location = new Point(784, 10);
            lblPlottingTimeEstimation.Margin = new Padding(6, 0, 6, 0);
            lblPlottingTimeEstimation.Name = "lblPlottingTimeEstimation";
            lblPlottingTimeEstimation.Size = new Size(24, 32);
            lblPlottingTimeEstimation.TabIndex = 22;
            lblPlottingTimeEstimation.Text = "-";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F);
            label2.Location = new Point(652, 10);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(110, 32);
            label2.TabIndex = 21;
            label2.Text = "Time est.";
            // 
            // lPort
            // 
            lPort.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lPort.AutoSize = true;
            lPort.Location = new Point(1512, 13);
            lPort.Margin = new Padding(6, 0, 6, 0);
            lPort.Name = "lPort";
            lPort.Size = new Size(24, 32);
            lPort.TabIndex = 20;
            lPort.Text = "-";
            // 
            // lStatus
            // 
            lStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lStatus.AutoSize = true;
            lStatus.Location = new Point(1729, 13);
            lStatus.Margin = new Padding(6, 0, 6, 0);
            lStatus.Name = "lStatus";
            lStatus.Size = new Size(24, 32);
            lStatus.TabIndex = 19;
            lStatus.Text = "-";
            // 
            // pbStateLed
            // 
            pbStateLed.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pbStateLed.Image = Resources.led_blue_soft;
            pbStateLed.Location = new Point(1681, 16);
            pbStateLed.Margin = new Padding(6, 8, 6, 8);
            pbStateLed.Name = "pbStateLed";
            pbStateLed.Size = new Size(30, 30);
            pbStateLed.SizeMode = PictureBoxSizeMode.StretchImage;
            pbStateLed.TabIndex = 18;
            pbStateLed.TabStop = false;
            // 
            // lPointer
            // 
            lPointer.AutoSize = true;
            lPointer.Font = new Font("Segoe UI", 9F);
            lPointer.Location = new Point(420, 10);
            lPointer.Margin = new Padding(6, 0, 6, 0);
            lPointer.Name = "lPointer";
            lPointer.Size = new Size(52, 32);
            lPointer.TabIndex = 16;
            lPointer.Text = "0, 0";
            // 
            // lOffset
            // 
            lOffset.AutoSize = true;
            lOffset.Font = new Font("Segoe UI", 9F);
            lOffset.Location = new Point(321, 10);
            lOffset.Margin = new Padding(6, 0, 6, 0);
            lOffset.Name = "lOffset";
            lOffset.Size = new Size(89, 32);
            lOffset.TabIndex = 15;
            lOffset.Text = "Pointer";
            // 
            // lOrigin
            // 
            lOrigin.AutoSize = true;
            lOrigin.Font = new Font("Segoe UI", 9F);
            lOrigin.Location = new Point(139, 10);
            lOrigin.Margin = new Padding(6, 0, 6, 0);
            lOrigin.Name = "lOrigin";
            lOrigin.Size = new Size(52, 32);
            lOrigin.TabIndex = 14;
            lOrigin.Text = "0, 0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F);
            label1.Location = new Point(52, 10);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(80, 32);
            label1.TabIndex = 13;
            label1.Text = "Origin";
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1896, 1560);
            Controls.Add(pBotton);
            Controls.Add(pMenu);
            Controls.Add(pRender);
            Controls.Add(mainMenu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = mainMenu;
            Margin = new Padding(6, 8, 6, 8);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "OpenIoT CNC";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            DragDrop += MainForm_DragDrop;
            DragEnter += MainForm_DragEnter;
            pRender.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbRender).EndInit();
            mainMenu.ResumeLayout(false);
            mainMenu.PerformLayout();
            pMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbZoomIcon).EndInit();
            pBotton.ResumeLayout(false);
            pBotton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbStateLed).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbRender;
        private System.Windows.Forms.Panel pRender;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.Panel pMenu;
        private System.Windows.Forms.Timer timerRender;
        private System.Windows.Forms.Panel pBotton;
        private System.Windows.Forms.PictureBox pbStateLed;
        private System.Windows.Forms.Label lPointer;
        private System.Windows.Forms.Label lOffset;
        private System.Windows.Forms.Label lOrigin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.ToolStripMenuItem plotToolStripMenuItem;
        private System.Windows.Forms.Label lPort;
        private System.Windows.Forms.ToolStripMenuItem manualConrolsToolStripMenuItem;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnManualControls;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnPlot;
        private System.Windows.Forms.ComboBox cbZoom;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripMenuItem centralizeViewToolStripMenuItem;
        private System.Windows.Forms.Button btnCenterView;
        private System.Windows.Forms.PictureBox pbZoomIcon;
        private System.Windows.Forms.ToolStripMenuItem consoleToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPlottingTimeEstimation;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.Button btnToolTest;
        private System.Windows.Forms.ToolStripMenuItem testToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Button btnSetOrigin;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem returnToOriginToolStripMenuItem;
        private System.Windows.Forms.Button button1;
    }
}
