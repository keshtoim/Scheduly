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
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            // ── Teachers ──
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
            // ── Subjects ──
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
            // ── SubjectByParallel ──
            this.tabSubjPar            = new System.Windows.Forms.TabPage();
            this.panelSubjPar          = new System.Windows.Forms.Panel();
            this.labelSubjParGrade     = new System.Windows.Forms.Label();
            this.comboSubjParGrade     = new System.Windows.Forms.ComboBox();
            this.labelSubjParSubject   = new System.Windows.Forms.Label();
            this.comboSubjParSubject   = new System.Windows.Forms.ComboBox();
            this.labelSubjParDiff      = new System.Windows.Forms.Label();
            this.txtSubjParDiff        = new System.Windows.Forms.TextBox();
            this.buttonAddSubjPar      = new System.Windows.Forms.Button();
            this.buttonDeleteSubjPar   = new System.Windows.Forms.Button();
            this.gridSubjPar           = new System.Windows.Forms.DataGridView();
            // ── Classrooms ──
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
            // ── Classes ──
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

            this.tabControl.SuspendLayout();
            this.tabTeachers.SuspendLayout();   this.panelTeacher.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTeachers)).BeginInit();
            this.tabSubjects.SuspendLayout();   this.panelSubject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSubjects)).BeginInit();
            this.tabSubjPar.SuspendLayout();    this.panelSubjPar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSubjPar)).BeginInit();
            this.tabClassrooms.SuspendLayout(); this.panelClassroom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridClassrooms)).BeginInit();
            this.tabClasses.SuspendLayout();    this.panelClass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridClasses)).BeginInit();
            this.SuspendLayout();

            // ── tabControl ─────────────────────────────────────────────────
            this.tabControl.Controls.Add(this.tabTeachers);
            this.tabControl.Controls.Add(this.tabSubjects);
            this.tabControl.Controls.Add(this.tabSubjPar);
            this.tabControl.Controls.Add(this.tabClassrooms);
            this.tabControl.Controls.Add(this.tabClasses);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1192, 684);
            this.tabControl.TabIndex = 0;

            // ════════════ УЧИТЕЛЯ ════════════════════════════════════════════
            this.tabTeachers.Controls.Add(this.gridTeachers);
            this.tabTeachers.Controls.Add(this.panelTeacher);
            this.tabTeachers.Name = "tabTeachers"; this.tabTeachers.TabIndex = 0;
            this.tabTeachers.Text = "Учителя"; this.tabTeachers.UseVisualStyleBackColor = true;

            this.panelTeacher.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelTeacher.Controls.Add(this.labelTeacherName); this.panelTeacher.Controls.Add(this.txtTeacherName);
            this.panelTeacher.Controls.Add(this.labelTeacherHours); this.panelTeacher.Controls.Add(this.txtTeacherHours);
            this.panelTeacher.Controls.Add(this.buttonAddTeacher); this.panelTeacher.Controls.Add(this.buttonEditTeacher); this.panelTeacher.Controls.Add(this.buttonDeleteTeacher);
            this.panelTeacher.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTeacher.Name = "panelTeacher"; this.panelTeacher.Size = new System.Drawing.Size(1184, 46);

            this.labelTeacherName.AutoSize = true; this.labelTeacherName.Location = new System.Drawing.Point(8, 14);
            this.labelTeacherName.Name = "labelTeacherName"; this.labelTeacherName.Text = "Имя:";
            this.txtTeacherName.Location = new System.Drawing.Point(44, 10); this.txtTeacherName.Name = "txtTeacherName";
            this.txtTeacherName.Size = new System.Drawing.Size(220, 20); this.txtTeacherName.TabIndex = 1;
            this.txtTeacherName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTeacherName_KeyDown);
            this.labelTeacherHours.AutoSize = true; this.labelTeacherHours.Location = new System.Drawing.Point(274, 14);
            this.labelTeacherHours.Name = "labelTeacherHours"; this.labelTeacherHours.Text = "Часов:";
            this.txtTeacherHours.Location = new System.Drawing.Point(320, 10); this.txtTeacherHours.Name = "txtTeacherHours";
            this.txtTeacherHours.Size = new System.Drawing.Size(60, 20); this.txtTeacherHours.TabIndex = 2;
            this.txtTeacherHours.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTeacherName_KeyDown);

            this.buttonAddTeacher.BackColor = System.Drawing.Color.SteelBlue; this.buttonAddTeacher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddTeacher.ForeColor = System.Drawing.Color.White; this.buttonAddTeacher.Location = new System.Drawing.Point(392, 9);
            this.buttonAddTeacher.Name = "buttonAddTeacher"; this.buttonAddTeacher.Size = new System.Drawing.Size(100, 26);
            this.buttonAddTeacher.TabIndex = 3; this.buttonAddTeacher.Text = "+ Добавить"; this.buttonAddTeacher.UseVisualStyleBackColor = false;
            this.buttonAddTeacher.Click += new System.EventHandler(this.buttonAddTeacher_Click);
            this.buttonEditTeacher.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.buttonEditTeacher.ForeColor = System.Drawing.Color.SteelBlue;
            this.buttonEditTeacher.Location = new System.Drawing.Point(500, 9); this.buttonEditTeacher.Name = "buttonEditTeacher";
            this.buttonEditTeacher.Size = new System.Drawing.Size(100, 26); this.buttonEditTeacher.TabIndex = 4;
            this.buttonEditTeacher.Text = "Изменить"; this.buttonEditTeacher.Click += new System.EventHandler(this.buttonEditTeacher_Click);
            this.buttonDeleteTeacher.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.buttonDeleteTeacher.ForeColor = System.Drawing.Color.Crimson;
            this.buttonDeleteTeacher.Location = new System.Drawing.Point(608, 9); this.buttonDeleteTeacher.Name = "buttonDeleteTeacher";
            this.buttonDeleteTeacher.Size = new System.Drawing.Size(100, 26); this.buttonDeleteTeacher.TabIndex = 5;
            this.buttonDeleteTeacher.Text = "Удалить"; this.buttonDeleteTeacher.Click += new System.EventHandler(this.buttonDeleteTeacher_Click);

            this.gridTeachers.AllowUserToAddRows = false; this.gridTeachers.AllowUserToDeleteRows = false;
            this.gridTeachers.Dock = System.Windows.Forms.DockStyle.Fill; this.gridTeachers.Location = new System.Drawing.Point(0, 46);
            this.gridTeachers.Name = "gridTeachers"; this.gridTeachers.ReadOnly = true; this.gridTeachers.RowHeadersWidth = 40;
            this.gridTeachers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTeachers.TabIndex = 6;
            this.gridTeachers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTeachers_CellDoubleClick);

            // ════════════ ПРЕДМЕТЫ ════════════════════════════════════════════
            this.tabSubjects.Controls.Add(this.gridSubjects); this.tabSubjects.Controls.Add(this.panelSubject);
            this.tabSubjects.Name = "tabSubjects"; this.tabSubjects.TabIndex = 1;
            this.tabSubjects.Text = "Предметы"; this.tabSubjects.UseVisualStyleBackColor = true;

            this.panelSubject.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelSubject.Controls.Add(this.labelSubjectName); this.panelSubject.Controls.Add(this.txtSubjectName);
            this.panelSubject.Controls.Add(this.labelSubjectDiff); this.panelSubject.Controls.Add(this.txtSubjectDiff);
            this.panelSubject.Controls.Add(this.buttonAddSubject); this.panelSubject.Controls.Add(this.buttonEditSubject); this.panelSubject.Controls.Add(this.buttonDeleteSubject);
            this.panelSubject.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSubject.Name = "panelSubject"; this.panelSubject.Size = new System.Drawing.Size(1184, 46);

            this.labelSubjectName.AutoSize = true; this.labelSubjectName.Location = new System.Drawing.Point(8, 14);
            this.labelSubjectName.Name = "labelSubjectName"; this.labelSubjectName.Text = "Название:";
            this.txtSubjectName.Location = new System.Drawing.Point(70, 10); this.txtSubjectName.Name = "txtSubjectName";
            this.txtSubjectName.Size = new System.Drawing.Size(200, 20); this.txtSubjectName.TabIndex = 1;
            this.txtSubjectName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubjectName_KeyDown);
            this.labelSubjectDiff.AutoSize = true; this.labelSubjectDiff.Location = new System.Drawing.Point(280, 14);
            this.labelSubjectDiff.Name = "labelSubjectDiff"; this.labelSubjectDiff.Text = "Сложность:";
            this.txtSubjectDiff.Location = new System.Drawing.Point(348, 10); this.txtSubjectDiff.Name = "txtSubjectDiff";
            this.txtSubjectDiff.Size = new System.Drawing.Size(50, 20); this.txtSubjectDiff.TabIndex = 2;
            this.txtSubjectDiff.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubjectName_KeyDown);

            this.buttonAddSubject.BackColor = System.Drawing.Color.SteelBlue; this.buttonAddSubject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddSubject.ForeColor = System.Drawing.Color.White; this.buttonAddSubject.Location = new System.Drawing.Point(410, 9);
            this.buttonAddSubject.Name = "buttonAddSubject"; this.buttonAddSubject.Size = new System.Drawing.Size(100, 26);
            this.buttonAddSubject.TabIndex = 3; this.buttonAddSubject.Text = "+ Добавить"; this.buttonAddSubject.UseVisualStyleBackColor = false;
            this.buttonAddSubject.Click += new System.EventHandler(this.buttonAddSubject_Click);
            this.buttonEditSubject.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.buttonEditSubject.ForeColor = System.Drawing.Color.SteelBlue;
            this.buttonEditSubject.Location = new System.Drawing.Point(518, 9); this.buttonEditSubject.Name = "buttonEditSubject";
            this.buttonEditSubject.Size = new System.Drawing.Size(100, 26); this.buttonEditSubject.TabIndex = 4;
            this.buttonEditSubject.Text = "Изменить"; this.buttonEditSubject.Click += new System.EventHandler(this.buttonEditSubject_Click);
            this.buttonDeleteSubject.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.buttonDeleteSubject.ForeColor = System.Drawing.Color.Crimson;
            this.buttonDeleteSubject.Location = new System.Drawing.Point(626, 9); this.buttonDeleteSubject.Name = "buttonDeleteSubject";
            this.buttonDeleteSubject.Size = new System.Drawing.Size(100, 26); this.buttonDeleteSubject.TabIndex = 5;
            this.buttonDeleteSubject.Text = "Удалить"; this.buttonDeleteSubject.Click += new System.EventHandler(this.buttonDeleteSubject_Click);

            this.gridSubjects.AllowUserToAddRows = false; this.gridSubjects.AllowUserToDeleteRows = false;
            this.gridSubjects.Dock = System.Windows.Forms.DockStyle.Fill; this.gridSubjects.Location = new System.Drawing.Point(0, 46);
            this.gridSubjects.Name = "gridSubjects"; this.gridSubjects.ReadOnly = true; this.gridSubjects.RowHeadersWidth = 40;
            this.gridSubjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSubjects.TabIndex = 6;
            this.gridSubjects.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSubjects_CellDoubleClick);

            // ════════════ ПРЕДМЕТЫ ПО ПАРАЛЛЕЛЯМ ═════════════════════════════
            this.tabSubjPar.Controls.Add(this.gridSubjPar); this.tabSubjPar.Controls.Add(this.panelSubjPar);
            this.tabSubjPar.Name = "tabSubjPar"; this.tabSubjPar.TabIndex = 2;
            this.tabSubjPar.Text = "Предметы / Парал."; this.tabSubjPar.UseVisualStyleBackColor = true;

            this.panelSubjPar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelSubjPar.Controls.Add(this.labelSubjParGrade); this.panelSubjPar.Controls.Add(this.comboSubjParGrade);
            this.panelSubjPar.Controls.Add(this.labelSubjParSubject); this.panelSubjPar.Controls.Add(this.comboSubjParSubject);
            this.panelSubjPar.Controls.Add(this.labelSubjParDiff); this.panelSubjPar.Controls.Add(this.txtSubjParDiff);
            this.panelSubjPar.Controls.Add(this.buttonAddSubjPar); this.panelSubjPar.Controls.Add(this.buttonDeleteSubjPar);
            this.panelSubjPar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSubjPar.Name = "panelSubjPar"; this.panelSubjPar.Size = new System.Drawing.Size(1184, 46);

            this.labelSubjParGrade.AutoSize = true; this.labelSubjParGrade.Location = new System.Drawing.Point(8, 14);
            this.labelSubjParGrade.Name = "labelSubjParGrade"; this.labelSubjParGrade.Text = "Парал.:";
            this.comboSubjParGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSubjParGrade.FormattingEnabled = true;
            this.comboSubjParGrade.Location = new System.Drawing.Point(54, 10); this.comboSubjParGrade.Name = "comboSubjParGrade";
            this.comboSubjParGrade.Size = new System.Drawing.Size(80, 21); this.comboSubjParGrade.TabIndex = 1;

            this.labelSubjParSubject.AutoSize = true; this.labelSubjParSubject.Location = new System.Drawing.Point(144, 14);
            this.labelSubjParSubject.Name = "labelSubjParSubject"; this.labelSubjParSubject.Text = "Предмет:";
            this.comboSubjParSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSubjParSubject.FormattingEnabled = true;
            this.comboSubjParSubject.Location = new System.Drawing.Point(202, 10); this.comboSubjParSubject.Name = "comboSubjParSubject";
            this.comboSubjParSubject.Size = new System.Drawing.Size(320, 21); this.comboSubjParSubject.TabIndex = 2;

            this.labelSubjParDiff.AutoSize = true; this.labelSubjParDiff.Location = new System.Drawing.Point(532, 14);
            this.labelSubjParDiff.Name = "labelSubjParDiff"; this.labelSubjParDiff.Text = "Сложн.:";
            this.txtSubjParDiff.Location = new System.Drawing.Point(580, 10); this.txtSubjParDiff.Name = "txtSubjParDiff";
            this.txtSubjParDiff.Size = new System.Drawing.Size(50, 20); this.txtSubjParDiff.TabIndex = 3;
            this.txtSubjParDiff.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubjParDiff_KeyDown);

            this.buttonAddSubjPar.BackColor = System.Drawing.Color.SteelBlue; this.buttonAddSubjPar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddSubjPar.ForeColor = System.Drawing.Color.White; this.buttonAddSubjPar.Location = new System.Drawing.Point(642, 9);
            this.buttonAddSubjPar.Name = "buttonAddSubjPar"; this.buttonAddSubjPar.Size = new System.Drawing.Size(110, 26);
            this.buttonAddSubjPar.TabIndex = 4; this.buttonAddSubjPar.Text = "+ Добавить"; this.buttonAddSubjPar.UseVisualStyleBackColor = false;
            this.buttonAddSubjPar.Click += new System.EventHandler(this.buttonAddSubjPar_Click);
            this.buttonDeleteSubjPar.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.buttonDeleteSubjPar.ForeColor = System.Drawing.Color.Crimson;
            this.buttonDeleteSubjPar.Location = new System.Drawing.Point(760, 9); this.buttonDeleteSubjPar.Name = "buttonDeleteSubjPar";
            this.buttonDeleteSubjPar.Size = new System.Drawing.Size(110, 26); this.buttonDeleteSubjPar.TabIndex = 5;
            this.buttonDeleteSubjPar.Text = "Удалить"; this.buttonDeleteSubjPar.Click += new System.EventHandler(this.buttonDeleteSubjPar_Click);

            this.gridSubjPar.AllowUserToAddRows = false; this.gridSubjPar.AllowUserToDeleteRows = false;
            this.gridSubjPar.Dock = System.Windows.Forms.DockStyle.Fill; this.gridSubjPar.Location = new System.Drawing.Point(0, 46);
            this.gridSubjPar.Name = "gridSubjPar"; this.gridSubjPar.ReadOnly = true; this.gridSubjPar.RowHeadersWidth = 40;
            this.gridSubjPar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSubjPar.TabIndex = 6;
            this.gridSubjPar.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSubjPar_CellDoubleClick);

            // ════════════ КАБИНЕТЫ ════════════════════════════════════════════
            this.tabClassrooms.Controls.Add(this.gridClassrooms); this.tabClassrooms.Controls.Add(this.panelClassroom);
            this.tabClassrooms.Name = "tabClassrooms"; this.tabClassrooms.TabIndex = 3;
            this.tabClassrooms.Text = "Кабинеты"; this.tabClassrooms.UseVisualStyleBackColor = true;

            this.panelClassroom.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelClassroom.Controls.Add(this.labelRoomNumber); this.panelClassroom.Controls.Add(this.txtRoomNumber);
            this.panelClassroom.Controls.Add(this.labelRoomCap); this.panelClassroom.Controls.Add(this.txtRoomCapacity);
            this.panelClassroom.Controls.Add(this.labelRoomType); this.panelClassroom.Controls.Add(this.comboClassroomType);
            this.panelClassroom.Controls.Add(this.buttonAddClassroom); this.panelClassroom.Controls.Add(this.buttonEditClassroom); this.panelClassroom.Controls.Add(this.buttonDeleteClassroom);
            this.panelClassroom.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelClassroom.Name = "panelClassroom"; this.panelClassroom.Size = new System.Drawing.Size(1184, 46);

            this.labelRoomNumber.AutoSize = true; this.labelRoomNumber.Location = new System.Drawing.Point(8, 14);
            this.labelRoomNumber.Name = "labelRoomNumber"; this.labelRoomNumber.Text = "Номер:";
            this.txtRoomNumber.Location = new System.Drawing.Point(58, 10); this.txtRoomNumber.Name = "txtRoomNumber";
            this.txtRoomNumber.Size = new System.Drawing.Size(80, 20); this.txtRoomNumber.TabIndex = 1;
            this.txtRoomNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRoomNumber_KeyDown);
            this.labelRoomCap.AutoSize = true; this.labelRoomCap.Location = new System.Drawing.Point(148, 14);
            this.labelRoomCap.Name = "labelRoomCap"; this.labelRoomCap.Text = "Вместимость:";
            this.txtRoomCapacity.Location = new System.Drawing.Point(232, 10); this.txtRoomCapacity.Name = "txtRoomCapacity";
            this.txtRoomCapacity.Size = new System.Drawing.Size(60, 20); this.txtRoomCapacity.TabIndex = 2;
            this.txtRoomCapacity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRoomNumber_KeyDown);
            this.labelRoomType.AutoSize = true; this.labelRoomType.Location = new System.Drawing.Point(302, 14);
            this.labelRoomType.Name = "labelRoomType"; this.labelRoomType.Text = "Тип:";
            this.comboClassroomType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboClassroomType.FormattingEnabled = true;
            this.comboClassroomType.Location = new System.Drawing.Point(332, 10); this.comboClassroomType.Name = "comboClassroomType";
            this.comboClassroomType.Size = new System.Drawing.Size(160, 21); this.comboClassroomType.TabIndex = 3;

            this.buttonAddClassroom.BackColor = System.Drawing.Color.SteelBlue; this.buttonAddClassroom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddClassroom.ForeColor = System.Drawing.Color.White; this.buttonAddClassroom.Location = new System.Drawing.Point(504, 9);
            this.buttonAddClassroom.Name = "buttonAddClassroom"; this.buttonAddClassroom.Size = new System.Drawing.Size(100, 26);
            this.buttonAddClassroom.TabIndex = 4; this.buttonAddClassroom.Text = "+ Добавить"; this.buttonAddClassroom.UseVisualStyleBackColor = false;
            this.buttonAddClassroom.Click += new System.EventHandler(this.buttonAddClassroom_Click);
            this.buttonEditClassroom.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.buttonEditClassroom.ForeColor = System.Drawing.Color.SteelBlue;
            this.buttonEditClassroom.Location = new System.Drawing.Point(612, 9); this.buttonEditClassroom.Name = "buttonEditClassroom";
            this.buttonEditClassroom.Size = new System.Drawing.Size(100, 26); this.buttonEditClassroom.TabIndex = 5;
            this.buttonEditClassroom.Text = "Изменить"; this.buttonEditClassroom.Click += new System.EventHandler(this.buttonEditClassroom_Click);
            this.buttonDeleteClassroom.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.buttonDeleteClassroom.ForeColor = System.Drawing.Color.Crimson;
            this.buttonDeleteClassroom.Location = new System.Drawing.Point(720, 9); this.buttonDeleteClassroom.Name = "buttonDeleteClassroom";
            this.buttonDeleteClassroom.Size = new System.Drawing.Size(100, 26); this.buttonDeleteClassroom.TabIndex = 6;
            this.buttonDeleteClassroom.Text = "Удалить"; this.buttonDeleteClassroom.Click += new System.EventHandler(this.buttonDeleteClassroom_Click);

            this.gridClassrooms.AllowUserToAddRows = false; this.gridClassrooms.AllowUserToDeleteRows = false;
            this.gridClassrooms.Dock = System.Windows.Forms.DockStyle.Fill; this.gridClassrooms.Location = new System.Drawing.Point(0, 46);
            this.gridClassrooms.Name = "gridClassrooms"; this.gridClassrooms.ReadOnly = true; this.gridClassrooms.RowHeadersWidth = 40;
            this.gridClassrooms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridClassrooms.TabIndex = 7;
            this.gridClassrooms.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridClassrooms_CellDoubleClick);

            // ════════════ КЛАССЫ ══════════════════════════════════════════════
            this.tabClasses.Controls.Add(this.gridClasses); this.tabClasses.Controls.Add(this.panelClass);
            this.tabClasses.Name = "tabClasses"; this.tabClasses.TabIndex = 4;
            this.tabClasses.Text = "Классы"; this.tabClasses.UseVisualStyleBackColor = true;

            this.panelClass.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelClass.Controls.Add(this.labelParallel); this.panelClass.Controls.Add(this.comboParallel);
            this.panelClass.Controls.Add(this.labelLetter); this.panelClass.Controls.Add(this.comboLetter);
            this.panelClass.Controls.Add(this.buttonAddClass); this.panelClass.Controls.Add(this.buttonEditClass); this.panelClass.Controls.Add(this.buttonDeleteClass);
            this.panelClass.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelClass.Name = "panelClass"; this.panelClass.Size = new System.Drawing.Size(1184, 46);

            this.labelParallel.AutoSize = true; this.labelParallel.Location = new System.Drawing.Point(8, 14);
            this.labelParallel.Name = "labelParallel"; this.labelParallel.Text = "Параллель:";
            this.comboParallel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboParallel.FormattingEnabled = true;
            this.comboParallel.Location = new System.Drawing.Point(76, 10); this.comboParallel.Name = "comboParallel";
            this.comboParallel.Size = new System.Drawing.Size(80, 21); this.comboParallel.TabIndex = 1;
            this.labelLetter.AutoSize = true; this.labelLetter.Location = new System.Drawing.Point(166, 14);
            this.labelLetter.Name = "labelLetter"; this.labelLetter.Text = "Буква:";
            this.comboLetter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLetter.FormattingEnabled = true;
            this.comboLetter.Location = new System.Drawing.Point(210, 10); this.comboLetter.Name = "comboLetter";
            this.comboLetter.Size = new System.Drawing.Size(80, 21); this.comboLetter.TabIndex = 2;

            this.buttonAddClass.BackColor = System.Drawing.Color.SteelBlue; this.buttonAddClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddClass.ForeColor = System.Drawing.Color.White; this.buttonAddClass.Location = new System.Drawing.Point(302, 9);
            this.buttonAddClass.Name = "buttonAddClass"; this.buttonAddClass.Size = new System.Drawing.Size(100, 26);
            this.buttonAddClass.TabIndex = 3; this.buttonAddClass.Text = "+ Добавить"; this.buttonAddClass.UseVisualStyleBackColor = false;
            this.buttonAddClass.Click += new System.EventHandler(this.buttonAddClass_Click);
            this.buttonEditClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.buttonEditClass.ForeColor = System.Drawing.Color.SteelBlue;
            this.buttonEditClass.Location = new System.Drawing.Point(410, 9); this.buttonEditClass.Name = "buttonEditClass";
            this.buttonEditClass.Size = new System.Drawing.Size(100, 26); this.buttonEditClass.TabIndex = 4;
            this.buttonEditClass.Text = "Изменить"; this.buttonEditClass.Click += new System.EventHandler(this.buttonEditClass_Click);
            this.buttonDeleteClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.buttonDeleteClass.ForeColor = System.Drawing.Color.Crimson;
            this.buttonDeleteClass.Location = new System.Drawing.Point(518, 9); this.buttonDeleteClass.Name = "buttonDeleteClass";
            this.buttonDeleteClass.Size = new System.Drawing.Size(100, 26); this.buttonDeleteClass.TabIndex = 5;
            this.buttonDeleteClass.Text = "Удалить"; this.buttonDeleteClass.Click += new System.EventHandler(this.buttonDeleteClass_Click);

            var sepClass = new System.Windows.Forms.Label();
            sepClass.AutoSize = false; sepClass.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            sepClass.Location = new System.Drawing.Point(622, 9); sepClass.Size = new System.Drawing.Size(2, 26);
            this.panelClass.Controls.Add(sepClass);
            this.labelNewLetter.AutoSize = true; this.labelNewLetter.Location = new System.Drawing.Point(634, 14);
            this.labelNewLetter.Name = "labelNewLetter"; this.labelNewLetter.Text = "Новая буква:";
            this.txtNewLetter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNewLetter.Location = new System.Drawing.Point(714, 11); this.txtNewLetter.MaxLength = 1;
            this.txtNewLetter.Name = "txtNewLetter"; this.txtNewLetter.Size = new System.Drawing.Size(36, 20); this.txtNewLetter.TabIndex = 6;
            this.txtNewLetter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewLetter_KeyDown);
            this.buttonAddLetter.BackColor = System.Drawing.Color.SeaGreen; this.buttonAddLetter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddLetter.ForeColor = System.Drawing.Color.White; this.buttonAddLetter.Location = new System.Drawing.Point(758, 9);
            this.buttonAddLetter.Name = "buttonAddLetter"; this.buttonAddLetter.Size = new System.Drawing.Size(130, 26);
            this.buttonAddLetter.TabIndex = 7; this.buttonAddLetter.Text = "+ Добавить букву"; this.buttonAddLetter.UseVisualStyleBackColor = false;
            this.buttonAddLetter.Click += new System.EventHandler(this.buttonAddLetter_Click);
            this.panelClass.Controls.Add(this.labelNewLetter);
            this.panelClass.Controls.Add(this.txtNewLetter);
            this.panelClass.Controls.Add(this.buttonAddLetter);

            this.gridClasses.AllowUserToAddRows = false; this.gridClasses.AllowUserToDeleteRows = false;
            this.gridClasses.Dock = System.Windows.Forms.DockStyle.Fill; this.gridClasses.Location = new System.Drawing.Point(0, 46);
            this.gridClasses.Name = "gridClasses"; this.gridClasses.ReadOnly = true; this.gridClasses.RowHeadersWidth = 40;
            this.gridClasses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridClasses.TabIndex = 8;
            this.gridClasses.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridClasses_CellDoubleClick);

            // ── ReferencesControl ──────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "ReferencesControl";
            this.Size = new System.Drawing.Size(1192, 684);

            this.tabControl.ResumeLayout(false);
            this.tabTeachers.ResumeLayout(false);    this.panelTeacher.ResumeLayout(false);    this.panelTeacher.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTeachers)).EndInit();
            this.tabSubjects.ResumeLayout(false);    this.panelSubject.ResumeLayout(false);    this.panelSubject.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSubjects)).EndInit();
            this.tabSubjPar.ResumeLayout(false);     this.panelSubjPar.ResumeLayout(false);    this.panelSubjPar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSubjPar)).EndInit();
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
        private System.Windows.Forms.TabPage        tabSubjPar;
        private System.Windows.Forms.Panel          panelSubjPar;
        private System.Windows.Forms.Label          labelSubjParGrade;
        private System.Windows.Forms.ComboBox       comboSubjParGrade;
        private System.Windows.Forms.Label          labelSubjParSubject;
        private System.Windows.Forms.ComboBox       comboSubjParSubject;
        private System.Windows.Forms.Label          labelSubjParDiff;
        private System.Windows.Forms.TextBox        txtSubjParDiff;
        private System.Windows.Forms.Button         buttonAddSubjPar;
        private System.Windows.Forms.Button         buttonDeleteSubjPar;
        private System.Windows.Forms.DataGridView   gridSubjPar;
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
