using Intersection;
using PointSpase;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Servo_Manipulator_COM
{
    public partial class Form1
    {
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
                    Point.tempPoint.write();
                    textBox1.Text = trackBar.Value.ToString();
                    label.Text = trackBar.Value.ToString();
                }
                else
                {
                    textBox.Text = trackBar.Value.ToString();
                    label.Text = trackBar.Value.ToString();
                    Point point = DecPointTransform.DecToPoint(getDec(),
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

        /// <summary>
        /// Устанавливает положение манипулятора в исходное состояние
        /// </summary>
        private void Home()
        {
            try
            {
                if (checkAlgoritm.Checked)
                {
                    if ((string)comboHomeMode.SelectedItem == "work")
                    {
                        Point homePoint = new Point(90, 40, 47, 160, 90, 140);
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
                else
                {
                    if ((string)comboHomeMode.SelectedItem == "work")
                    {
                        Dec homeDec = new Dec(0, 85, 135, 120, 0);
                        trackBarSet(homeDec);
                        trackBar_A_Scroll(new object(), new EventArgs());
                    }
                    else if ((string)comboHomeMode.SelectedItem == "steady")
                    {
                        Dec homeDec = new Dec(0, 80, 170, 1, 0);
                        trackBarSet(homeDec);
                        trackBar_A_Scroll(new object(), new EventArgs());
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
        private void SendData(Queue<char> data)
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

        private void trackBarSet(Point p)
        {

            trackBar_A.Value = p.CanA - 90;
            trackBar_B.Value = (p.CanB - 180) * (-1);
            trackBar_C.Value = p.CanC + 40;
            trackBar_D.Value = p.CanD + 100;
            trackBar_E.Value = p.CanE - 90;
            trackBar_F.Value = p.CanF;

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
            trackBar_D.Value = (int)d.decA;
            trackBar_E.Value = (int)d.decB;
            trackBar_F.Value = grab;

            label_A.Text = d.decX.ToString();
            label_B.Text = d.decY.ToString();
            label_C.Text = d.decZ.ToString();
            label_D.Text = d.decA.ToString();
            label_E.Text = d.decB.ToString();
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
        private void Execution()
        {
            try
            {
                eexecutionTokenSource = new CancellationTokenSource();
                eexecutionToken = eexecutionTokenSource.Token;

                if (!serialPort.IsOpen) throw new InvalidOperationException();
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
                            foreach (Point p in tempPoints)
                            {
                                if (eexecutionToken.IsCancellationRequested) return; //принудительное закрытие задачи
                                Passing.sinFunc(Passing.pastPoint,
                                                p, serialPort.Write);
                                Passing.pastPoint = p;
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
        private async void Execution2()
        {
            try
            {
                eexecutionTokenSource = new CancellationTokenSource();
                eexecutionToken = eexecutionTokenSource.Token;

                if (!serialPort.IsOpen) throw new InvalidOperationException();
                if (!startExecution_status)
                {
                    startExecution_status = true;
                    startExecution.Text = "Идёт отправка";
                    startExecution.BackColor = System.Drawing.Color.GreenYellow;
                    await Task.Run(() =>
                    {
                        sentData = new List<string>();
                        sentTime = new List<int>();
                        Point[] tempPoints = new Point[points.Count];
                        points.CopyTo(tempPoints);
                        Passing.pastPoint = tempPoints[0];

                        foreach (Point p in tempPoints)
                        {

                            Passing.sinFunc(Passing.pastPoint,
                                            p,
                                            (string data) =>
                                            {
                                                sentData.Add(data);
                                            },
                                            (string data) =>
                                            {
                                                sentTime.Add(Convert.ToInt32(data));
                                            });
                            Passing.pastPoint = p;
                        }

                    });

                    execution = Task.Run(new Action(() =>
                    {
                        Point[] tempPoints = new Point[points.Count];
                        points.CopyTo(tempPoints);
                        do
                        {

                            for (int i = 0; i < sentData.Count; i++)
                            {
                                if (eexecutionToken.IsCancellationRequested) return; //принудительное закрытие задачи
                                serialPort.Write(sentData[i]);
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

    }

}