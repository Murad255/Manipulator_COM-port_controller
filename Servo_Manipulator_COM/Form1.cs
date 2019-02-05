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
            if (!serialPort.IsOpen)
            {
                serialPort.PortName = ((string)comboBox.SelectedItem);
                connectButton.Text = "вкл";
                serialPort.Open();
            }
            else
            {
                connectButton.Text = "Откл";
                serialPort.Close();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            try
            {
                serialPort.Write(trackBar1.Value.ToString());
                textBox1.Text = trackBar1.Value.ToString();
            }
            catch (Exception ce)
            {
                MessageBox.Show(ce.ToString());
            }

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            try
            {
                serialPort.Write('b'+trackBar2.Value.ToString()+'z');
                textBox1.Text = trackBar2.Value.ToString();
            }
            
            catch (InvalidOperationException)
            {
                MessageBox.Show("COM порт закрыт","Ошибка");
            }
            catch (Exception ce)
            {
                MessageBox.Show(ce.ToString(),"Ошибка");
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.Write(textBox2.Text);
                textBox2.Text = " ";
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("COM порт закрыт", "Ошибка");
            }
            catch (Exception ce)
            {
                MessageBox.Show(ce.ToString(),"Ошибка");
            }
        }
    }
}
