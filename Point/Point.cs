using System;
using System.IO.Ports;

namespace Points
{
    public delegate void Sent(string c);

    public class Point
    {
        static int numPoints = 0; //общее количество созданных точек 
        public static Sent sent;
        private int canA, canB, canC, canD, canE, canF;
        private long time; //задержка от начала выполнения (сначала устанавливается поворот, затем задержка)

        public Point(int canA = 0, int canB = 0, int canC = 0, int canD = 0, int canE = 0, int canF = 0, long time = 0)
        {
            setAllCan(canA, canB, canC, canD, canE, canF, time);
            numPoints++;
        }
        public void setAllCan(int canA, int canB, int canC, int canD, int canE, int canF, long time)  //функция принимает значения для каждого канала
        {
            this.canA = canA;
            this.canB = canB;
            this.canC = canC;
            this.canD = canD;
            this.canE = canE;
            this.canF = canF;
            this.time = time;
        }
        public void write(int canA, int canB, int canC, int canD, int canE, int canF, long time)
        {
            setAllCan(canA, canB, canC, canD, canE, canF, time);
            sent(this.canA.ToString());
            sent(this.canB.ToString());
            sent(this.canC.ToString());
            sent(this.canD.ToString());
            sent(this.canE.ToString());
            sent(this.canF.ToString());
        }
        public void write()
        {
            sent(this.canA.ToString());
            sent(this.canB.ToString());
            sent(this.canC.ToString());
            sent(this.canD.ToString());
            sent(this.canE.ToString());
            sent(this.canF.ToString());
        }
    }
}


