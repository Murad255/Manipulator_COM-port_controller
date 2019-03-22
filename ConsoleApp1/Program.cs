using System;
 
namespace ConsoleApp1
{
    
    class Dec
    {
        public int decX, decY, decZ, pXY;
        public Dec(int decX,int decY,int decZ)
        {
            this.decX = decX;
            this.decY = decY;
            this.decZ = decZ;
            this.pXY = (int)Math.Sqrt((double)(decX*decX+decY*decY));
        }
    } 

    static class shape
    {
        const int L1 = 147;
        const int L2 = 105;
        
      static  Dec inDec, outDec;

        static public  Dec write(Dec decIn)
        {
            int X = decIn.pXY;
            int Y = decIn.decZ;
            int a = 1 - ( X/ Y);
            int b = 2 * X / Y * (X * X + Y * Y + 10584) * (X * X + Y * Y + 10584);
            int c = (X * X + Y * Y + 10584) * (X * X + Y * Y + 10584) / (2 * Y);
            
            {
                int X1 = (-1 * b + (int)Math.Sqrt((double)(b * b - 4 * a * c))) / (2 * a);
                int X2 = (-1 * b - (int)Math.Sqrt((double)(b * b - 4 * a * c))) / (2 * a);
                if (X1 > X2) outDec.pXY = X1;
                else outDec.pXY = X2;
            }
            outDec.decZ = -1 * outDec.pXY * X / Y + c;
            outDec.decX = inDec.pXY / inDec.decX * outDec.pXY;
            outDec.decY = inDec.pXY / inDec.decY * outDec.pXY;
          
            return outDec;
        } 
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Введите значения координат.\nX= ");
            //int x=Console.Read();
            // Console.WriteLine("\nY=");
            // int y = Console.Read();
            // Console.WriteLine("\nZ=");
            // int z = Console.Read();
            int x = 85, y = 69, z = 75;
            Dec dec = new Dec(x,y,z);
            Dec outDec= shape.write(dec);
            Console.WriteLine("Координаты верхней точки  пересечения шаров:");
            Console.WriteLine("\nX=");
            Console.Write(outDec.decX);
            Console.WriteLine("\nY=");
            Console.Write(outDec.decY);
            Console.WriteLine("\nZ=");
            Console.Write(outDec.decZ);

            Console.Read();
        }
    }
}
