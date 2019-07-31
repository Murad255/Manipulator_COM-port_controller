using System;
using Intersection;
using PointSpase;

namespace Servo_Manipulator_COM 
{
   public static class  DecPointTransform 
    {
        private static Dec axisB, axisC;
        const int L3 = 130; // CB, mm.
        public static Point Algoritm(double coordX, double coordY, double coordZ, double degreeA, double degreeB, int grab)
        {
            var dec = new Dec(coordX,coordY,coordZ);
            return Algoritm(dec, degreeA, degreeB, grab);
        }

        public static Point Algoritm(Dec dec, double degreeA, double degreeB, int grab)
        {
            axisC.decZ = dec.decZ + Math.Sin(180 / Math.PI * degreeB) * L3;
            axisC.pXY = dec.pXY + Math.Cos(180 / Math.PI * degreeB) * L3;
            axisC.decX = dec.decX * axisC.pXY / dec.pXY;
            axisC.decY = dec.decY * axisC.pXY / dec.pXY;

            axisB = Shape.Algoritm(axisC);

            var AC = Math.Sqrt(axisC.pXY* axisC.pXY+ axisC.decZ* axisC.decZ);

            return new Point(
                            /* CanA */ Convert.ToInt32( Math.Acos(dec.decX/dec.pXY) * 180 / Math.PI),
                            /* CanB */ Convert.ToInt32(Math.Acos(axisB.decZ / axisB.pXY) * 180 / Math.PI),
                            /* CanC */ Convert.ToInt32(Math.Acos( (AC*AC-Shape.L1* Shape.L1- Shape.L2* Shape.L2)/(2* Shape.L1* Shape.L2)) * 180 / Math.PI),
                            /* CanE */ Convert.ToInt32(degreeA),
                            /* CanD */ Convert.ToInt32(degreeB),
                            /* CanF */ grab
                            );
        }
    }
}
