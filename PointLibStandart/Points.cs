using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;


namespace PointSpase
{
    public class Points : List<Point>
    {

        private int pointsCoint ;
        public int PointsCoint
        {
            get { return pointsCoint; }
        }

        public Points()
        {
            pointsCoint = 0;    
        }
#pragma warning disable CS0108 // Член скрывает унаследованный член: отсутствует новое ключевое слово
        public void Add(Point temp)
#pragma warning restore CS0108 // Член скрывает унаследованный член: отсутствует новое ключевое слово
        {
            base.Add(temp);
            temp.IncrementPoint();
            this.pointsCoint++;
        }

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
