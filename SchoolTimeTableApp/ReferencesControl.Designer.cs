namespace testing
{
    partial class ReferencesControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        // Вспомогательный метод: стиль DataGridView (вызывается для всех 4 гридов)
        private static void StyleGrid(System.Windows.Forms.DataGridView g)
        {
            g.AllowUserToAddRows    = false;
            g.AllowUserToDeleteRows = false;
            g.ReadOnly              = true;
            g.RowHeadersWidth       = 40;
            g.SelectionMode         = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            g.Dock                  = System.Windows.Forms.DockStyle.Fill;
            g.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            g.RowTemplate.Height    = 28;
            g.ColumnHeadersDefaultCellStyle.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            g.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.SteelBlue;
            g.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersHeight = 32;
        }

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            // ── Teachers tab ──
            this.tabTeachers         = new System.Windows.Forms.TabPage();
            this.panelTeacher        = new System.Windows.Forms.Panel();
            this.labelTeacherName    = new System.Windows.Forms.Label();
            this.txtTeacherName      = new System.Windows.Forms.TextBox();
            this.labelTeacherHours   = new System.Windows.Forms.Label();
            this.txtTeacherHours     = new System.Windows.Forms.TextBox();
            this.buttonAddTeacher    = new System.Windows.Forms.Button();
            this.buttonEditTeacher   = new System.Windows.Forms.Button();
            this.buttonDeleteTeacher = new System.Windows.Forms.Button();
            this.gridTeachers        = new System.Windows.Forms.DataGridView();
            // ── Subjects tab ──
            this.tabSubjects         = new System.Windows.Forms.TabPage();
            this.panelSubject        = new System.Windows.Forms.Panel();
            this.labelSubjectName    = new System.Windows.Forms.Label();
            this.txtSubjectName      = new System.Windows.Forms.TextBox();
            this.labelSubjectDiff    = new System.Windows.Forms.Label();
            this.txtSubjectDiff      = new System.Windows.Forms.TextBox();
            this.buttonAddSubject    = new System.Windows.Forms.Button();
            this.buttonEditSubject   = new System.Windows.Forms.Button();
            this.buttonDeleteSubject = new System.Windows.Forms.Button();
            this.gridSubjects        = new System.Windows.Forms.DataGridView();
            // ── Classrooms tab ──
            this.tabClassrooms         = new System.Windows.Forms.TabPage();
            this.panelClassroom        = new System.Windows.Forms.Panel();
            this.labelRoomNumber       = new System.Windows.Forms.Label();
            this.txtRoomNumber         = new System.Windows.Forms.TextBox();
            this.labelRoomCap          = new System.Windows.Forms.Label();
            this.txtRoomCapacity       = new System.Windows.Forms.TextBox();
            this.labelRoomType         = new System.Windows.Forms.Label();
            this.comboClassroomType    = new System.Windows.Forms.ComboBox();
            this.buttonAddClassroom    = new System.Windows.Forms.Button();
            this.buttonEditClassroom   = new System.Windows.Forms.Button();
            this.buttonDeleteClassroom = new System.Windows.Forms.Button();
            this.gridClassrooms        = new System.Windows.Forms.DataGridView();
            // ── Classes tab ──
            this.tabClasses      = new System.Windows.Forms.TabPage();
            this.panelClass      = new System.Windows.Forms.Panel();
            this.labelParallel   = new System.Windows.Forms.Label();
            this.comboParallel   = new System.Windows.Forms.ComboBox();
            this.labelLetter     = new System.Windows.Forms.Label();
            this.comboLetter     = new System.Windows.Forms.ComboBox();
            this.labelNewLetter  = new System.Windows.Forms.Label();
            this.txtNewLetter    = new System.Windows.Forms.TextBox();
            this.buttonAddLetter = new System.Windows.Forms.Button();
            this.buttonAddClass  = new System.Windows.Forms.Button();
            this.buttonEditClass = new System.Windows.Forms.Button();
            this.buttonDeleteClass = new System.Windows.Forms.Button();
            this.gridClasses     = new System.Windows.Forms.DataGridView();

            var BIG = new System.Drawing.Font("Segoe UI", 10F);

            this.tabControl.SuspendLayout();
            this.tabTeachers.SuspendLayout();   this.panelTeacher.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTeachers)).BeginInit();
            this.tabSubjects.SuspendLayout();   this.panelSubject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSubjects)).BeginInit();
            this.tabClassrooms.SuspendLayout(); this.panelClassroom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridClassrooms)).BeginInit();
            this.tabClasses.SuspendLayout();    this.panelClass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridClasses)).BeginInit();
            this.SuspendLayout();

            // ── tabControl ─────────────────────────────────────────────────
            this.tabControl.Controls.Add(this.tabTeachers);
            this.tabControl.Controls.Add(this.tabSubjects);
            this.tabControl.Controls.Add(this.tabClassrooms);
            this.tabControl.Controls.Add(this.tabClasses);
            this.tabControl.Dock          = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font          = BIG;
            this.tabControl.Location      = new System.Drawing.Point(0, 0);
            this.tabControl.Name          = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size          = new System.Drawing.Size(1192, 684);
            this.tabControl.TabIndex      = 0;

            // ════════════════════ УЧИТЕЛЯ ════════════════════════════════════
            this.tabTeachers.Controls.Add(this.gridTeachers);
            this.tabTeachers.Controls.Add(this.panelTeacher);
            this.tabTeachers.Name = "tabTeachers"; this.tabTeachers.TabIndex = 0;
            this.tabTeachers.Text = "Учителя"; this.tabTeachers.UseVisualStyleBackColor = true;

            this.panelTeacher.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelTeacher.Controls.Add(this.labelTeacherName);
            this.panelTeacher.Controls.Add(this.txtTeacherName);
            this.panelTeacher.Controls.Add(this.labelTeacherHours);
            this.panelTeacher.Controls.Add(this.txtTeacherHours);
            this.panelTeacher.Controls.Add(this.buttonAddTeacher);
            this.panelTeacher.Controls.Add(this.buttonEditTeacher);
            this.panelTeacher.Controls.Add(this.buttonDeleteTeacher);
            this.panelTeacher.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTeacher.Name = "panelTeacher";
            this.panelTeacher.Size = new System.Drawing.Size(1184, 60);

            this.labelTeacherName.AutoSize = true;
            this.labelTeacherName.Font     = BIG;
            this.labelTeacherName.Location = new System.Drawing.Point(8, 18);
            this.labelTeacherName.Text     = "Фамилия И.О.:";
            this.txtTeacherName.Font       = BIG;
            this.txtTeacherName.Location   = new System.Drawing.Point(120, 14);
            this.txtTeacherName.Name       = "txtTeacherName";
            this.txtTeacherName.Size       = new System.Drawing.Size(280, 26);
            this.txtTeacherName.TabIndex   = 1;
            this.txtTeacherName.KeyDown   += new System.Windows.Forms.KeyEventHandler(this.txtTeacherName_KeyDown);

            this.labelTeacherHours.AutoSize = true;
            this.labelTeacherHours.Font     = BIG;
            this.labelTeacherHours.Location = new System.Drawing.Point(412, 18);
            this.labelTeacherHours.Text     = "Ставка:";
            this.txtTeacherHours.Font       = BIG;
            this.txtTeacherHours.Location   = new System.Drawing.Point(472, 14);
            this.txtTeacherHours.Name       = "txtTeacherHours";
            this.txtTeacherHours.Size       = new System.Drawing.Size(70, 26);
            this.txtTeacherHours.TabIndex   = 2;
            this.txtTeacherHours.KeyDown   += new System.Windows.Forms.KeyEventHandler(this.txtTeacherName_KeyDown);

            this.buttonAddTeacher.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAddTeacher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddTeacher.Font      = BIG;
            this.buttonAddTeacher.ForeColor = System.Drawing.Color.White;
            this.buttonAddTeacher.Location  = new System.Drawing.Point(554, 12);
            this.buttonAddTeacher.Name      = "buttonAddTeacher";
            this.buttonAddTeacher.Size      = new System.Drawing.Size(130, 32);
            this.buttonAddTeacher.TabIndex  = 3;
            this.buttonAddTeacher.Text      = "+ Добавить";
            this.buttonAddTeacher.UseVisualStyleBackColor = false;
            this.buttonAddTeacher.Click += new System.EventHandler(this.buttonAddTeacher_Click);

            this.buttonEditTeacher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEditTeacher.Font      = BIG;
            this.buttonEditTeacher.ForeColor = System.Drawing.Color.SteelBlue;
            this.buttonEditTeacher.Location  = new System.Drawing.Point(694, 12);
            this.buttonEditTeacher.Name      = "buttonEditTeacher";
            this.buttonEditTeacher.Size      = new System.Drawing.Size(130, 32);
            this.buttonEditTeacher.TabIndex  = 4;
            this.buttonEditTeacher.Text      = "Изменить";
            this.buttonEditTeacher.Click += new System.EventHandler(this.buttonEditTeacher_Click);

            this.buttonDeleteTeacher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteTeacher.Font      = BIG;
            this.buttonDeleteTeacher.ForeColor = System.Drawing.Color.Crimson;
            this.buttonDeleteTeacher.Location  = new System.Drawing.Point(834, 12);
            this.buttonDeleteTeacher.Name      = "buttonDeleteTeacher";
            this.buttonDeleteTeacher.Size      = new System.Drawing.Size(130, 32);
            this.buttonDeleteTeacher.TabIndex  = 5;
            this.buttonDeleteTeacher.Text      = "Удалить";
            this.buttonDeleteTeacher.Click += new System.EventHandler(this.buttonDeleteTeacher_Click);

            StyleGrid(this.gridTeachers);
            this.gridTeachers.Location = new System.Drawing.Point(0, 60);
            this.gridTeachers.Name     = "gridTeachers";
            this.gridTeachers.TabIndex = 6;
            this.gridTeachers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTeachers_CellDoubleClick);

            // ════════════════════ ПРЕДМЕТЫ ════════════════════════════════════
            this.tabSubjects.Controls.Add(this.gridSubjects);
            this.tabSubjects.Controls.Add(this.panelSubject);
            this.tabSubjects.Name = "tabSubjects"; this.tabSubjects.TabIndex = 1;
            this.tabSubjects.Text = "Предметы"; this.tabSubjects.UseVisualStyleBackColor = true;

            this.panelSubject.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelSubject.Controls.Add(this.labelSubjectName);
            this.panelSubject.Controls.Add(this.txtSubjectName);
            this.panelSubject.Controls.Add(this.labelSubjectDiff);
            this.panelSubject.Controls.Add(this.txtSubjectDiff);
            this.panelSubject.Controls.Add(this.buttonAddSubject);
            this.panelSubject.Controls.Add(this.buttonEditSubject);
            this.panelSubject.Controls.Add(this.buttonDeleteSubject);
            this.panelSubject.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSubject.Name = "panelSubject";
            this.panelSubject.Size = new System.Drawing.Size(1184, 60);

            this.labelSubjectName.AutoSize = true;
            this.labelSubjectName.Font     = BIG;
            this.labelSubjectName.Location = new System.Drawing.Point(8, 18);
            this.labelSubjectName.Text     = "Название:";
            this.txtSubjectName.Font       = BIG;
            this.txtSubjectName.Location   = new System.Drawing.Point(88, 14);
            this.txtSubjectName.Name       = "txtSubjectName";
            this.txtSubjectName.Size       = new System.Drawing.Size(260, 26);
            this.txtSubjectName.TabIndex   = 1;
            this.txtSubjectName.KeyDown   += new System.Windows.Forms.KeyEventHandler(this.txtSubjectName_KeyDown);

            this.labelSubjectDiff.AutoSize = true;
            this.labelSubjectDiff.Font     = BIG;
            this.labelSubjectDiff.Location = new System.Drawing.Point(360, 18);
            this.labelSubjectDiff.Text     = "Сложность:";
            this.txtSubjectDiff.Font       = BIG;
            this.txtSubjectDiff.Location   = new System.Drawing.Point(454, 14);
            this.txtSubjectDiff.Name       = "txtSubjectDiff";
            this.txtSubjectDiff.Size       = new System.Drawing.Size(60, 26);
            this.txtSubjectDiff.TabIndex   = 2;
            this.txtSubjectDiff.KeyDown   += new System.Windows.Forms.KeyEventHandler(this.txtSubjectName_KeyDown);

            this.buttonAddSubject.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAddSubject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddSubject.Font      = BIG;
            this.buttonAddSubject.ForeColor = System.Drawing.Color.White;
            this.buttonAddSubject.Location  = new System.Drawing.Point(526, 12);
            this.buttonAddSubject.Name      = "buttonAddSubject";
            this.buttonAddSubject.Size      = new System.Drawing.Size(130, 32);
            this.buttonAddSubject.TabIndex  = 3;
            this.buttonAddSubject.Text      = "+ Добавить";
            this.buttonAddSubject.UseVisualStyleBackColor = false;
            this.buttonAddSubject.Click += new System.EventHandler(this.buttonAddSubject_Click);

            this.buttonEditSubject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEditSubject.Font      = BIG;
            this.buttonEditSubject.ForeColor = System.Drawing.Color.SteelBlue;
            this.buttonEditSubject.Location  = new System.Drawing.Point(666, 12);
            this.buttonEditSubject.Name      = "buttonEditSubject";
            this.buttonEditSubject.Size      = new System.Drawing.Size(130, 32);
            this.buttonEditSubject.TabIndex  = 4;
            this.buttonEditSubject.Text      = "Изменить";
            this.buttonEditSubject.Click += new System.EventHandler(this.buttonEditSubject_Click);

            this.buttonDeleteSubject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteSubject.Font      = BIG;
            this.buttonDeleteSubject.ForeColor = System.Drawing.Color.Crimson;
            this.buttonDeleteSubject.Location  = new System.Drawing.Point(806, 12);
            this.buttonDeleteSubject.Name      = "buttonDeleteSubject";
            this.buttonDeleteSubject.Size      = new System.Drawing.Size(130, 32);
            this.buttonDeleteSubject.TabIndex  = 5;
            this.buttonDeleteSubject.Text      = "Удалить";
            this.buttonDeleteSubject.Click += new System.EventHandler(this.buttonDeleteSubject_Click);

            StyleGrid(this.gridSubjects);
            this.gridSubjects.Location = new System.Drawing.Point(0, 60);
            this.gridSubjects.Name     = "gridSubjects";
            this.gridSubjects.TabIndex = 6;
            this.gridSubjects.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSubjects_CellDoubleClick);

            // ════════════════════ КАБИНЕТЫ ════════════════════════════════════
            this.tabClassrooms.Controls.Add(this.gridClassrooms);
            this.tabClassrooms.Controls.Add(this.panelClassroom);
            this.tabClassrooms.Name = "tabClassrooms"; this.tabClassrooms.TabIndex = 2;
            this.tabClassrooms.Text = "Кабинеты"; this.tabClassrooms.UseVisualStyleBackColor = true;

            this.panelClassroom.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelClassroom.Controls.Add(this.labelRoomNumber);
            this.panelClassroom.Controls.Add(this.txtRoomNumber);
            this.panelClassroom.Controls.Add(this.labelRoomCap);
            this.panelClassroom.Controls.Add(this.txtRoomCapacity);
            this.panelClassroom.Controls.Add(this.labelRoomType);
            this.panelClassroom.Controls.Add(this.comboClassroomType);
            this.panelClassroom.Controls.Add(this.buttonAddClassroom);
            this.panelClassroom.Controls.Add(this.buttonEditClassroom);
            this.panelClassroom.Controls.Add(this.buttonDeleteClassroom);
            this.panelClassroom.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelClassroom.Name = "panelClassroom";
            this.panelClassroom.Size = new System.Drawing.Size(1184, 60);

            this.labelRoomNumber.AutoSize = true;
            this.labelRoomNumber.Font     = BIG;
            this.labelRoomNumber.Location = new System.Drawing.Point(8, 18);
            this.labelRoomNumber.Text     = "Номер:";
            this.txtRoomNumber.Font       = BIG;
            this.txtRoomNumber.Location   = new System.Drawing.Point(68, 14);
            this.txtRoomNumber.Name       = "txtRoomNumber";
            this.txtRoomNumber.Size       = new System.Drawing.Size(90, 26);
            this.txtRoomNumber.TabIndex   = 1;
            this.txtRoomNumber.KeyDown   += new System.Windows.Forms.KeyEventHandler(this.txtRoomNumber_KeyDown);

            this.labelRoomCap.AutoSize = true;
            this.labelRoomCap.Font     = BIG;
            this.labelRoomCap.Location = new System.Drawing.Point(170, 18);
            this.labelRoomCap.Text     = "Вместимость:";
            this.txtRoomCapacity.Font     = BIG;
            this.txtRoomCapacity.Location = new System.Drawing.Point(284, 14);
            this.txtRoomCapacity.Name     = "txtRoomCapacity";
            this.txtRoomCapacity.Size     = new System.Drawing.Size(70, 26);
            this.txtRoomCapacity.TabIndex = 2;
            this.txtRoomCapacity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRoomNumber_KeyDown);

            this.labelRoomType.AutoSize = true;
            this.labelRoomType.Font     = BIG;
            this.labelRoomType.Location = new System.Drawing.Point(366, 18);
            this.labelRoomType.Text     = "Тип:";
            this.comboClassroomType.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboClassroomType.Font              = BIG;
            this.comboClassroomType.FormattingEnabled = true;
            this.comboClassroomType.Location          = new System.Drawing.Point(400, 14);
            this.comboClassroomType.Name              = "comboClassroomType";
            this.comboClassroomType.Size              = new System.Drawing.Size(190, 28);
            this.comboClassroomType.TabIndex          = 3;

            this.buttonAddClassroom.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAddClassroom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddClassroom.Font      = BIG;
            this.buttonAddClassroom.ForeColor = System.Drawing.Color.White;
            this.buttonAddClassroom.Location  = new System.Drawing.Point(604, 12);
            this.buttonAddClassroom.Name      = "buttonAddClassroom";
            this.buttonAddClassroom.Size      = new System.Drawing.Size(130, 32);
            this.buttonAddClassroom.TabIndex  = 4;
            this.buttonAddClassroom.Text      = "+ Добавить";
            this.buttonAddClassroom.UseVisualStyleBackColor = false;
            this.buttonAddClassroom.Click += new System.EventHandler(this.buttonAddClassroom_Click);

            this.buttonEditClassroom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEditClassroom.Font      = BIG;
            this.buttonEditClassroom.ForeColor = System.Drawing.Color.SteelBlue;
            this.buttonEditClassroom.Location  = new System.Drawing.Point(744, 12);
            this.buttonEditClassroom.Name      = "buttonEditClassroom";
            this.buttonEditClassroom.Size      = new System.Drawing.Size(130, 32);
            this.buttonEditClassroom.TabIndex  = 5;
            this.buttonEditClassroom.Text      = "Изменить";
            this.buttonEditClassroom.Click += new System.EventHandler(this.buttonEditClassroom_Click);

            this.buttonDeleteClassroom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteClassroom.Font      = BIG;
            this.buttonDeleteClassroom.ForeColor = System.Drawing.Color.Crimson;
            this.buttonDeleteClassroom.Location  = new System.Drawing.Point(884, 12);
            this.buttonDeleteClassroom.Name      = "buttonDeleteClassroom";
            this.buttonDeleteClassroom.Size      = new System.Drawing.Size(130, 32);
            this.buttonDeleteClassroom.TabIndex  = 6;
            this.buttonDeleteClassroom.Text      = "Удалить";
            this.buttonDeleteClassroom.Click += new System.EventHandler(this.buttonDeleteClassroom_Click);

            StyleGrid(this.gridClassrooms);
            this.gridClassrooms.Location = new System.Drawing.Point(0, 60);
            this.gridClassrooms.Name     = "gridClassrooms";
            this.gridClassrooms.TabIndex = 7;
            this.gridClassrooms.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridClassrooms_CellDoubleClick);

            // ════════════════════ КЛАССЫ ══════════════════════════════════════
            this.tabClasses.Controls.Add(this.gridClasses);
            this.tabClasses.Controls.Add(this.panelClass);
            this.tabClasses.Name = "tabClasses"; this.tabClasses.TabIndex = 3;
            this.tabClasses.Text = "Классы"; this.tabClasses.UseVisualStyleBackColor = true;

            this.panelClass.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelClass.Controls.Add(this.labelParallel);
            this.panelClass.Controls.Add(this.comboParallel);
            this.panelClass.Controls.Add(this.labelLetter);
            this.panelClass.Controls.Add(this.comboLetter);
            this.panelClass.Controls.Add(this.buttonAddClass);
            this.panelClass.Controls.Add(this.buttonEditClass);
            this.panelClass.Controls.Add(this.buttonDeleteClass);
            this.panelClass.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelClass.Name = "panelClass";
            this.panelClass.Size = new System.Drawing.Size(1184, 60);

            this.labelParallel.AutoSize = true;
            this.labelParallel.Font     = BIG;
            this.labelParallel.Location = new System.Drawing.Point(8, 18);
            this.labelParallel.Text     = "Параллель:";
            this.comboParallel.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboParallel.Font              = BIG;
            this.comboParallel.FormattingEnabled = true;
            this.comboParallel.Location          = new System.Drawing.Point(96, 13);
            this.comboParallel.Name              = "comboParallel";
            this.comboParallel.Size              = new System.Drawing.Size(90, 28);
            this.comboParallel.TabIndex          = 1;

            this.labelLetter.AutoSize = true;
            this.labelLetter.Font     = BIG;
            this.labelLetter.Location = new System.Drawing.Point(198, 18);
            this.labelLetter.Text     = "Буква:";
            this.comboLetter.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLetter.Font              = BIG;
            this.comboLetter.FormattingEnabled = true;
            this.comboLetter.Location          = new System.Drawing.Point(250, 13);
            this.comboLetter.Name              = "comboLetter";
            this.comboLetter.Size              = new System.Drawing.Size(90, 28);
            this.comboLetter.TabIndex          = 2;

            this.buttonAddClass.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAddClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddClass.Font      = BIG;
            this.buttonAddClass.ForeColor = System.Drawing.Color.White;
            this.buttonAddClass.Location  = new System.Drawing.Point(352, 12);
            this.buttonAddClass.Name      = "buttonAddClass";
            this.buttonAddClass.Size      = new System.Drawing.Size(130, 32);
            this.buttonAddClass.TabIndex  = 3;
            this.buttonAddClass.Text      = "+ Добавить";
            this.buttonAddClass.UseVisualStyleBackColor = false;
            this.buttonAddClass.Click += new System.EventHandler(this.buttonAddClass_Click);

            this.buttonEditClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEditClass.Font      = BIG;
            this.buttonEditClass.ForeColor = System.Drawing.Color.SteelBlue;
            this.buttonEditClass.Location  = new System.Drawing.Point(492, 12);
            this.buttonEditClass.Name      = "buttonEditClass";
            this.buttonEditClass.Size      = new System.Drawing.Size(130, 32);
            this.buttonEditClass.TabIndex  = 4;
            this.buttonEditClass.Text      = "Изменить";
            this.buttonEditClass.Click += new System.EventHandler(this.buttonEditClass_Click);

            this.buttonDeleteClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteClass.Font      = BIG;
            this.buttonDeleteClass.ForeColor = System.Drawing.Color.Crimson;
            this.buttonDeleteClass.Location  = new System.Drawing.Point(632, 12);
            this.buttonDeleteClass.Name      = "buttonDeleteClass";
            this.buttonDeleteClass.Size      = new System.Drawing.Size(130, 32);
            this.buttonDeleteClass.TabIndex  = 5;
            this.buttonDeleteClass.Text      = "Удалить";
            this.buttonDeleteClass.Click += new System.EventHandler(this.buttonDeleteClass_Click);

            // Разделитель + добавление буквы класса
            var sepClass = new System.Windows.Forms.Label();
            sepClass.AutoSize    = false;
            sepClass.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            sepClass.Location    = new System.Drawing.Point(775, 10);
            sepClass.Size        = new System.Drawing.Size(2, 36);
            this.panelClass.Controls.Add(sepClass);

            this.labelNewLetter.AutoSize = true;
            this.labelNewLetter.Font     = BIG;
            this.labelNewLetter.Location = new System.Drawing.Point(786, 18);
            this.labelNewLetter.Text     = "Новая буква:";

            this.txtNewLetter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNewLetter.Font            = BIG;
            this.txtNewLetter.Location        = new System.Drawing.Point(890, 14);
            this.txtNewLetter.MaxLength       = 1;
            this.txtNewLetter.Name            = "txtNewLetter";
            this.txtNewLetter.Size            = new System.Drawing.Size(40, 26);
            this.txtNewLetter.TabIndex        = 6;
            this.txtNewLetter.KeyDown        += new System.Windows.Forms.KeyEventHandler(this.txtNewLetter_KeyDown);

            this.buttonAddLetter.BackColor = System.Drawing.Color.SeaGreen;
            this.buttonAddLetter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddLetter.Font      = BIG;
            this.buttonAddLetter.ForeColor = System.Drawing.Color.White;
            this.buttonAddLetter.Location  = new System.Drawing.Point(940, 12);
            this.buttonAddLetter.Name      = "buttonAddLetter";
            this.buttonAddLetter.Size      = new System.Drawing.Size(170, 32);
            this.buttonAddLetter.TabIndex  = 7;
            this.buttonAddLetter.Text      = "+ Добавить букву";
            this.buttonAddLetter.UseVisualStyleBackColor = false;
            this.buttonAddLetter.Click += new System.EventHandler(this.buttonAddLetter_Click);

            this.panelClass.Controls.Add(this.labelNewLetter);
            this.panelClass.Controls.Add(this.txtNewLetter);
            this.panelClass.Controls.Add(this.buttonAddLetter);

            StyleGrid(this.gridClasses);
            this.gridClasses.Location = new System.Drawing.Point(0, 60);
            this.gridClasses.Name     = "gridClasses";
            this.gridClasses.TabIndex = 8;
            this.gridClasses.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridClasses_CellDoubleClick);

            // ── ReferencesControl ──────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "ReferencesControl";
            this.Size = new System.Drawing.Size(1192, 684);

            this.tabControl.ResumeLayout(false);
            this.tabTeachers.ResumeLayout(false);    this.panelTeacher.ResumeLayout(false);   this.panelTeacher.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTeachers)).EndInit();
            this.tabSubjects.ResumeLayout(false);    this.panelSubject.ResumeLayout(false);    this.panelSubject.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSubjects)).EndInit();
            this.tabClassrooms.ResumeLayout(false);  this.panelClassroom.ResumeLayout(false);  this.panelClassroom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridClassrooms)).EndInit();
            this.tabClasses.ResumeLayout(false);     this.panelClass.ResumeLayout(false);      this.panelClass.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridClasses)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.TabControl     tabControl;
        private System.Windows.Forms.TabPage        tabTeachers;
        private System.Windows.Forms.Panel          panelTeacher;
        private System.Windows.Forms.Label          labelTeacherName;
        private System.Windows.Forms.TextBox        txtTeacherName;
        private System.Windows.Forms.Label          labelTeacherHours;
        private System.Windows.Forms.TextBox        txtTeacherHours;
        private System.Windows.Forms.Button         buttonAddTeacher;
        private System.Windows.Forms.Button         buttonEditTeacher;
        private System.Windows.Forms.Button         buttonDeleteTeacher;
        private System.Windows.Forms.DataGridView   gridTeachers;
        private System.Windows.Forms.TabPage        tabSubjects;
        private System.Windows.Forms.Panel          panelSubject;
        private System.Windows.Forms.Label          labelSubjectName;
        private System.Windows.Forms.TextBox        txtSubjectName;
        private System.Windows.Forms.Label          labelSubjectDiff;
        private System.Windows.Forms.TextBox        txtSubjectDiff;
        private System.Windows.Forms.Button         buttonAddSubject;
        private System.Windows.Forms.Button         buttonEditSubject;
        private System.Windows.Forms.Button         buttonDeleteSubject;
        private System.Windows.Forms.DataGridView   gridSubjects;
        private System.Windows.Forms.TabPage        tabClassrooms;
        private System.Windows.Forms.Panel          panelClassroom;
        private System.Windows.Forms.Label          labelRoomNumber;
        private System.Windows.Forms.TextBox        txtRoomNumber;
        private System.Windows.Forms.Label          labelRoomCap;
        private System.Windows.Forms.TextBox        txtRoomCapacity;
        private System.Windows.Forms.Label          labelRoomType;
        private System.Windows.Forms.ComboBox       comboClassroomType;
        private System.Windows.Forms.Button         buttonAddClassroom;
        private System.Windows.Forms.Button         buttonEditClassroom;
        private System.Windows.Forms.Button         buttonDeleteClassroom;
        private System.Windows.Forms.DataGridView   gridClassrooms;
        private System.Windows.Forms.TabPage        tabClasses;
        private System.Windows.Forms.Panel          panelClass;
        private System.Windows.Forms.Label          labelParallel;
        private System.Windows.Forms.ComboBox       comboParallel;
        private System.Windows.Forms.Label          labelLetter;
        private System.Windows.Forms.ComboBox       comboLetter;
        private System.Windows.Forms.Button         buttonAddClass;
        private System.Windows.Forms.Button         buttonEditClass;
        private System.Windows.Forms.Button         buttonDeleteClass;
        private System.Windows.Forms.Label          labelNewLetter;
        private System.Windows.Forms.TextBox        txtNewLetter;
        private System.Windows.Forms.Button         buttonAddLetter;
        private System.Windows.Forms.DataGridView   gridClasses;
    }
}
