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

            if (programConfig.Strategy  is SinPassingStrategy) trackBarStrategy.Value = 0;
            
            else if (programConfig.Strategy is LinearPassingStrategy) trackBarStrategy.Value = 1;
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

        private void TrackBarStrategy_Scroll(object sender, EventArgs e)
        {
            if (trackBarStrategy.Value == 0)
            {
                programConfig.Strategy = new SinPassingStrategy();
                Passing.ContextStrategy = new SinPassingStrategy();
            }
            else if (trackBarStrategy.Value == 1)
            {
                programConfig.Strategy = new LinearPassingStrategy();
                Passing.ContextStrategy = new LinearPassingStrategy();
            }
        }
    }
}
