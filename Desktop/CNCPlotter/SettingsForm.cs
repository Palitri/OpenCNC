using Palitri.CNCPlotter.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNCPlotter
{
    public partial class SettingsForm : Form
    {
        private CNCSettings settings;

        public SettingsForm(CNCSettings settings)
        {
            InitializeComponent();

            this.settings = settings;

            this.pgAppSettings.SelectedObject = this.settings;
        }

    }
}
