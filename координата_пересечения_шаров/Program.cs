using System;

namespace Intersection
{

    class Dec
    {
        public double decX, decY, decZ, pXY;
        public Dec(double decX = 0.0, double decY = 0.0, double decZ = 0.0)
        {
            this.decX = decX;
            this.decY = decY;
            this.decZ = decZ;
            this.pXY = Math.Sqrt(decX * decX + decY * decY);
        }
    }

    static class shape
    {
        const double L2 = 147;
        const double L1 = 105;

        static public Dec write(Dec decIn)
        {
            {
                double val = Math.Sqrt(decIn.decX * decIn.decX + decIn.decY * decIn.decY + decIn.decZ * decIn.decZ);
                if (((L1 + L2) < val) || ((L1 - L2) > val))
                {
                    throw new Exception("Координата выходит за пределы зоны обслуживания");
                }
            }
            double X = decIn.pXY;
            double Y = decIn.decZ;
            double a = -2 * X;
            double b = -2 * Y;
            double c = X * X + Y * Y + L1 * L1 - L2 * L2;



            double x0 = -a * c / (a * a + b * b), y0 = -b * c / (a * a + b * b);

            double d = L1 * L1 - c * c / (a * a + b * b);
            double mult = Math.Sqrt(d / (a * a + b * b));
            double ax, ay, bx, by;
            ax = x0 + b * mult;
            bx = x0 - b * mult;
            ay = y0 - a * mult;
            by = y0 + a * mult;
            Dec decOut = new Dec();
            if (ay > by)
            {
                decOut.pXY = ax;
                decOut.decZ = ay;
            }
            else
            {
                decOut.pXY = bx;
                decOut.decZ = by;
            }
            if (decIn.decX == 0) decOut.decX = 0;
            else decOut.decX = decIn.pXY / decIn.decX * decOut.pXY;
            if (decIn.decY == 0) decOut.decY = 0;
            else decOut.decY = decIn.pXY / decIn.decY * decOut.pXY;

            return decOut;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Введите значения координат.\nX= ");
            //int x=Console.Read();
            // Console.WriteLine("\nY=");
            // int y = Console.Read();
            // Console.WriteLine("\nZ=");
            // int z = Console.Read();
            int x = 100, y = 50, z = 50;
            Dec dec = new Dec(x, y, z);
            Dec outDec = shape.write(dec);
            Console.WriteLine("Координаты верхней точки  пересечения шаров:");
            Console.WriteLine("\nX=");
            Console.Write(outDec.decX);
            Console.WriteLine("\nY=");
            Console.Write(outDec.decY);
            Console.WriteLine("\nZ=");
            Console.Write(outDec.decZ);

            Console.Read();
        }
    }
}
