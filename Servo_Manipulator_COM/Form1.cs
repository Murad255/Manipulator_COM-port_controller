using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using Intersection;
using PointSpase;

namespace Servo_Manipulator_COM
{
    public partial class Form1 : Form
    {
        private const int WAIT_ANSWER_TIMEOUT = 500;
        private const int speed = 2;
        private bool textBox2_status = false;
        Task send;
        Queue<char> RX_data;

        List<Point> points =new List<Point>();   // коллекция с точками, задающими координаты
       
        bool gripFlag = true;

        public Form1()
        {
            InitializeComponent();
            Point.sent = serialWrite;
            comboBox.Items.Clear();
            foreach (string portName in System.IO.Ports.SerialPort.GetPortNames())
            {
                comboBox.Items.Add(portName);
            }

            try
            {
                comboBox.SelectedIndex = 0;
            }
            catch (ArgumentOutOfRangeException aore)
            {
                MessageBox.Show(aore.ToString(),
                               "Ошибка",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
            connectButton.Text = "отк";
            connectButton.BackColor = System.Drawing.Color.Tomato;
            
        }
        

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        private void connectButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.PortName = ((string)comboBox.SelectedItem);
                    serialPort.Open();
                    Home();
                    connectButton.Text = "вкл";
                    connectButton.BackColor = System.Drawing.Color.GreenYellow;
                }
                else
                {
                    serialPort.Close();
                    connectButton.Text = "откл";
                    connectButton.BackColor = System.Drawing.Color.Tomato;
                }
            }
            catch (UnauthorizedAccessException )
            {
                MessageBox.Show("COM порт занят.",
                                "Ошибка!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            catch (Exception ce)
            {
                MessageBox.Show(ce.ToString(),
                                "Ошибка!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }


        private void trackBar_A_Scroll(object sender, EventArgs e)
        {

            serialWrite('a' + trackBar_A.Value.ToString() + 'z');
            textBox1.Text = trackBar_A.Value.ToString();
            label_A.Text = trackBar_A.Value.ToString();
        }

        private void trackBar_B_Scroll(object sender, EventArgs e)
        {
            serialWrite('b'+trackBar_B.Value.ToString()+'z');
            textBox1.Text = trackBar_B.Value.ToString();
            label_B.Text = trackBar_B.Value.ToString();
        }


        private void trackBar_C_Scroll(object sender, EventArgs e)
        {
            serialWrite('c' + trackBar_C.Value.ToString() + 'z');
            textBox1.Text = trackBar_C.Value.ToString();
            label_C.Text = trackBar_C.Value.ToString();
        }

        private void trackBar_D_Scroll(object sender, EventArgs e)
        {
            serialWrite('d' + trackBar_D.Value.ToString() + 'z');
            textBox1.Text = trackBar_D.Value.ToString();
            label_D.Text = trackBar_D.Value.ToString();
        }

        private void trackBar_E_Scroll(object sender, EventArgs e)
        {

            serialWrite('e' + trackBar_E.Value.ToString() + 'z');
            textBox1.Text = trackBar_E.Value.ToString();
            label_E.Text = trackBar_E.Value.ToString();
        }

        private void trackBar_F_Scroll(object sender, EventArgs e)
        {

            serialWrite('f' + trackBar_F.Value.ToString() + 'z');
            textBox1.Text = trackBar_F.Value.ToString();
            label_F.Text = trackBar_F.Value.ToString();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            serialWrite(textBox2.Text);
            textBox2.Text = " ";
        }
              
        

        private void HomeButton_Click(object sender, EventArgs e)=>Home();
        

        private void gripButton_Click(object sender, EventArgs e)
        {
            if (gripFlag == true)
            {
                gripFlag = false;
                serialWrite("f90z");
                trackBar_F.Value = Convert.ToInt32(100);
            }
            else if (gripFlag == false)
            {
                gripFlag = true;
                serialWrite("f1z");
                trackBar_F.Value = Convert.ToInt32(180);
            }
        }
        private void serialWrite(string message)
        {
            try
            {
                serialPort.Write(message);
                textBox1.Text += "\r\n";
                textBox1.Text += message;
             
            }

            catch (TimeoutException)
            {
                MessageBox.Show("Время ожидания операции истекло. попробуйте перезаустить подключение", 
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            catch (InvalidOperationException)
            {
                MessageBox.Show("COM порт закрыт", "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            catch (Exception ce)
            {
                MessageBox.Show(ce.ToString(), "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

       private void Home()
        {
            if ((string)comboHomeMode.SelectedItem == "work")
            {

                serialWrite("a90z");
                serialWrite("b40z");
                serialWrite("c47z");
                serialWrite("d160z");
                serialWrite("e90z");
                serialWrite("f140z");
                trackBar_A.Value = Convert.ToInt32(90);
                trackBar_B.Value = Convert.ToInt32(40);
                trackBar_C.Value = Convert.ToInt32(47);
                trackBar_D.Value = Convert.ToInt32(160);
                trackBar_E.Value = Convert.ToInt32(90);
                trackBar_F.Value = Convert.ToInt32(140);
                label_D.Text = trackBar_D.Value.ToString();
                label_D.Text = trackBar_D.Value.ToString();
                label_D.Text = trackBar_D.Value.ToString();
                label_D.Text = trackBar_D.Value.ToString();
                label_D.Text = trackBar_D.Value.ToString();
                label_D.Text = trackBar_D.Value.ToString();
            }
            else if ((string)comboHomeMode.SelectedItem == "steady")
            {
                serialWrite("a90z");
                serialWrite("b30z");
                serialWrite("c14z");
                serialWrite("d1z");
                serialWrite("e90z");
                serialWrite("f155z");
                trackBar_A.Value = Convert.ToInt32(90);
                trackBar_B.Value = Convert.ToInt32(30);
                trackBar_C.Value = Convert.ToInt32(14);
                trackBar_D.Value = Convert.ToInt32(1);
                trackBar_E.Value = Convert.ToInt32(90);
                trackBar_F.Value = Convert.ToInt32(155);
                label_D.Text = trackBar_D.Value.ToString();
                label_D.Text = trackBar_D.Value.ToString();
                label_D.Text = trackBar_D.Value.ToString();
                label_D.Text = trackBar_D.Value.ToString();
                label_D.Text = trackBar_D.Value.ToString();
                label_D.Text = trackBar_D.Value.ToString();
            }
        }

        private  void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                RX_data = new Queue<char>();
                while (0 != sp.BytesToRead) RX_data.Enqueue((char)sp.ReadByte());
                if(send.IsCompleted)
                    send= Task.Run(new Action(() =>{ SendData(RX_data);})); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }

        private void SendData(Queue<char> data){
            
            if (InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    textBox1.Text += "\r\n";
                    foreach (char c in data)
                    {
                        textBox1.Text += c;
                    }
                }));
            }
            else
            {
                foreach (char c in RX_data)
                {
                    textBox1.Text += c;
                }
            }
        }
        private void SendData()   //программа-заглушка для старта потока
        {
            //TO-DO
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)    // Отправка текста при нажатии на Enter 
        {
            if (e.KeyCode == Keys.Enter) button1_Click(sender, e);
        }


        /*
         * управление  каналами посредством нажатия клавиш:
         *  Up и Down      для канала A
         *  Left и Right   для канала B
         *  W и S          для канала C
         *  R и F          для канала D
         *  A и D          для канала E
         *  Spase для упровления захватом
         *  H для возвращения на базу
         */
        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {

            if (textBox2_status == false)
            {
                if (e.KeyCode == Keys.Space) gripButton_Click(sender, e);
                if (e.KeyCode == Keys.Up)
                {
                    if (trackBar_B.Value != trackBar_B.Maximum)
                    {
                        trackBar_B.Value+= speed;
                        trackBar_B_Scroll(sender, e);
                    }
                }
                if (e.KeyCode == Keys.Down)
                {
                    if (trackBar_B.Value != trackBar_B.Minimum)
                    {
                        trackBar_B.Value-=speed;
                        trackBar_B_Scroll(sender, e);
                    }
                }

                if (e.KeyCode == Keys.Right)
                {
                    if (trackBar_A.Value != trackBar_A.Maximum)
                    {
                        trackBar_A.Value += speed;
                        trackBar_A_Scroll(sender, e);
                    }
                }
                if (e.KeyCode == Keys.Left)
                {
                    if (trackBar_A.Value != trackBar_B.Minimum)
                    {
                        trackBar_A.Value -= speed;
                        trackBar_A_Scroll(sender, e);
                    }
                }

                if (e.KeyCode == Keys.W)
                {
                    if (trackBar_C.Value != trackBar_C.Maximum)
                    {
                        trackBar_C.Value += speed;
                        trackBar_C_Scroll(sender, e);
                    }
                }
                if (e.KeyCode == Keys.S)
                {
                    if (trackBar_C.Value != trackBar_C.Minimum)
                    {
                        trackBar_C.Value -= speed;
                        trackBar_C_Scroll(sender, e);
                    }
                }

                if (e.KeyCode == Keys.D)
                {
                    if (trackBar_E.Value != trackBar_E.Maximum)
                    {
                        trackBar_E.Value += speed*2;
                        trackBar_E_Scroll(sender, e);
                    }
                }
                if (e.KeyCode == Keys.A)
                {
                    if (trackBar_E.Value != trackBar_E.Minimum)
                    {
                        trackBar_E.Value -= speed*2;
                        trackBar_E_Scroll(sender, e);
                    }
                }

                if (e.KeyCode == Keys.R)
                {
                    if (trackBar_D.Value != trackBar_D.Maximum)
                    {
                        trackBar_D.Value += speed;
                        trackBar_D_Scroll(sender, e);
                    }
                }
                if (e.KeyCode == Keys.F)
                {
                    if (trackBar_D.Value != trackBar_D.Minimum)
                    {
                        trackBar_D.Value -= speed;
                        trackBar_D_Scroll(sender, e);
                    }
                }

                if (e.KeyCode == Keys.H) Home();
                
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                points.Add(new Point(
                                    trackBar_A.Value,
                                    trackBar_B.Value,
                                    trackBar_C.Value,
                                    trackBar_D.Value,
                                    trackBar_E.Value,
                                    trackBar_F.Value,
                                    Convert.ToInt32(delay.Text)
                                      ));
                PointListView.Text ="";
                foreach (Point p in points) PointListView.Text += p.ToString(); //выводит список точек

            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректное значение задержки!", "Ошибка",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
            catch (Exception sve)
            {
                MessageBox.Show(sve.ToString(), "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

        }

        private void SentButton_Click(object sender, EventArgs e)
        {
            //отправляет координаты и заключает их в символs: n- начало пакета, k- конец пакета
            serialWrite("n");
            foreach (Point p in points)  p.write();
            serialWrite("k");
        }
        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2_status = true;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox2_status = false;
        }
    }
}