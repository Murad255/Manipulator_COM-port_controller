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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.filePozition = new System.Windows.Forms.TextBox();
            this.LoadListButton = new System.Windows.Forms.Button();
            this.SaveListButton = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.clearPoints = new System.Windows.Forms.Button();
            this.PointListView = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.startExecution = new System.Windows.Forms.Button();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.cycleStatus = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.delay = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SentButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_A)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_F)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_E)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_D)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_B)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBar_A
            // 
            this.trackBar_A.Location = new System.Drawing.Point(96, 53);
            this.trackBar_A.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBar_A.Maximum = 180;
            this.trackBar_A.Name = "trackBar_A";
            this.trackBar_A.Size = new System.Drawing.Size(304, 56);
            this.trackBar_A.TabIndex = 0;
            this.trackBar_A.Value = 90;
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
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(408, 480);
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
            this.comboHomeMode.Location = new System.Drawing.Point(12, 430);
            this.comboHomeMode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboHomeMode.Name = "comboHomeMode";
            this.comboHomeMode.Size = new System.Drawing.Size(81, 24);
            this.comboHomeMode.TabIndex = 19;

            // 
            // gripButton
            // 
            this.gripButton.Location = new System.Drawing.Point(249, 430);
            this.gripButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gripButton.Name = "gripButton";
            this.gripButton.Size = new System.Drawing.Size(125, 28);
            this.gripButton.TabIndex = 18;
            this.gripButton.Text = "Захват";
            this.gripButton.UseVisualStyleBackColor = true;
            this.gripButton.Click += new System.EventHandler(this.gripButton_Click);
            // 
            // HomeButton
            // 
            this.HomeButton.Location = new System.Drawing.Point(103, 430);
            this.HomeButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.HomeButton.Name = "HomeButton";
            this.HomeButton.Size = new System.Drawing.Size(139, 28);
            this.HomeButton.TabIndex = 17;
            this.HomeButton.Text = "Home";
            this.HomeButton.UseVisualStyleBackColor = true;
            this.HomeButton.Click += new System.EventHandler(this.HomeButton_Click);
            // 
            // label_F
            // 
            this.label_F.AutoSize = true;
            this.label_F.Location = new System.Drawing.Point(8, 390);
            this.label_F.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_F.Name = "label_F";
            this.label_F.Size = new System.Drawing.Size(46, 17);
            this.label_F.TabIndex = 16;
            this.label_F.Text = "label9";
            // 
            // label_E
            // 
            this.label_E.AutoSize = true;
            this.label_E.Location = new System.Drawing.Point(8, 332);
            this.label_E.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_E.Name = "label_E";
            this.label_E.Size = new System.Drawing.Size(46, 17);
            this.label_E.TabIndex = 15;
            this.label_E.Text = "label9";
            // 
            // label_D
            // 
            this.label_D.AutoSize = true;
            this.label_D.Location = new System.Drawing.Point(8, 270);
            this.label_D.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_D.Name = "label_D";
            this.label_D.Size = new System.Drawing.Size(46, 17);
            this.label_D.TabIndex = 14;
            this.label_D.Text = "label9";
            // 
            // label_C
            // 
            this.label_C.AutoSize = true;
            this.label_C.Location = new System.Drawing.Point(8, 207);
            this.label_C.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_C.Name = "label_C";
            this.label_C.Size = new System.Drawing.Size(46, 17);
            this.label_C.TabIndex = 13;
            this.label_C.Text = "label9";
            // 
            // label_B
            // 
            this.label_B.AutoSize = true;
            this.label_B.Location = new System.Drawing.Point(8, 144);
            this.label_B.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_B.Name = "label_B";
            this.label_B.Size = new System.Drawing.Size(47, 17);
            this.label_B.TabIndex = 12;
            this.label_B.Text = "labelB";
            // 
            // label_A
            // 
            this.label_A.AutoSize = true;
            this.label_A.Location = new System.Drawing.Point(8, 81);
            this.label_A.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_A.Name = "label_A";
            this.label_A.Size = new System.Drawing.Size(47, 17);
            this.label_A.TabIndex = 11;
            this.label_A.Text = "labelA";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 362);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Канал F";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 304);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Канал E";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 241);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Канал D";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 178);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Канал С";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 116);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "канал В";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "канал А";
            // 
            // trackBar_F
            // 
            this.trackBar_F.Location = new System.Drawing.Point(96, 367);
            this.trackBar_F.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBar_F.Maximum = 180;
            this.trackBar_F.Minimum = 100;
            this.trackBar_F.Name = "trackBar_F";
            this.trackBar_F.Size = new System.Drawing.Size(304, 56);
            this.trackBar_F.TabIndex = 5;
            this.trackBar_F.Value = 115;
            this.trackBar_F.Scroll += new System.EventHandler(this.trackBar_F_Scroll);
            // 
            // trackBar_E
            // 
            this.trackBar_E.Location = new System.Drawing.Point(96, 304);
            this.trackBar_E.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBar_E.Maximum = 180;
            this.trackBar_E.Name = "trackBar_E";
            this.trackBar_E.Size = new System.Drawing.Size(304, 56);
            this.trackBar_E.TabIndex = 4;
            this.trackBar_E.Value = 90;
            this.trackBar_E.Scroll += new System.EventHandler(this.trackBar_E_Scroll);
            // 
            // trackBar_D
            // 
            this.trackBar_D.Location = new System.Drawing.Point(96, 241);
            this.trackBar_D.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBar_D.Maximum = 180;
            this.trackBar_D.Name = "trackBar_D";
            this.trackBar_D.Size = new System.Drawing.Size(304, 56);
            this.trackBar_D.TabIndex = 3;
            this.trackBar_D.Value = 90;
            this.trackBar_D.Scroll += new System.EventHandler(this.trackBar_D_Scroll);
            // 
            // trackBar_C
            // 
            this.trackBar_C.Location = new System.Drawing.Point(96, 178);
            this.trackBar_C.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBar_C.Maximum = 180;
            this.trackBar_C.Name = "trackBar_C";
            this.trackBar_C.Size = new System.Drawing.Size(304, 56);
            this.trackBar_C.TabIndex = 2;
            this.trackBar_C.Value = 47;
            this.trackBar_C.Scroll += new System.EventHandler(this.trackBar_C_Scroll);
            // 
            // trackBar_B
            // 
            this.trackBar_B.Location = new System.Drawing.Point(96, 116);
            this.trackBar_B.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBar_B.Maximum = 180;
            this.trackBar_B.Name = "trackBar_B";
            this.trackBar_B.Size = new System.Drawing.Size(304, 56);
            this.trackBar_B.TabIndex = 1;
            this.trackBar_B.Value = 40;
            this.trackBar_B.Scroll += new System.EventHandler(this.trackBar_B_Scroll);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(4, 58);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(399, 381);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "text";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // connectButton
            // 
            this.connectButton.BackColor = System.Drawing.SystemColors.Control;
            this.connectButton.Location = new System.Drawing.Point(325, 25);
            this.connectButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(100, 28);
            this.connectButton.TabIndex = 3;
            this.connectButton.Text = "button1";
            this.connectButton.UseVisualStyleBackColor = false;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 22);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "Ввод вывод";
            // 
            // serialPort
            // 
            this.serialPort.WriteTimeout = 50;
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(219, 25);
            this.comboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(97, 24);
            this.comboBox.TabIndex = 12;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(-1, 58);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(861, 511);
            this.tabControl1.TabIndex = 13;
            this.tabControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabControl1_KeyDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(853, 482);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Консоль";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(304, 447);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Отправить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(4, 447);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(291, 22);
            this.textBox2.TabIndex = 13;
            this.textBox2.Enter += new System.EventHandler(this.textBox2_Enter);
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            this.textBox2.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(853, 482);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "управление";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Controls.Add(this.clearPoints);
            this.tabPage3.Controls.Add(this.PointListView);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Size = new System.Drawing.Size(853, 482);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "перемещения по точкам";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.filePozition);
            this.groupBox3.Controls.Add(this.LoadListButton);
            this.groupBox3.Controls.Add(this.SaveListButton);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Location = new System.Drawing.Point(17, 308);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(387, 160);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Записи";
            // 
            // filePozition
            // 
            this.filePozition.Location = new System.Drawing.Point(153, 34);
            this.filePozition.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.filePozition.Name = "filePozition";
            this.filePozition.Size = new System.Drawing.Size(205, 22);
            this.filePozition.TabIndex = 3;
            this.filePozition.Text = "log.txt";
            // 
            // LoadListButton
            // 
            this.LoadListButton.Location = new System.Drawing.Point(217, 87);
            this.LoadListButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LoadListButton.Name = "LoadListButton";
            this.LoadListButton.Size = new System.Drawing.Size(143, 28);
            this.LoadListButton.TabIndex = 2;
            this.LoadListButton.Text = "Загрузить список";
            this.LoadListButton.UseVisualStyleBackColor = true;
            this.LoadListButton.Click += new System.EventHandler(this.LoadListButton_Click);
            // 
            // SaveListButton
            // 
            this.SaveListButton.Location = new System.Drawing.Point(23, 87);
            this.SaveListButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SaveListButton.Name = "SaveListButton";
            this.SaveListButton.Size = new System.Drawing.Size(143, 28);
            this.SaveListButton.TabIndex = 1;
            this.SaveListButton.Text = "Сохранить список";
            this.SaveListButton.UseVisualStyleBackColor = true;
            this.SaveListButton.Click += new System.EventHandler(this.SaveListButton_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(19, 38);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(123, 17);
            this.label15.TabIndex = 0;
            this.label15.Text = "Название файла:";
            // 
            // clearPoints
            // 
            this.clearPoints.Location = new System.Drawing.Point(688, 20);
            this.clearPoints.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clearPoints.Name = "clearPoints";
            this.clearPoints.Size = new System.Drawing.Size(128, 28);
            this.clearPoints.TabIndex = 6;
            this.clearPoints.Text = "удалить все";
            this.clearPoints.UseVisualStyleBackColor = true;
            this.clearPoints.Click += new System.EventHandler(this.clearPoints_Click);
            // 
            // PointListView
            // 
            this.PointListView.Font = new System.Drawing.Font("T-FLEX Type B", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PointListView.Location = new System.Drawing.Point(411, 54);
            this.PointListView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PointListView.Multiline = true;
            this.PointListView.Name = "PointListView";
            this.PointListView.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PointListView.Size = new System.Drawing.Size(433, 414);
            this.PointListView.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.startExecution);
            this.groupBox2.Controls.Add(this.checkBox3);
            this.groupBox2.Controls.Add(this.cycleStatus);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.delay);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.SentButton);
            this.groupBox2.Controls.Add(this.SaveButton);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.textBox7);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.textBox6);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(17, 27);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(388, 273);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Координыты и значения";
            // 
            // startExecution
            // 
            this.startExecution.Location = new System.Drawing.Point(23, 217);
            this.startExecution.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.startExecution.Name = "startExecution";
            this.startExecution.Size = new System.Drawing.Size(337, 37);
            this.startExecution.TabIndex = 17;
            this.startExecution.Text = "запуск локально";
            this.startExecution.UseVisualStyleBackColor = true;
            this.startExecution.Click += new System.EventHandler(this.startExecution_Click);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Cursor = System.Windows.Forms.Cursors.No;
            this.checkBox3.Location = new System.Drawing.Point(201, 122);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(130, 22);
            this.checkBox3.TabIndex = 16;
            this.checkBox3.Text = "из управления";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // cycleStatus
            // 
            this.cycleStatus.AutoSize = true;
            this.cycleStatus.Location = new System.Drawing.Point(9, 122);
            this.cycleStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cycleStatus.Name = "cycleStatus";
            this.cycleStatus.Size = new System.Drawing.Size(83, 22);
            this.cycleStatus.TabIndex = 15;
            this.cycleStatus.Text = "в цикле";
            this.cycleStatus.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(175, 95);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 18);
            this.label14.TabIndex = 13;
            this.label14.Text = "Задержка:";
            // 
            // delay
            // 
            this.delay.Location = new System.Drawing.Point(271, 91);
            this.delay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.delay.Name = "delay";
            this.delay.Size = new System.Drawing.Size(59, 24);
            this.delay.TabIndex = 14;
            this.delay.Text = "0";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(108, 122);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(76, 22);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "захват";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // SentButton
            // 
            this.SentButton.Enabled = false;
            this.SentButton.Location = new System.Drawing.Point(217, 167);
            this.SentButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SentButton.Name = "SentButton";
            this.SentButton.Size = new System.Drawing.Size(143, 42);
            this.SentButton.TabIndex = 1;
            this.SentButton.Text = "отправка";
            this.SentButton.UseVisualStyleBackColor = true;
            this.SentButton.Click += new System.EventHandler(this.SentButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(23, 167);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(143, 42);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "сохранить";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(179, 60);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 18);
            this.label13.TabIndex = 10;
            this.label13.Text = "Горизонт:";
            // 
            // textBox7
            // 
            this.textBox7.Enabled = false;
            this.textBox7.Location = new System.Drawing.Point(271, 60);
            this.textBox7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(59, 24);
            this.textBox7.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(179, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 18);
            this.label12.TabIndex = 8;
            this.label12.Text = "Наклон:";
            // 
            // textBox6
            // 
            this.textBox6.Enabled = false;
            this.textBox6.Location = new System.Drawing.Point(271, 27);
            this.textBox6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(59, 24);
            this.textBox6.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 89);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 18);
            this.label11.TabIndex = 6;
            this.label11.Text = "ось Z:";
            // 
            // textBox5
            // 
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(77, 89);
            this.textBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(59, 24);
            this.textBox5.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 18);
            this.label10.TabIndex = 4;
            this.label10.Text = "ось Y:";
            // 
            // textBox4
            // 
            this.textBox4.Enabled = false;
            this.textBox4.Location = new System.Drawing.Point(77, 57);
            this.textBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(59, 24);
            this.textBox4.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "ось X:";
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(77, 27);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(59, 24);
            this.textBox3.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(123, 28);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 18);
            this.label8.TabIndex = 14;
            this.label8.Text = "COM порт";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 567);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.connectButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "COM-консоль";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_A)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_F)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_E)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_D)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_B)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label label7;
        private System.IO.Ports.SerialPort serialPort;
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
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button SentButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox delay;
        private System.Windows.Forms.CheckBox cycleStatus;

        private System.Windows.Forms.Button button4;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.ComboBox comboHomeMode;
        private System.Windows.Forms.Button startExecution;
        private System.Windows.Forms.Button clearPoints;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox filePozition;
        private System.Windows.Forms.Button LoadListButton;
        private System.Windows.Forms.Button SaveListButton;
        private System.Windows.Forms.Label label15;
    }
}

