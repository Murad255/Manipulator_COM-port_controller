using System;
using System.Collections.Generic;
using PointSpase;

namespace KinematicTask
{
    public class Dec : PointParam
    {

        private double decX, decY, decZ, pXY, anglA, anglB, anglC;

        private const double    maxXYZ=400.0;
        private const double    minXYZ= -400.0;
        private const double    maxAngle = 180.0;
        private const double    minAngle = -180.0;
        
        public static readonly Dec MinDec = new Dec(maxXYZ, maxXYZ, maxXYZ, maxAngle, maxAngle, maxAngle);
        public static readonly Dec MaxDec = new Dec(minXYZ, minXYZ, minXYZ, minAngle, minAngle, minAngle);

        public double DecX { get { return decX; } set { decX = value; } }
        public double DecY { get { return decY; } set { decY = value; } }
        public double DecZ { get { return decZ; } set { decZ = value; } }
        public double XY { get { return pXY; } set { pXY = value; } }


        public double AnglA { get { return anglA; } set { anglA = value; } }
        public double AnglB { get { return anglB; } set { anglB = value; } }
        public double AnglC { get { return anglC; } set { anglC = value; } }

        public Dec(double decX  , double decY , double decZ , double decA , double decB , double decC, float canGrab = 0, long time = 0, int numPoint = 0)
        {
            this.decX  = decX;
            this.decY  = decY;
            this.decZ  = decZ;
            this.anglA = decA;
            this.anglB = decB;
            this.anglC = decC;

            if (canGrab > Point.MaxPoint[(char)Point.pointEnum.Grab]) throw new MaxValueException($"Значение подвижности на канале g больше предельного!\t {canGrab} > {Point.MaxPoint[(char)Point.pointEnum.Grab]}");
            if (canGrab < Point.MinPoint[(char)Point.pointEnum.Grab]) throw new MinValueException($"Значение подвижности на канале g меньше предельного!\t {canGrab} < {Point.MinPoint[(char)Point.pointEnum.Grab]}");

            this.canGrab    = canGrab;
            this.time       = time;
            this.numPoint   = numPoint;

            this.pXY = Math.Sqrt(decX * decX + decY * decY);
        }

        public Dec()
        {
            this.decX = 0;
            this.decY = 0;
            this.decZ = 0;
            this.anglA = 0;
            this.anglB = 0;
            this.anglC = 0;

            this.pXY = 0;
        }
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
                        canGrab = (float)value;
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


    }
}
