using System.Windows.Forms;

namespace testing
{
    public partial class ConflictDialogForm : Form
    {
        public enum ConflictChoice { EditThis, EditOther, Cancel }
        public ConflictChoice Choice { get; private set; } = ConflictChoice.Cancel;

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
