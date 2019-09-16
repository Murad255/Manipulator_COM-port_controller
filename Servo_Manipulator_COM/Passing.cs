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
        private const long passingConstValue = 15;
        private const int passingTimeLimit = 750;
        static Task sendTask;
        public static Point pastPoint = new Point();
        static private long passingValue =0;

        public static void write(string mes)
        {
            Console.Write(mes + '\t');

        }
        //для чисел
        static public void sinFunc(int pastCoint, int nextCoint, Sent func, int time = 0)
        {

            double a = (nextCoint - pastCoint) / 2;
            int delay = (int)(time / passingValue);
            for (int i = 0; i < passingValue; i++)
            {

                int coint = Convert.ToInt32(a * (-Math.Cos(3.14 / Convert.ToDouble(passingValue) * Convert.ToDouble(i)) + 1) + pastCoint);
                func(coint.ToString());
                if (time != 0) Thread.Sleep(delay);
            }
        }
        //для точек
            //для отправки точек 
        static public void sinFunc(Point pastCoint, Point nextCoint, Sent func)
        {
            long time = nextCoint.Time;
            Point.sent = func;
            sendTask = new Task(() => { });
            sendTask.Start();
            if (time < passingTimeLimit) passingValue = passingConstValue;
            else passingValue = time / 50;

            int delay = (int)(time / passingValue * 9 / 10);
            for (int i = 0; i <= passingValue; i++)
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
                //для записи точек
        static public void sinFunc(Point pastCoint, Point nextCoint, Sent funcData, Sent funcTime)
        {
            long time = nextCoint.Time;
            //Point.sent = funcData;
            sendTask = new Task(() => { });
            sendTask.Start();
            if (time < passingTimeLimit) passingValue = passingConstValue;
            else passingValue = time / 50;


            int delay = (int)((time / passingValue) *  92/ 100);
            for (int i = 0; i <= passingValue; i++)
            {
                Point point = new Point(
                oneSinFuncData(pastCoint.CanA, nextCoint.CanA, i),
                oneSinFuncData(pastCoint.CanB, nextCoint.CanB, i),
                oneSinFuncData(pastCoint.CanC, nextCoint.CanC, i),
                oneSinFuncData(pastCoint.CanD, nextCoint.CanD, i),
                oneSinFuncData(pastCoint.CanE, nextCoint.CanE, i),
                oneSinFuncData(pastCoint.CanF, nextCoint.CanF, i),
                0);

                funcData(point.ToString());
                funcTime(delay.ToString());
            }

        }

        static private void oneSinFunc(int pastCoint, int nextCoint, Sent func,char numCanal , int cycle)
        {
            double a = (nextCoint - pastCoint) / 2;
            int coint = Convert.ToInt32(a * (-Math.Cos(3.14 / Convert.ToDouble(passingValue) * Convert.ToDouble(cycle)) + 1) + pastCoint);
            sendTask.Wait();
            sendTask = Task.Run(() => func(numCanal + coint.ToString() + 'z'));
        }

        static private int oneSinFuncData(int pastCoint, int nextCoint, int cycle)
        {
            double a = (nextCoint - pastCoint) / 2;
            return Convert.ToInt32(a * (-Math.Cos(3.14 / Convert.ToDouble(passingValue) * Convert.ToDouble(cycle)) + 1) + pastCoint);
             
        }
    }
}