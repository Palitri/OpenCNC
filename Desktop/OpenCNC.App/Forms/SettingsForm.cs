using OpenCNC.App.Settings;

namespace OpenCNC.App
{
    public partial class SettingsForm : Form
    {
        private OpenCNCAppSettings settings;

        public SettingsForm(OpenCNCAppSettings settings)
        {
            InitializeComponent();

            this.settings = settings;

            this.pgAppSettings.SelectedObject = this.settings;
        }

    }
}
