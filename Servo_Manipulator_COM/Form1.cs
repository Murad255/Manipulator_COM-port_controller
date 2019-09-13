using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using PointSpase;
using System.Threading;
using System.IO;
using Intersection;

namespace Servo_Manipulator_COM
{
    public partial class Form1 : Form
    {
        private const int WAIT_ANSWER_TIMEOUT = 500;
        private const int speed = 1 ;            
        private bool textBox_status = false;                //флаг направленности фокуса на элемент textBox2
        private volatile bool startExecution_status = false; //статус передачи комманд каналам
        private bool gripFlag = true;                        //флаг статуса сжатия/разжатия клешни

        Task send;                      //поток для приняти данных
        Task execution;                 //поток для отправки коллекции точек
        private Queue<char> RX_data;    //буфер для принятых данных
                    
        Points        points    =new Points();      //коллекция с точками, задающими координаты
        List<string>  sentData  =new List<string>();//коллекция со всеми данными координат точек для отправки
        List<int>     sentTime  =new List<int>();   //коллекция с задержками между координатами

        //признак отмены и признак получения этого признака при отправке точек
        static CancellationTokenSource eexecutionTokenSource = new CancellationTokenSource();
        static CancellationToken eexecutionToken = eexecutionTokenSource.Token;

        ProgramConfig programConfig;
        string[] loadArgument;

