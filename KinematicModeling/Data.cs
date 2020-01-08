using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinematicModeling
{
    public class Vector
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public Vector(double x, double y, double z) 
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
    public class Quaternion
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public double W { get; }

    }
}
