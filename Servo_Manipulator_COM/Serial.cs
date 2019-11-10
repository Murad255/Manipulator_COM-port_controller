using System;
using System.IO.Ports;

namespace Servo_Manipulator_COM
{
    /// <summary>
    /// Класс для отправки точек с использованием многоканального протокола передачи данных
    /// </summary>
    class Serial
    {
        private SerialPort port;

        //Константы, учитывающие изн чальный поворот осей сервоприводов
        //private const int qA = 90;
        //private const int qB = 180 + 45;
        //private const int qC = 90 + 45;
        //private const int qD = -40;
        //private const int qE = -100;
        //private const int qF = 90;

        public const float qAmin = -90;
        public const float qBmin = -45;
        public const float qCmin = -45;
        public const float qDmin = -90;
        public const float qEmin = 100;
        public const float qGmin = 0;
        public const float qFmin = -90;

        public const float qAmax = qAmin+180;
        public const float qBmax = qBmin+270;
        public const float qCmax = qCmin+ 270;
        public const float qDmax = qDmin + 180;
        public const float qEmax = qEmin + 180;
        public const float qFmax = qFmin + 180;
        public const float qGmax = qGmin + 180;

         enum chanal {  chanalA = 1, chanalB, chanalC, chanalD, chanalE, chanalF, grabChanal };


        public SerialPort Port { get => port; }
        public Serial(SerialPort p) => port = p;
        public Serial() { }
        private static int Map(float degre, float min, float max)
        {
            return (int)((degre - min) / (max - min) * 2000 );
        }

        private static string BinPacskage(int index, int numbers)
        {
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
        public void Write(int i) => port.Write(i.ToString());
        public void Write(String st) => port.Write(st);

        public string Write(PointSpase.Point p)
        {
            string writeSrt = null;
            string  deb= "str";
            writeSrt += BinPacskage((int)chanal.chanalA, Map(p.CanA, qAmin, qAmax));

            writeSrt += BinPacskage((int)chanal.chanalB, Map(p.CanB, qBmin, qBmax));

            writeSrt += BinPacskage((int)chanal.chanalC, Map(p.CanC, qCmin, qCmax));

            writeSrt += BinPacskage((int)chanal.chanalD, Map(p.CanD, qDmin, qDmax));

            writeSrt += BinPacskage((int)chanal.chanalE, Map(p.CanE, qEmin, qEmax));

            writeSrt += BinPacskage((int)chanal.chanalF, Map(p.CanF, qFmin, qFmax));

            writeSrt += BinPacskage((int)chanal.grabChanal, Map(p.CanGrab, qGmin, qGmax));

            deb += BinPacskage2((int)chanal.chanalA, Map(p.CanA, qAmin, qAmax))+'\t';
            deb += BinPacskage2((int)chanal.chanalB, Map(p.CanB, qBmin, qBmax)) + '\t';
            deb += BinPacskage2((int)chanal.chanalC, Map(p.CanC, qCmin, qCmax)) + '\t';
            deb += BinPacskage2((int)chanal.chanalD, Map(p.CanD, qDmin, qDmax)) + '\t';
            deb += BinPacskage2((int)chanal.chanalE, Map(p.CanE, qEmin, qEmax)) + '\t';
            deb += BinPacskage2((int)chanal.chanalF, Map(p.CanF, qFmin, qFmax)) + '\t';
            deb += BinPacskage2((int)chanal.grabChanal, Map(p.CanGrab, qGmin, qGmax));
            deb += '\n';
            port.Write(writeSrt);
            return deb;
        }

    }
}
