using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PointSpase;
using Windows.UI;
using ManipulatorSerialInterfase;
using KinematicModeling;
using Windows.UI.Core;
using KinematicTask;

namespace Manipulator_UWP
{

    public delegate void SendMessage(string message,  Color colors);

    public static class CommonFunction
    {
        static SendMessage  sendMessage;
        static Point        commonPoint = new Point();
        static Dec          commonDec   = new Dec();
        static Points       pointList   = new Points();
        static private Task execution;               //поток для отправки коллекции точек

        static ManipulatorSerialPort    serialPort = ManipulatorSerialPort.Instance;

        static CancellationTokenSource  eexecutionTokenSource = new CancellationTokenSource();
        static CancellationToken        eexecutionToken       = eexecutionTokenSource.Token;
        static private volatile bool    startExecution_status = false; //статус передачи комманд каналам

        static public  bool StartExecution_status
        {
            get { return startExecution_status;}
        }

        static public SendMessage SendMessage
        {
            set { sendMessage=value; }
        }
        static private volatile bool cycleStatus = false;
        /// <summary>
        /// флаг зацикленности процесса отправки точек
        /// </summary>
        static public bool CycleStatus
        {
            get { return cycleStatus; }
            set { cycleStatus = value; }
        }
        static public Points PointList
        {
                get{ return pointList; }
                set { if (value != null) pointList = value; }
        }

        /// <summary>
        /// текущее значение точек
        /// </summary>
        static public Point CommonPoint
        {
            get{ return commonPoint; }
            set{ commonPoint = value; }         
        }
        static public Dec CommonDec
        {
            get { return commonDec; }
            set { commonDec = value; }
        }
        /// <summary>
        /// поток для отправки коллекции точек
        /// </summary>
        static public Task Execution
        {
            get { return execution; }
        }

        static void Home()
        {

        }
        /// <summary>
        /// Установить функцию для отправки сообщений в консоль
        /// </summary>
        /// <param name="send"></param>
        static public void SetSendMessage(SendMessage send)
        {
            sendMessage = send;
        }
    

        static public void CommonConsoleWrite(string message, Color colors)
        {
            if (sendMessage != null) sendMessage(message, colors);
        }
        static public void CommonConsoleWrite(string message)
        {
            if (sendMessage != null) sendMessage(message,Colors.Black);
        }


        /// <summary>
        /// Выполняет проход по массиву точек pointList
        /// </summary>
        /// <param name="action">Метод, вызываемый для обновления индикатора работы 
        /// (метод проверяет флаг StartExecution_status)</param>
        /// <param name="debugFlag"></param>
        static public void ExecutionProcess(Action action,bool debugFlag=false)
        {
            try
            {
                //для предварительной остановки
                eexecutionTokenSource = new CancellationTokenSource();
                eexecutionToken = eexecutionTokenSource.Token;

                if (!serialPort.IsOpen) throw new InvalidOperationException();
                if (!startExecution_status)
                {                    
                    startExecution_status = true;
                    action();

                    execution = new Task(()=>
                    {
                        try
                        {
                            Point[] tempPoints = new Point[pointList.Count];
                            pointList.CopyTo(tempPoints);
                            do
                            {
                                Passing.pastPoint = tempPoints[0];
                                //проходимся по всем точкам и выролняем их отправку 
                                foreach (Point point in tempPoints)
                                {
                                    if (eexecutionToken.IsCancellationRequested) return; //принудительное закрытие задачи
                                    Passing.PassingAlgoritm(Passing.pastPoint,
                                                    point,
                                                    serialPort.Write);
                                    Passing.pastPoint = point;
                                    if (debugFlag) CommonConsoleWrite(point.ToString());
                                }

                            } while (cycleStatus && startExecution_status);

                            startExecution_status = false;

                            action();
                        }
                        catch(Exception e)
                        {
                            CommonConsoleWrite("ExecutionProcess:\t"+e.Message);
                        }
                    });
                    execution.Start();
                                                       }
                else
                {
                    startExecution_status = false;  //в зависимости от него переключить внешний вид кнопки запуска программы точек
                    action();
                    eexecutionTokenSource.Cancel();
                }
            }
            catch (Exception ce)
            {
                CommonConsoleWrite(ce.Message, Colors.Red);
            }
        }

        static public void ExecutionProcess2(Action action, bool debugFlag = false)
        {
            try
            {
                //для предварительной остановки
                eexecutionTokenSource = new CancellationTokenSource();
                eexecutionToken = eexecutionTokenSource.Token;

                if (!serialPort.IsOpen) throw new InvalidOperationException();
                if ((!startExecution_status) && pointList.Count > 1)
                {
                    startExecution_status = true;
                    action();

                    execution = new Task(() =>
                    {
                        try
                        {
                            List<Point> tempPoints = new List<Point>();
                            Passing.pastPoint = pointList[0];
                            //проходимся по всем точкам и выролняем их отправку 
                            foreach (Point point in pointList)
                            {
                                tempPoints.AddRange(Passing.PassingAlgoritm(Passing.pastPoint, point));
                                Passing.pastPoint = point;
                            }

                            do
                            {
                                Passing.PassingAlgoritm(CommonPoint,
                                    pointList[0],
                                    serialPort.Write);
                                foreach (Point point in tempPoints)
                                {
                                    if (eexecutionToken.IsCancellationRequested) return; //принудительное закрытие задачи
                                    serialPort.Write(point);
                                    Thread.Sleep((int)point.Time);

                                    if (debugFlag) CommonConsoleWrite(point.ToString());
                                }
                                commonPoint  = Point.equivalent(tempPoints[tempPoints.Count-1]);
                            } while (cycleStatus && startExecution_status);

                            startExecution_status = false;
                            action();
                        }
                        catch (Exception e)
                        {
                            CommonConsoleWrite("ExecutionProcess:\t" + e.Message);
                        }
                    });
                    execution.Start();
                }
                else
                {
                    startExecution_status = false;  //в зависимости от него переключить внешний вид кнопки запуска программы точек
                    action();
                    eexecutionTokenSource.Cancel();
                }
            }
            catch (Exception ce)
            {
                CommonConsoleWrite(ce.Message, Colors.Red);
            }
        }

    }

   
}
