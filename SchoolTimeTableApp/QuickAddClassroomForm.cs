using System;
using System.Data;
using System.Windows.Forms;

namespace testing
{
    /// <summary>
    /// Мини-диалог для быстрого добавления кабинета прямо из формы редактирования урока.
    /// В отличие от QuickAddForm содержит ComboBox для выбора типа кабинета.
    /// </summary>
    public partial class QuickAddClassroomForm : Form
    {
        /// <summary>Номер кабинета введённый пользователем.</summary>
        public int RoomNumber { get; private set; }
        /// <summary>Вместимость кабинета.</summary>
        public int Capacity   { get; private set; }
        /// <summary>ID выбранного типа кабинета.</summary>
        public int TypeId     { get; private set; }

        /// <param name="roomTypes">DataTable с типами кабинетов.</param>
        public QuickAddClassroomForm(DataTable roomTypes)
            : this(roomTypes, 0, 30) { }

        /// <param name="roomTypes">DataTable с типами кабинетов.</param>
        /// <param name="roomNumber">Начальный номер кабинета (для редактирования).</param>
        /// <param name="capacity">Начальная вместимость (для редактирования).</param>
        public QuickAddClassroomForm(DataTable roomTypes, int roomNumber, int capacity)
        {
            InitializeComponent();
            comboType.DisplayMember = "name";
            comboType.ValueMember   = "ID_типа_кабинета";
            comboType.DataSource    = roomTypes;
            if (roomNumber > 0) txtRoomNumber.Text = roomNumber.ToString();
            if (capacity > 0)   txtCapacity.Text   = capacity.ToString();
            this.Text = roomNumber > 0 ? "Изменить кабинет" : "Новый кабинет";
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomNumber.Text))
            {
                MessageBox.Show("Введите номер кабинета.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtRoomNumber.Text, out int room))
            {
                MessageBox.Show("Номер кабинета должен быть числом.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int.TryParse(txtCapacity.Text, out int cap);

            RoomNumber = room;
            Capacity   = cap;
            TypeId     = Convert.ToInt32(comboType.SelectedValue);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
