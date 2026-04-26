using System.Windows.Forms;

namespace testing
{
    /// <summary>
    /// Кастомный диалог конфликта расписания с тремя кнопками.
    /// Заменяет стандартный MessageBox, так как в нём нельзя изменять текст кнопок.
    /// </summary>
    public partial class ConflictDialogForm : Form
    {
        /// <summary>Варианты выбора пользователя в диалоге конфликта.</summary>
        public enum ConflictChoice
        {
            /// <summary>Изменить текущую запись (ту по которой кликнули).</summary>
            EditThis,
            /// <summary>Изменить конфликтующую запись (другой класс или урок).</summary>
            EditOther,
            /// <summary>Закрыть без изменений.</summary>
            Cancel
        }

        /// <summary>
        /// Выбор пользователя после закрытия диалога.
        /// По умолчанию Cancel.
        /// </summary>
        public ConflictChoice Choice { get; private set; } = ConflictChoice.Cancel;

        /// <summary>
        /// Создаёт диалог с описанием конкретного конфликта.
        /// </summary>
        /// <param name="conflictDescription">Текст описания конфликта для отображения.</param>
        public ConflictDialogForm(string conflictDescription)
        {
            InitializeComponent();
            labelDescription.Text = "Обнаружен конфликт:\n" + conflictDescription + "\n\nЧто изменить?";
        }

        private void buttonEditThis_Click(object sender, System.EventArgs e)
        {
            Choice = ConflictChoice.EditThis;
            Close();
        }

        private void buttonEditOther_Click(object sender, System.EventArgs e)
        {
            Choice = ConflictChoice.EditOther;
            Close();
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            Choice = ConflictChoice.Cancel;
            Close();
        }
    }
}
