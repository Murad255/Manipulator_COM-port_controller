using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace sinFunc
{
    class SinProgram
    {
        public delegate void SendFunc(int num);
        static void Main(string[] args)
        {
            int  pastCoint=20, nextCoint=138;
             sinFunc(pastCoint, nextCoint,Console.WriteLine,1000);
            Console.ReadLine();

        }

        static public void sinFunc(int pastCoint, int nextCoint,SendFunc func, int time)
        {
            const int value = 40;
            double a = (nextCoint - pastCoint) / 2;
            for (int i = 0; i <= value; i++)
            {
                
                int coint = Convert.ToInt32(a * (-Math.Cos(3.14 / Convert.ToDouble(value) * Convert.ToDouble(i)) + 1) + pastCoint);
                func(coint);
                Thread.Sleep(time/value-1); //-1 мс для компенсации
            }
        }

    }
}
