using System;
using KinematicModeling;
using PointSpase;

namespace KinematicTaskTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dec dec = new Dec(1, 5, 5, -90, 0, 0);
            // Dec dec = new Dec(1, 5, 5, -90, 0, 0);

            //act
            //   Point pointOut = TaskDecision.DecToPoint(dec, 0);
            //assert
            Dec dec = new Dec(0, 330, 212, 0, 0, 0);
            Point comparison = new Point();
            comparison['a'] = 0;
            comparison['b'] = 90;
            comparison['c'] = 0;
            comparison['d'] = 0;
            comparison['e'] = 180;
            comparison['f'] = 0;
            Point pointOut = TaskDecision.DecToPoint(dec);
            Dec decOut = TaskDecision.PointToDec(pointOut);
            //Assert.AreEqual(pointOut, comparison);
            Console.WriteLine("Hello World!");
        }
    }
}
