﻿namespace Servo_Manipulator_COM
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            send.Dispose();
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.trackBar_A = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboHomeMode = new System.Windows.Forms.ComboBox();
            this.gripButton = new System.Windows.Forms.Button();
            this.HomeButton = new System.Windows.Forms.Button();
            this.label_F = new System.Windows.Forms.Label();
            this.label_E = new System.Windows.Forms.Label();
            this.label_D = new System.Windows.Forms.Label();
            this.label_C = new System.Windows.Forms.Label();
            this.label_B = new System.Windows.Forms.Label();
            this.label_A = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar_F = new System.Windows.Forms.TrackBar();
            this.trackBar_E = new System.Windows.Forms.TrackBar();
            this.trackBar_D = new System.Windows.Forms.TrackBar();
            this.trackBar_C = new System.Windows.Forms.TrackBar();
            this.trackBar_B = new System.Windows.Forms.TrackBar();
            this.Console = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.serialPortBase = new System.IO.Ports.SerialPort(this.components);
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.return_point = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.HowSaveButton = new System.Windows.Forms.Button();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.filePozition = new System.Windows.Forms.TextBox();
            this.LoadListButton = new System.Windows.Forms.Button();
            this.SaveListButton = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.clearPoints = new System.Windows.Forms.Button();
            this.PointListView = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.valueCoordF = new System.Windows.Forms.TextBox();
            this.startExecution = new System.Windows.Forms.Button();
            this.checkAlgoritm = new System.Windows.Forms.CheckBox();
            this.cycleStatus = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.delay = new System.Windows.Forms.TextBox();
            this.checkGrip = new System.Windows.Forms.CheckBox();
            this.SentButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.valueCoordA = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.valueCoordB = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.valueCoordZ = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.valueCoordY = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.valueCoordX = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.openPointFile = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.configButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.savePointFile = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_A)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_F)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_E)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_D)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_B)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBar_A
            // 
            this.trackBar_A.Location = new System.Drawing.Point(72, 43);
            this.trackBar_A.Maximum = 90;
            this.trackBar_A.Minimum = -90;
            this.trackBar_A.Name = "trackBar_A";
            this.trackBar_A.Size = new System.Drawing.Size(228, 45);
            this.trackBar_A.TabIndex = 0;
            this.trackBar_A.Value = -90;
            this.trackBar_A.Scroll += new System.EventHandler(this.trackBar_A_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.comboHomeMode);
            this.groupBox1.Controls.Add(this.gripButton);
            this.groupBox1.Controls.Add(this.HomeButton);
            this.groupBox1.Controls.Add(this.label_F);
            this.groupBox1.Controls.Add(this.label_E);
            this.groupBox1.Controls.Add(this.label_D);
            this.groupBox1.Controls.Add(this.label_C);
            this.groupBox1.Controls.Add(this.label_B);
            this.groupBox1.Controls.Add(this.label_A);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.trackBar_F);
            this.groupBox1.Controls.Add(this.trackBar_E);
            this.groupBox1.Controls.Add(this.trackBar_D);
            this.groupBox1.Controls.Add(this.trackBar_C);
            this.groupBox1.Controls.Add(this.trackBar_B);
            this.groupBox1.Controls.Add(this.trackBar_A);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 421);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Углы серв";
            // 
            // comboHomeMode
            // 
            this.comboHomeMode.AutoCompleteCustomSource.AddRange(new string[] {
            "steady",
            "work"});
            this.comboHomeMode.FormattingEnabled = true;
            this.comboHomeMode.Items.AddRange(new object[] {
            "steady",
            "work"});
            this.comboHomeMode.Location = new System.Drawing.Point(9, 349);
            this.comboHomeMode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboHomeMode.Name = "comboHomeMode";
            this.comboHomeMode.Size = new System.Drawing.Size(62, 21);
            this.comboHomeMode.TabIndex = 19;
            // 
            // gripButton
            // 
            this.gripButton.Location = new System.Drawing.Point(187, 349);
            this.gripButton.Name = "gripButton";
            this.gripButton.Size = new System.Drawing.Size(94, 23);
            this.gripButton.TabIndex = 18;
            this.gripButton.Text = "Захват";
            this.gripButton.UseVisualStyleBackColor = true;
            this.gripButton.Click += new System.EventHandler(this.gripButton_Click);
            // 
            // HomeButton
            // 
            this.HomeButton.Location = new System.Drawing.Point(77, 349);
            this.HomeButton.Name = "HomeButton";
            this.HomeButton.Size = new System.Drawing.Size(104, 23);
            this.HomeButton.TabIndex = 17;
            this.HomeButton.Text = "Home";
            this.HomeButton.UseVisualStyleBackColor = true;
            this.HomeButton.Click += new System.EventHandler(this.HomeButton_Click);
            // 
            // label_F
            // 
            this.label_F.AutoSize = true;
            this.label_F.Location = new System.Drawing.Point(6, 317);
            this.label_F.Name = "label_F";
            this.label_F.Size = new System.Drawing.Size(35, 13);
            this.label_F.TabIndex = 16;
            this.label_F.Text = "label9";
            // 
            // label_E
            // 
            this.label_E.AutoSize = true;
            this.label_E.Location = new System.Drawing.Point(6, 270);
            this.label_E.Name = "label_E";
            this.label_E.Size = new System.Drawing.Size(35, 13);
            this.label_E.TabIndex = 15;
            this.label_E.Text = "label9";
            // 
            // label_D
            // 
            this.label_D.AutoSize = true;
            this.label_D.Location = new System.Drawing.Point(6, 219);
            this.label_D.Name = "label_D";
            this.label_D.Size = new System.Drawing.Size(35, 13);
            this.label_D.TabIndex = 14;
            this.label_D.Text = "label9";
            // 
            // label_C
            // 
            this.label_C.AutoSize = true;
            this.label_C.Location = new System.Drawing.Point(6, 168);
            this.label_C.Name = "label_C";
            this.label_C.Size = new System.Drawing.Size(35, 13);
            this.label_C.TabIndex = 13;
            this.label_C.Text = "label9";
            // 
            // label_B
            // 
            this.label_B.AutoSize = true;
            this.label_B.Location = new System.Drawing.Point(6, 117);
            this.label_B.Name = "label_B";
            this.label_B.Size = new System.Drawing.Size(36, 13);
            this.label_B.TabIndex = 12;
            this.label_B.Text = "labelB";
            // 
            // label_A
            // 
            this.label_A.AutoSize = true;
            this.label_A.Location = new System.Drawing.Point(6, 66);
            this.label_A.Name = "label_A";
            this.label_A.Size = new System.Drawing.Size(36, 13);
            this.label_A.TabIndex = 11;
            this.label_A.Text = "labelA";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 294);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Канал F";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 247);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Канал E";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Канал D";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Канал С";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "канал В";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "канал А";
            // 
            // trackBar_F
            // 
            this.trackBar_F.Location = new System.Drawing.Point(72, 298);
            this.trackBar_F.Maximum = 90;
            this.trackBar_F.Minimum = -90;
            this.trackBar_F.Name = "trackBar_F";
            this.trackBar_F.Size = new System.Drawing.Size(228, 45);
            this.trackBar_F.TabIndex = 5;
            this.trackBar_F.Value = 90;
            this.trackBar_F.Scroll += new System.EventHandler(this.trackBar_F_Scroll);
            // 
            // trackBar_E
            // 
            this.trackBar_E.Location = new System.Drawing.Point(72, 247);
            this.trackBar_E.Maximum = 280;
            this.trackBar_E.Minimum = 100;
            this.trackBar_E.Name = "trackBar_E";
            this.trackBar_E.Size = new System.Drawing.Size(228, 45);
            this.trackBar_E.TabIndex = 4;
            this.trackBar_E.Value = 100;
            this.trackBar_E.Scroll += new System.EventHandler(this.trackBar_E_Scroll);
            // 
            // trackBar_D
            // 
            this.trackBar_D.Location = new System.Drawing.Point(72, 196);
            this.trackBar_D.Maximum = 90;
            this.trackBar_D.Minimum = -90;
            this.trackBar_D.Name = "trackBar_D";
            this.trackBar_D.Size = new System.Drawing.Size(228, 45);
            this.trackBar_D.TabIndex = 3;
            this.trackBar_D.Value = 90;
            this.trackBar_D.Scroll += new System.EventHandler(this.trackBar_D_Scroll);
            // 
            // trackBar_C
            // 
            this.trackBar_C.Location = new System.Drawing.Point(72, 145);
            this.trackBar_C.Maximum = 220;
            this.trackBar_C.Minimum = 40;
            this.trackBar_C.Name = "trackBar_C";
            this.trackBar_C.Size = new System.Drawing.Size(228, 45);
            this.trackBar_C.TabIndex = 2;
            this.trackBar_C.Value = 47;
            this.trackBar_C.Scroll += new System.EventHandler(this.trackBar_C_Scroll);
            // 
            // trackBar_B
            // 
            this.trackBar_B.Location = new System.Drawing.Point(72, 94);
            this.trackBar_B.Maximum = 225;
            this.trackBar_B.Minimum = -45;
            this.trackBar_B.Name = "trackBar_B";
            this.trackBar_B.Size = new System.Drawing.Size(228, 45);
            this.trackBar_B.TabIndex = 1;
            this.trackBar_B.Value = 40;
            this.trackBar_B.Scroll += new System.EventHandler(this.trackBar_B_Scroll);
            // 
            // Console
            // 
            this.Console.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Console.Location = new System.Drawing.Point(3, 30);
            this.Console.Multiline = true;
            this.Console.Name = "Console";
            this.Console.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Console.Size = new System.Drawing.Size(630, 318);
            this.Console.TabIndex = 2;
            this.Console.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // connectButton
            // 
            this.connectButton.BackColor = System.Drawing.SystemColors.Control;
            this.connectButton.Location = new System.Drawing.Point(232, 7);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 3;
            this.connectButton.Text = "button1";
            this.connectButton.UseVisualStyleBackColor = false;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Ввод вывод";
            // 
            // serialPortBase
            // 
            this.serialPortBase.BaudRate = 38400;
            this.serialPortBase.WriteTimeout = 50;
            this.serialPortBase.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(152, 6);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(74, 21);
            this.comboBox.TabIndex = 12;
            this.comboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 42);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(644, 453);
            this.tabControl1.TabIndex = 13;
            this.tabControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabControl1_KeyDown);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.return_point);
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Controls.Add(this.clearPoints);
            this.tabPage3.Controls.Add(this.PointListView);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage3.Size = new System.Drawing.Size(636, 427);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "перемещения по точкам";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // return_point
            // 
            this.return_point.Location = new System.Drawing.Point(308, 16);
            this.return_point.Name = "return_point";
            this.return_point.Size = new System.Drawing.Size(111, 23);
            this.return_point.TabIndex = 8;
            this.return_point.Text = "Прошлое значение";
            this.return_point.UseVisualStyleBackColor = true;
            this.return_point.Click += new System.EventHandler(this.return_point_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.HowSaveButton);
            this.groupBox3.Controls.Add(this.buttonOpenFile);
            this.groupBox3.Controls.Add(this.filePozition);
            this.groupBox3.Controls.Add(this.LoadListButton);
            this.groupBox3.Controls.Add(this.SaveListButton);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Location = new System.Drawing.Point(13, 292);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(290, 130);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Записи";
            // 
            // HowSaveButton
            // 
            this.HowSaveButton.Location = new System.Drawing.Point(17, 100);
            this.HowSaveButton.Name = "HowSaveButton";
            this.HowSaveButton.Size = new System.Drawing.Size(105, 21);
            this.HowSaveButton.TabIndex = 5;
            this.HowSaveButton.Text = "Созранить как";
            this.HowSaveButton.UseVisualStyleBackColor = true;
            this.HowSaveButton.Click += new System.EventHandler(this.HowSaveButton_Click);
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Location = new System.Drawing.Point(245, 28);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(25, 20);
            this.buttonOpenFile.TabIndex = 4;
            this.buttonOpenFile.Text = "...";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.ButtonOpenFile_Click);
            // 
            // filePozition
            // 
            this.filePozition.Location = new System.Drawing.Point(115, 28);
            this.filePozition.Name = "filePozition";
            this.filePozition.Size = new System.Drawing.Size(133, 20);
            this.filePozition.TabIndex = 3;
            this.filePozition.TextChanged += new System.EventHandler(this.filePozition_TextChanged);
            this.filePozition.Enter += new System.EventHandler(this.filePozition_Enter);
            this.filePozition.Leave += new System.EventHandler(this.filePozition_Leave);
            // 
            // LoadListButton
            // 
            this.LoadListButton.Location = new System.Drawing.Point(163, 71);
            this.LoadListButton.Name = "LoadListButton";
            this.LoadListButton.Size = new System.Drawing.Size(107, 23);
            this.LoadListButton.TabIndex = 2;
            this.LoadListButton.Text = "Загрузить список";
            this.LoadListButton.UseVisualStyleBackColor = true;
            this.LoadListButton.Click += new System.EventHandler(this.LoadListButton_Click);
            // 
            // SaveListButton
            // 
            this.SaveListButton.Location = new System.Drawing.Point(17, 71);
            this.SaveListButton.Name = "SaveListButton";
            this.SaveListButton.Size = new System.Drawing.Size(107, 23);
            this.SaveListButton.TabIndex = 1;
            this.SaveListButton.Text = "Сохранить список";
            this.SaveListButton.UseVisualStyleBackColor = true;
            this.SaveListButton.Click += new System.EventHandler(this.SaveListButton_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(14, 31);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Название файла:";
            // 
            // clearPoints
            // 
            this.clearPoints.Location = new System.Drawing.Point(537, 16);
            this.clearPoints.Name = "clearPoints";
            this.clearPoints.Size = new System.Drawing.Size(96, 23);
            this.clearPoints.TabIndex = 6;
            this.clearPoints.Text = "удалить все";
            this.clearPoints.UseVisualStyleBackColor = true;
            this.clearPoints.Click += new System.EventHandler(this.clearPoints_Click);
            // 
            // PointListView
            // 
            this.PointListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PointListView.Location = new System.Drawing.Point(308, 44);
            this.PointListView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PointListView.Multiline = true;
            this.PointListView.Name = "PointListView";
            this.PointListView.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PointListView.Size = new System.Drawing.Size(326, 378);
            this.PointListView.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.valueCoordF);
            this.groupBox2.Controls.Add(this.startExecution);
            this.groupBox2.Controls.Add(this.checkAlgoritm);
            this.groupBox2.Controls.Add(this.cycleStatus);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.delay);
            this.groupBox2.Controls.Add(this.checkGrip);
            this.groupBox2.Controls.Add(this.SentButton);
            this.groupBox2.Controls.Add(this.SaveButton);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.valueCoordA);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.valueCoordB);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.valueCoordZ);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.valueCoordY);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.valueCoordX);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(13, 22);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(291, 265);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Координыты и значения";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(134, 75);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 15);
            this.label16.TabIndex = 19;
            this.label16.Text = "Ротация:";
            // 
            // valueCoordF
            // 
            this.valueCoordF.Enabled = false;
            this.valueCoordF.Location = new System.Drawing.Point(203, 74);
            this.valueCoordF.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.valueCoordF.Name = "valueCoordF";
            this.valueCoordF.Size = new System.Drawing.Size(45, 21);
            this.valueCoordF.TabIndex = 18;
            // 
            // startExecution
            // 
            this.startExecution.BackColor = System.Drawing.Color.White;
            this.startExecution.Location = new System.Drawing.Point(17, 230);
            this.startExecution.Name = "startExecution";
            this.startExecution.Size = new System.Drawing.Size(253, 30);
            this.startExecution.TabIndex = 17;
            this.startExecution.Text = "начать отправку";
            this.startExecution.UseVisualStyleBackColor = true;
            this.startExecution.Click += new System.EventHandler(this.startExecution_Click);
            // 
            // checkAlgoritm
            // 
            this.checkAlgoritm.AutoSize = true;
            this.checkAlgoritm.Checked = true;
            this.checkAlgoritm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAlgoritm.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.checkAlgoritm.Location = new System.Drawing.Point(148, 168);
            this.checkAlgoritm.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkAlgoritm.Name = "checkAlgoritm";
            this.checkAlgoritm.Size = new System.Drawing.Size(122, 19);
            this.checkAlgoritm.TabIndex = 16;
            this.checkAlgoritm.Text = "установить углы";
            this.checkAlgoritm.UseVisualStyleBackColor = true;
            this.checkAlgoritm.CheckedChanged += new System.EventHandler(this.checkAlgoritm_CheckedChanged);
            // 
            // cycleStatus
            // 
            this.cycleStatus.AutoSize = true;
            this.cycleStatus.Location = new System.Drawing.Point(4, 168);
            this.cycleStatus.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cycleStatus.Name = "cycleStatus";
            this.cycleStatus.Size = new System.Drawing.Size(70, 19);
            this.cycleStatus.TabIndex = 15;
            this.cycleStatus.Text = "в цикле";
            this.cycleStatus.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 113);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 15);
            this.label14.TabIndex = 13;
            this.label14.Text = "Задержка:";
            // 
            // delay
            // 
            this.delay.Location = new System.Drawing.Point(84, 110);
            this.delay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.delay.Name = "delay";
            this.delay.Size = new System.Drawing.Size(45, 21);
            this.delay.TabIndex = 14;
            this.delay.Text = "500";
            // 
            // checkGrip
            // 
            this.checkGrip.AutoSize = true;
            this.checkGrip.Location = new System.Drawing.Point(78, 168);
            this.checkGrip.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkGrip.Name = "checkGrip";
            this.checkGrip.Size = new System.Drawing.Size(66, 19);
            this.checkGrip.TabIndex = 12;
            this.checkGrip.Text = "захват";
            this.checkGrip.UseVisualStyleBackColor = true;
            this.checkGrip.CheckedChanged += new System.EventHandler(this.checkGrip_CheckedChanged);
            // 
            // SentButton
            // 
            this.SentButton.Enabled = false;
            this.SentButton.Location = new System.Drawing.Point(163, 191);
            this.SentButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SentButton.Name = "SentButton";
            this.SentButton.Size = new System.Drawing.Size(107, 34);
            this.SentButton.TabIndex = 1;
            this.SentButton.Text = "отправка";
            this.SentButton.UseVisualStyleBackColor = true;
            this.SentButton.Click += new System.EventHandler(this.SentButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(17, 191);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(107, 34);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "сохранить";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(134, 49);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 15);
            this.label13.TabIndex = 10;
            this.label13.Text = "Горизонт:";
            // 
            // valueCoordA
            // 
            this.valueCoordA.Enabled = false;
            this.valueCoordA.Location = new System.Drawing.Point(203, 49);
            this.valueCoordA.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.valueCoordA.Name = "valueCoordA";
            this.valueCoordA.Size = new System.Drawing.Size(45, 21);
            this.valueCoordA.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(134, 22);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 15);
            this.label12.TabIndex = 8;
            this.label12.Text = "Наклон:";
            // 
            // valueCoordB
            // 
            this.valueCoordB.Enabled = false;
            this.valueCoordB.Location = new System.Drawing.Point(203, 22);
            this.valueCoordB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.valueCoordB.Name = "valueCoordB";
            this.valueCoordB.Size = new System.Drawing.Size(45, 21);
            this.valueCoordB.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 72);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 15);
            this.label11.TabIndex = 6;
            this.label11.Text = "ось Z:";
            // 
            // valueCoordZ
            // 
            this.valueCoordZ.Enabled = false;
            this.valueCoordZ.Location = new System.Drawing.Point(58, 72);
            this.valueCoordZ.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.valueCoordZ.Name = "valueCoordZ";
            this.valueCoordZ.Size = new System.Drawing.Size(45, 21);
            this.valueCoordZ.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 46);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 15);
            this.label10.TabIndex = 4;
            this.label10.Text = "ось Y:";
            // 
            // valueCoordY
            // 
            this.valueCoordY.Enabled = false;
            this.valueCoordY.Location = new System.Drawing.Point(58, 46);
            this.valueCoordY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.valueCoordY.Name = "valueCoordY";
            this.valueCoordY.Size = new System.Drawing.Size(45, 21);
            this.valueCoordY.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 22);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 15);
            this.label9.TabIndex = 0;
            this.label9.Text = "ось X:";
            // 
            // valueCoordX
            // 
            this.valueCoordX.Enabled = false;
            this.valueCoordX.Location = new System.Drawing.Point(58, 22);
            this.valueCoordX.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.valueCoordX.Name = "valueCoordX";
            this.valueCoordX.Size = new System.Drawing.Size(45, 21);
            this.valueCoordX.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage1.Size = new System.Drawing.Size(636, 427);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "управление";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = global::Servo_Manipulator_COM.Properties.Resources.src_keyboard_3161;
            this.pictureBox1.Location = new System.Drawing.Point(317, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(316, 421);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.flowLayoutPanel2);
            this.tabPage2.Controls.Add(this.flowLayoutPanel1);
            this.tabPage2.Controls.Add(this.Console);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage2.Size = new System.Drawing.Size(637, 426);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Консоль";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label7);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(631, 21);
            this.flowLayoutPanel2.TabIndex = 16;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.textBox2);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 388);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(631, 35);
            this.flowLayoutPanel1.TabIndex = 15;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(3, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(540, 20);
            this.textBox2.TabIndex = 13;
            this.textBox2.Enter += new System.EventHandler(this.textBox2_Enter);
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            this.textBox2.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(549, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 19);
            this.button1.TabIndex = 14;
            this.button1.Text = "Отправить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(80, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "COM порт";
            // 
            // openPointFile
            // 
            this.openPointFile.FileName = "openPointFile";
            this.openPointFile.Filter = "(*.man)|*.man|(*.json)|*.json";
            this.openPointFile.InitialDirectory = "C:\\Users\\murad\\source\\repos\\Servo_Manipulator_COM\\Servo_Manipulator_COM\\bin\\Debug" +
    "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.configButton);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.comboBox);
            this.panel1.Controls.Add(this.connectButton);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(644, 33);
            this.panel1.TabIndex = 15;
            // 
            // configButton
            // 
            this.configButton.Location = new System.Drawing.Point(560, 6);
            this.configButton.Name = "configButton";
            this.configButton.Size = new System.Drawing.Size(75, 23);
            this.configButton.TabIndex = 15;
            this.configButton.Text = "настройки";
            this.configButton.UseVisualStyleBackColor = true;
            this.configButton.Click += new System.EventHandler(this.ConfigButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(650, 498);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // savePointFile
            // 
            this.savePointFile.DefaultExt = "man";
            this.savePointFile.Filter = "(*.man)|*.man";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 498);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "COM-консоль";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_A)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_F)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_E)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_D)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_B)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar_A;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar_F;
        private System.Windows.Forms.TrackBar trackBar_E;
        private System.Windows.Forms.TrackBar trackBar_D;
        private System.Windows.Forms.TrackBar trackBar_C;
        private System.Windows.Forms.TrackBar trackBar_B;
        private System.Windows.Forms.TextBox Console;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label label7;
        private System.IO.Ports.SerialPort serialPortBase;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label_A;
        private System.Windows.Forms.Label label_F;
        private System.Windows.Forms.Label label_E;
        private System.Windows.Forms.Label label_D;
        private System.Windows.Forms.Label label_C;
        private System.Windows.Forms.Label label_B;
        private System.Windows.Forms.Button gripButton;
        private System.Windows.Forms.Button HomeButton;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox PointListView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkGrip;
        private System.Windows.Forms.Button SentButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox valueCoordA;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox valueCoordB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox valueCoordZ;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox valueCoordY;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox valueCoordX;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox delay;
        private System.Windows.Forms.CheckBox cycleStatus;

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox checkAlgoritm;
        private System.Windows.Forms.ComboBox comboHomeMode;
        private System.Windows.Forms.Button startExecution;
        private System.Windows.Forms.Button clearPoints;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button LoadListButton;
        private System.Windows.Forms.Button SaveListButton;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button return_point;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.OpenFileDialog openPointFile;
        private System.Windows.Forms.TextBox filePozition;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SaveFileDialog savePointFile;
        private System.Windows.Forms.Button HowSaveButton;
        private System.Windows.Forms.Button configButton;
        private System.Windows.Forms.TextBox valueCoordF;
        private System.Windows.Forms.Label label16;
    }
}