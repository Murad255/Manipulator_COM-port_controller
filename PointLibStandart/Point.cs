using System;

namespace PointSpase
{
    public class Point : PointParam
    {
       public static Point tempPoint = new Point();


        public static readonly Point MinPoint = new Point() { canA = -90, canB = -50, canC = -140, canD = -90, canE = -80, canF = -98, canGrab = 0, time = 0 };
        public static readonly Point MaxPoint = new Point() { canA = 90, canB = 228, canC = 130, canD = 90, canE = 100, canF = 82, canGrab = 180, time = long.MaxValue };
        private float canA, canB, canC, canD, canE, canF; //обобщенные координаты (углы поворота сервориводов)

        #region индексаторы полей
        public float CanA
        {
            set
            {
                this[(char)pointEnum.A] = value;
            }
            get { return canA; }
        }
        public float CanB
        {
            set
            {
                this[(char)pointEnum.B] = value;
            }
            get { return canB; }
        }
        public float CanC
        {
            set
            {
                this[(char)pointEnum.C] = value;
            }
            get { return canC; }
        }
        public float CanD
        {
            set
            {
                this[(char)pointEnum.D] = value;
            }
            get { return canD; }
        }
        public float CanE
        {
            set
            {
                this[(char)pointEnum.E] = value;
            }
            get { return canE; }
        }
        public float CanF
        {
            set
            {
                this[(char)pointEnum.F] = value;
            }
            get { return canF; }
        }
        #endregion

        public Point(float canA, float canB, float canC, float canD,
                float canE, float canF, float canGrab, long time=0, int index = 0)
        {

            this[(char)pointEnum.A] = canA;
            this[(char)pointEnum.B] = canB;
            this[(char)pointEnum.C] = canC;
            this[(char)pointEnum.D] = canD;
            this[(char)pointEnum.E] = canE;
            this[(char)pointEnum.F] = canF;
            this[(char)pointEnum.Grab] = canGrab;
            this[(char)pointEnum.Time] = time;
            this.numPoint = index;
        }

        public Point() { }

        public enum pointEnum { A = 'a', B = 'b', C = 'c', D = 'd', E = 'e', F = 'f',Grab='g', Time = 't' }
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

        public void IncrementPoint() => numPoint = ++numPoints;

        //public static Point operator =(Point p1, Point p2) => equivalent(p);

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

            np.Time                 = p.Time;
            np.CanGrab              = p.CanGrab;
            np.NumPoint             = p.NumPoint;
            np.PointPassingStrategy = p.PointPassingStrategy;
            np.MovementType         = p.MovementType;

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

        /// <summary>
        /// Возвращает строку с выписанными в ряд значениями индекса канала, координаты и завершающего знака
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Point " + this.numPoint.ToString() + '\t' + this.time.ToString() + " ms." + '\t'+"Grab ="+this.canGrab.ToString();
        }

        public string numString()
        {
            return "Point " + this.numPoint.ToString() + '\t' + this.time.ToString() + " ms." + '\r' + '\n';
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
