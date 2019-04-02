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
        int rxidx;
        private byte[] rxdata=new byte[500];
        private const int WAIT_ANSWER_TIMEOUT = 500;

        bool gripFlag = true;
        public Form1()
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
            serialWrite("a90z");
            serialWrite("b40z");
            serialWrite("c47z");
            serialWrite("d90z");
            serialWrite("e150z");
            serialWrite("f155z");
            trackBar_A.Value = Convert.ToInt32(90);
            trackBar_B.Value = Convert.ToInt32(40);
            trackBar_C.Value = Convert.ToInt32(47);
            trackBar_D.Value = Convert.ToInt32(90);
            trackBar_E.Value = Convert.ToInt32(150);
            trackBar_F.Value = Convert.ToInt32(155);
            label_D.Text = trackBar_D.Value.ToString();
            label_D.Text = trackBar_D.Value.ToString();
            label_D.Text = trackBar_D.Value.ToString();
            label_D.Text = trackBar_D.Value.ToString();
            label_D.Text = trackBar_D.Value.ToString();
            label_D.Text = trackBar_D.Value.ToString();
        }

        private  void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                while (0 != sp.BytesToRead)
                {
                    if (rxidx < rxdata.Length - 2)
                    {
                        rxdata[rxidx++] = (byte)sp.ReadByte();
                        rxdata[rxidx] = 0;
                    }
                    else
                        sp.ReadByte();
                }
                

                bool uiMarshal = textBox1.InvokeRequired;
                if (uiMarshal)
                {
                    textBox1.Invoke(new Action(() => {
                        foreach (char c in rxdata)
                        {
                            textBox1.Text += c;
                        }
                    }));
                }
                else
                {
                    foreach (char c in rxdata)
                    {
                        textBox1.Text += c;
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }

   
    }
}
