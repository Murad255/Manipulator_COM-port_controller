using System;

namespace PointSpase
{
    public class Point : PointParam
    {
        public static Point tempPoint = new Point();


        public static readonly Point MinPoint = new Point(-90, -50, -50 - 90, -90, -80, -98, 0,0);
        public static readonly Point MaxPoint = new Point(90, 220 , 220 - 90, 90, 100, 82, 180,long.MaxValue);
        private float canA, canB, canC, canD, canE, canF; //обобщенные координаты (углы поворота сервориводов)

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

        //public Point(float canA, float canB , float canC , float canD ,
        //                    float canE , float canF , float canGrab , long time ) =>
        //        setAllDegree(canA, canB, canC, canD, canE, canF, canGrab, time);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canA">значение канала A</param>
        /// <param name="canB">значение канала B</param>
        /// <param name="canC">значение канала C</param>
        /// <param name="canD">значение канала D</param>
        /// <param name="canE">значение канала E</param>
        /// <param name="canF">значение канала F</param>
        /// <param name="canGrab">значение канала рабочего органа</param>
        /// <param name="time">время перехода к данной точки из прошлой</param>
        /// <param name="numPoint">индекс точки (рекомендуется не устанавливать, нужно для десериализации)</param>
        public Point(float canA = 0, float canB = 0, float canC = 0, float canD = 0,
                          float canE = 0, float canF = 0, float canGrab = 0, long time = 0, int numPoint = 0)
        {
            this[(char)pointEnum.A] = canA;
            this[(char)pointEnum.B] = canB;
            this[(char)pointEnum.C] = canC;
            this[(char)pointEnum.D] = canD;
            this[(char)pointEnum.E] = canE;
            this[(char)pointEnum.F] = canF;
            this[(char)pointEnum.Grab] = canGrab;
            this[(char)pointEnum.Time] = time;

            this.numPoint = numPoint;
        }


        public enum  pointEnum { A = 'a', B = 'b', C = 'c', D = 'd', E = 'e', F = 'f', Grab = 'g', Time = 't' }
        public float this[char ch]
        {
            set
            {
                if (value > MaxPoint[ch]) throw new MaxValueException($"Значение подвижности на канале {ch} больше предельного!\t {value} > {MaxPoint[ch]}");
                if (value < MinPoint[ch]) throw new MinValueException($"Значение подвижности на канале {ch} меньше предельного!\t {value} < {MinPoint[ch]}");
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

       

        public void IncrementPoint() => numPoint = ++numPoints;

        public static Point operator ~(Point p) => equivalent(p);

        /// <summary>
        /// создаёт эквивалентный обьект
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Point equivalent(Point p)
        {
            Point np = new Point(p.CanA, p.CanB,
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
        //private void setAllDegree(float canA, float canB, float canC, float canD, float canE, float canF, float grabCan, long time)
        //{
        //    this.canA = canA;
        //    this.canB = canB;
        //    this.canC = canC;
        //    this.canD = canD;
        //    this.canE = canE;
        //    this.canF = canF;
        //    canGrab = grabCan;
        //    this.time = time;
        //}

        /// <summary>
        /// Возвращает строку с выписанными в ряд значениями индекса канала, координаты и завершающего знака
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Point " + numPoint.ToString() + '\t' + time.ToString() + " ms.";
        }

        public string numString()
        {
            return "Point " + numPoint.ToString() + '\t' + time.ToString() + " ms." + '\r' + '\n';
        }
    }


    /// <summary>
    /// исключение, возникающее при значения угла, меньшим чем минимально допустимое
    /// </summary>
    public class MinValueException : Exception
    {

        public MinValueException() : base("Значение подвижности ниже предела!") { }

        public MinValueException(string message) : base(message) { }

    }

    /// <summary>
    /// исключение, возникающёё при значения угла, большим чем максимально допустимое
    /// </summary>
    public class MaxValueException : Exception
    {

        public MaxValueException() : base("Значение подвижности превысило предел!") { }

        public MaxValueException(string message) : base(message) { }

    }
}
