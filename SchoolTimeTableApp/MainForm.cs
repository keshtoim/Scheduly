using System;
using System.Windows.Forms;

namespace testing
{
    /// <summary>
    /// Главное окно приложения.
    /// Содержит TabControl с четырьмя вкладками: Расписание, Составление, Нагрузка, Справочники.
    /// UserControl-ы создаются программно в событии Load (не в Designer) —
    /// это сделано намеренно, чтобы Designer корректно отображал форму.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>Текущий авторизованный пользователь.</summary>
        private AppUser _currentUser;

        // Индексы вкладок TabControl
        private const int TAB_SCHEDULE   = 0;
        private const int TAB_COMPOSE    = 1;
        private const int TAB_WORKLOAD   = 2;
        private const int TAB_REFERENCES = 3;

        // UserControl-ы создаются программно в MainForm_Load
        private ViewScheduleControl    _viewSchedule;
        private ComposeScheduleControl _composeSchedule;
        private WorkloadControl        _workload;
        private ReferencesControl      _references;

        /// <summary>Конструктор без параметров — нужен для Designer.</summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Основной конструктор — принимает авторизованного пользователя.
        /// </summary>
        public MainForm(AppUser user) : this()
        {
            _currentUser = user;
        }

        /// <summary>
        /// Инициализация при загрузке формы:
        /// создаёт UserControl-ы, применяет ограничения по роли,
        /// открывает вкладку расписания.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Защита от запуска в режиме Designer
            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;
            if (_currentUser == null) return;

            // Создаём UserControl-ы и размещаем их в TabPage
            _viewSchedule = new ViewScheduleControl { Dock = DockStyle.Fill };
            tabPageSchedule.Controls.Add(_viewSchedule);

            _composeSchedule = new ComposeScheduleControl { Dock = DockStyle.Fill };
            tabPageCompose.Controls.Add(_composeSchedule);

            _workload = new WorkloadControl { Dock = DockStyle.Fill };
            tabPageWorkload.Controls.Add(_workload);

            _references = new ReferencesControl { Dock = DockStyle.Fill };
            tabPageReferences.Controls.Add(_references);

            // Отображаем информацию о пользователе в шапке
            labelUserInfo.Text = string.Format("{0}  |  {1}",
                _currentUser.DisplayName, _currentUser.Role);

            // Учитель видит только вкладку расписания
            if (!_currentUser.CanEdit)
            {
                tabControl.TabPages[TAB_COMPOSE].Enabled    = false;
                tabControl.TabPages[TAB_WORKLOAD].Enabled   = false;
                tabControl.TabPages[TAB_REFERENCES].Enabled = false;
            }

            // При запуске всегда открываем расписание
            tabControl.SelectedIndex = TAB_SCHEDULE;
            _viewSchedule.LoadSchedule();
        }

        /// <summary>
        /// При переключении вкладки загружаем данные нужного UserControl.
        /// Используем null-условный оператор на случай если контрол ещё не создан.
        /// </summary>
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case TAB_SCHEDULE:   _viewSchedule?.LoadSchedule();  break;
                case TAB_COMPOSE:    _composeSchedule?.LoadData();   break;
                case TAB_WORKLOAD:   _workload?.LoadData();          break;
                case TAB_REFERENCES: _references?.LoadData();        break;
            }
        }

        /// <summary>
        /// Выход из системы: скрывает главное окно, открывает форму авторизации.
        /// При успешном повторном входе обновляет пользователя и перезагружает данные.
        /// При отмене — завершает приложение.
        /// </summary>
        private void buttonSettings_Click(object sender, EventArgs e)
        {
            using (SettingsForm dlg = new SettingsForm())
                dlg.ShowDialog(this);
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

                // Обновляем доступность вкладок для новой роли
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
