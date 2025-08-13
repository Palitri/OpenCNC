using Palitri.OpenCNC.App.Components;
using Palitri.OpenCNC.App.Settings;
using Palitri.OpenCNC.Driver;
using Palitri.OpenCNC.Driver.Settings;
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
using static Palitri.OpenCNC.Driver.Settings.OpenIoTBoardSettings;

namespace Palitri.OpenCNC.App
{
    public partial class ManualControlsForm : Form
    {
        private ICNC cnc;
        private OpenCNCAppSettings settings;
        private OpenIoTBoardSettings boardSettings;

        public ManualControlsForm(ICNC cnc, OpenCNCAppSettings settings, OpenIoTBoardSettings boardSettings)
        {
            InitializeComponent();

            this.cnc = cnc;
            this.settings = settings;
            this.boardSettings = boardSettings;

            this.thumbwheelX.Visible = this.boardSettings.AxesSettings.Count > 0;
            this.thumbwheelY.Visible = this.boardSettings.AxesSettings.Count > 1;
            this.thumbwheelZ.Visible = this.boardSettings.AxesSettings.Count > 2;

            this.thumbwheelArbitrary.Visible = this.boardSettings.AxesSettings.Count > 3;
            this.comboArbitraryAxis.Visible = this.boardSettings.AxesSettings.Count > 4;
            this.comboArbitraryAxis.Items.Clear();
            for (int i = 3; i < this.boardSettings.AxesSettings.Count; i++)
                this.comboArbitraryAxis.Items.Add(i.ToString());
            this.comboArbitraryAxis.SelectedIndex = 0;
        }

        private void thumbwheelAxis_ValueChanged(object sender, EventArgs e)
        {
            Thumbwheel senderThumbwheel = (Thumbwheel)sender;
            AsyncChannelSetting setting = this.boardSettings.AxesSettings[int.Parse((string)senderThumbwheel.Tag)];
            float speed = senderThumbwheel.Value / 50.0f;
            this.cnc.SetPropertyValue(setting.PropertyIdSpeed, speed);
        }

        private void thumbwheelAxis_MouseDown(object sender, MouseEventArgs e)
        {
            Thumbwheel senderThumbwheel = (Thumbwheel)sender;
            AsyncChannelSetting setting = this.boardSettings.AxesSettings[int.Parse((string)senderThumbwheel.Tag)];
            new Thread(() =>
            {
                this.cnc.Begin();
                this.cnc.SetMotorsPowerMode(true);
                this.cnc.End();
                this.cnc.Execute();

                this.cnc.SetPropertyValue(setting.PropertyIdSpeed, 0.0f);
                this.cnc.SetPropertyValue(setting.PropertyIdTurn, true);
            }).Start();
        }

        private void thumbwheelAxis_MouseUp(object sender, MouseEventArgs e)
        {
            Thumbwheel senderThumbwheel = (Thumbwheel)sender;
            AsyncChannelSetting setting = this.boardSettings.AxesSettings[int.Parse((string)senderThumbwheel.Tag)];
            new Thread(() =>
            {
                this.cnc.SetPropertyValue(setting.PropertyIdTurn, false);

                this.cnc.Begin();
                this.cnc.SetMotorsPowerMode(false);
                this.cnc.End();
                this.cnc.Execute();
            }).Start();
        }

        private void comboArbitraryAxis_TextChanged(object sender, EventArgs e)
        {
            this.thumbwheelArbitrary.Tag = this.comboArbitraryAxis.Text;
        }
    }
}
