using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace PointSpase
{
    public delegate void Sent(string message); //делегат для отправки сообщения в COM-порт

    public class Point
    {
        protected static int numPoints = 0; //общее количество созданных точек 
        protected int numPoint;             //номер данного экземпляра
        public static Sent sent;
        public static Point tempPoint = new Point();
      
        public int NumPoint
        {
            get { return numPoint; }
        }


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
                    default:
                        return;
                }
            }
        }

        public Point(   float canA = 0, float canB = 0, float canC = 0, 
                        float canD = 0, float canE = 0, float canF = 0, long time = 0)=>
            setAllDegree(canA, canB, canC, canD, canE, canF, time);
        

        public void IncrementPoint()=> numPoint =++numPoints;

        public static Point operator ~(Point p) => equivalent(p);   

        /// <summary>
        /// создаёт эквивалентный обьект
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Point equivalent(Point p)
        {
            return new Point(   p.CanA, p.CanB,
                                p.CanC, p.CanD,
                                p.CanE, p.CanF, p.Time);
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
        private void setAllDegree(float canA, float canB, float canC, float canD, float canE, float canF, long time)  
        {
            this.canA = canA;
            this.canB = canB;
            this.canC = canC;
            this.canD = canD;
            this.canE = canE;
            this.canF = canF;
            this.time = time;
        }

        /// <summary>
        /// Возвращает строку с выписанными в ряд значениями индекса канала, координаты и завершающего знака
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return      'a' + this.canA.ToString()+ 'z' + 'b' + this.canB.ToString()+ 'z' + 'c' + this.canC.ToString()+ 'z' +
                        'd' + this.canD.ToString()+ 'z' + 'e' + this.canE.ToString()+ 'z' + 'f' + this.canF.ToString()+ 'z' +
                        'g' + this.time.ToString()+'z' ;
        }
 
        public string numString()
        {
            return "Point " + this.numPoint.ToString() + '\t' + this.time.ToString() + " ms." + '\r' + '\n';
        }
    }



    public class Points: List<Point>
    {
        //private Point pastPoint= new Point();
        //public Point PastPoint
        //{
        //    get { return pastPoint; }
        //}

        private int pointsCoint = 0;
        public  int PointsCoint
        {
            get { return pointsCoint; }
        }

        public void Add(Point temp)
        {
        //if (pointsCoint != 0) pastPoint = this[pointsCoint - 1]; //помещаем предыдущую точку в pastPoint
            base.Add(temp);
            temp.IncrementPoint();
            this.pointsCoint++;
        }

        public void Add(int canA, int canB, int canC,int canD,
                        int canE, int canF, long time)
        {
            Point temp = new Point(canA, canB, canC, canD, canE, canF, time);
            base.Add(temp);
        }

        public void Load(string Path)
        {
            try
            {
            /*
            using (StreamReader sr = new StreamReader(Path))
            {
                // Считываем файл
                // В файле каждая строчка должна соотвествовать
                // координатам с индексами через "z"
                while (!sr.EndOfStream)
                {
                    int[] intPoint = new int[8];
                string[] tmp = sr.ReadLine().Split('z').ToArray(); //помещаем все координаты строки в массив из 7 элементов

                    foreach(string st in tmp)
                    {
                    num n = new num(st);
                    int i = (int)n.index - 97;

                    if(i>=0&&i<7)intPoint[i] = n.toint();    //т.к. координата поределяется буквой от a  до f + g--для времени, то можно отнять из индекса 97
                    }    

                Point temp = new Point( intPoint[0], intPoint[1], intPoint[2], intPoint[3],
                                        intPoint[4], intPoint[5], intPoint[6]);
                    Add(temp);
                }
                sr.Close();
            }
            */
            var data = File.ReadAllText(Path);//File.ReadAllText($"{Environment.CurrentDirectory}\\{Path}");
            Points temp = JsonConvert.DeserializeObject<Points>(data);
            this.AddRange(temp);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Save(string Path)
        {
            try
            {
            using (StreamWriter sr = new StreamWriter(Path, false))
            {
                sr.WriteLine(JsonConvert.SerializeObject(this));
            }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