        public Form1(string[] args)
        {
            InitializeComponent();
            loadArgument = args;
            Point.sent = serialPort.Write;

            comboBox.Items.Clear();
            int portCount = 0; 
            foreach (string portName in System.IO.Ports.SerialPort.GetPortNames())
            {
                comboBox.Items.Add(portName);
                portCount++;
            }

            send        = new Task(() => { });
            execution   = new Task(() => { });
            send.Start();
            execution.Start();

            try
            {
                if(portCount>=programConfig.PortNum)
                    comboBox.SelectedIndex = programConfig.PortNum;
                comboHomeMode.SelectedIndex = 1; 
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
        
        public void Form1_ToString(string message)
        {
            this.Text = message;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        private async void connectButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.PortName = ((string)comboBox.SelectedItem);

                    await Task.Run(() =>
                    {   
                        try
                        {
                            serialPort.Open();
                            Home();
                            this.Invoke(new Action(()=> {
                                connectButton.Text = "вкл";
                                connectButton.BackColor = System.Drawing.Color.GreenYellow;
                            }));
                        }
                        catch (UnauthorizedAccessException)
                        {
                            MessageBox.Show("COM порт занят.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception soe)
                        {
                            MessageBox.Show(soe.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    });
                    
                }
                else
                {
                    serialPort.Close();
                    connectButton.Text = "откл";
                    connectButton.BackColor = System.Drawing.Color.Tomato;
                }
            }
            catch (IOException)
            {
                Console.Text += "\nПовторное подключение\n";
                connectButton_Click_1(new object(),new EventArgs() );
            }
            catch (ArgumentNullException )
            {
                DialogResult dialogResult= MessageBox.Show("Не выбран COM-порт.\nПовторить поиск?",
                                                            "Ошибка!",
                                                            MessageBoxButtons.OKCancel,
                                                            MessageBoxIcon.Error);
                if(dialogResult == DialogResult.OK)
                {
                    comboBox.Items.Clear();
                    int portCount = 0;
                    foreach (string portName in System.IO.Ports.SerialPort.GetPortNames())
                    {
                        comboBox.Items.Add(portName);
                        portCount++;
                    }
                    if (portCount >= 2) comboBox.SelectedIndex = 2;
                    comboHomeMode.SelectedIndex = 1;

                    connectButton_Click_1(sender, e);
                }
            }
            catch (Exception ce)
            {
                MessageBox.Show(ce.ToString(),
                                "Ошибка!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void trackBar_A_Scroll(object sender, EventArgs e)=> ScrollFunction('a', trackBar_A, label_A, valueCoordX);
        
        private void trackBar_B_Scroll(object sender, EventArgs e)=> ScrollFunction('b', trackBar_B, label_B, valueCoordY);
        
        private void trackBar_C_Scroll(object sender, EventArgs e)=> ScrollFunction('c', trackBar_C, label_C, valueCoordZ);

        private void trackBar_D_Scroll(object sender, EventArgs e)=> ScrollFunction('d', trackBar_D, label_D, valueCoordB);

        private void trackBar_E_Scroll(object sender, EventArgs e)=> ScrollFunction('e', trackBar_E, label_E, valueCoordA);

        private void trackBar_F_Scroll(object sender, EventArgs e)
        {
            serialWrite('f' + trackBar_F.Value.ToString() + 'z');
            Console.Text = trackBar_F.Value.ToString();
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
                checkGrip.Checked = false;
                gripFlag = false;
                serialWrite("f90z");
                trackBar_F.Value = Convert.ToInt32(100);
            }
            else if (gripFlag == false)
            {
                checkGrip.Checked = true;
                gripFlag = true;
                serialWrite("f1z");
                trackBar_F.Value = Convert.ToInt32(180);
            }
        }
        private void checkGrip_CheckedChanged(object sender, EventArgs e) => gripButton_Click(sender,e);


        private void serialWrite(string message)
        {
            try
            {
                serialPort.Write(message);
                Console.Text += "\r\n";
                Console.Text += message;
             
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


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Console.SelectionStart = Console.Text.Length;
            Console.ScrollToCaret();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)   // Отправка текста при нажатии на Enter 
        {
            if (e.KeyCode == Keys.Enter) button1_Click(sender, e);
        }


        /*
         * управление  каналами посредством нажатия клавиш:
         *  I и K           для канала A
         *  J и L           для канала B
         *  W и S           для канала C
         *  R и F           для канала D
         *  A и D           для канала E
         *  Spase для упровления захватом
         *  H для возвращения на базу
         *  Shift   сохраняет точку
         *  ctrl запуск операции
         *  G вернуться к прошлой операции
         */
        /// <summary>
        /// Обработчик нажатий клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {

            if (textBox_status == false)
            {
                if (e.KeyCode == Keys.Space)        gripButton_Click(sender, e);
                if (e.KeyCode == Keys.ShiftKey)     SaveButton_Click(sender, e);
                if (e.KeyCode == Keys.H)            Home();
                if (e.KeyCode == Keys.G)            return_point_Click(sender, e);
                if (e.KeyCode == Keys.ControlKey)  startExecution_Click(sender, e);

                KeyDownChangeValue(e, Keys.I, Keys.K, trackBar_B, () => trackBar_B_Scroll(sender, e));
                KeyDownChangeValue(e, Keys.J, Keys.L, trackBar_A, () => trackBar_A_Scroll(sender, e));
                KeyDownChangeValue(e, Keys.W, Keys.S, trackBar_C, () => trackBar_C_Scroll(sender, e));
                KeyDownChangeValue(e, Keys.F, Keys.R, trackBar_D, () => trackBar_D_Scroll(sender, e));
                KeyDownChangeValue(e, Keys.D, Keys.A, trackBar_E, () => trackBar_E_Scroll(sender, e));
                
            }
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(delay.Text) < 249) throw  new Exception("Задержка меньше 250 мс.\n");

                // if (points.PointsCoint != 0) Passing.pastPoint = points[points.PointsCoint ];
                if(checkAlgoritm.Checked)
                    points.Add(trackBar_A.Value,
                               trackBar_B.Value,
                               trackBar_C.Value,
                               trackBar_D.Value,
                               trackBar_E.Value,
                               trackBar_F.Value,
                               Convert.ToInt32(delay.Text)
                                   );
                else
                {
                     Point point = DecPointTransform.DecToPoint(  getDec(),
                                                                trackBar_F.Value,
                                                                Convert.ToInt32(delay.Text)
                                                                    );
                    points.Add(point);
                } 

                PointListView.Text = "";
                    foreach (Point p in points) PointListView.Text += p.numString(); //выводит список точек
              
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

        private void textBox2_Enter(object sender, EventArgs e)=>textBox_status = true;
        
        private void textBox2_Leave(object sender, EventArgs e)=>textBox_status = false;
        
        private void filePozition_Enter(object sender, EventArgs e)=>textBox_status = true;
        
        private void filePozition_Leave(object sender, EventArgs e)=>textBox_status = false;
        
        private void startExecution_Click(object sender, EventArgs e) => Execution2();

        private void clearPoints_Click(object sender, EventArgs e)
        {
            points = new Points();
            PointListView.Clear();
        }
        private void HowSaveButton_Click(object sender, EventArgs e)
        {
            if (savePointFile.ShowDialog() == DialogResult.Cancel) return;
            // получаем выбранный файл
            //string filename = openPointFile.FileName=Path.GetFileNameWithoutExtension(openPointFile.FileName);
            //openPointFile.SafeFileName;--если нужно расширение
            // читаем файл в строку
            filePozition.Text = savePointFile.FileName;
            points.Save(savePointFile.FileName);
           
           // LoadListButton.Enabled = false;
           
        }

        private void SaveListButton_Click(object sender, EventArgs e) => points.Save(filePozition.Text);

        private void LoadListButton_Click(object sender, EventArgs e)
        {
            points.Load(filePozition.Text);
            PointListView.Text = "";
            foreach (Point p in points) PointListView.Text += p.numString(); //выводит список точек
        }

        private void return_point_Click(object sender, EventArgs e)
        {
            Point pastP = new Point();
            pastP= Point.equivalent(points[points.Count-2]);
            trackBarSet(pastP);

            points.Add(pastP);
            pastP.writeCanal();
            PointListView.Text = "";
            foreach (Point p in points) PointListView.Text += p.numString(); //выводит список точек
        }

        private void checkAlgoritm_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAlgoritm.Checked)
            {
                valueCoordX.Enabled = false;
                valueCoordY.Enabled = false;
                valueCoordZ.Enabled = false;
                valueCoordA.Enabled = false;
                valueCoordB.Enabled = false;

                label1.Text = "канал А";
                label2.Text = "канал B";
                label3.Text = "канал C";
                label4.Text = "канал D";
                label5.Text = "канал E";

                Dec dec = getDec();
                Point point = DecPointTransform.DecToPoint(dec,trackBar_F.Value,0);

                trackBar_A.Maximum = 90;
                trackBar_A.Minimum = -90;
                trackBar_B.Maximum = 180;
                trackBar_B.Minimum = 0;
                trackBar_C.Maximum = 220;
                trackBar_C.Minimum = 40;
                trackBar_D.Maximum = 280;
                trackBar_D.Minimum = 100;
                trackBar_E.Maximum = 90;
                trackBar_E.Minimum = -90;

                trackBar_A.Value = point.CanA;
                trackBar_B.Value = point.CanB;
                trackBar_C.Value = point.CanC;
                trackBar_D.Value = point.CanD;
                trackBar_E.Value = point.CanE;

              

            }
            else
            {
                valueCoordX.Enabled = true;
                valueCoordY.Enabled = true;
                valueCoordZ.Enabled = true;
                valueCoordA.Enabled = true;
                valueCoordB.Enabled = true;

                label1.Text = "Ось X";
                label2.Text = "Ось Y";
                label3.Text = "Ось Z";
                label4.Text = "Наклон";
                label5.Text = "Горизонт";

                Point point = getPoint();
                Dec dec = DecPointTransform.PointToDec(point);

                trackBar_A.Maximum = DecPointTransform.Lmax;
                trackBar_A.Minimum = -DecPointTransform.Lmax;
                trackBar_B.Maximum = DecPointTransform.Lmax;
                trackBar_B.Minimum = 0;
                trackBar_C.Maximum = DecPointTransform.Lmax;
                trackBar_C.Minimum = -100;
                trackBar_D.Maximum = 180;
                trackBar_D.Minimum = 0;
                trackBar_E.Maximum = 90;
                trackBar_E.Minimum = -90;

                trackBar_A.Value = (int)dec.decX;
                trackBar_B.Value = (int)dec.decY;
                trackBar_C.Value = (int)dec.decZ;
                trackBar_D.Value = (int)dec.decB;
                trackBar_E.Value = (int)dec.decA;

                valueCoordX.Text = trackBar_A.Value.ToString();
                valueCoordY.Text = trackBar_B.Value.ToString();
                valueCoordZ.Text = trackBar_C.Value.ToString();
                valueCoordA.Text = trackBar_E.Value.ToString();
                valueCoordB.Text = trackBar_D.Value.ToString();
            }
        }

        private void ButtonOpenFile_Click(object sender, EventArgs e)
        {
            if (openPointFile.ShowDialog() == DialogResult.Cancel)return;
            // получаем выбранный файл
            //string filename = openPointFile.FileName=Path.GetFileNameWithoutExtension(openPointFile.FileName);
                //openPointFile.SafeFileName;--если нужно расширение
                // читаем файл в строку
            filePozition.Text = openPointFile.FileName;
            LoadListButton_Click(sender,e);
            LoadListButton.Enabled = false;
        }

        private void filePozition_TextChanged(object sender, EventArgs e)
        {
            LoadListButton.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (loadArgument.Length>0)
            {
                
                string filename = loadArgument[0]; //openPointFile.SafeFileName;--если нужно расширение
                                                                                     // читаем файл в строку
                filePozition.Text = filename;
                LoadListButton_Click(sender, e);
                LoadListButton.Enabled = false;

                programConfig = ProgramConfig.Instance;
                ProgramConfig.Load();

            }
        }

        
    }
}