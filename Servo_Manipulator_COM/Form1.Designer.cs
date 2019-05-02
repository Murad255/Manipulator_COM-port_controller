namespace Servo_Manipulator_COM
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
            this.label8 = new System.Windows.Forms.Label();
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
            this.SuspendLayout();
            // 
            // trackBar_A
            // 
            this.trackBar_A.Location = new System.Drawing.Point(72, 43);
            this.trackBar_A.Maximum = 180;
            this.trackBar_A.Name = "trackBar_A";
            this.trackBar_A.Size = new System.Drawing.Size(228, 45);
            this.trackBar_A.TabIndex = 0;
            this.trackBar_A.Value = 90;
            this.trackBar_A.Scroll += new System.EventHandler(this.trackBar_A_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
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
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 390);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Углы серв";
            // 
            // gripButton
            // 
            this.gripButton.Location = new System.Drawing.Point(146, 349);
            this.gripButton.Name = "gripButton";
            this.gripButton.Size = new System.Drawing.Size(135, 23);
            this.gripButton.TabIndex = 18;
            this.gripButton.Text = "Захват";
            this.gripButton.UseVisualStyleBackColor = true;
            this.gripButton.Click += new System.EventHandler(this.gripButton_Click);
            // 
            // HomeButton
            // 
            this.HomeButton.Location = new System.Drawing.Point(21, 349);
            this.HomeButton.Name = "HomeButton";
            this.HomeButton.Size = new System.Drawing.Size(119, 23);
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
            this.trackBar_F.Maximum = 180;
            this.trackBar_F.Minimum = 100;
            this.trackBar_F.Name = "trackBar_F";
            this.trackBar_F.Size = new System.Drawing.Size(228, 45);
            this.trackBar_F.TabIndex = 5;
            this.trackBar_F.Value = 115;
            this.trackBar_F.Scroll += new System.EventHandler(this.trackBar_F_Scroll_1);
            // 
            // trackBar_E
            // 
            this.trackBar_E.Location = new System.Drawing.Point(72, 247);
            this.trackBar_E.Maximum = 180;
            this.trackBar_E.Name = "trackBar_E";
            this.trackBar_E.Size = new System.Drawing.Size(228, 45);
            this.trackBar_E.TabIndex = 4;
            this.trackBar_E.Value = 150;
            this.trackBar_E.Scroll += new System.EventHandler(this.trackBar_E_Scroll_1);
            // 
            // trackBar_D
            // 
            this.trackBar_D.Location = new System.Drawing.Point(72, 196);
            this.trackBar_D.Maximum = 180;
            this.trackBar_D.Name = "trackBar_D";
            this.trackBar_D.Size = new System.Drawing.Size(228, 45);
            this.trackBar_D.TabIndex = 3;
            this.trackBar_D.Value = 90;
            this.trackBar_D.Scroll += new System.EventHandler(this.trackBar_D_Scroll);
            // 
            // trackBar_C
            // 
            this.trackBar_C.Location = new System.Drawing.Point(72, 145);
            this.trackBar_C.Maximum = 180;
            this.trackBar_C.Name = "trackBar_C";
            this.trackBar_C.Size = new System.Drawing.Size(228, 45);
            this.trackBar_C.TabIndex = 2;
            this.trackBar_C.Value = 47;
            this.trackBar_C.Scroll += new System.EventHandler(this.trackBar_C_Scroll);
            // 
            // trackBar_B
            // 
            this.trackBar_B.Location = new System.Drawing.Point(72, 94);
            this.trackBar_B.Maximum = 180;
            this.trackBar_B.Name = "trackBar_B";
            this.trackBar_B.Size = new System.Drawing.Size(228, 45);
            this.trackBar_B.TabIndex = 1;
            this.trackBar_B.Value = 40;
            this.trackBar_B.Scroll += new System.EventHandler(this.trackBar_B_Scroll);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(3, 47);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(300, 310);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "text";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // connectButton
            // 
            this.connectButton.BackColor = System.Drawing.SystemColors.Control;
            this.connectButton.Location = new System.Drawing.Point(244, 20);
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
            this.label7.Location = new System.Drawing.Point(6, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
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
            this.comboBox.Location = new System.Drawing.Point(164, 20);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(74, 21);
            this.comboBox.TabIndex = 12;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 47);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(317, 415);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(309, 389);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Консоль";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(228, 363);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 19);
            this.button1.TabIndex = 14;
            this.button1.Text = "Отправить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(3, 363);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(219, 20);
            this.textBox2.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(309, 389);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "управление";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(92, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "COM порт";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 474);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.connectButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
    }
}

