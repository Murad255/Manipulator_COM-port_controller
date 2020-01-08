using System;

namespace Intersection
{

    public static class Shape
    {
        public const double L2 = 147;
        public const double L1 = 105;
        public const double L3 = 130;

        static public Dec Algoritm(Dec decIn)
        {
            if (Math.Sqrt(decIn.decZ * decIn.decZ + decIn.pXY * decIn.pXY) < Convert.ToDouble(L2 - L1)) throw new ExitFromZoneException("Координата выходит за пределы зоны обслуживания");
            {
                double val = Math.Sqrt(decIn.decX * decIn.decX + decIn.decY * decIn.decY + decIn.decZ * decIn.decZ);
                if (((L1 + L2) < val) || ((L1 - L2) > val))
                {
                    throw new ExitFromZoneException("Координата выходит за пределы зоны обслуживания");
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
            if ((ax < bx && decIn.decZ >= 0) || (ay > by && decIn.decZ < 0))
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

            if (decOut.decY > 0 && decOut.pXY < 0) decOut.decY *= (-1);
            return decOut;
        }
    }

    class ExitFromZoneException : Exception
    {
        public ExitFromZoneException(string message)
        : base(message)
        { }
    }

}
