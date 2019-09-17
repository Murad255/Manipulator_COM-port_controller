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
    public partial class ConfigForm : Form
    {
        private ProgramConfig programConfig = ProgramConfig.Instance;
        public ConfigForm()
        {
            InitializeComponent();
            SerialRate.Text = programConfig.Speed.ToString();
            this.SerialRate.SelectedIndexChanged += new System.EventHandler(this.SerialRate_SelectedIndexChanged);
        }

        private void SerialRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1 frm = (Form1)this.Owner;
            if (frm.SerialPort.IsOpen)
            {
               // frm.SerialPort.Close();
                frm.SerialPort.BaudRate = Convert.ToInt32(SerialRate.Text);
               // frm.SerialPort.Open();
            }
            programConfig.Speed= Convert.ToInt32(SerialRate.Text);
        }
    }
}
