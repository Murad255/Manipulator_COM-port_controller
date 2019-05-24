using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PointSpase;

namespace sinFunc
{
    class SinProgram
    {
        private const int value = 40;

        public delegate void SendFunc(int num);
        static void Main(string[] args)
        {
           
            
            Point past = new Point(32,56,95,82,64,84,500);
            Point next = new Point(100, 30, 125, 175, 64, 10, 500);
            sinFunc(past, next, write, Convert.ToInt32( past.getTime()));
            Console.ReadLine();

        }
        public static void write(string mes)
        {
            Console.Write(mes+'\t');
   
        }
        static public void sinFunc(int pastCoint, int nextCoint, Sent func, int time=0)
        {
            
            double a = (nextCoint - pastCoint) / 2;
            for (int i = 0; i < value; i++)
            {
                
                int coint = Convert.ToInt32(a * (-Math.Cos(3.14 / Convert.ToDouble(value) * Convert.ToDouble(i)) + 1) + pastCoint);
                func(coint.ToString());
                if(time!=0) Thread.Sleep(time/value-1); //-1 мс для компенсации
            }
        }
        static public void sinFunc(Point pastCoint, Point nextCoint, Sent func, int time)
        {
            Point.sent = func;
         
           //double a = (nextCoint - pastCoint) / 2;
            for (int i = 0; i <= value; i++)
            {

                oneSinFunc(pastCoint.CanA, nextCoint.CanA, func,i);//int coint = Convert.ToInt32(a * (-Math.Cos(3.14 / Convert.ToDouble(value) * Convert.ToDouble(i)) + 1) + pastCoint);
                oneSinFunc(pastCoint.CanB, nextCoint.CanB, func, i);
                oneSinFunc(pastCoint.CanC, nextCoint.CanC, func, i);
                oneSinFunc(pastCoint.CanD, nextCoint.CanD, func, i);
                oneSinFunc(pastCoint.CanE, nextCoint.CanE, func, i);
                oneSinFunc(pastCoint.CanF, nextCoint.CanF, func, i);
                Console.WriteLine();
                Thread.Sleep(time / value - 1); //-1 мс для компенсации
            }
        }
        static private void oneSinFunc(int pastCoint, int nextCoint, Sent func,int cycle)
        {
            double a = (nextCoint - pastCoint) / 2;
            int coint = Convert.ToInt32(a * (-Math.Cos(3.14 / Convert.ToDouble(value) * Convert.ToDouble(cycle)) + 1) + pastCoint);
            func(coint.ToString());
        }

    }
}
