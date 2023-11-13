namespace CNCPlotter
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pRender = new System.Windows.Forms.Panel();
            this.pbRender = new System.Windows.Forms.PictureBox();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centralizeViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.returnToOriginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualConrolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.consoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pMenu = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSetOrigin = new System.Windows.Forms.Button();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.btnToolTest = new System.Windows.Forms.Button();
            this.pbZoomIcon = new System.Windows.Forms.PictureBox();
            this.btnManualControls = new System.Windows.Forms.Button();
            this.btnPlot = new System.Windows.Forms.Button();
            this.cbZoom = new System.Windows.Forms.ComboBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnCenterView = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.timerRender = new System.Windows.Forms.Timer(this.components);
            this.pBotton = new System.Windows.Forms.Panel();
            this.lblPlottingTimeEstimation = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lPort = new System.Windows.Forms.Label();
            this.lStatus = new System.Windows.Forms.Label();
            this.pbStateLed = new System.Windows.Forms.PictureBox();
            this.lPointer = new System.Windows.Forms.Label();
            this.lOffset = new System.Windows.Forms.Label();
            this.lOrigin = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pRender.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRender)).BeginInit();
            this.mainMenu.SuspendLayout();
            this.pMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbZoomIcon)).BeginInit();
            this.pBotton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStateLed)).BeginInit();
            this.SuspendLayout();
            // 
            // pRender
            // 
            this.pRender.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pRender.Controls.Add(this.pbRender);
            this.pRender.Location = new System.Drawing.Point(0, 52);
            this.pRender.Name = "pRender";
            this.pRender.Size = new System.Drawing.Size(875, 557);
            this.pRender.TabIndex = 1;
            // 
            // pbRender
            // 
            this.pbRender.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pbRender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbRender.Location = new System.Drawing.Point(0, 0);
            this.pbRender.Name = "pbRender";
            this.pbRender.Size = new System.Drawing.Size(875, 557);
            this.pbRender.TabIndex = 0;
            this.pbRender.TabStop = false;
            this.pbRender.SizeChanged += new System.EventHandler(this.pbRender_SizeChanged);
            this.pbRender.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbRender_MouseDown);
            this.pbRender.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbSVG_MouseMove);
            this.pbRender.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbRender_MouseUp);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.deviceToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(875, 24);
            this.mainMenu.TabIndex = 2;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.centralizeViewToolStripMenuItem,
            this.toolStripMenuItem1,
            this.settingsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::CNCPlotter.Properties.Resources.icon_btn_open;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // centralizeViewToolStripMenuItem
            // 
            this.centralizeViewToolStripMenuItem.Image = global::CNCPlotter.Properties.Resources.icon_btn_center;
            this.centralizeViewToolStripMenuItem.Name = "centralizeViewToolStripMenuItem";
            this.centralizeViewToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.centralizeViewToolStripMenuItem.Text = "&Centralize view";
            this.centralizeViewToolStripMenuItem.Click += new System.EventHandler(this.centralizeViewToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(150, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = global::CNCPlotter.Properties.Resources.icon_btn_settings;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(150, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::CNCPlotter.Properties.Resources.icon_btn_exit;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // deviceToolStripMenuItem
            // 
            this.deviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plotToolStripMenuItem,
            this.returnToOriginToolStripMenuItem,
            this.manualConrolsToolStripMenuItem,
            this.testToolToolStripMenuItem,
            this.toolStripMenuItem3,
            this.consoleToolStripMenuItem,
            this.toolStripMenuItem4,
            this.infoToolStripMenuItem});
            this.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
            this.deviceToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.deviceToolStripMenuItem.Text = "&Device";
            // 
            // plotToolStripMenuItem
            // 
            this.plotToolStripMenuItem.Image = global::CNCPlotter.Properties.Resources.icon_btn_plot;
            this.plotToolStripMenuItem.Name = "plotToolStripMenuItem";
            this.plotToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.plotToolStripMenuItem.Text = "&Plot";
            this.plotToolStripMenuItem.Click += new System.EventHandler(this.plotToolStripMenuItem_Click);
            // 
            // returnToOriginToolStripMenuItem
            // 
            this.returnToOriginToolStripMenuItem.Image = global::CNCPlotter.Properties.Resources.icon_btn_back_to_origin;
            this.returnToOriginToolStripMenuItem.Name = "returnToOriginToolStripMenuItem";
            this.returnToOriginToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.returnToOriginToolStripMenuItem.Text = "Return to &Origin";
            this.returnToOriginToolStripMenuItem.Click += new System.EventHandler(this.returnToOriginToolStripMenuItem_Click);
            // 
            // manualConrolsToolStripMenuItem
            // 
            this.manualConrolsToolStripMenuItem.Image = global::CNCPlotter.Properties.Resources.icon_btn_axes;
            this.manualConrolsToolStripMenuItem.Name = "manualConrolsToolStripMenuItem";
            this.manualConrolsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.manualConrolsToolStripMenuItem.Text = "&Manual Conrols";
            this.manualConrolsToolStripMenuItem.Click += new System.EventHandler(this.manualConrolsToolStripMenuItem_Click);
            // 
            // testToolToolStripMenuItem
            // 
            this.testToolToolStripMenuItem.Image = global::CNCPlotter.Properties.Resources.icon_btn_lamp_off;
            this.testToolToolStripMenuItem.Name = "testToolToolStripMenuItem";
            this.testToolToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.testToolToolStripMenuItem.Text = "&Test Tool mode";
            this.testToolToolStripMenuItem.Click += new System.EventHandler(this.testToolToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(156, 6);
            // 
            // consoleToolStripMenuItem
            // 
            this.consoleToolStripMenuItem.Image = global::CNCPlotter.Properties.Resources.icon_btn_console;
            this.consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
            this.consoleToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.consoleToolStripMenuItem.Text = "&Console";
            this.consoleToolStripMenuItem.Click += new System.EventHandler(this.consoleToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(156, 6);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Image = global::CNCPlotter.Properties.Resources.icon_btn_sysinfo;
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.infoToolStripMenuItem.Text = "&Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // pMenu
            // 
            this.pMenu.Controls.Add(this.button1);
            this.pMenu.Controls.Add(this.btnSetOrigin);
            this.pMenu.Controls.Add(this.splitter3);
            this.pMenu.Controls.Add(this.btnToolTest);
            this.pMenu.Controls.Add(this.pbZoomIcon);
            this.pMenu.Controls.Add(this.btnManualControls);
            this.pMenu.Controls.Add(this.btnPlot);
            this.pMenu.Controls.Add(this.cbZoom);
            this.pMenu.Controls.Add(this.splitter1);
            this.pMenu.Controls.Add(this.btnSettings);
            this.pMenu.Controls.Add(this.btnCenterView);
            this.pMenu.Controls.Add(this.btnOpen);
            this.pMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMenu.Location = new System.Drawing.Point(0, 24);
            this.pMenu.Name = "pMenu";
            this.pMenu.Size = new System.Drawing.Size(875, 29);
            this.pMenu.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.Location = new System.Drawing.Point(272, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 29);
            this.button1.TabIndex = 24;
            this.toolTip.SetToolTip(this.button1, "Press the button and either left-click on the viewport to set an origin point or " +
        "right-click to cancel.\r\nTo disable the origin point, click the button again.\r\n");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSetOrigin
            // 
            this.btnSetOrigin.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSetOrigin.Image = global::CNCPlotter.Properties.Resources.icon_btn_origin;
            this.btnSetOrigin.Location = new System.Drawing.Point(236, 0);
            this.btnSetOrigin.Name = "btnSetOrigin";
            this.btnSetOrigin.Size = new System.Drawing.Size(36, 29);
            this.btnSetOrigin.TabIndex = 23;
            this.toolTip.SetToolTip(this.btnSetOrigin, "Press the button and either left-click on the viewport to set an origin point or " +
        "right-click to cancel.\r\nTo disable the origin point, click the button again.\r\n");
            this.btnSetOrigin.UseVisualStyleBackColor = true;
            this.btnSetOrigin.Click += new System.EventHandler(this.btnSetOrigin_Click);
            // 
            // splitter3
            // 
            this.splitter3.Location = new System.Drawing.Point(226, 0);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(10, 29);
            this.splitter3.TabIndex = 22;
            this.splitter3.TabStop = false;
            // 
            // btnToolTest
            // 
            this.btnToolTest.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnToolTest.Image = global::CNCPlotter.Properties.Resources.icon_btn_lamp_off;
            this.btnToolTest.Location = new System.Drawing.Point(190, 0);
            this.btnToolTest.Name = "btnToolTest";
            this.btnToolTest.Size = new System.Drawing.Size(36, 29);
            this.btnToolTest.TabIndex = 21;
            this.toolTip.SetToolTip(this.btnToolTest, "Enable or disable tool test mode.\r\nIn tool test mode, the tool is powered typical" +
        "ly at a minimal value, which is configurable in settings.");
            this.btnToolTest.UseVisualStyleBackColor = true;
            this.btnToolTest.Click += new System.EventHandler(this.btnToolTest_Click);
            // 
            // pbZoomIcon
            // 
            this.pbZoomIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbZoomIcon.Image = global::CNCPlotter.Properties.Resources.icon_btn_zoom;
            this.pbZoomIcon.ImageLocation = "";
            this.pbZoomIcon.Location = new System.Drawing.Point(756, 8);
            this.pbZoomIcon.Name = "pbZoomIcon";
            this.pbZoomIcon.Size = new System.Drawing.Size(16, 16);
            this.pbZoomIcon.TabIndex = 19;
            this.pbZoomIcon.TabStop = false;
            this.pbZoomIcon.Click += new System.EventHandler(this.pbZoomIcon_Click);
            // 
            // btnManualControls
            // 
            this.btnManualControls.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnManualControls.Image = global::CNCPlotter.Properties.Resources.icon_btn_axes;
            this.btnManualControls.Location = new System.Drawing.Point(154, 0);
            this.btnManualControls.Name = "btnManualControls";
            this.btnManualControls.Size = new System.Drawing.Size(36, 29);
            this.btnManualControls.TabIndex = 4;
            this.toolTip.SetToolTip(this.btnManualControls, "Open the manual axes controls");
            this.btnManualControls.UseVisualStyleBackColor = true;
            this.btnManualControls.Click += new System.EventHandler(this.btnManualControls_Click);
            // 
            // btnPlot
            // 
            this.btnPlot.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPlot.Image = global::CNCPlotter.Properties.Resources.icon_btn_plot;
            this.btnPlot.Location = new System.Drawing.Point(118, 0);
            this.btnPlot.Name = "btnPlot";
            this.btnPlot.Size = new System.Drawing.Size(36, 29);
            this.btnPlot.TabIndex = 3;
            this.toolTip.SetToolTip(this.btnPlot, "Start plotting sequence");
            this.btnPlot.UseVisualStyleBackColor = true;
            this.btnPlot.Click += new System.EventHandler(this.btnPlot_Click);
            // 
            // cbZoom
            // 
            this.cbZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbZoom.FormattingEnabled = true;
            this.cbZoom.Items.AddRange(new object[] {
            "10%",
            "20%",
            "50%",
            "100%",
            "200%",
            "500%",
            "1000%"});
            this.cbZoom.Location = new System.Drawing.Point(778, 5);
            this.cbZoom.Name = "cbZoom";
            this.cbZoom.Size = new System.Drawing.Size(85, 21);
            this.cbZoom.TabIndex = 18;
            this.cbZoom.SelectedIndexChanged += new System.EventHandler(this.cbZoom_SelectedIndexChanged);
            this.cbZoom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbZoom_KeyPress);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(108, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 29);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // btnSettings
            // 
            this.btnSettings.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSettings.Image = global::CNCPlotter.Properties.Resources.icon_btn_settings;
            this.btnSettings.Location = new System.Drawing.Point(72, 0);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(36, 29);
            this.btnSettings.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnSettings, "Settings");
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnCenterView
            // 
            this.btnCenterView.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCenterView.Image = global::CNCPlotter.Properties.Resources.icon_btn_center;
            this.btnCenterView.Location = new System.Drawing.Point(36, 0);
            this.btnCenterView.Name = "btnCenterView";
            this.btnCenterView.Size = new System.Drawing.Size(36, 29);
            this.btnCenterView.TabIndex = 1;
            this.toolTip.SetToolTip(this.btnCenterView, "Center the viewport on the current graphic");
            this.btnCenterView.UseVisualStyleBackColor = true;
            this.btnCenterView.Click += new System.EventHandler(this.btnCenterView_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOpen.Image = global::CNCPlotter.Properties.Resources.icon_btn_open;
            this.btnOpen.Location = new System.Drawing.Point(0, 0);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(36, 29);
            this.btnOpen.TabIndex = 0;
            this.toolTip.SetToolTip(this.btnOpen, "Open a graphic");
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // timerRender
            // 
            this.timerRender.Interval = 1;
            this.timerRender.Tick += new System.EventHandler(this.timerRender_Tick);
            // 
            // pBotton
            // 
            this.pBotton.Controls.Add(this.lblPlottingTimeEstimation);
            this.pBotton.Controls.Add(this.label2);
            this.pBotton.Controls.Add(this.lPort);
            this.pBotton.Controls.Add(this.lStatus);
            this.pBotton.Controls.Add(this.pbStateLed);
            this.pBotton.Controls.Add(this.lPointer);
            this.pBotton.Controls.Add(this.lOffset);
            this.pBotton.Controls.Add(this.lOrigin);
            this.pBotton.Controls.Add(this.label1);
            this.pBotton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBotton.Location = new System.Drawing.Point(0, 609);
            this.pBotton.Name = "pBotton";
            this.pBotton.Size = new System.Drawing.Size(875, 25);
            this.pBotton.TabIndex = 4;
            // 
            // lblPlottingTimeEstimation
            // 
            this.lblPlottingTimeEstimation.AutoSize = true;
            this.lblPlottingTimeEstimation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPlottingTimeEstimation.Location = new System.Drawing.Point(362, 4);
            this.lblPlottingTimeEstimation.Name = "lblPlottingTimeEstimation";
            this.lblPlottingTimeEstimation.Size = new System.Drawing.Size(12, 15);
            this.lblPlottingTimeEstimation.TabIndex = 22;
            this.lblPlottingTimeEstimation.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.Location = new System.Drawing.Point(301, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 21;
            this.label2.Text = "Time est.";
            // 
            // lPort
            // 
            this.lPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lPort.AutoSize = true;
            this.lPort.Location = new System.Drawing.Point(698, 5);
            this.lPort.Name = "lPort";
            this.lPort.Size = new System.Drawing.Size(10, 13);
            this.lPort.TabIndex = 20;
            this.lPort.Text = "-";
            // 
            // lStatus
            // 
            this.lStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lStatus.AutoSize = true;
            this.lStatus.Location = new System.Drawing.Point(798, 5);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(10, 13);
            this.lStatus.TabIndex = 19;
            this.lStatus.Text = "-";
            // 
            // pbStateLed
            // 
            this.pbStateLed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbStateLed.Image = global::CNCPlotter.Properties.Resources.led_blue_soft;
            this.pbStateLed.Location = new System.Drawing.Point(776, 4);
            this.pbStateLed.Name = "pbStateLed";
            this.pbStateLed.Size = new System.Drawing.Size(16, 16);
            this.pbStateLed.TabIndex = 18;
            this.pbStateLed.TabStop = false;
            // 
            // lPointer
            // 
            this.lPointer.AutoSize = true;
            this.lPointer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lPointer.Location = new System.Drawing.Point(194, 4);
            this.lPointer.Name = "lPointer";
            this.lPointer.Size = new System.Drawing.Size(25, 15);
            this.lPointer.TabIndex = 16;
            this.lPointer.Text = "0, 0";
            // 
            // lOffset
            // 
            this.lOffset.AutoSize = true;
            this.lOffset.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lOffset.Location = new System.Drawing.Point(148, 4);
            this.lOffset.Name = "lOffset";
            this.lOffset.Size = new System.Drawing.Size(45, 15);
            this.lOffset.TabIndex = 15;
            this.lOffset.Text = "Pointer";
            // 
            // lOrigin
            // 
            this.lOrigin.AutoSize = true;
            this.lOrigin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lOrigin.Location = new System.Drawing.Point(64, 4);
            this.lOrigin.Name = "lOrigin";
            this.lOrigin.Size = new System.Drawing.Size(25, 15);
            this.lOrigin.TabIndex = 14;
            this.lOrigin.Text = "0, 0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(24, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "Origin";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 634);
            this.Controls.Add(this.pBotton);
            this.Controls.Add(this.pMenu);
            this.Controls.Add(this.pRender);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CNC Plotter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.pRender.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbRender)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.pMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbZoomIcon)).EndInit();
            this.pBotton.ResumeLayout(false);
            this.pBotton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStateLed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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

