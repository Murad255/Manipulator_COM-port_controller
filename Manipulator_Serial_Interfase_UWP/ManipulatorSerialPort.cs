using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using PointSpase;
using System.Threading;

namespace ManipulatorSerialInterfase
{
    public class ManipulatorSerialPort : SerialPort
    {
        private static readonly Object s_lock = new Object();
        private static ManipulatorSerialPort instance = null;

        private ManipulatorSerialPort() { }

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

        //Константы, учитывающие изначальный поворот осей сервоприводов
        public const float qAmin = -90;
        public const float qBmin = -45;
        public const float qCmin = -45;
        public const float qDmin = -90;
        public const float qEmin = 100;
        public const float qGmin = 0;
        public const float qFmin = -90;

        public const float qAmax = qAmin + 180;
        public const float qBmax = qBmin + 270;
        public const float qCmax = qCmin + 270;
        public const float qDmax = qDmin + 180;
        public const float qEmax = qEmin + 180;
        public const float qFmax = qFmin + 180;
        public const float qGmax = qGmin + 180;

        public  enum chanal { chanalA = 1, chanalB, chanalC, chanalD, chanalE, chanalF, grabChanal };


        private static int Map(float degre, float min, float max)
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
            if (numbers > 2000) throw new Exception("Значение подвижности на канале " + index.ToString() + " превысило предел!");
            if (numbers < 0) throw new Exception("Значение подвижности на канале " + index.ToString() + " ниже предела!");

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
            writeSrt += BinPacskage((int)chanal.chanalA, Map(p.CanA, qAmin, qAmax));

            writeSrt += BinPacskage((int)chanal.chanalB, Map(p.CanB, qBmin, qBmax));

            writeSrt += BinPacskage((int)chanal.chanalC, Map(p.CanC, qCmin, qCmax));

            writeSrt += BinPacskage((int)chanal.chanalD, Map(p.CanD, qDmin, qDmax));

            writeSrt += BinPacskage((int)chanal.chanalE, Map(p.CanE, qEmin, qEmax));

            writeSrt += BinPacskage((int)chanal.chanalF, Map(p.CanF, qFmin, qFmax));

            writeSrt += BinPacskage((int)chanal.grabChanal, Map(p.CanGrab, qGmin, qGmax));

            deb += BinPacskage2((int)chanal.chanalA, Map(p.CanA, qAmin, qAmax)) + '\t';
            deb += BinPacskage2((int)chanal.chanalB, Map(p.CanB, qBmin, qBmax)) + '\t';
            deb += BinPacskage2((int)chanal.chanalC, Map(p.CanC, qCmin, qCmax)) + '\t';
            deb += BinPacskage2((int)chanal.chanalD, Map(p.CanD, qDmin, qDmax)) + '\t';
            deb += BinPacskage2((int)chanal.chanalE, Map(p.CanE, qEmin, qEmax)) + '\t';
            deb += BinPacskage2((int)chanal.chanalF, Map(p.CanF, qFmin, qFmax)) + '\t';
            deb += BinPacskage2((int)chanal.grabChanal, Map(p.CanGrab, qGmin, qGmax));
            deb += '\n';
            Write(writeSrt);
            return deb;
        }

        /// <summary>
        /// Преобразование и отправка точки
        /// </summary>
        /// <param name="p"></param>
        public void Write(PointSpase.Point p)
        {
            string writeSrt = null;

            writeSrt += BinPacskage((int)chanal.chanalA, Map(p.CanA, qAmin, qAmax));
            writeSrt += BinPacskage((int)chanal.chanalB, Map(p.CanB, qBmin, qBmax));
            writeSrt += BinPacskage((int)chanal.chanalC, Map(p.CanC, qCmin, qCmax));
            writeSrt += BinPacskage((int)chanal.chanalD, Map(p.CanD, qDmin, qDmax));
            writeSrt += BinPacskage((int)chanal.chanalE, Map(p.CanE, qEmin, qEmax));
            writeSrt += BinPacskage((int)chanal.chanalF, Map(p.CanF, qFmin, qFmax));
            writeSrt += BinPacskage((int)chanal.grabChanal, Map(p.CanGrab, qGmin, qGmax));

            Write(writeSrt);
        }
    }
}
