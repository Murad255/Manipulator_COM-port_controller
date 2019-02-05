using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Servo_Manipulator_COM
{
    public partial class Form1 : Form
    {
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


        private void SerialPort_DataReceived(object sender, EventArgs e) => textBox1.Text = serialPort.ReadExisting();

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
                    connectButton.Text = "вкл";
                    connectButton.BackColor = Color.GreenYellow; 
                    serialPort.Open();
                }
                else
                {
                    connectButton.Text = "откл";
                    connectButton.BackColor = Color.Tomato;
                    serialPort.Close();
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


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            serialWrite(trackBar1.Value.ToString());
            textBox1.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            serialWrite('b'+trackBar2.Value.ToString()+'z');
            textBox1.Text = trackBar2.Value.ToString();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
                serialWrite(textBox2.Text);
                textBox2.Text = " ";
        }

        private   void serialWrite(string message)
        {
            try
            {
                serialPort.Write(message);
            }

            catch (InvalidOperationException)
            {
                MessageBox.Show("COM порт закрыт", "Ошибка");
            }
            catch (Exception ce)
            {
                MessageBox.Show(ce.ToString(), "Ошибка");
            }
        }
    }
}
