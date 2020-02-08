using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;


namespace PointSpase
{
    public class Points : List<Point>
    {
        //private Point pastPoint= new Point();
        //public Point PastPoint
        //{
        //    get { return pastPoint; }
        //}

        private int pointsCoint = 0;
        public int PointsCoint
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

        public void Add(int canA, int canB, int canC, int canD,
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
