using System;
using System.Windows.Forms;

namespace testing
{
    /// <summary>
    /// Универсальный мини-диалог для быстрого добавления простой записи.
    /// Принимает список названий полей и возвращает введённые значения.
    /// Используется для добавления учителя прямо из формы редактирования урока.
    /// </summary>
    public partial class QuickAddForm : Form
    {
        /// <summary>Введённые пользователем значения в том же порядке что и fieldNames.</summary>
        public string[] Values { get; private set; }

        private readonly string[]   _fieldNames;
        private readonly string[]   _initialValues;
        private readonly TextBox[]  _textBoxes;

        /// <param name="title">Заголовок диалога.</param>
        /// <param name="fieldNames">Названия полей для ввода.</param>
        public QuickAddForm(string title, string[] fieldNames)
            : this(title, fieldNames, null) { }

        /// <param name="title">Заголовок диалога.</param>
        /// <param name="fieldNames">Названия полей для ввода.</param>
        /// <param name="initialValues">Начальные значения полей (для режима редактирования).</param>
        public QuickAddForm(string title, string[] fieldNames, string[] initialValues)
        {
            _fieldNames    = fieldNames;
            _initialValues = initialValues;
            _textBoxes     = new TextBox[fieldNames.Length];
            InitializeComponent();
            this.Text = title;
            BuildFields();
        }

        /// <summary>Динамически строит поля ввода по переданным названиям.</summary>
        private void BuildFields()
        {
            int y = 16;
            for (int i = 0; i < _fieldNames.Length; i++)
            {
                var lbl = new Label { AutoSize = true, Location = new System.Drawing.Point(12, y), Text = _fieldNames[i] + ":" };
                var tb  = new TextBox { Location = new System.Drawing.Point(12, y + 18), Size = new System.Drawing.Size(320, 20), TabIndex = i };
                if (_initialValues != null && i < _initialValues.Length)
                    tb.Text = _initialValues[i];
                panelFields.Controls.Add(lbl);
                panelFields.Controls.Add(tb);
                _textBoxes[i] = tb;
                y += 52;
            }
            panelFields.Height  = y + 8;
            this.ClientSize     = new System.Drawing.Size(344, panelFields.Bottom + 50);
            buttonOk.Top        = panelFields.Bottom + 10;
            buttonCancel.Top    = panelFields.Bottom + 10;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Values = new string[_textBoxes.Length];
            for (int i = 0; i < _textBoxes.Length; i++)
                Values[i] = _textBoxes[i].Text;
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
