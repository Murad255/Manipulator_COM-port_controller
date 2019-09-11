using System;
using Intersection;
using PointSpase;

namespace Servo_Manipulator_COM 
{
    public static class DecPointTransform
    {
        private static Dec axisB, axisC;

        const int L3 = 75; // CB, mm.
        private static int l_max = Convert.ToInt32(Shape.L1 + Shape.L2) + L3;
        public static int  Lmax { get { return l_max; } }


        public static Point DecToPoint(Dec dec, int grab, int time)
        {
            return DecToPoint(dec, dec.decA, dec.decB, grab, time);
        }

        public static Point Algoritm2(Dec dec, double degreeA, double degreeB, int grab,int time)
        {
            try
            {
                axisB = new Dec();
                axisC = new Dec();

                axisB = Shape.Algoritm(dec);

                var AC = Math.Sqrt(dec.pXY * dec.pXY + dec.decZ * dec.decZ);
                int CanA = Convert.ToInt32(Math.Acos(dec.decX / dec.pXY) * 180 / Math.PI)-90;
                int CanB = Convert.ToInt32(Math.Acos(axisB.pXY / Shape.L1) * 180 / Math.PI);
                int CanC = 180 - Convert.ToInt32(Math.Acos((AC * AC - Shape.L1 * Shape.L1 - Shape.L2 * Shape.L2) / (2 * Shape.L1 * Shape.L2)) * 180 / Math.PI);
                int CanD = Convert.ToInt32(degreeB);
                int CanE = Convert.ToInt32(degreeA);

                return new Point(CanA, CanB, CanC, CanD, CanE, grab, time,true);
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }
        public static Point DecToPoint(Dec dec, double degreeA, double degreeB, int grab, int time)
        {
            try
            {
                axisB = new Dec();
                axisC = new Dec();
                //axisC.decZ = dec.decZ + Math.Sin(180 / Math.PI * degreeB) * L3;
                //axisC.pXY = dec.pXY + Math.Cos(180 / Math.PI * degreeB) * L3;
                axisC.decZ = dec.decZ + Math.Sin(-(degreeB + 90) * Math.PI / 180) * L3; //coord Y
                axisC.pXY = dec.pXY + Math.Cos(-(degreeB + 90) * Math.PI / 180) * L3;  //coord X
                axisC.decX = dec.decX * axisC.pXY / dec.pXY;
                axisC.decY = dec.decY * axisC.pXY / dec.pXY;

                if (axisC.decY < 0 || axisC.pXY < 0) throw new Exception("К данной координате нельзя переместиться.");

                axisB = Shape.Algoritm(axisC);

                var AC = Math.Sqrt(axisC.pXY * axisC.pXY + axisC.decZ * axisC.decZ);
                int CanA = Convert.ToInt32(Math.Acos(dec.decX / dec.pXY) * 180 / Math.PI)-90; 
                int CanB = Convert.ToInt32(Math.Acos(axisB.pXY / Shape.L1) * 180 / Math.PI);
                int CanC = 180 - Convert.ToInt32(Math.Acos((AC * AC - Shape.L1 * Shape.L1 - Shape.L2 * Shape.L2) / (2 * Shape.L1 * Shape.L2)) * 180 / Math.PI);
                int CanD = -90 + CanB + CanC + Convert.ToInt32(degreeB);
                int CanE = Convert.ToInt32(degreeA);

                return new Point(CanA, CanB, CanC, CanD, CanE, grab, time,true);
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public static Dec PointToDec(Point point)
        {
            Dec dec = new Dec();
            var pXY1 = Math.Cos(point.CanB * Math.PI / 180) * Shape.L1;
            var pXY2 = Math.Cos((point.CanB + point.CanC - 180) * Math.PI / 180) * Shape.L2;
            var pXY3 = Math.Sin(((point.CanD + 90 - point.CanB - point.CanC) % 360) * Math.PI / 180) * L3;

            var decZ1 = Math.Sin(point.CanB * Math.PI / 180) * Shape.L1;
            var decZ2 = Math.Sin((point.CanB + point.CanC - 180) * Math.PI / 180) * Shape.L2;
            var decZ3 = Math.Cos(((point.CanD + 90 - point.CanB - point.CanC) % 360) * Math.PI / 180) * L3;

            dec.pXY = pXY1 + pXY2 + pXY3;
            dec.decX = Math.Sin((point.CanA) * Math.PI / 180) * dec.pXY;
            dec.decY = Math.Cos((point.CanA) * Math.PI / 180) * dec.pXY;
            dec.decZ = decZ1 + decZ2 + decZ3;
            dec.decB = (point.CanD + 90 - point.CanB - point.CanC) % 360;

            return dec;
        }
    }
}
