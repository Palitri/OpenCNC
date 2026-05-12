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
using static Palitri.OpenCNC.Driver.Settings.OpenIoTBoardConfiguration;

namespace Palitri.OpenCNC.App
{
    public partial class ManualControlsForm : Form
    {
        private ICNC cnc;
        private OpenCNCAppSettings settings;
        private OpenIoTBoardConfiguration boardConfiguration;

        public ManualControlsForm(ICNC cnc, OpenCNCAppSettings settings, OpenIoTBoardConfiguration boardConfiguration)
        {
            InitializeComponent();

            this.cnc = cnc;
            this.settings = settings;
            this.boardConfiguration = boardConfiguration;

            this.thumbwheelX.Visible = this.boardConfiguration.Axes.Count > 0;
            this.thumbwheelY.Visible = this.boardConfiguration.Axes.Count > 1;
            this.thumbwheelZ.Visible = this.boardConfiguration.Axes.Count > 2;
            
            this.thumbwheelXY.Visible = this.boardConfiguration.Axes.Count > 1;
            this.thumbwheelXZ.Visible = this.boardConfiguration.Axes.Count > 2;
            this.thumbwheelYZ.Visible = this.boardConfiguration.Axes.Count > 2;

            this.thumbwheelArbitrary.Visible = this.boardConfiguration.Axes.Count > 3;
            this.comboArbitraryAxis.Visible = this.boardConfiguration.Axes.Count > 4;
            this.comboArbitraryAxis.Items.Clear();
            for (int i = 3; i < this.boardConfiguration.Axes.Count; i++)
                this.comboArbitraryAxis.Items.Add(i.ToString());
            if (this.comboArbitraryAxis.Items.Count > 0)
                this.comboArbitraryAxis.SelectedIndex = 0;
        }

        private void thumbwheelAxis_ValueChanged(object sender, EventArgs e)
        {
            Thumbwheel senderThumbwheel = (Thumbwheel)sender;
            int[] axes = senderThumbwheel.Tag.ToString().Split(',').Select(s => int.Parse(s.Trim())).ToArray();
            float speed = senderThumbwheel.Value / 50.0f;
            if (axes.Length == 2)
            {
                AsyncChannelConfiguration configX = this.boardConfiguration.Axes[axes[0]];
                this.cnc.SetPropertyValue(configX.PropertyIdSpeed, -senderThumbwheel.ValueComponents.X / 50.0f);

                AsyncChannelConfiguration configY = this.boardConfiguration.Axes[axes[1]];
                this.cnc.SetPropertyValue(configY.PropertyIdSpeed, senderThumbwheel.ValueComponents.Y  / 50.0f);
            }
            else
            {
                foreach (int axis in axes)
                {
                    AsyncChannelConfiguration configuration = this.boardConfiguration.Axes[axis];
                    this.cnc.SetPropertyValue(configuration.PropertyIdSpeed, speed);
                }
            }
        }

        private void thumbwheelAxis_MouseDown(object sender, MouseEventArgs e)
        {
            Thumbwheel senderThumbwheel = (Thumbwheel)sender;
            new Thread(() =>
            {
                this.cnc.Begin();
                this.cnc.SetMotorsPowerMode(true);
                this.cnc.End();
                this.cnc.Execute();

                int[] axes = senderThumbwheel.Tag.ToString().Split(',').Select(s => int.Parse(s.Trim())).ToArray();
                foreach (int axis in axes)
                {
                    AsyncChannelConfiguration configuration = this.boardConfiguration.Axes[axis];
                    this.cnc.SetPropertyValue(configuration.PropertyIdSpeed, 0.0f);
                    this.cnc.SetPropertyValue(configuration.PropertyIdTurn, true);
                }

            }).Start();
        }

        private void thumbwheelAxis_MouseUp(object sender, MouseEventArgs e)
        {
            Thumbwheel senderThumbwheel = (Thumbwheel)sender;
            new Thread(() =>
            {
                int[] axes = senderThumbwheel.Tag.ToString().Split(',').Select(s => int.Parse(s.Trim())).ToArray();
                foreach (int axis in axes)
                {
                    AsyncChannelConfiguration configuration = this.boardConfiguration.Axes[axis];
                    this.cnc.SetPropertyValue(configuration.PropertyIdSpeed, 0.0f);
                    this.cnc.SetPropertyValue(configuration.PropertyIdTurn, false);
                }


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
