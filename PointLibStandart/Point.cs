using System;

namespace PointSpase
{
    public class Point
    {
        protected static int numPoints = 0; //общее количество созданных точек 
        protected int numPoint;             //номер данного экземпляра
        public static Point tempPoint = new Point();

        public int NumPoint
        {
            get { return numPoint; }
        }

        public static readonly Point MinPoint = new Point(-90, -45, -45, -90,100,-90,0);
        public static readonly Point MaxPoint = new Point( 90, 225, 225,  90, 280, 90,180);
        private float canA, canB, canC, canD, canE, canF, canGrab; //обобщенные координаты (углы поворота сервориводов)
        protected long time;                //задержка от начала выполнения (сначала устанавливается поворот, затем задержка)

        public float CanA
        {
            get { return canA; }
        }
        public float CanB
        {
            get { return canB; }
        }
        public float CanC
        {
            get { return canC; }
        }
        public float CanD
        {
            get { return canD; }
        }
        public float CanE
        {
            get { return canE; }
        }
        public float CanF
        {
            get { return canF; }
        }
        public float CanGrab
        {
            get { return canGrab; }
        }
        public long Time { get { return time; } }

        public float this[char ch]
        {
            set
            {
                switch (ch)
                {
                    case 'a':
                        canA = value;
                        return;
                    case 'b':
                        canB = value;
                        return;
                    case 'c':
                        canC = value;
                        return;
                    case 'd':
                        canD = value;
                        return;
                    case 'e':
                        canE = value;
                        return;
                    case 'f':
                        canF = value;
                        return;
                    case 'g':
                        canGrab = value;
                        return;
                    case 't':
                        time = (long)value;
                        return ;
                    default:
                        return;
                }
            }
            get
            {
                switch (ch)
                {
                    case 'a':
                        return canA;
                    case 'b':
                        return canB;
                    case 'c':
                        return canC;
                    case 'd':
                        return canD;
                    case 'e':
                        return canE;
                    case 'f':
                        return canF;
                    case 'g':
                        return canGrab;
                    case 't':
                        return time;
                    default:
                        return 0;
                }
            }
        }

        public Point(   float canA=0 , float canB=0 , float canC=0 ,float canD =0, 
                        float canE =0, float canF=0 , float canGrab = 0, long time=0 ) =>
            setAllDegree(canA, canB, canC, canD, canE, canF, canGrab, time);

        //public Point() => setAllDegree(0, 0, 0, 0, 0, 0, 0, 0);



        public void IncrementPoint() => numPoint = ++numPoints;

        public static Point operator ~(Point p) => equivalent(p);

        /// <summary>
        /// создаёт эквивалентный обьект
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Point equivalent(Point p)
        {
           Point np= new Point(    p.CanA, p.CanB,
                                   p.CanC, p.CanD,
                                   p.CanE, p.CanF,
                                   p.canGrab, p.Time);
            
            return np;
        }

        /// <summary>
        /// функция принимает значения для каждого канала обобщенных координат
        /// </summary>
        /// <param name="canA"></param>
        /// <param name="canB"></param>
        /// <param name="canC"></param>
        /// <param name="canD"></param>
        /// <param name="canE"></param>
        /// <param name="canF"></param>
        /// <param name="time"></param>
        private void setAllDegree(float canA, float canB, float canC, float canD, float canE, float canF, float grabCan, long time)
        {
            this.canA = canA;
            this.canB = canB;
            this.canC = canC;
            this.canD = canD;
            this.canE = canE;
            this.canF = canF;
            this.canGrab = grabCan;
            this.time = time;
        }

        /// <summary>
        /// Возвращает строку с выписанными в ряд значениями индекса канала, координаты и завершающего знака
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Point " + this.numPoint.ToString() + '\t' + this.time.ToString() + " ms.";
        }

        public string numString()
        {
            return "Point " + this.numPoint.ToString() + '\t' + this.time.ToString() + " ms." + '\r' + '\n';
        }
    }
}
