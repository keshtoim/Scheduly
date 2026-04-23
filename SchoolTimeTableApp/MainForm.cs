using System;
using System.Windows.Forms;

namespace testing
{
    public partial class MainForm : Form
    {
        private AppUser _currentUser;

        private const int TAB_SCHEDULE   = 0;
        private const int TAB_COMPOSE    = 1;
        private const int TAB_WORKLOAD   = 2;
        private const int TAB_REFERENCES = 3;

        // UserControls created programmatically so Designer stays clean
        private ViewScheduleControl   _viewSchedule;
        private ComposeScheduleControl _composeSchedule;
        private WorkloadControl       _workload;
        private ReferencesControl     _references;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(AppUser user) : this()
        {
            _currentUser = user;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;
            if (_currentUser == null) return;

            // Create and dock UserControls into tab pages
            _viewSchedule = new ViewScheduleControl { Dock = DockStyle.Fill };
            tabPageSchedule.Controls.Add(_viewSchedule);

            _composeSchedule = new ComposeScheduleControl { Dock = DockStyle.Fill };
            tabPageCompose.Controls.Add(_composeSchedule);

            _workload = new WorkloadControl { Dock = DockStyle.Fill };
            tabPageWorkload.Controls.Add(_workload);

            _references = new ReferencesControl { Dock = DockStyle.Fill };
            tabPageReferences.Controls.Add(_references);

            // Apply role restrictions
            labelUserInfo.Text = string.Format("{0}  |  {1}",
                _currentUser.DisplayName, _currentUser.Role);

            if (!_currentUser.CanEdit)
            {
                tabControl.TabPages[TAB_COMPOSE].Enabled    = false;
                tabControl.TabPages[TAB_WORKLOAD].Enabled   = false;
                tabControl.TabPages[TAB_REFERENCES].Enabled = false;
            }

            // Start on schedule view
            tabControl.SelectedIndex = TAB_SCHEDULE;
            _viewSchedule.LoadSchedule();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case TAB_SCHEDULE:
                    _viewSchedule?.LoadSchedule();
                    break;
                case TAB_COMPOSE:
                    _composeSchedule?.LoadData();
                    break;
                case TAB_WORKLOAD:
                    _workload?.LoadData();
                    break;
                case TAB_REFERENCES:
                    _references?.LoadData();
                    break;
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выйти из системы?", "Выход",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            this.Hide();
            AuthForm auth = new AuthForm();
            if (auth.ShowDialog() == DialogResult.OK)
            {
                _currentUser = auth.AuthenticatedUser;
                labelUserInfo.Text = string.Format("{0}  |  {1}",
                    _currentUser.DisplayName, _currentUser.Role);

                bool canEdit = _currentUser.CanEdit;
                tabControl.TabPages[TAB_COMPOSE].Enabled    = canEdit;
                tabControl.TabPages[TAB_WORKLOAD].Enabled   = canEdit;
                tabControl.TabPages[TAB_REFERENCES].Enabled = canEdit;

                this.Show();
                tabControl.SelectedIndex = TAB_SCHEDULE;
                _viewSchedule?.LoadSchedule();
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
