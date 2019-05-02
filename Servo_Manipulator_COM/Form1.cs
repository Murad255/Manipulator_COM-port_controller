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

namespace Servo_Manipulator_COM
{
    public partial class Form1 : Form
    {
        private const int WAIT_ANSWER_TIMEOUT = 500;
      
        Task send;
        Queue<char> RX_data;

        bool gripFlag = true;
        public Form1()
        {
            try
            {
                InitializeComponent();
                comboBox.Items.Clear();
                foreach (string portName in System.IO.Ports.SerialPort.GetPortNames())
                {
                    comboBox.Items.Add(portName);
                }
                comboBox.SelectedIndex = 0;
                connectButton.Text = "отк";
                connectButton.BackColor = Color.Tomato;

                send = new Task(SendData);
                send.Start();
            }
            catch (ArgumentOutOfRangeException ) {
                MessageBox.Show("Не найдено ни одного COM порта!",
                               "Ошибка",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
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
                    connectButton.BackColor = Color.GreenYellow;
                }
                else
                {
                    serialPort.Close();
                    connectButton.Text = "откл";
                    connectButton.BackColor = Color.Tomato;
                }
            }
            catch (UnauthorizedAccessException )
            {
                MessageBox.Show("COM порт занят.", "Ошибка!");
            }
            catch (Exception ce)
            {
                MessageBox.Show(ce.ToString(), "Ошибка!");
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

        private void trackBar_E_Scroll_1(object sender, EventArgs e)
        {

            serialWrite('e' + trackBar_E.Value.ToString() + 'z');
            textBox1.Text = trackBar_E.Value.ToString();
            label_E.Text = trackBar_E.Value.ToString();
        }

        private void trackBar_F_Scroll_1(object sender, EventArgs e)
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
                serialWrite("f100z");
                trackBar_F.Value = Convert.ToInt32(100);
            }
            else if (gripFlag == false)
            {
                gripFlag = true;
                serialWrite("f180z");
                trackBar_F.Value = Convert.ToInt32(180);
            }
        }
        private   void serialWrite(string message)
        {
            try
            {
                serialPort.Write(message);
                textBox1.Text = message;
             
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
        private void SendData()   
        {
            //TO-DO
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           // textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }
    }
}