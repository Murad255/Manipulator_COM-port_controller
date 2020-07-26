﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using PointSpase;
using System.Threading;
using static PointSpase.Point;

namespace ManipulatorSerialInterfase
{
    public class ManipulatorSerialPort : SerialPort
    {
        private static readonly Object s_lock = new Object();
        private static ManipulatorSerialPort instance = null;

        private ManipulatorSerialPort()
        {
            RX_data = new Queue<char>();
        }

        public static ManipulatorSerialPort Instance
        {
            get
            {
                if (instance != null) return instance;
                Monitor.Enter(s_lock);
                ManipulatorSerialPort temp = new ManipulatorSerialPort();
                Interlocked.Exchange(ref instance, temp);
                Monitor.Exit(s_lock);

                return instance;
            }
        }

        private Queue<char> RX_data;    //буфер для принятых данных
        public Queue<char> RX_Data
        {
            get { return RX_data; }
            set { RX_data = value; }
        }

        public enum chanal { chanalA = 1, chanalB, chanalC, chanalD, chanalE, chanalF, grabChanal };

        /// <summary>
        /// Конвертирует значение угла с заданым минимальным и максимальным углом в ззначение от 0 до 2000
        /// </summary>
        /// <param name="degre"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Map(float degre, float min, float max)
        {
            return (int)((degre - min) / (max - min) * 2000);
        }
        /// <summary>
        /// Алгоритм, преобразующий Канал и значение от 0 до 2000 в 3-х битное значение
        /// </summary>
        /// <param name="index"></param>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private static string BinPacskage(int index, int numbers)
        {
            if (numbers > 2000) throw new MaxValueException("Значение подвижности на канале " + index.ToString() + " превысило предел!");
            if (numbers < 0) throw new MinValueException("Значение подвижности на канале " + index.ToString() + " ниже предела!");

            char[] mas = new char[3];
            mas[0] = Convert.ToChar((index << 4) | (numbers & 0xF));
            mas[1] = Convert.ToChar((index << 4) | ((numbers & 0xF0) >> 4));
            mas[2] = Convert.ToChar((index << 4) | ((numbers & 0xF00) >> 8));

            string str = null;
            str += mas[0];
            str += mas[1];
            str += mas[2];
            return str;

        }
        private static string BinPacskage2(int index, int numbers)
        {
            char[] mas = new char[3];
            mas[0] = Convert.ToChar((index << 4) | (numbers % 32));
            mas[1] = Convert.ToChar((index << 4) | ((numbers % 512) >> 4));
            mas[2] = Convert.ToChar((index << 4) | ((numbers % 8192) >> 8));
            string str = null;

            str += Convert.ToString(Convert.ToInt32(mas[2]), 2);
            str += Convert.ToString(Convert.ToInt32(mas[1]), 2);
            str += Convert.ToString(Convert.ToInt32(mas[0]), 2);
            str += ' ';
            return str;

        }
        public void Write(int i) => base.Write(i.ToString());


        public string WriteEndDebug(PointSpase.Point p)
        {
            string writeSrt = null;
            string deb = "str";
            writeSrt += BinPacskage((int)chanal.chanalA, Map(p.CanA, Point.PhysicalMinPoint.CanA, Point.PhysicalMaxPoint.CanA));
            writeSrt += BinPacskage((int)chanal.chanalB, Map(p.CanB, Point.PhysicalMaxPoint.CanB, Point.PhysicalMinPoint.CanB));
            writeSrt += BinPacskage((int)chanal.chanalC, Map(p.CanC, Point.PhysicalMinPoint.CanC, Point.PhysicalMaxPoint.CanC));
            writeSrt += BinPacskage((int)chanal.chanalD, Map(p.CanD, Point.PhysicalMinPoint.CanD, Point.PhysicalMaxPoint.CanD));
            writeSrt += BinPacskage((int)chanal.chanalE, Map(p.CanE, Point.PhysicalMinPoint.CanE, Point.PhysicalMaxPoint.CanE));
            writeSrt += BinPacskage((int)chanal.chanalF, Map(p.CanF, Point.PhysicalMinPoint.CanF, Point.PhysicalMaxPoint.CanF));
            writeSrt += BinPacskage((int)chanal.grabChanal, Map(p.CanGrab, Point.PhysicalMinPoint.CanGrab, Point.PhysicalMaxPoint.CanGrab));

            deb += BinPacskage2((int)chanal.chanalA, Map(p.CanA, Point.PhysicalMinPoint.CanA, Point.PhysicalMaxPoint.CanA)) + '\t';
            deb += BinPacskage2((int)chanal.chanalB, Map(p.CanB, Point.PhysicalMaxPoint.CanB, Point.PhysicalMinPoint.CanB)) + '\t';
            deb += BinPacskage2((int)chanal.chanalC, Map(p.CanC, Point.PhysicalMinPoint.CanC, Point.PhysicalMaxPoint.CanC)) + '\t';
            deb += BinPacskage2((int)chanal.chanalD, Map(p.CanD, Point.PhysicalMinPoint.CanD, Point.PhysicalMaxPoint.CanD)) + '\t';
            deb += BinPacskage2((int)chanal.chanalE, Map(p.CanE, Point.PhysicalMinPoint.CanE, Point.PhysicalMaxPoint.CanE)) + '\t';
            deb += BinPacskage2((int)chanal.chanalF, Map(p.CanF, Point.PhysicalMinPoint.CanF, Point.PhysicalMaxPoint.CanF)) + '\t';
            deb += BinPacskage2((int)chanal.grabChanal, Map(p.CanGrab, Point.PhysicalMinPoint.CanGrab, Point.PhysicalMaxPoint.CanGrab));
            deb += '\n';
            if (this.IsOpen)
                Write(writeSrt);
            else throw new Exception("COM порт закрыт");
            return deb;
        }

        /// <summary>
        /// Преобразование и отправка точки
        /// </summary>
        /// <param name="p"></param>
        public void Write(PointSpase.Point p)
        {
            try
            {
                string writeSrt = null;
                //собираем строку для отправки
                writeSrt += BinPacskage((int)chanal.chanalA, Map(p.CanA, PhysicalMinPoint.CanA, PhysicalMaxPoint.CanA));
                writeSrt += BinPacskage((int)chanal.chanalB, Map(p.CanB, PhysicalMaxPoint.CanB, PhysicalMinPoint.CanB));//для B инвертированно
                writeSrt += BinPacskage((int)chanal.chanalC, Map(p.CanC, PhysicalMinPoint.CanC, PhysicalMaxPoint.CanC));
                writeSrt += BinPacskage((int)chanal.chanalD, Map(p.CanD, PhysicalMinPoint.CanD, PhysicalMaxPoint.CanD));
                writeSrt += BinPacskage((int)chanal.chanalE, Map(p.CanE, PhysicalMinPoint.CanE, PhysicalMaxPoint.CanE));
                writeSrt += BinPacskage((int)chanal.chanalF, Map(p.CanF, PhysicalMinPoint.CanF, PhysicalMaxPoint.CanF));
                writeSrt += BinPacskage((int)chanal.grabChanal, Map(p.CanGrab, PhysicalMinPoint.CanGrab, PhysicalMaxPoint.CanGrab));
                if (this.IsOpen) // && this.CtsHolding)
                    Write(writeSrt);
                else throw new Exception("COM порт закрыт");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }


}
