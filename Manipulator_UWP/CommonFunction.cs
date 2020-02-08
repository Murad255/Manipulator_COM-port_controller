using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointSpase;
namespace Manipulator_UWP
{
    static class CommonFunction
    {
        static Point commonPoint = new Point();
        static public Point CommonPoint
            {
                get{ return commonPoint; }
                set{ commonPoint = value; }         
            }
        static void Home()
        {

        }
    }

   
}
