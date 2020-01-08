using System;
using System.Collections.Generic;
using System.Text;

namespace Intersection
{
    public class Dec
    {
        public double decX, decY, decZ, pXY, decA, decB;
        public double update_pXY()
        {
            this.pXY = Math.Sqrt(decX * decX + decY * decY);
            if (this.decY < 0 && this.pXY > 0) this.pXY *= (-1);
            return pXY;
        }

        public Dec(double decX = 0.0, double decY = 0.0, double decZ = 0.0, double decA = 0.0, double decB = 0.0)
        {
            this.decX = decX;
            this.decY = decY;
            this.decZ = decZ;
            this.decA = decA;
            this.decB = decB;
            this.pXY = Math.Sqrt(decX * decX + decY * decY);
        }
    }
}
