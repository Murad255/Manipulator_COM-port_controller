using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using Intersection;
using PointSpase;
using System.Threading;


namespace Servo_Manipulator_COM
{
    public partial class Form1 : Form
    {
        private const int WAIT_ANSWER_TIMEOUT = 500;
        private const int speed = 1 ;            
        private bool textBox2_status = false;                //флаг направленности фокуса на элемент textBox2
        private volatile bool startExecution_status = false; //статус передачи комманд каналам
        private bool gripFlag = true;                        //флаг статуса сжатия/разжатия клешни

        Task send;                      //поток для приняти данных
        Task execution;                 //поток для отправки коллекции точек
        private Queue<char> RX_data;    //буфер для принятых данных
                    
        Points points =new Points();    //коллекция с точками, задающими координаты
                                        //Point pastPoint = new Point();

        //признак отмены и признак получения этого признака при отправке точек
        static CancellationTokenSource eexecutionTokenSource = new CancellationTokenSource();
        static CancellationToken eexecutionToken = eexecutionTokenSource.Token;

        public Form1()
        {
            InitializeComponent();
            Point.sent = serialPort.Write;  // serialWrite;
            comboBox.Items.Clear();
            int portCount = 0; 
            foreach (string portName in System.IO.Ports.SerialPort.GetPortNames())
            {
                comboBox.Items.Add(portName);
                portCount++;
            }

            send        = new Task(()=> { });
            execution   = new Task(() => { });
            send.Start();
            execution.Start();

            try
            {
                if(portCount>=2)comboBox.SelectedIndex = 2;
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
          
            catch (Exception ce)
            {
                MessageBox.Show(ce.ToString(),
                                "Ошибка!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        /*обработка действия слайдеров*/

        private void ScrollFunction(char ch,TrackBar trackBar, Label label,TextBox textBox)
        {
            try
            {
                if (checkAlgoritm.Checked)
                {
                    //serialWrite(ch + trackBar.Value.ToString() + 'z');
                    Point.tempPoint[ch] = trackBar.Value;
                    Point.tempPoint.write();
                    textBox1.Text = trackBar.Value.ToString();
                    label.Text = trackBar.Value.ToString();
                }
                else
                {
                    textBox.Text = trackBar.Value.ToString();
                    label.Text = trackBar.Value.ToString();
                    Point point = DecPointTransform.Algoritm(getDec(),
                                                                trackBar_F.Value,
                                                                Convert.ToInt32(delay.Text)
                                                                );
                    point.writeCanal();
                }
                this.Text = "COM-консоль";
            }
            catch (Exception)
            {
                this.Text = "Ошибка!";
            }
        }

        private void trackBar_A_Scroll(object sender, EventArgs e)=> ScrollFunction('a', trackBar_A, label_A, valueCoordX);
        
        private void trackBar_B_Scroll(object sender, EventArgs e)=> ScrollFunction('b', trackBar_B, label_B, valueCoordY);
        
        private void trackBar_C_Scroll(object sender, EventArgs e)=> ScrollFunction('c', trackBar_C, label_C, valueCoordZ);

        private void trackBar_D_Scroll(object sender, EventArgs e)=> ScrollFunction('d', trackBar_D, label_D, valueCoordA);

        private void trackBar_E_Scroll(object sender, EventArgs e)=> ScrollFunction('e', trackBar_E, label_E, valueCoordB);

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
                Point homePoint = new Point(90,40,47,160,90,140);
                Point.tempPoint = homePoint;
                homePoint.write();
                trackBarSet(homePoint);

            }
            else if ((string)comboHomeMode.SelectedItem == "steady")
            {
                Point homePoint = new Point(90, 30, 14, 1, 90, 155);
                Point.tempPoint = homePoint;
                homePoint.write();
                trackBarSet(homePoint);
            }
        }


        private void trackBarSet(Point p)
        {
            trackBar_A.Value = p.CanA-90;
            trackBar_B.Value = (p.CanB-180)*(-1);
            trackBar_C.Value = p.CanC+40;
            trackBar_D.Value = p.CanD+100;
            trackBar_E.Value = p.CanE-90;
            trackBar_F.Value = p.CanF;
            
            label_A.Text = p.CanA.ToString(); 
            label_B.Text = p.CanB.ToString();
            label_C.Text = p.CanC.ToString();
            label_D.Text = p.CanD.ToString();
            label_E.Text = p.CanE.ToString();
            label_F.Text = p.CanF.ToString();
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
        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {

            if (textBox2_status == false)
            {
                if (e.KeyCode == Keys.Space)        gripButton_Click(sender, e);
                if (e.KeyCode == Keys.ShiftKey)     SaveButton_Click(sender, e);
                if (e.KeyCode == Keys.H)            Home();
                if (e.KeyCode == Keys.G)            return_point_Click(sender, e);
                if (e.KeyCode == Keys.ControlKey)  startExecution_Click(sender, e);

                if (e.KeyCode == Keys.I)
                {
                    if (trackBar_B.Value != trackBar_B.Maximum)
                    {
                        trackBar_B.Value+= speed;
                        trackBar_B_Scroll(sender, e);
                    }
                }
                if (e.KeyCode == Keys.K)
                {
                    if (trackBar_B.Value != trackBar_B.Minimum)
                    {
                        trackBar_B.Value-=speed;
                        trackBar_B_Scroll(sender, e);
                    }
                }

                if (e.KeyCode == Keys.J)
                {
                    if (trackBar_A.Value != trackBar_A.Maximum)
                    {
                        trackBar_A.Value += speed;
                        trackBar_A_Scroll(sender, e);
                    }
                }
                if (e.KeyCode == Keys.L)
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
                    if ((trackBar_E.Value != trackBar_E.Maximum) && (trackBar_E.Value + speed * 2 != trackBar_E.Maximum))
                    {
                        trackBar_E.Value += speed*2;
                        trackBar_E_Scroll(sender, e);
                    }
                }
                if (e.KeyCode == Keys.A)
                {
                    if ((trackBar_E.Value != trackBar_E.Minimum)&&(trackBar_E.Value- speed * 2 != trackBar_E.Minimum))
                    {
                        trackBar_E.Value -= speed*2;
                        trackBar_E_Scroll(sender, e);
                    }
                }

                if (e.KeyCode == Keys.F)
                {
                    if (trackBar_D.Value != trackBar_D.Maximum)
                    {
                        trackBar_D.Value += speed;
                        trackBar_D_Scroll(sender, e);
                    }
                }
                if (e.KeyCode == Keys.R)
                {
                    if (trackBar_D.Value != trackBar_D.Minimum)
                    {
                        trackBar_D.Value -= speed;
                        trackBar_D_Scroll(sender, e);
                    }
                }
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
                     Point point = DecPointTransform.Algoritm(  getDec(),
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
        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2_status = true;
        
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox2_status = false;
        }

        private void startExecution_Click(object sender, EventArgs e)
        {
            try
            {
                eexecutionTokenSource = new CancellationTokenSource();
                eexecutionToken = eexecutionTokenSource.Token;

                if (!serialPort.IsOpen)  throw new InvalidOperationException();
                if (!startExecution_status)
                {
                    
                    startExecution_status =true;
                    startExecution.Text = "Идёт отправка";
                    startExecution.BackColor = System.Drawing.Color.GreenYellow;
                   
                    execution = Task.Run(new Action(() => {
                        Point[] tempPoints=new Point[points.Count];
                        points.CopyTo(tempPoints);
                        do {
                            foreach (Point p in tempPoints)
                            {
                                if (eexecutionToken.IsCancellationRequested) return; //принудительное закрытие задачи
                                Passing.sinFunc(Passing.pastPoint, 
                                                p, serialPort.Write, 
                                                Convert.ToInt32(p.getTime()));
                                Passing.pastPoint = p;
                            }
                            
                        } while (cycleStatus.Checked && startExecution_status);

                       //доступ к элементам формы не из главного потока
                            if (InvokeRequired)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    startExecution_status = false;
                                    startExecution.Text = "начать отправку";
                                    startExecution.BackColor = System.Drawing.Color.White;
                                }));
                            }
                            else
                            {
                                startExecution_status = false;
                                startExecution.Text = "начать отправку";
                                startExecution.BackColor = System.Drawing.Color.White;
                            }

                    }));
                }
                else
                {
                    startExecution_status = false;
                    eexecutionTokenSource.Cancel();
                    startExecution.Text = "начать отправку";
                    startExecution.BackColor = System.Drawing.Color.White;
                    
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("COM порт закрыт!",
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
        

        private void clearPoints_Click(object sender, EventArgs e)
        {
            points = new Points();
            PointListView.Clear();
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
                label4.Text = "Горизонт";
                label5.Text = "Наклон";

                trackBar_A.Maximum = DecPointTransform.Lmax;
                trackBar_A.Minimum = -DecPointTransform.Lmax;
                trackBar_B.Maximum = DecPointTransform.Lmax;
                trackBar_B.Minimum = 0;
                trackBar_C.Maximum = DecPointTransform.Lmax;
                trackBar_C.Minimum = -DecPointTransform.Lmax;
                trackBar_D.Maximum = 280;
                trackBar_D.Minimum = 100;
                trackBar_E.Maximum = 180;
                trackBar_E.Minimum = 0;

                valueCoordX.Text = trackBar_A.Value.ToString();
                valueCoordY.Text = trackBar_B.Value.ToString();
                valueCoordZ.Text = trackBar_C.Value.ToString();
                valueCoordA.Text = trackBar_D.Value.ToString();
                valueCoordB.Text = trackBar_E.Value.ToString();
            }
        }

        private Dec getDec()
        {
            return new Dec( Convert.ToDouble(valueCoordX.Text),
                            Convert.ToDouble(valueCoordY.Text),
                            Convert.ToDouble(valueCoordZ.Text),
                            Convert.ToDouble(valueCoordA.Text),
                            Convert.ToDouble(valueCoordB.Text)
                            );
        }
       
    }
}