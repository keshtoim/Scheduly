using System;
using System.Windows.Forms;

namespace testing
{
    /// <summary>
    /// Главное окно. Доступность вкладок и кнопок определяется ролью пользователя:
    /// — Администратор:         все вкладки, Настройки. Без управления пользователями.
    /// — Заместитель директора: все вкладки + Настройки + кнопка Пользователи.
    /// — Учитель:               только просмотр расписания.
    /// </summary>
    public partial class MainForm : Form
    {
        private AppUser _currentUser;
        private const int TAB_SCHEDULE   = 0;
        private const int TAB_COMPOSE    = 1;
        private const int TAB_WORKLOAD   = 2;
        private const int TAB_REFERENCES = 3;

        private ViewScheduleControl    _viewSchedule;
        private ComposeScheduleControl _composeSchedule;
        private WorkloadControl        _workload;
        private ReferencesControl      _references;

        public MainForm() { InitializeComponent(); }
        public MainForm(AppUser user) : this() { _currentUser = user; }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;
            if (_currentUser == null) return;

            _viewSchedule    = new ViewScheduleControl    { Dock = DockStyle.Fill };
            _composeSchedule = new ComposeScheduleControl { Dock = DockStyle.Fill };
            _workload        = new WorkloadControl        { Dock = DockStyle.Fill };
            _references      = new ReferencesControl      { Dock = DockStyle.Fill };

            tabPageSchedule.Controls.Add(_viewSchedule);
            tabPageCompose.Controls.Add(_composeSchedule);
            tabPageWorkload.Controls.Add(_workload);
            tabPageReferences.Controls.Add(_references);

            ApplyRoleRestrictions();

            tabControl.SelectedIndex = TAB_SCHEDULE;
            _viewSchedule.LoadSchedule();
        }

        /// <summary>
        /// Применяет ограничения доступа в зависимости от роли пользователя.
        /// </summary>
        private void ApplyRoleRestrictions()
        {
            labelUserInfo.Text = string.Format("{0}  |  {1}",
                _currentUser.DisplayName, _currentUser.RoleName);

            bool canEdit         = _currentUser.CanEdit;
            bool canManageUsers  = _currentUser.CanManageUsers;

            // Вкладки редактирования — только Администратор и Зам. директора
            tabControl.TabPages[TAB_COMPOSE].Enabled    = canEdit;
            tabControl.TabPages[TAB_WORKLOAD].Enabled   = canEdit;
            tabControl.TabPages[TAB_REFERENCES].Enabled = canEdit;

            // Кнопка Настройки — только Администратор и Зам. директора
            buttonSettings.Visible = canEdit;

            // Кнопка Пользователи — только Заместитель директора
            buttonUsers.Visible = canManageUsers;
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case TAB_SCHEDULE:   _viewSchedule?.LoadSchedule(); break;
                case TAB_COMPOSE:    _composeSchedule?.LoadData();  break;
                case TAB_WORKLOAD:   _workload?.LoadData();         break;
                case TAB_REFERENCES: _references?.LoadData();       break;
            }
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            using (var dlg = new SettingsForm()) dlg.ShowDialog(this);
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            using (var dlg = new UserManagementForm(_currentUser))
                dlg.ShowDialog(this);
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выйти из системы?", "Выход",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            this.Hide();
            var auth = new AuthForm();
            if (auth.ShowDialog() == DialogResult.OK)
            {
                _currentUser = auth.AuthenticatedUser;
                ApplyRoleRestrictions();
                this.Show();
                tabControl.SelectedIndex = TAB_SCHEDULE;
                _viewSchedule?.LoadSchedule();
            }
            else { Application.Exit(); }
        }
    }
}
