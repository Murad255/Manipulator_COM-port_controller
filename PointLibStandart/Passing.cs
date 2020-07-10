using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PointSpase;
//using KinematicTask;

namespace KinematicModeling
{

    public delegate void Sent(Point point); //делегат для отправки сообщения в COM-порт

    /// <summary>
    /// Класс реализации интерполяции
    /// </summary>
    public class Passing
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



        static public float PassingAlgoritmData(float pastCoint, float nextCoint, int cycle) =>
            Passing.contextStrategy.PassingAlgoritmData(pastCoint, nextCoint, cycle, passingValue);

        static public void PassingAlgoritm(Point pastCoint, Point nextCoint, Sent func)
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
                //создаём промежуточную точку и отправляем её в порт (через func)
                Point temp = new Point(
                    PassingAlgoritmData(pastCoint.CanA, nextCoint.CanA, i),
                    PassingAlgoritmData(pastCoint.CanB, nextCoint.CanB, i),
                    PassingAlgoritmData(pastCoint.CanC, nextCoint.CanC, i),
                    PassingAlgoritmData(pastCoint.CanD, nextCoint.CanD, i),
                    PassingAlgoritmData(pastCoint.CanE, nextCoint.CanE, i),
                    PassingAlgoritmData(pastCoint.CanF, nextCoint.CanF, i),
                    PassingAlgoritmData(pastCoint.CanGrab, nextCoint.CanGrab, i),
                    0
                );

                func(temp);
                Thread.Sleep(delay);
            }

        }

        /// <summary>
        /// Возвращает массив точек с промежуточными точками
        /// </summary>
        /// <param name="pastCoint">начальная точка</param>
        /// <param name="nextCoint">конечная точка</param>
        /// <returns></returns>
        static public Point[] PassingAlgoritm(Point pastCoint, Point nextCoint)
        {

            List<Point> points = new List<Point>();
            if (nextCoint.Time < passingTimeLimit) passingValue = passingConstValue;
            else passingValue = nextCoint.Time / 50;

            int delay = (int)((nextCoint.Time / passingValue) * 92 / 100);
            for (int i = 0; i <= passingValue; i++)
            {
                points.Add(new Point(
                PassingAlgoritmData(pastCoint.CanA, nextCoint.CanA, i),
                PassingAlgoritmData(pastCoint.CanB, nextCoint.CanB, i),
                PassingAlgoritmData(pastCoint.CanC, nextCoint.CanC, i),
                PassingAlgoritmData(pastCoint.CanD, nextCoint.CanD, i),
                PassingAlgoritmData(pastCoint.CanE, nextCoint.CanE, i),
                PassingAlgoritmData(pastCoint.CanF, nextCoint.CanF, i),
                PassingAlgoritmData(pastCoint.CanGrab, nextCoint.CanGrab, i),
                delay
                ));
            }
            return points.ToArray();
        }

        //static public void PassingAlgoritm(Dec pastCoint, Dec nextCoint, Sent func)
        //{
        //    long time = nextCoint.Time;
        //    // Point.sent = func;

        //    sendTask = new Task(() => { });
        //    sendTask.Start();
        //    if (time < passingTimeLimit) passingValue = passingConstValue;
        //    else passingValue = time / 50;

        //    int delay = (int)(time / passingValue * 9 / 10);
        //    for (int i = 0; i <= passingValue; i++)
        //    {
        //        //создаём промежуточную точку и отправляем её в порт (через func)
        //        Dec temp = new Dec(
        //            PassingAlgoritmData((float)pastCoint.DecX, (float)nextCoint.DecX, i),
        //            PassingAlgoritmData((float)pastCoint.DecX, (float)nextCoint.DecX, i),
        //            PassingAlgoritmData((float)pastCoint.DecX, (float)nextCoint.DecX, i),
        //            PassingAlgoritmData((float)pastCoint.AnglA, (float)nextCoint.AnglA, i),
        //            PassingAlgoritmData((float)pastCoint.AnglA, (float)nextCoint.AnglA, i),
        //            PassingAlgoritmData((float)pastCoint.AnglA, (float)nextCoint.AnglA, i),
        //            PassingAlgoritmData((float)pastCoint.CanGrab, (float)nextCoint.CanGrab, i),
        //            0
        //        );

        //        func(KinematicTask.TaskDecision.DecToPoint(temp));
        //        Thread.Sleep(delay);
        //    }

        //}

        ///// <summary>
        ///// Возвращает массив точек с промежуточными точками
        ///// </summary>
        ///// <param name="pastCoint">начальная точка</param>
        ///// <param name="nextCoint">конечная точка</param>
        ///// <returns></returns>
        //static public Dec[] PassingAlgoritm(Dec pastCoint, Dec nextCoint)
        //{
        //    List<Dec> decs = new List<Dec>();
        //    if (nextCoint.Time < passingTimeLimit) passingValue = passingConstValue;
        //    else passingValue = nextCoint.Time / 50;

        //    int delay = (int)((nextCoint.Time / passingValue) * 92 / 100);
        //    for (int i = 0; i <= passingValue; i++)
        //    {
        //        decs.Add(new Dec(
        //            PassingAlgoritmData((float)pastCoint.DecX, (float)nextCoint.DecX, i),
        //            PassingAlgoritmData((float)pastCoint.DecX, (float)nextCoint.DecX, i),
        //            PassingAlgoritmData((float)pastCoint.DecX, (float)nextCoint.DecX, i),
        //            PassingAlgoritmData((float)pastCoint.AnglA, (float)nextCoint.AnglA, i),
        //            PassingAlgoritmData((float)pastCoint.AnglA, (float)nextCoint.AnglA, i),
        //            PassingAlgoritmData((float)pastCoint.AnglA, (float)nextCoint.AnglA, i),
        //            PassingAlgoritmData((float)pastCoint.CanGrab, (float)nextCoint.CanGrab, i),
        //            delay
        //        ));

        //    }
        //    return decs.ToArray();
        //}
    }



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
    public class SinPassingStrategy : PassingStrategy
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
    public class LinearPassingStrategy : PassingStrategy
    {
        public float PassingAlgoritmData(float pastCoint, float nextCoint, int cycle, long maxCycle)
        {
            double a = (nextCoint - pastCoint) / maxCycle;
            return Convert.ToInt32((a * cycle) + pastCoint);
        }
    }
}

