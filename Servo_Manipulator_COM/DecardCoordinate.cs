using System;
using Intersection;
using PointSpase;

namespace Servo_Manipulator_COM 
{
    public static class DecPointTransform
    {
        private static Dec axisB, axisC;

        const int L3 = 130; // CB, mm.
        private static int l_max = Convert.ToInt32(Shape.L1 + Shape.L2) + L3;
        public static int  Lmax { get { return l_max; } }

        private const int qA = 0;
        private const int qB = 180;
        private const int qC = -40;
        private const int qD = 0;//-7;
        private const int qE = 90;

        public static Point Algoritm(double coordX, double coordY, double coordZ, double degreeA, double degreeB, int grab, int time)
        {
            var dec = new Dec(coordX,coordY,coordZ);
            return Algoritm(dec, degreeA, degreeB, grab,time);
        }

        public static Point Algoritm(Dec dec, int grab, int time)
        {
            return Algoritm(dec, dec.decA, dec.decB, grab, time);
        }

        public static Point Algoritm(Dec dec, double degreeA, double degreeB, int grab,int time)
        {
            try
            {
                axisB = new Dec();
                axisC = new Dec();
                //axisC.decZ = dec.decZ + Math.Sin(180 / Math.PI * degreeB) * L3;
                //axisC.pXY = dec.pXY + Math.Cos(180 / Math.PI * degreeB) * L3;
                //axisC.decX = dec.decX * axisC.pXY / dec.pXY;
                //axisC.decY = dec.decY * axisC.pXY / dec.pXY;

                axisB = Shape.Algoritm(dec);

                var AC = Math.Sqrt(dec.pXY * dec.pXY + dec.decZ * dec.decZ);
                int CanA = Convert.ToInt32(Math.Acos(dec.decX / dec.pXY) * 180 / Math.PI);
                int CanB =180- Convert.ToInt32(Math.Acos(axisB.decZ / axisB.pXY) * 180 / Math.PI);// * -1;
                int CanC = Convert.ToInt32(Math.Acos((AC * AC - Shape.L1 * Shape.L1 - Shape.L2 * Shape.L2) / (2 * Shape.L1 * Shape.L2)) * 180 / Math.PI) + qC;
                int CanD = Convert.ToInt32(degreeB) + qD;
                int CanE = Convert.ToInt32(degreeA) + qE;

                return new Point(CanA, CanB, CanC, CanD, CanE, grab, time);
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }
        public static Point Algoritm2(Dec dec, double degreeA, double degreeB, int grab, int time)
        {
            try
            {
                axisB = new Dec();
                axisC = new Dec();
                axisC.decZ = dec.decZ + Math.Sin(180 / Math.PI * degreeB) * L3;
                axisC.pXY = dec.pXY + Math.Cos(180 / Math.PI * degreeB) * L3;
                axisC.decX = dec.decX * axisC.pXY / dec.pXY;
                axisC.decY = dec.decY * axisC.pXY / dec.pXY;

                axisB = Shape.Algoritm(axisC);

                var AC = Math.Sqrt(axisC.pXY * axisC.pXY + axisC.decZ * axisC.decZ);
                int CanA = Convert.ToInt32(Math.Acos(dec.decX / dec.pXY) * 180 / Math.PI);
                int CanB =  Convert.ToInt32(Math.Acos(axisB.decZ / axisB.pXY) * 180 / Math.PI);
                int CanC = Convert.ToInt32(Math.Acos((AC * AC - Shape.L1 * Shape.L1 - Shape.L2 * Shape.L2) / (2 * Shape.L1 * Shape.L2)) * 180 / Math.PI) ;
                int CanD = 360 - Convert.ToInt32(degreeB - Math.Atan((axisC.decZ - axisB.decZ) / (axisC.pXY - axisB.pXY))) ;
                int CanE = Convert.ToInt32(degreeA);

                return new Point(CanA, CanB, CanC, CanD, CanE, grab, time,true);
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }
    }
}
