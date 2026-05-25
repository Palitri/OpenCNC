using OpenCNC.Script;
using Palitri.OpenCNC.App.Rendering;
using Palitri.OpenCNC.Driver;
using Palitri.OpenCNC.Driver.Settings;
using Palitri.OpenCNC.Script;
using Palitri.OpenCNC.Script.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palitri.OpenCNC.App
{
    public partial class ConsoleForm : Form, ICNCScriptExtensionsHandler
    {
        private ICNC cnc;
        private ScriptHintRenderer hintRenderer;

        private int historyIndex = 0;

        #region ICNCScriptExtensionsHandler properties
        public CNCScriptEngine ScriptEngine { get; private set; }
        public OpenIoTBoardConfiguration BoardConfig { get; private set; }
        #endregion


        public ConsoleForm(ICNC cnc, int dimensions, OpenIoTBoardConfiguration boardConfig)
        {
            this.cnc = cnc;
            this.cnc.StateChanged += cnc_StateChanged;
            this.ScriptEngine = new CNCScriptEngine(dimensions, this);
            this.BoardConfig = boardConfig;

            InitializeComponent();

            this.tbCommand.AutoCompleteCustomSource.AddRange(this.ScriptEngine.commands.Select(c => c.Name).ToArray());

            this.pbHint_SizeChanged(this, new EventArgs());
        }

        private void ConsoleForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.cnc.StateChanged -= cnc_StateChanged;
        }

        void cnc_StateChanged(ICNC cnc, CNCState newState, CNCState oldState)
        {
            bool enabled = newState == CNCState.Ready;

            this.Invoke((MethodInvoker)delegate
            {
                this.tbCommand.Enabled = enabled;
                this.btnSend.Enabled = enabled;
                this.tbCommand.Focus();

                if (enabled)
                    this.tbLog.AppendText(Environment.NewLine);
            });
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string inputCommand = this.tbCommand.Text.Trim();

            if (string.IsNullOrWhiteSpace(inputCommand))
                return;

            CNCScriptCommandResult result = this.ScriptEngine.Execute(this.cnc, inputCommand);

            if ((result.ResultType != CNCScriptCommandResultType.Error) || inputCommand.StartsWith("@"))
                this.Write(inputCommand);
            else
                return;

            this.tbCommand.Clear();
        }

        private void tbCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                this.btnSend_Click(sender, e);

            
            int historyDelta = e.KeyCode == Keys.Up ? 1 : e.KeyCode == Keys.Down ? -1 : 0;
            if (historyDelta != 0)
            {
                string[] history = this.tbLog.Lines.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
                this.historyIndex = Math.Min(Math.Max(this.historyIndex + historyDelta, 1), history.Length);
                if (history.Length > 0)
                {
                    this.tbCommand.Text = history[history.Length - this.historyIndex];
                    this.tbCommand.SelectionStart = this.tbCommand.Text.Length;
                }
            }
            else
                this.historyIndex = 0;
        }

        private void tbCommand_KeyUp(object sender, KeyEventArgs e)
        {
            this.UpdateHint();
        }

        private void pbHint_SizeChanged(object sender, EventArgs e)
        {
            this.hintRenderer = new ScriptHintRenderer(this.pbHint.Width, this.pbHint.Height);
            this.UpdateHint();
        }

        private void UpdateHint()
        {
            this.hintRenderer.Render(this.ScriptEngine, this.tbCommand.Text, this.tbCommand.SelectionStart);

            this.pbHint.Image = this.hintRenderer.Bitmap;
            this.pbHint.Invalidate();
        }

        #region ICNCScriptExtensionsHandler methods
        
        public void Write(string text, bool newLine = true)
        {
            this.tbLog.AppendText(text);

            if (newLine)
                this.tbLog.AppendText(Environment.NewLine);
        }

        public void Clear()
        {
            this.tbLog.Clear();
        }

        public void Exit()
        {
            this.Close();
        }


        #endregion
    }
}
