using System;
using System.Collections.Generic;
using System.Text;

namespace KinematicTask
{
    public class Dec
    {

        private double decX, decY, decZ, pXY, anglA, anglB, anglC,canGrab;
        private long time;                //задержка от начала выполнения (сначала устанавливается поворот, затем задержка)

        private const  double maxXYZ=400.0;
        private const double  minXYZ= -400.0;
        private const double maxAngle = 180.0;
        private const double minAngle = -180.0;


        public static readonly Dec MinDec = new Dec(maxXYZ, maxXYZ, maxXYZ, maxAngle, maxAngle, maxAngle);
        public static readonly Dec MaxDec = new Dec(minXYZ, minXYZ, minXYZ, minAngle, minAngle, minAngle);

        public double DecX { get { return decX; } set { decX = value; } }
        public double DecY { get { return decY; } set { decY = value; } }
        public double DecZ { get { return decZ; } set { decZ = value; } }
        public double XY { get { return pXY; } set { pXY = value; } }


        public double AnglA { get { return anglA; } set { anglA = value; } }
        public double AnglB { get { return anglB; } set { anglB = value; } }
        public double AnglC { get { return anglC; } set { anglC = value; } }

        public long Time { get { return time; } set { time = value; } }

        public enum decEnum { X='a',Y='b',Z='c',A='d',B='e',C='f', Grab = 'g', Time ='t'}
        public double this[char ch]
        {
            set
            {
                switch (ch)
                {
                    case 'a':
                        decX = value;
                        return;
                    case 'b':
                        decY = value;
                        return;
                    case 'c':
                        decZ = value;
                        return;
                    case 'd':
                        anglA = value;
                        return;
                    case 'e':
                        anglB = value;
                        return;
                    case 'f':
                        anglC = value;
                        return;
                    case 'g':
                        canGrab = value;
                        return;
                    case 't':
                        time = (long)value;
                        return;
                    default:
                        return;
                }
            }
            get
            {
                switch (ch)
                {
                    case 'a':
                        return decX;
                    case 'b':
                        return decY;
                    case 'c':
                        return decZ;
                    case 'd':
                        return anglA;
                    case 'e':
                        return anglB;
                    case 'f':
                        return anglC;
                    case 'g':
                        return canGrab;
                    case 't':
                        return time;
                    default:
                        return 0;
                }
            }
        }
        public double update_pXY()
        {
            this.pXY = Math.Sqrt(decX * decX + decY * decY);
            if (this.decY < 0 && this.pXY > 0) this.pXY *= (-1);
            return pXY;
        }

        public Dec(double decX = 0.0, double decY = 0.0, double decZ = 0.0, double decA = 0.0, double decB = 0.0, double decC = 0.0)
        {
            this.decX = decX;
            this.decY = decY;
            this.decZ = decZ;
            this.anglA = decA;
            this.anglB = decB;
            this.anglC = decC;

            this.pXY = Math.Sqrt(decX * decX + decY * decY);
        }
    }
}
