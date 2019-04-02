using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointSpase
{
        public delegate void Sent(string message);

        public class Point
        {
            protected static int numPoints = 0; //общее количество созданных точек 
            protected int numPoint;             //номер данного экземпляра
            public static Sent sent;
            private int canA, canB, canC, canD, canE, canF; //обобщенные координаты (углы поворота сервориводов)
            protected long time;                //задержка от начала выполнения (сначала устанавливается поворот, затем задержка)

            public Point(int canA = 0, int canB = 0, int canC = 0, int canD = 0, int canE = 0, int canF = 0, long time = 0)
            {
                setAllCan(canA, canB, canC, canD, canE, canF, time);
                numPoint= ++numPoints;
            }
            public void setAllCan(int canA, int canB, int canC, int canD, int canE, int canF, long time)  //функция принимает значения для каждого канала обобщенных координат
        {
                this.canA = canA;
                this.canB = canB;
                this.canC = canC;
                this.canD = canD;
                this.canE = canE;
                this.canF = canF;
                this.time = time;
            }
            public void write(  int canA, int canB, int canC, 
                                int canD, int canE, int canF, long time)    //функция для ввода и отправки значений обобщенных координат
            {
                setAllCan(canA, canB, canC, canD, canE, canF, time);
            //отправляет координаты и заключает их в символs: $- начало точки #- конец точки, 
                sent("$");
                    sent('a' + this.canA.ToString() + 'z');
                    sent('b' + this.canB.ToString() + 'z');
                    sent('c' + this.canC.ToString() + 'z');
                    sent('d' + this.canD.ToString() + 'z');
                    sent('e' + this.canE.ToString() + 'z');
                    sent('f' + this.canF.ToString() + 'z');
                    sent('t' + this.time.ToString() + 'z');
                sent("#");
        }
            public void write() => write(   this.canA, 
                                            this.canB, 
                                            this.canC, 
                                            this.canD, 
                                            this.canE, 
                                            this.canF,
                                            this.time   );

            public override string ToString()
            {
                return "Point " + this.numPoint.ToString()+'\t'+this.time.ToString()+" ms."+ '\r'+'\n';
            }

        public int getNumPoint() { return this.numPoint; }
        static public int getNumPoints() { return numPoints; }
        public long getTime() { return this.time; }
    }
}
