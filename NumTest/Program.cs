using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointSpase;

namespace NumTest
{
    class Program
    {
        static void Main(string[] args)
        {

            string st = "a53";
            num n = new num(st);

            Console.WriteLine("index: {0}\nnumber: {1}", n.index, n.toint());
            Console.Read();
        }

    }
}