using Palitri.OpenCNC.App.Rendering;
using Palitri.OpenCNC.Script;
using Palitri.OpenCNC.Driver;
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
    public partial class ConsoleForm : Form
    {
        private ICNC cnc;
        private CNCScriptEngine scriptEngine;
        private ScriptHintRenderer hintRenderer;

        private int historyIndex = 0;
       

        public ConsoleForm(ICNC cnc, int dimensions)
        {
            this.cnc = cnc;
            this.cnc.StateChanged += cnc_StateChanged;
            this.scriptEngine = new CNCScriptEngine(dimensions);

            InitializeComponent();

            this.tbCommand.AutoCompleteCustomSource.AddRange(this.scriptEngine.commands.Select(c => c.Name).ToArray());

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

            CNCScriptCommandResult result = this.scriptEngine.Execute(this.cnc, inputCommand);

            if ((result.ResultType != CNCScriptCommandResultType.Error) || inputCommand.StartsWith("@"))
                this.tbLog.AppendText(inputCommand + Environment.NewLine);
            else if ("?".Equals(inputCommand, StringComparison.OrdinalIgnoreCase))
                this.ListCommands();
            else if ("clear".Equals(inputCommand, StringComparison.OrdinalIgnoreCase))
                this.tbLog.Clear();
            else if ("exit".Equals(inputCommand, StringComparison.OrdinalIgnoreCase))
                this.Close();
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
            this.hintRenderer.Render(this.scriptEngine, this.tbCommand.Text, this.tbCommand.SelectionStart);

            this.pbHint.Image = this.hintRenderer.Bitmap;
            this.pbHint.Invalidate();
        }

        private void ListCommands()
        {
            foreach (ICNCScriptCommand command in this.scriptEngine.commands)
                this.tbLog.AppendText(string.Format("@{0} {1}{2}", command.Name, string.Join(", ", command.Parameters.ToArray()), Environment.NewLine));
        }

    }

}
