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

#pragma warning disable CS0108 // Член скрывает унаследованный член: отсутствует новое ключевое слово
        public void Add(Point temp)
#pragma warning restore CS0108 // Член скрывает унаследованный член: отсутствует новое ключевое слово
        {
            //if (pointsCoint != 0) pastPoint = this[pointsCoint - 1]; //помещаем предыдущую точку в pastPoint
            base.Add(temp);
            temp.IncrementPoint();
            this.pointsCoint++;
        }

        //public void Add(int canA, int canB, int canC, int canD,
        //                int canE, int canF, long time)
        //{
        //    Point temp = new Point(canA, canB, canC, canD, canE, canF, time);
        //    this.Add(temp);
        //}

        public void Add(float canA, float canB, float canC, float canD,
                float canE, float canF, float grabCan, long time)
        {
            Point temp = new Point(canA, canB, canC, canD, canE, canF, grabCan, time);
            this.Add(temp);
        }
        public void Load(string Path)
        {
            try
            {
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

        public void SetJsonConvertPoint(string data)
        {
            try
            {
                Points temp = JsonConvert.DeserializeObject<Points>(data);
                this.AddRange(temp);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string GetJsonConvertString()
        {
            try
            {
                return JsonConvert.SerializeObject(this);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
