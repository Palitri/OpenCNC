using OpenCNC.App.Devices;
using OpenCNC.App.Rendering;
using OpenCNC.App.Settings;
using Palitri.AudioSynthesis;
using Palitri.Graphics;
using Palitri.Graphics.Devices;
using Palitri.OpenCNC.Driver;
using Palitri.OpenCNC.Driver.Settings;
using Palitri.OpenIoT.Board.Api;
using Palitri.OpenIoT.Board.Protocol;
using Palitri.OpenIoT.Board.Protocol.Events;
using Palitri.OpenIoT.Board.Scanner;
using Palitri.OpenIoT.Board.Transmission.Com;
using Palitri.OpenIoT.Composite;
using Palitri.SVG;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace OpenCNC.App
{
    // TODO:
    // - check laser power pwm
    // - make laser power options
    // - fix svg
    // - console, parameter and hints, suggestions, history
    // - test mode button
    // - make laser mosfet turning on/off, use shift register
    // - add svg "transform" support
    // - svg "use" element support
    // - software rendering device for render time estimation
    // - software rendering device for picking
    // - setting origin
    // multiple objects
    // proper svg viewbox
    // parallel port probing with bigger timeout
    // scaling, moving, rotation
    // optimize slow drawing
    // queue second command
    // real-time drawing
    // pause/resume plotting
    // panic button in app
    // - optimize drawing close items sequentially
    // vertical inversion
    // manual stop and start inertia settings
    // hardware buttons
    // add boundary switches/sensors
    // stl file support https://en.wikipedia.org/wiki/STL_%28file_format%29
    // separate main rendering area from rulers, make one universal class for h/v rulers
    // total code refactor - better names, class separation, 3d entirely (check graphics vector), seamless Y inversion and device/canvas transformations
    // optimize - remove casual creation of collections and objects, avoid conversions, keep buffers with converted values if needed
    // select individual lines/objects and set them with properties (such as speed, power, thickness(z), etc.)
    // own format
    // each primitive/group with own settings/preset
    
    public partial class MainForm : Form
    {
        private OpenIoTBoard board;
        private OpenIoTBoardScanner scanner;

        private ViewerRenderer viewer;

        private CNCCanvasGraphicsDevice canvasRenderer;

        private OpenCNCAppSettings appSettings;
        private OpenIoTBoardSettings boardSettings;


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.appSettings = new OpenCNCAppSettings(settingsFileName);
            this.boardSettings = OpenIoTBoardSettings.LoadSettings(iotBoardSettingsFileName);

            this.board = new OpenIoTBoard();
            this.board.EventHandlers.Add(new MainFormBoardEventHandler(this));

            this.scanner = new OpenIoTBoardScanner();
            this.scanner.OnBoardAvailable += this.BoardScanner_OnBoardAvailable;
            this.scanner.StartScanning();

            this.InvokeUpdateState(CNCState.Unavailable);
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.isClosing = true;

            if (scanner.IsScanning)
                this.scanner.StopScanning(true);

            if (this.cnc != null)
            {
                if ((this.cnc.State != CNCState.None) && (this.cnc.State != CNCState.Unavailable))
                {
                    this.cnc.Begin();
                    this.cnc.SetToolPowerMode(false);
                    this.cnc.SetMotorsSleepMode(true);
                    this.cnc.End();
                    this.cnc.Execute();
                }
            }

            this.appSettings.SaveToFile(settingsFileName);

            this.board.Close();
        }

        public void BoardScanner_OnBoardAvailable(object sender, BordInfoEventArgs args)
        {
            this.board.transmissionChannel = new ComTransmissionChannel(args.Info.Port);
            this.board.Open();
            //this.board.RequestBoardInfo();

            this.cnc = new CNCOpenIoTDevice(this.board, this.boardSettings);

            this.cnc_Connected(this);

            this.scanner.StopScanning(false);
        }

        public void Board_OnInfoReceived(object sender, string info)
        {
            MessageBox.Show(info, Resources.BoardInfoTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }








        /// <summary>
        /// //////////////////
        /// </summary>


        private const string settingsFileName = "settings.xml";
        private const string iotBoardSettingsFileName = "boardSettings.json";

        private CNCOpenIoTDevice cnc;

        private CNCPlotterGraphicsDevice plottingDevice;

        private IGraphicsObject svg;
        private Matrix3 transformation;

        bool isToolTestOn = false;
        bool isRendering = false;

        bool originMode = false;
        bool settingOriginMode = false;
        Vector2 originPoint;
        Cursor setOriginCursor;

        bool IsInitialized { get; set; } = false;

        private float Zoom
        {
            get
            {
                return this.viewer.zoom / this.appSettings.GetPhysicalSizeAspectRatio(this.Handle);
            }

            set
            {
                this.viewer.zoom = value * this.appSettings.GetPhysicalSizeAspectRatio(this.Handle);
            }
        }

        private Point mouseDownLeft, mouseDownRight, mouseLast, mouseDelta;
        private bool isMouseDownLeft, isMouseDownRight, isPlotting, isClosing;
        private bool presentRulers, updateRulers, presentMain, updateMain;

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.pbRender.MouseWheel += pbRender_MouseWheel;

            this.viewer = new ViewerRenderer(this.pbRender.Width, this.pbRender.Height);
            this.viewer.mainGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            this.transformation = new Palitri.Graphics.Matrix3();

            this.canvasRenderer = new CNCCanvasGraphicsDevice(this.viewer.mainGraphics);

            this.originMode = false;
            this.originPoint = new Vector2(0.0f, 0.0f);
            this.setOriginCursor = new Cursor(new MemoryStream(Resources.cursor_origin));

            this.Render(true, true, true, true);

            this.IsInitialized = true;
        }

        void cnc_Connected(object sender)
        {
            this.cnc.StateChanged += cnc_StateChanged;

            this.InvokeUpdatePort(this.board.transmissionChannel.Name);
            this.InvokeUpdateState(this.cnc.State);

            this.cnc.Begin();
            this.cnc.SetMotorsSleepMode(false);
            this.cnc.SetToolPowerMode(true);
            this.cnc.SetMotorsPowerMode(false);
            this.cnc.SetMotorsStepMode(0, this.appSettings.StepMode);
            this.cnc.SetSpeed(this.appSettings.MoveSpeed);
            this.cnc.End();
            this.cnc.Execute();

            if (this.appSettings.InitSound)
            {
                List<INote> initMelody = new List<INote>()
                {
                    new Note(Tone.C, 4, 0.25f),
                    new Note(Tone.F, 4, 0.25f),
                    new Note(Tone.G, 4, 0.25f),
                    new Note(Tone.A, 5, 0.25f),

                    new Note(Tone.G, 4, 0.25f),
                    new Note(Tone.F, 4, 0.25f),
                    new Note(Tone.G, 4, 0.25f),

                    new Note(Tone.A, 5, 0.5f),
                    new Note(Tone.F, 4, 0.5f),
                    new Note(Tone.F, 4, 1.25f),


                    //new Note(Tone.C, 4, 0.25f),
                    //new Note(Tone.F, 4, 0.25f),
                    //new Note(Tone.G, 4, 0.25f),
                    //new Note(Tone.A, 5, 0.25f),

                    //new Note(Tone.G, 4, 0.25f),
                    //new Note(Tone.F, 4, 0.25f),
                    //new Note(Tone.G, 4, 0.25f),

                    //new Note(Tone.A, 5, 0.5f),
                    //new Note(Tone.F, 4, 0.5f),
                    //new Note(Tone.F, 4, 1.25f),


                    //new Note(Tone.D, 4, 0.25f),
                    //new Note(Tone.G, 4, 0.25f),
                    //new Note(Tone.A, 4, 0.25f),
                    //new Note(Tone.G, 4, 0.25f),

                    //new Note(Tone.D, 4, 0.25f),
                    //new Note(Tone.G, 4, 0.25f),
                    //new Note(Tone.A, 4, 0.25f),
                    //new Note(Tone.G, 4, 0.25f),

                    //new Note(Tone.D, 4, 0.25f),
                    //new Note(Tone.B, 3, 0.25f),
                    //new Note(Tone.D, 4, 0.25f),
                    //new Note(Tone.G, 4, 1.25f),


                    //new Note(Tone.C, 4, 0.25f),
                    //new Note(Tone.F, 4, 0.25f),
                    //new Note(Tone.G, 4, 0.25f),
                    //new Note(Tone.F, 4, 0.25f),

                    //new Note(Tone.C, 4, 0.25f),
                    //new Note(Tone.A, 4, 0.25f),
                    //new Note(Tone.C, 4, 0.25f),
                    //new Note(Tone.F, 4, 0.25f),

                    //new Note(Tone.C, 4, 0.25f),
                    //new Note(Tone.A, 3, 0.25f),
                    //new Note(Tone.C, 4, 0.25f),
                    //new Note(Tone.F, 4, 1.25f),
                };

                new AudioSynthesizer(new CNCAudioDevice(this.cnc, 7)) { Tempo = 360.0f }.Play(initMelody);
            }
        }

        void cnc_StateChanged(ICNC cnc, CNCState newState, CNCState oldState)
        {
            this.InvokeUpdateState(newState);
        }

        public void UpdateState(CNCState state)
        {
            Dictionary<CNCState, Image> stateImages = new Dictionary<CNCState, Image>()
            {
                { CNCState.None, Resources.led_red },
                { CNCState.Ready, Resources.led_blue_soft },
                { CNCState.Busy, Resources.led_amber_soft },
                { CNCState.Unavailable, Resources.led_red }
            };

            this.pbStateLed.Image = stateImages[state];
            this.lStatus.Text = state.ToString();
        }

        public void InvokeUpdateState(CNCState state)
        {
            if (!this.isClosing)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.UpdateState(state);
                });
            }
        }

        public void InvokeUpdatePort(string port)
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.lPort.Text = port;
            });
        }

        public void Render(bool presentRulers, bool updateRulers, bool presentMain, bool updateMain)
        {
            this.presentRulers |= presentRulers;
            this.updateRulers |= updateRulers;
            this.presentMain |= presentMain;
            this.updateMain |= updateMain;

            this.timerRender.Enabled = true;
        }

        public void InvokeRender(bool presentRulers, bool updateRulers, bool presentMain, bool updateMain)
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.Render(presentRulers, updateRulers, presentMain, updateMain);
            });
        }

        public void CenterView(float fillFactor = 0.8f)
        {
            if (this.svg == null)
                return;

            float hzoom = fillFactor * this.viewer.mainScreenWidth / this.svg.Width;
            float vzoom = fillFactor * this.viewer.mainScreenHeight / this.svg.Height;
            this.viewer.zoom = Math.Min(hzoom, vzoom);
            this.viewer.offset.X = (this.viewer.mainScreenWidth / this.viewer.zoom - this.svg.Width) / 2.0f;
            this.viewer.offset.Y = (this.viewer.mainScreenHeight / this.viewer.zoom - this.svg.Height) / 2.0f;

            this.cbZoom.Text = string.Format("{0:0.0}%", this.Zoom * 100);

            this.Render(true, true, true, true);
        }

        private void timerRender_Tick(object sender, EventArgs e)
        {
            this.isRendering = true;

            if (this.updateRulers)
                this.viewer.PrerenderRulers();
            if (this.updateMain)
                this.viewer.PrerenderMainScreen();

            if (this.presentMain)
                if (this.svg != null)
                    this.svg.Render(this.viewer.offset.X * this.viewer.zoom, this.viewer.mainScreenHeight - (this.svg.Height + this.viewer.offset.Y) * this.viewer.zoom, this.viewer.zoom, this.canvasRenderer);

            if (this.originMode && !this.settingOriginMode)
            {
                this.viewer.mainGraphics.FillEllipse(new SolidBrush(this.viewer.zeroBackColor), new RectangleF((this.viewer.offset.X + this.originPoint.x) * this.viewer.zoom - 3, this.viewer.mainScreenHeight - (this.viewer.offset.Y + this.originPoint.y) * this.viewer.zoom - 3, 6, 6));
                this.viewer.mainGraphics.DrawEllipse(new Pen(new SolidBrush(this.viewer.zeroBackColor), 2), new RectangleF((this.viewer.offset.X + this.originPoint.x) * this.viewer.zoom - 7, this.viewer.mainScreenHeight - (this.viewer.offset.Y + this.originPoint.y) * this.viewer.zoom - 7, 14, 14));
            }

            this.viewer.Present(this.presentRulers, this.presentMain);

            this.pbRender.Image = this.viewer.backBuffer;

            this.pbRender.Invalidate();

            this.presentMain = false;
            this.updateMain = false;
            this.presentRulers = false;
            this.updateRulers = false;

            this.timerRender.Enabled = false;
        }

        private void pbRender_SizeChanged(object sender, EventArgs e)
        {
            if (!this.IsInitialized)
                return;

            this.viewer.SetSize(this.pbRender.Width, this.pbRender.Height);
            this.viewer.mainGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.canvasRenderer.canvasGraphics = this.viewer.mainGraphics;

            this.Render(true, true, true, true);
        }

        private void pbSVG_MouseMove(object sender, MouseEventArgs e)
        {
            this.pbRender.Focus();

            this.mouseDelta = new Point(e.X - this.mouseLast.X, e.Y - this.mouseLast.Y);

            if (this.isMouseDownRight)
            {
                this.viewer.offset.X = this.viewer.offset.X + this.mouseDelta.X / this.viewer.zoom;
                this.viewer.offset.Y = this.viewer.offset.Y - this.mouseDelta.Y / this.viewer.zoom;
                this.updateRulers = true;
                this.updateMain = true;
                this.presentMain = true;
            }

            this.viewer.SetMouseCoords(e.X, e.Y);

            this.lOrigin.Text = string.Format("{0:0.00}, {1:0.00}", viewer.offset.X, viewer.offset.Y);
            this.lPointer.Text = string.Format("{0:0.00}, {1:0.00}", viewer.pointer.X, viewer.pointer.Y);

            this.mouseLast = e.Location;

            this.Render(true, false, false, false);

            if (this.svg != null)
            {
                PickerGraphicsDevice p = new PickerGraphicsDevice();
                p.PickPoint.x = this.viewer.pointer.X;
                p.PickPoint.y = this.svg.Height - this.viewer.pointer.Y;
                p.MaxPickingDistance = 4.0f / this.viewer.zoom;
                this.svg.Render(p);
                this.pbRender.Cursor = p.IsPicked ? Cursors.Hand : this.settingOriginMode ? this.setOriginCursor : Cursors.Cross;
            }
        }

        private void pbRender_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.isMouseDownRight = true;
                this.mouseDownRight = e.Location;

                if (this.settingOriginMode)
                    this.DisableOrigin();
            }

            if (e.Button == MouseButtons.Left)
            {
                this.isMouseDownLeft = true;
                this.mouseDownLeft = e.Location;

                if (this.settingOriginMode)
                    this.EnableOrigin(this.viewer.pointer.X, this.viewer.pointer.Y);
            }

            this.mouseLast = e.Location;
        }

        private void pbRender_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                this.isMouseDownRight = false;

            if (e.Button == MouseButtons.Left)
                this.isMouseDownLeft = false;
        }

        void pbRender_MouseWheel(object sender, MouseEventArgs e)
        {
            float scrollsDelta = (e.Delta / SystemInformation.MouseWheelScrollDelta);
            float scrollsCount = Math.Abs(scrollsDelta);
            bool scrollUp = scrollsDelta > 0.0f;

            if (scrollUp)
                this.Zoom *= 1.1f * scrollsCount;
            else
                this.Zoom /= 1.1f * scrollsCount;

            this.viewer.offset.X = this.viewer.mouse.X / this.viewer.zoom - this.viewer.pointer.X;
            this.viewer.offset.Y = this.viewer.mouse.Y / this.viewer.zoom - this.viewer.pointer.Y;
            this.viewer.SetMouseCoords(e.X, e.Y);

            this.cbZoom.Text = string.Format("{0:0.0}%", this.Zoom * 100);

            this.Render(true, true, true, true);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "SVG - Scalable Vector Graphics|*.svg|All files|*.*";
            if (openDialog.ShowDialog() == DialogResult.OK)
                this.OpenFile(openDialog.FileName);
        }

        private void OpenFile(string fileName)
        {
            this.svg = new SVGContainer(fileName);
            this.viewer.workArea = new RectangleF(0.0f, 0.0f, this.svg.Width, this.svg.Height);
            this.CenterView();

            this.EstimatePlottingTime();

            this.DisableOrigin();
        }

        private void EnableSelectOrigin()
        {
            this.originMode = true;
            this.settingOriginMode = true;
            this.pbRender.Cursor = this.setOriginCursor;
            this.btnSetOrigin.Image = Resources.icon_btn_origin_before_set;
        }

        private void EnableOrigin(float x, float y)
        {
            this.originPoint = new Vector2(x, y);
            this.originMode = true;
            this.settingOriginMode = false;
            this.pbRender.Cursor = Cursors.Cross;
            this.btnSetOrigin.Image = Resources.icon_btn_origin_set;
            this.Render(true, true, true, true);
        }

        private void DisableOrigin()
        {
            this.originPoint = new Vector2(0.0f, 0.0f);
            this.originMode = false;
            this.settingOriginMode = false;
            this.pbRender.Cursor = Cursors.Cross;
            this.btnSetOrigin.Image = Resources.icon_btn_origin;
            this.Render(true, true, true, true);
        }

        private void plotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.cnc == null || this.svg == null)
                return;

            if (this.isPlotting)
            {
                if (MessageBox.Show("Stop plotting?", "Plotting sequence", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                    return;

                this.plottingDevice.CancelPlotting();

                this.isPlotting = false;
                this.plotToolStripMenuItem.Enabled = false;
                this.btnPlot.Image = this.isPlotting ? Resources.icon_btn_plotting : Resources.icon_btn_plot;
            }
            else
            {
                if (MessageBox.Show("Start plotting?", "Plotting sequence", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;

                this.isPlotting = true;
                this.plotToolStripMenuItem.Text = "Stop plotting";
                this.btnPlot.Image = this.isPlotting ? Resources.icon_btn_plotting : Resources.icon_btn_plot;

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;

                    this.plottingDevice = new CNCPlotterGraphicsDevice(this.cnc, this.appSettings);
                    plottingDevice.SetOrigin(this.originPoint.x, this.originPoint.y - this.svg.Height);

                    plottingDevice.RenderPrimitive += cncRenderer_RenderPrimitive;
                    this.svg.Render(0, 0, this.svg.Width, -this.svg.Height, plottingDevice);

                    this.canvasRenderer.SetHighlight();
                    this.InvokeRender(false, false, true, false);

                    if (this.appSettings.ReturnToOrigin && !this.plottingDevice.IsCancelled)
                    {
                        plottingDevice.Begin();
                        plottingDevice.ReturnToOrigin();
                        plottingDevice.End();
                    }

                    this.Invoke((MethodInvoker)delegate
                    {
                        this.plotToolStripMenuItem.Text = "Plot";
                        this.plotToolStripMenuItem.Enabled = true;
                        this.isPlotting = false;
                        this.btnPlot.Image = this.isPlotting ? Resources.icon_btn_plotting : Resources.icon_btn_plot;
                    });

                }).Start();
            }
        }

        void cncRenderer_RenderPrimitive(object sender, CNCPlotterGraphicsDeviceRenderPrimitiveEventArgs e)
        {
            e.Continue = this.isPlotting;
            this.canvasRenderer.SetHighlight(e.PrimitiveIndex);
            this.InvokeRender(false, false, true, false);
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.board.RequestAllDeviceProperties();
            //MessageBox.Show(this.cnc.Info, "Device info");
        }

        private void manualConrolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManualControlsForm controlsForm = new ManualControlsForm(this.cnc, this.appSettings);
            controlsForm.ShowDialog();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            this.openToolStripMenuItem_Click(sender, e);
        }

        private void btnCenterView_Click(object sender, EventArgs e)
        {
            this.centralizeViewToolStripMenuItem_Click(sender, e);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            this.settingsToolStripMenuItem_Click(sender, e);
        }

        private void btnPlot_Click(object sender, EventArgs e)
        {
            this.plotToolStripMenuItem_Click(sender, e);
        }

        private void btnManualControls_Click(object sender, EventArgs e)
        {
            this.manualConrolsToolStripMenuItem_Click(sender, e);
        }


        private void centralizeViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CenterView();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(this.appSettings);
            settingsForm.ShowDialog();
        }

        private void cbZoom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                this.cbZoom_SelectedIndexChanged(sender, e);
            }
        }

        private void cbZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            float zoom;
            if (float.TryParse(this.cbZoom.Text.Replace("%", string.Empty), out zoom))
            {
                this.Zoom = zoom / 100.0f;
                this.Render(true, true, true, true);
            }
        }

        private void pbZoomIcon_Click(object sender, EventArgs e)
        {
            this.cbZoom.Text = "100%";
            this.cbZoom_SelectedIndexChanged(sender, e);
        }

        private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsoleForm consoleForm = new ConsoleForm(this.cnc, this.boardSettings.AxesSettings.Count);
            consoleForm.ShowDialog();
        }

        private void pbRender_Validated(object sender, EventArgs e)
        {
            this.isRendering = false;
        }

        private void btnToolTest_Click(object sender, EventArgs e)
        {
            this.testToolToolStripMenuItem_Click(sender, e);
        }

        private void testToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.cnc == null)
                return;

            this.isToolTestOn = !this.isToolTestOn;

            new Thread(() =>
            {
                this.cnc.Begin();

                this.cnc.SetPower(this.isToolTestOn ? this.appSettings.TestingPower : this.appSettings.IdlePower);

                this.cnc.End();
                this.cnc.Execute();
            }).Start();

            this.btnToolTest.Image = this.isToolTestOn ? Resources.icon_btn_lamp_on : Resources.icon_btn_lamp_off;
            this.testToolToolStripMenuItem.Image = this.isToolTestOn ? Resources.icon_btn_lamp_on : Resources.icon_btn_lamp_off;
            this.testToolToolStripMenuItem.Checked = this.isToolTestOn;
        }


        private void EstimatePlottingTime()
        {
            CNCPlotTimeEstimationGraphicsDevice t = new CNCPlotTimeEstimationGraphicsDevice(this.appSettings);
            t.SetOrigin(0.0f, -this.svg.Height);
            this.svg.Render(t);
            this.lblPlottingTimeEstimation.Text = this.TimeSpanToString(TimeSpan.FromSeconds(t.ProjectedTime));
        }

        private string TimeSpanToString(TimeSpan t)
        {
            StringBuilder s = new StringBuilder();
            bool print = t.Days > 1;
            if (print)
                s.Append(string.Format("{0}d ", t.Days));
            print |= t.Hours > 1;
            if (print)
                s.Append(string.Format("{0}h ", t.Hours));
            print |= t.Minutes > 1;
            if (print)
                s.Append(string.Format("{0:00}m ", t.Minutes));
            print |= t.Seconds > 1;
            if (print)
                s.Append(string.Format("{0:00}s", t.Seconds));
            return s.ToString();
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            this.OpenFile(files[0]);
        }

        private void btnSetOrigin_Click(object sender, EventArgs e)
        {
            if (this.originMode)
                this.DisableOrigin();
            else
                this.EnableSelectOrigin();
        }

        private void returnToOriginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.plottingDevice == null)
                return;

            new Thread(() =>
            {
                this.plottingDevice.Begin();
                this.plottingDevice.ReturnToOrigin();
                this.plottingDevice.End();
            }).Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // performa test-burn: plot a small test-shape, using curent settings. Shape can either be manual (one continuous path, like a circle) or can be selected from a prest file
        }





        /////////////////////


        internal class MainFormBoardEventHandler : OpenIoTProtocolEventsHandler
        {
            private MainForm mainForm;

            private IntPtr handle;

            public MainFormBoardEventHandler(MainForm mainForm)
            {
                this.mainForm = mainForm;
                this.handle = mainForm.Handle;
            }
            public override void OnInfoReceived(object sender, string info)
            {
                this.mainForm.Board_OnInfoReceived(sender, info);
            }

            public override void OnDevicePropertiesReceived(object sender, Dictionary<int, byte[]> properties)
            {
                MessageBox.Show(String.Join(Environment.NewLine, properties.Select(p =>
                {
                    string name;
                    object value;
                    OpenIoTProtocol.GetDevicePropertyFriendly(p.Key, p.Value, out name, out value);
                    return String.Format("{0}: {1}", name, value);
                })), Resources.BoardInfoTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
