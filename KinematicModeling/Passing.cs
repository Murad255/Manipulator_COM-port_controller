using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PointSpase;


namespace KinematicModeling
{

    public delegate void Sent(Point point); //делегат для отправки сообщения в COM-порт

    public interface PassingStrategy
    {


        float PassingAlgoritmData(float pastCoint,
                                    float nextCoint,
                                    int cycle,
                                    long maxCycle);
    }
    /// <summary>
    /// интерполяция по синусоидальному закону
    /// </summary>
    class SinPassingStrategy : PassingStrategy
    {

        public float PassingAlgoritmData(float pastCoint, float nextCoint, int cycle, long maxCycle)
        {
            double a = (nextCoint - pastCoint) / 2;
            return (float)(a * (-Math.Cos(3.14 / Convert.ToDouble(maxCycle) * Convert.ToDouble(cycle)) + 1) + pastCoint);
        }


    }

    /// <summary>
    /// интерполяция по линейному закону
    /// </summary>
    class LinearPassingStrategy : PassingStrategy
    {
        public float PassingAlgoritmData(float pastCoint, float nextCoint, int cycle, long maxCycle)
        {
            double a = (nextCoint - pastCoint) / maxCycle;
            return Convert.ToInt32((a * cycle) + pastCoint);
        }

    }


    /// <summary>
    /// Класс реализации интерполяции
    /// </summary>
    class Passing
    {
        public const long passingConstValue = 15;
        public const int passingTimeLimit = 750;
        static Task sendTask;
        public static Point pastPoint = new Point();
        static private long passingValue = 0;

        static public Sent sentPointFunction;
        static private PassingStrategy contextStrategy;
        static public PassingStrategy ContextStrategy
        {
            set { contextStrategy = value; }
        }

        static public Sent SentPointFunction
        { set { sentPointFunction = value; } }

        public void Context(PassingStrategy _strategy)
        {
            ContextStrategy = _strategy;
        }


        static public float PassingAlgoritmData(float pastCoint, float nextCoint, int cycle) =>
            Passing.contextStrategy.PassingAlgoritmData(pastCoint, nextCoint, cycle, passingValue);



        static public void sinFunc(Point pastCoint, Point nextCoint, Sent func)
        {
            long time = nextCoint.Time;
            // Point.sent = func;

            sendTask = new Task(() => { });
            sendTask.Start();
            if (time < passingTimeLimit) passingValue = passingConstValue;
            else passingValue = time / 50;

            int delay = (int)(time / passingValue * 9 / 10);
            for (int i = 0; i <= passingValue; i++)
            {
                Point temp = new Point(
                    PassingAlgoritmData(pastCoint.CanA, nextCoint.CanA, i),
                    PassingAlgoritmData(pastCoint.CanB, nextCoint.CanB, i),
                    PassingAlgoritmData(pastCoint.CanC, nextCoint.CanC, i),
                    PassingAlgoritmData(pastCoint.CanD, nextCoint.CanD, i),
                    PassingAlgoritmData(pastCoint.CanE, nextCoint.CanE, i),
                    PassingAlgoritmData(pastCoint.CanF, nextCoint.CanF, i)
                );
                func(temp);
                Thread.Sleep(delay);
            }

        }
        //для записи точек
        static public void sinFunc(Point pastCoint, Point nextCoint, Sent funcData, Sent funcTime)
        {

            sendTask = new Task(() => { });
            sendTask.Start();
            if (nextCoint.Time < passingTimeLimit) passingValue = passingConstValue;
            else passingValue = nextCoint.Time / 50;

            int delay = (int)((nextCoint.Time / passingValue) * 92 / 100);
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

                // funcData(point.ToString());
                //funcTime(delay.ToString());
            }

        }

        static private void oneSinFunc(int pastCoint, int nextCoint, Sent func, char numCanal, int cycle)
        {
            double a = (nextCoint - pastCoint) / 2;
            int coint = Convert.ToInt32(a * (-Math.Cos(3.14 / Convert.ToDouble(passingValue) * Convert.ToDouble(cycle)) + 1) + pastCoint);
            sendTask.Wait();
            //sendTask = Task.Run(() => func(numCanal + coint.ToString() + 'z'));
        }

        static private float oneSinFuncData(float pastCoint, float nextCoint, int cycle)
        {
            float a = (nextCoint - pastCoint) / 2;
            return (float)(a * (-Math.Cos(3.14 / Convert.ToDouble(passingValue) * Convert.ToDouble(cycle)) + 1) + pastCoint);

        }
    }
    
}
