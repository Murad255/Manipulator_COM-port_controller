using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using PointSpase;

namespace Servo_Manipulator_COM
{
    class Passing
    {
        private const int value = 15;
        public delegate void SendFunc(int num);
        static Task sendTask;
        public static Point pastPoint = new Point();


        public static void write(string mes)
        {
            Console.Write(mes + '\t');

        }
        static public void sinFunc(int pastCoint, int nextCoint, Sent func, int time = 0)
        {

            double a = (nextCoint - pastCoint) / 2;
            int delay = (time / value);
            for (int i = 0; i < value; i++)
            {

                int coint = Convert.ToInt32(a * (-Math.Cos(3.14 / Convert.ToDouble(value) * Convert.ToDouble(i)) + 1) + pastCoint);
                func(coint.ToString());
                if (time != 0) Thread.Sleep(delay);
            }
        }

        static public void sinFunc(Point pastCoint, Point nextCoint, Sent func, int time)
        {
            Point.sent = func;
            sendTask = new Task(() => { });
            sendTask.Start();
            int delay = (time / value) * 6 / 10;
            for (int i = 0; i <= value; i++)
            {
                oneSinFunc(pastCoint.CanA, nextCoint.CanA, func,'a', i);
                oneSinFunc(pastCoint.CanB, nextCoint.CanB, func,'b', i);
                oneSinFunc(pastCoint.CanC, nextCoint.CanC, func,'c', i);
                oneSinFunc(pastCoint.CanD, nextCoint.CanD, func,'d', i);
                oneSinFunc(pastCoint.CanE, nextCoint.CanE, func,'e', i);
                oneSinFunc(pastCoint.CanF, nextCoint.CanF, func,'f', i);
                Thread.Sleep(delay); 
            }
            
        }

        static private void oneSinFunc(int pastCoint, int nextCoint, Sent func,char numCanal , int cycle)
        {
            double a = (nextCoint - pastCoint) / 2;
            int coint = Convert.ToInt32(a * (-Math.Cos(3.14 / Convert.ToDouble(value) * Convert.ToDouble(cycle)) + 1) + pastCoint);
            sendTask.Wait();
            sendTask = Task.Run(() => func(numCanal + coint.ToString() + 'z'));
            //sendTask.ContinueWith((Task t) =>func(numCanal + coint.ToString() + 'z') , TaskContinuationOptions.NotOnFaulted);
           
        } 
    }
}

