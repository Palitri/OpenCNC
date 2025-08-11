using OpenCNC.App.Settings;
using Palitri.OpenCNC.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCNC.App
{
    public partial class ManualControlsForm : Form
    {
        private ICNC cnc;
        private OpenCNCAppSettings settings;

        public ManualControlsForm(ICNC cnc, OpenCNCAppSettings settings)
        {
            InitializeComponent();

            this.cnc = cnc;
            this.settings = settings;
        }

        private void IssueTurning(int motor, float speed)
        {
            new Thread(() =>
            {
                this.cnc.Begin();
                this.cnc.SetMotorsPowerMode(speed != 0.0f);
                this.cnc.IssueMotorTurning(motor, speed);
                this.cnc.End();
                this.cnc.Execute();
            }).Start();
        }

        private Tuple<int, int, bool> GetButtonDetails(object tag)
        {
            int value = int.Parse(tag.ToString());
            int motor = value / 4;
            value %= 4;
            bool isFine = (value / 2) == 1;
            value %= 2;
            int direction = value == 0 ? -1 : 1;

            return new Tuple<int, int, bool>(motor, direction, isFine);
        }

        private void btnAxis_MouseDown(object sender, MouseEventArgs e)
        {
            Tuple<int, int, bool> details = this.GetButtonDetails((sender as Control).Tag);
            this.IssueTurning(details.Item1, details.Item2 * (details.Item3 ? this.settings.ManualSpeedFine : this.settings.ManualSpeed));
        }

        private void btnAxis_MouseUp(object sender, MouseEventArgs e)
        {
            Tuple<int, int, bool> details = this.GetButtonDetails((sender as Control).Tag);
            this.IssueTurning(details.Item1, 0.0f);
        }
    }
}
