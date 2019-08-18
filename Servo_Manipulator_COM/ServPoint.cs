﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace PointSpase
{
        public delegate void Sent(string message);

        public class Point
        {
            protected static int numPoints = 0; //общее количество созданных точек 
            protected int numPoint;             //номер данного экземпляра
            public static Sent sent;
        public static Point tempPoint = new Point();
        public int NumPoints
        {
            get { return numPoints; }
        }
        public int NumPoint
        {
            get { return numPoint; }
        }

        //Константы, учитывающие изначальный поворот осей сервоприводов
        private const int qA = 90;
        private const int qB = 180;
        private const int qC = -40;
        private const int qD = -100;
        private const int qE = 90;

        private int canA, canB, canC, canD, canE, canF; //обобщенные координаты (углы поворота сервориводов)
            protected long time;                //задержка от начала выполнения (сначала устанавливается поворот, затем задержка)
        public int CanA
        {
            get { return canA; }
        }
        public int CanB
        {
            get { return canB; }
        }
        public int CanC
        {
            get { return canC; }
        }
        public int CanD
        {
            get { return canD; }
        }
        public int CanE
        {
            get { return canE; }
        }
        public int CanF
        {
            get { return canF; }
        }

        public int this[char ch]
        {
            set
            {
                switch (ch)
                {
                    case 'a':
                        canA = value+qA;
                        return;
                    case 'b':
                        canB = value*(-1)+qB;
                        return;
                    case 'c':
                        canC = value+qC;
                        return;
                    case 'd':
                        canD = value+qD;
                        return;
                    case 'e':
                        canE = value+qE;
                        return;
                    case 'f':
                        canF = value;
                        return;
                    default:
                        return;
                }
            }
        }

        public Point(int canA = 0, int canB = 0, int canC = 0, int canD = 0, int canE = 0, int canF = 0, long time = 0, bool config = false)
            {
            if (!config) setAllCanal(canA, canB, canC, canD, canE, canF, time);
            else setAllDegree(canA, canB, canC, canD, canE, canF, time);
            numPoint = ++numPoints;
            }

        public static Point operator ~(Point p) => equivalent(p);   //создаёт эквивалентный обьект

        public static Point equivalent(Point p)
        {
            return new Point(   p.CanA, p.CanB,
                                p.CanC, p.CanD,
                                p.CanE, p.CanF, p.getTime());
        }
        public void setAllCanal(int canA, int canB, int canC, int canD, int canE, int canF, long time)  //функция принимает значения для каждого канала обобщенных координат
            {
                this.canA = canA;
                this.canB = canB;
                this.canC = canC;
                this.canD = canD;
                this.canE = canE;
                this.canF = canF;
                this.time = time;
            }
        public void setAllDegree(int canA, int canB, int canC, int canD, int canE, int canF, long time) => //функция принимает значения для каждого канала обобщенных координат
        
             setAllCanal(   canA+qA,        //-90/90
                            (canB*-1)+qB,   // 0/180
                            canC+qC,        //40/220
                            canD+qD,        //100/280
                            canE+qE,        //-90/90 
                            canF, 
                            time);
        

        public void write(  int canA, int canB, int canC, 
                            int canD, int canE, int canF, 
                            long time, bool config = false)    //функция для ввода и отправки значений обобщенных координат
            {
                try
                {
                    if(!config) setAllCanal(canA, canB, canC, canD, canE, canF, time);
                    else setAllDegree(canA, canB, canC, canD, canE, canF, time);
                //отправляет координаты и заключает их в символs: $- начало точки #- конец точки, 
                sent("$");
                    writeCanal();
                    sent("#");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(),
                                    "Ошибка!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }

            public void write(bool config = false) => write(    this.canA, 
                                                                this.canB, 
                                                                this.canC, 
                                                                this.canD, 
                                                                this.canE, 
                                                                this.canF,
                                                                this.time,
                                                                config);

            public void writeCanal()    //функция для отправки значений обобщенных координат не в виде пакета
            {
                try
                {
                    sent('a' + this.canA.ToString() + 'z');
                    sent('b' + this.canB.ToString() + 'z');
                    sent('c' + this.canC.ToString() + 'z');
                    sent('d' + this.canD.ToString() + 'z');
                    sent('e' + this.canE.ToString() + 'z');
                    sent('f' + this.canF.ToString() + 'z');
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(),
                                    "Ошибка!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            

            public override string ToString()
            {
                return   'a' + this.canA.ToString()+ 'z' + 'b' + this.canB.ToString()+ 'z' + 'c' + this.canC.ToString()+ 'z' +
                         'd' + this.canD.ToString()+ 'z' + 'e' + this.canE.ToString()+ 'z' + 'f' + this.canF.ToString()+ 'z' +
                         'g' + this.time.ToString()+'z' ;
            }
            public string numString()
            {
                return "Point " + this.numPoint.ToString() + '\t' + this.time.ToString() + " ms." + '\r' + '\n';
            }
        
            public long getTime() { return this.time; }
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
                this.pointsCoint++;
            }

            public void Add(int canA, int canB, int canC,int canD,
                            int canE, int canF, long time, bool config = false)
            {
                Point temp = new Point(canA, canB, canC, canD, canE, canF, time, config);
                base.Add(temp);
            }

            public void Load(string Path)
            {
                try
                {
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
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }

            public void Save(string Path)
            {
                try
                {
                    using (StreamWriter sr = new StreamWriter(Path, false))
                    {
                        foreach (var item in this)
                        {
                            sr.WriteLine(item.ToString());
                        }
                        sr.Close();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }


        public class num
        {
            public char index;
            public char[] num_c=new char[4];
            public int coint;
            public void invert()
            {
                char temp;
                switch (coint)
                {
                    case 2:
                        temp = num_c[0];
                        num_c[0] = num_c[1];
                        num_c[1] = temp;
                        break;
                    case 3:
                        temp = num_c[0];
                        num_c[0] = num_c[2];
                        num_c[2] = temp;
                        break;
                    default:
                        break;
                }
                return;
            }
            public int toint()
            {
                invert();
                int toint = 0;
                for (int i = 0; i < coint; i++)
                {
                    if (num_c[i] >= '0' && num_c[i] <= '9')
                    {
                        toint += (num_c[i] - 48) * Convert.ToInt32(Math.Pow(10,Convert.ToDouble(i)));
                    }
                }
                return toint;
            }
            public void processing(string str) //преобразует строку в значение и индекс координаты
            {
                foreach (char temp in str)
                {
                    if (temp >= 'A' && temp < 'z') index = temp; 

                    else if (temp >= '0' && temp <= '9')
                    {
                        num_c[coint] = temp;
                        coint++;
                    }
                }
            }
            public num(){}
            public num(string str) => processing(str);
        }
}
