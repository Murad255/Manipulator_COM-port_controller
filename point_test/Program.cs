using System;
using Points;
using System.Collections.Generic;

namespace point_test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Point> points= new List<Point>();
            Point.sent = Console.WriteLine;

           
            points.Add(new Point(20, 51, 56, 47, 68, 153, 56));
            points.Add(new Point(13,50,90,56,81,1,1));
           
            points[0].write();
            Console.Write('\n');
            points[1].write();

            Console.Read();
        }
    }
}
