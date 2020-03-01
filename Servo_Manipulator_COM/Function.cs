using Intersection;
using PointSpase;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Servo_Manipulator_COM
{
    public partial class Form1: Form
    {
        private int errorCount = 0;
        /// <summary>
        /// обработка действия слайдеров
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="trackBar"></param>
        /// <param name="label"></param>
        /// <param name="textBox"></param>
        private void ScrollFunction(char ch, TrackBar trackBar, Label label, TextBox textBox)
        {
            try
            {
                if (checkAlgoritm.Checked)
                {
                    //serialWrite(ch + trackBar.Value.ToString() + 'z');
                    Point.tempPoint[ch] = trackBar.Value;
                    Console.Text = serialPort.WriteEndDebug(Point.tempPoint);
                    //trackBar.Value.ToString();
                    //label.Text = trackBar.Value.ToString();
                    labelUpdate();
                }
                else
                {
                    textBox.Text = trackBar.Value.ToString();
                    label.Text = trackBar.Value.ToString();
                    Point point = DecPointTransform.DecToPoint( getDec(),
                                                                trackBar_F.Value,
                                                                Convert.ToInt32(delay.Text)
                                                                );
                    serialPort.Write(Point.tempPoint);
                }
                this.Text = "COM-консоль";
            }
            catch (Exception e)
            {
                
                this.Text = "Ошибка!";
                Console.Text += "Ошибка!"+ errorCount .ToString()+ e.Message+'\n';
            }
        }
         
        private void labelUpdate()
        {
            if (checkAlgoritm.Checked)
            {
                Point p = Point.tempPoint;
                label_A.Text = p.CanA.ToString();
                label_B.Text = p.CanB.ToString();
                label_C.Text = p.CanC.ToString();
                label_D.Text = p.CanD.ToString();
                label_E.Text = p.CanE.ToString();
                label_F.Text = p.CanF.ToString();
            }
        }

        /// <summary>
        /// Устанавливает положение манипулятора в исходное состояние
        /// </summary>
        public void Home()
        {
            try
            {
                if (checkAlgoritm.Checked)
                {
                    if ((string)comboHomeMode.SelectedItem == "work")
                    {
                        Point homePoint = new Point(0, 0, 8, 0, 245, 0);

                        Point.tempPoint = homePoint;
                        serialPort.Write(homePoint);
                        trackBarSet(homePoint);

                    }
                    else if ((string)comboHomeMode.SelectedItem == "steady")
                    {
                        Point homePoint = new Point(0, 45, 87, 0, 240, 0);
                        Point.tempPoint = homePoint;
                        serialPort.Write(homePoint);
                        trackBarSet(homePoint);
                    }
                }
                else
                {
                    if ((string)comboHomeMode.SelectedItem == "work")
                    {
                        //Dec homeDec = new Dec(0, 85, 135, 120, 0);
                        //trackBarSet(homeDec);
                        //trackBar_A_Scroll(new object(), new EventArgs());
                        Point homePoint = new Point(0, 53, 95, 0, 245, 0);
                        Dec homeDec = DecPointTransform.PointToDec(homePoint);
                        trackBarSet(homeDec);
                        trackBar_D_Scroll(new object(), new EventArgs());

                    }
                    else if ((string)comboHomeMode.SelectedItem == "steady")
                    {
                        Point homePoint = new Point(0, 150, 54, 120, 0, 155);
                        Dec homeDec = DecPointTransform.PointToDec(homePoint);
                        trackBarSet(homeDec);
                        trackBar_D_Scroll(new object(), new EventArgs());
                    }
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

        /// <summary>
        /// функция для отправки сообщения в textBox1
        /// </summary>
        /// <param name="data"></param>
      

        private void trackBarSet(Point p)
        {
            trackBar_A.Value = (int)p.CanA;
            trackBar_B.Value = (int)p.CanB;
            trackBar_C.Value = (int)p.CanC;
            trackBar_D.Value = (int)p.CanD;
            trackBar_E.Value = (int)p.CanE;
            trackBar_F.Value = (int)p.CanF;

            label_A.Text = p.CanA.ToString();
            label_B.Text = p.CanB.ToString();
            label_C.Text = p.CanC.ToString();
            label_D.Text = p.CanD.ToString();
            label_E.Text = p.CanE.ToString();
            label_F.Text = p.CanF.ToString();
        }

        private void trackBarSet(Dec d, int grab = 155)
        {
            trackBar_A.Value = (int)d.decX;
            trackBar_B.Value = (int)d.decY;
            trackBar_C.Value = (int)d.decZ;
            trackBar_D.Value = (int)d.decB;
            trackBar_E.Value = (int)d.decA; 
            trackBar_F.Value = grab;

            label_A.Text = ((int)d.decX).ToString();
            label_B.Text = ((int)d.decY).ToString();
            label_C.Text = ((int)d.decZ).ToString();
            label_D.Text = ((int)d.decB).ToString();
            label_E.Text = ((int)d.decA).ToString();
            label_F.Text = grab.ToString();

            valueCoordX.Text = d.decX.ToString();
            valueCoordY.Text = d.decY.ToString();
            valueCoordZ.Text = d.decZ.ToString();
            valueCoordA.Text = d.decB.ToString();
            valueCoordB.Text = d.decA.ToString();
        }

        private void KeyDownChangeValue(KeyEventArgs e, Keys upKeys, Keys downKeys, TrackBar trackBar, Action action)
        {
            if (e.KeyCode == upKeys)
            {
                if (trackBar.Value != trackBar.Maximum)
                {
                    trackBar.Value += speed;
                    action();
                }
            }
            if (e.KeyCode == downKeys)
            {
                if (trackBar.Value != trackBar.Minimum)
                {
                    trackBar.Value -= speed;
                    action();
                }
            }
        }
        
        /// <summary>
        /// отправка точек через функцию интерполяции
        /// </summary>
        private void Execution()
        {
            try
            {
                eexecutionTokenSource = new CancellationTokenSource();
                eexecutionToken = eexecutionTokenSource.Token;

                if (!serialPort.Port.IsOpen) throw new InvalidOperationException();
                if (!startExecution_status)
                {
                    startExecution_status = true;
                    startExecution.Text = "Идёт отправка";
                    startExecution.BackColor = System.Drawing.Color.GreenYellow;

                    execution = Task.Run(new Action(() =>
                    {
                        Point[] tempPoints = new Point[points.Count];
                        points.CopyTo(tempPoints);
                        do
                        {
                            Passing.pastPoint = tempPoints[0];
                            foreach (Point point in tempPoints)
                            {
                                if (eexecutionToken.IsCancellationRequested) return; //принудительное закрытие задачи
                                Passing.sinFunc(Passing.pastPoint,
                                                point, 
                                                serialPort.Write);
                                Passing.pastPoint = point;
                            }

                        } while (cycleStatus.Checked && startExecution_status);

                        this.Invoke(new Action(() =>
                        {
                            startExecution_status = false;
                            startExecution.Text = "начать отправку";
                            startExecution.BackColor = System.Drawing.Color.White;
                        }));
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
        /// <summary>
        /// запись в лист и отправка точек
        /// </summary>
        private async void Execution2()
        {
            try
            {
                if (points.Count == 0) return;
                eexecutionTokenSource = new CancellationTokenSource();
                eexecutionToken = eexecutionTokenSource.Token;

                if (!serialPortBase.IsOpen) throw new InvalidOperationException();
                if (!startExecution_status)
                {
                    startExecution_status = true;
                    startExecution.Text = "Идёт отправка";
                    startExecution.BackColor = System.Drawing.Color.GreenYellow;
                    //создание пакета команд
                    await Task.Run(() =>
                    {
                        sentData = new List<string>();
                        sentTime = new List<int>();
                        Point[] tempPoints = new Point[points.Count];
                        points.CopyTo(tempPoints);
                        Passing.pastPoint = tempPoints[0];

                        foreach (Point p in tempPoints)
                        {

                            //Passing.sinFunc(Passing.pastPoint,
                            //                p,
                                            //(string data) =>
                                            //{
                                            //    sentData.Add(data);
                                            //},
                                            //(string data) =>
                                            //{
                                            //    sentTime.Add(Convert.ToInt32(data));
                                            //});
                            Passing.pastPoint = p;
                        }

                    });
                    
                    //отправка покета комманд
                    execution = Task.Run(new Action(() =>
                    {
                        var diag = new Stopwatch();
                        Point[] tempPoints = new Point[points.Count];
                        points.CopyTo(tempPoints);
                        do
                        {
                            for (int i = 0; i < sentData.Count; i++)
                            {
                                //принудительное закрытие задачи
                                if (eexecutionToken.IsCancellationRequested) return; 
                                serialPortBase.Write(sentData[i]);
                                Thread.Sleep(sentTime[i]);
                            }
                        } while (cycleStatus.Checked && startExecution_status);

                        this.Invoke(new Action(() =>
                        {
                            startExecution_status = false;
                            startExecution.Text = "начать отправку";
                            startExecution.BackColor = System.Drawing.Color.White;
                        }));
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


        /// <summary>
        /// Возвращает обьект Dec по данным в текстовых полях 
        /// </summary>
        /// <returns></returns>
        private Dec getDec()
        {
            return new Dec(Convert.ToDouble(valueCoordX.Text),
                            Convert.ToDouble(valueCoordY.Text),
                            Convert.ToDouble(valueCoordZ.Text),
                            Convert.ToDouble(valueCoordA.Text),
                            Convert.ToDouble(valueCoordB.Text)
                            );
        }

        private Point getPoint()
        {
            return new Point(   trackBar_A.Value,
                                trackBar_B.Value,
                                trackBar_C.Value,
                                trackBar_D.Value,
                                trackBar_E.Value,
                                trackBar_F.Value,
                                0
                             );
        }

    }
}