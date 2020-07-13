using PointSpase;
using System;
using System.Diagnostics;
using static System.Math;

namespace KinematicModeling
{
    public static class TaskDecision
    {

        public static readonly double L_01 = 80;
        public static readonly double L_11 = 20;
        public static readonly double L_2 = 106;
        public static readonly double L_40 = 26;
        public static readonly double L_41 = 135;
        public static readonly double L_4 = Sqrt(L_40 * L_40 + L_41 * L_41);
        public static readonly double L_56 = 135;
        private const int accuracy = 6; //точность--количество точек после запятой

        /// <summary>
        /// Решение ОЗК манипулятора
        /// </summary>
        /// <param name="dec"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        static public Point DecToPoint(Dec dec)
        {
            Quaternion Q_S = new Quaternion(0, 1, 0, 0);
            Quaternion Q_p = KinematicMath.toQuaternion(
                -1 * dec.AnglA * PI * 2 / 180.0,
                -1 * dec.AnglB * PI * 2 / 180.0,
                -1 * dec.AnglC * PI * 2 / 180.0);

            Quaternion Q_7 = Q_S * Q_p;

            Vector V_p = new Vector(dec.DecX, dec.DecY, dec.DecZ);
            Vector V_7 = V_p;
            Vector V_75 = (new Vector(Q_7.Reverse())) * L_56;
            Vector V_05 = V_7 + V_75;
            double FI_1;
            //обработка сингулярности
            if ((V_05.y == 0) && (V_05.x == 0))
                FI_1 = Atan2(dec.DecY, dec.DecX);
            else if (V_05.y < 0)
                FI_1 = Atan2(V_05.Reverse().y, V_05.Reverse().x);
            else
                FI_1 = Atan2(V_05.y, V_05.x);

            Vector V_02 = new Vector(L_11 * Cos(FI_1), L_11 * Sin(FI_1), L_01);
            Vector V_25 = V_05 - V_02;
            Vector V_M = new Vector(V_05.x, V_05.y, V_02.z);
            Vector X_2 = KinematicMath.RotationOfVector(new Vector(1, 0, 0), new Vector(0, 0, 1), FI_1); //V_M - V_02;
            double ALFA_1 = KinematicMath.AngleBetweenVectors(X_2, V_25);
            double BETTA_1 = Atan2(L_41, L_40);
            double BETTA_2 = Atan2(L_40, L_41);
            double L_25 = V_25.Abs();
            double ALFA_2 = Acos((L_2 * L_2 + L_25 * L_25 - L_4 * L_4) / (2 * L_2 * L_25));
            double ALFA_3 = Acos((L_2 * L_2 + L_4 * L_4 - L_25 * L_25) / (2 * L_2 * L_4));
            double ALFA_5 = Acos((L_4 * L_4 + L_25 * L_25 - L_2 * L_2) / (2 * L_4 * L_25));
            double FI_2 = ALFA_1 + ALFA_2;
            double FI_3 = BETTA_1 + ALFA_3 - PI;

            Vector Z_2 = (X_2 * V_02).Normalized();//
            //Vector X_2 = V_X2.Normalized();
            Vector V_23 = KinematicMath.RotationOfVector(X_2, Z_2, FI_2) * L_2;

            Vector V_3 = V_23 + V_02;
            Vector X_3 = V_23.Normalized();
            Vector V_Z40 = KinematicMath.RotationOfVector(X_3, Z_2, FI_3) * L_40;

            Vector V_4 = V_3 + V_Z40;
            Vector V_47 = V_7 - V_4;
            Vector V_45 = V_47 + V_75;
            Vector X_5 = V_45.Normalized();
            Vector Z_7 = V_75.Reverse().Normalized();
            Vector Z_5;
            if (X_5 == Z_7) Z_5 = Z_2;
            else Z_5 = (V_45 * V_75).Normalized();//

            Vector Z_5_ = KinematicMath.RotationOfVector(Z_5, new Vector(0, 0, 1), FI_1 - PI / 2);
            Debug.WriteLine(Z_5_.ToString());

            if (Z_5_.x < 0)
            {
                Z_5_ = Z_5.Reverse();
                Debug.WriteLine("Z_5_ = ~Z_5");

            }
            //обработка сингулярности
            if (Z_5.x == -1) Z_5 = Z_5.Reverse();
            double FI_4 = KinematicMath.AngleBetweenVectors(new Vector(Round(Z_2.x, accuracy), Round(Z_2.y, accuracy), Round(Z_2.z, accuracy)),
                                                                new Vector(Round(Z_5.x, accuracy), Round(Z_5.y, accuracy), Round(Z_5.z, accuracy)));
            if (FI_4 == PI) FI_4 = 0;
            double FI_42 = KinematicMath.AngleBetweenVectors(Z_2, Z_5.Reverse());
            double FI_5 = KinematicMath.AngleBetweenVectors(X_5, Z_7, Z_5_);
            Vector Y_7 = (Z_5 * (new Vector(0, 0, -1))).Normalized();
            double FI_6 = KinematicMath.AngleBetweenVectors(Z_5, Y_7) + Acos(Q_7.w) - PI / 2;

            Point point = new Point();


            point.CanA = (float)Round((180.0 / PI * FI_1 - 90), accuracy);
            point.CanB = (float)Round((180.0 / PI * FI_2), accuracy);
            point.CanC = (float)Round(180.0 / PI * FI_3, accuracy);
            point.CanD = (float)Round((180.0 / PI * FI_4), accuracy);
            point.CanE = (float)Round((180.0 / PI * FI_5), accuracy);
            point.CanF = (float)Round((180.0 / PI * FI_6 - 90), accuracy);

            point.Time                  = dec.Time;
            point.CanGrab               = dec.CanGrab;
            point.NumPoint              = dec.NumPoint;
            point.PointPassingStrategy  = dec.PointPassingStrategy;
            point.MovementType          = dec.MovementType;

            return point;
        }

        /// <summary>
        /// Решение ПЗК манипулятора
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        static public Dec PointToDec(Point point)
        {
            VectorQuaternion T1 = new VectorQuaternion(
               new Vector(0, 0, 0),
               new Quaternion(new Vector(0, 0, 1), (point.CanA / 180.0 - 90.0) * PI)
               );
            VectorQuaternion T2 = new VectorQuaternion(
               new Vector(0, L_11, L_01),
               new Quaternion(new Vector(1, 0, 0), point.CanB / 180.0 * PI)
               );
            VectorQuaternion T3 = new VectorQuaternion(
               new Vector(0, L_2, 0),
               new Quaternion(new Vector(1, 0, 0), (point.CanC - 90.0) / 180.0 * PI)
               );
            VectorQuaternion T4 = new VectorQuaternion(
               new Vector(0, L_41, L_40),
               new Quaternion(new Vector(0, 0, 1), point.CanD / 180.0 * PI)
               );
            VectorQuaternion T5 = new VectorQuaternion(
               new Vector(0, 0, 0),
               new Quaternion(new Vector(1, 0, 0), point.CanE / 180.0 * PI)
               );
            VectorQuaternion T6 = new VectorQuaternion(
               new Vector(0, L_56, 0),
               new Quaternion(new Vector(0, 0, 1), point.CanF / 180.0 * PI)
               );
            VectorQuaternion T7 = new VectorQuaternion(
               new Vector(0, 0, 0),
               new Quaternion(0, 0, 0, 1)
               );

            VectorQuaternion Tp = T1 * T2 * T3 * T4 * T5 * T6 * T7;

            double heading;
            double attitude;
            double bank;

            double test = Tp.q.x * Tp.q.y + Tp.q.z * Tp.q.w;
            if (test > 0.499)
            { // singularity at north pole
                heading = 2 * Atan2(Tp.q.x, Tp.q.w);
                attitude = PI / 2;
                bank = 0;
            }
            if (test < -0.499)
            { // singularity at south pole
                heading = -2 * Atan2(Tp.q.x, Tp.q.w);
                attitude = -PI / 2;
                bank = 0;
            }
            double sqx = Tp.q.x * Tp.q.x;
            double sqy = Tp.q.y * Tp.q.y;
            double sqz = Tp.q.z * Tp.q.z;
            heading = Atan2(2 * Tp.q.y * Tp.q.w - 2 * Tp.q.x * Tp.q.z, 1 - 2 * sqy - 2 * sqz);
            attitude = Asin(2 * test);
            bank = Atan2(2 * Tp.q.x * Tp.q.w - 2 * Tp.q.y * Tp.q.z, 1 - 2 * sqx - 2 * sqz);

            Dec dec = new Dec(Round(Tp.v.x, accuracy),
                             Round(Tp.v.y, accuracy),
                             Round(Tp.v.z, accuracy),
                             Round(-180.0 / PI * heading, accuracy),
                             Round(180.0 / PI * attitude, accuracy),
                             Round(180.0 / PI * bank, accuracy)
                          );

            dec.Time                    = point.Time;
            dec.CanGrab                 = point.CanGrab;
            dec.NumPoint                = point.NumPoint;
            dec.PointPassingStrategy    = point.PointPassingStrategy;
            dec.MovementType            = point.MovementType;

            return dec;
        }

        [DebuggerDisplay("X={x}; Y={y}; Z={z}; W={w}")]
        private struct Quaternion
        {
            public double x, y, z; // Вектор
            public double w;     // Скаляр

            public Quaternion(double X, double Y, double Z, double W)
            {
                x = X; y = Y; z = Z; w = W;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="V">Вектор</param>
            /// <param name="angle">угол поворота вектора</param>
            public Quaternion(Vector V, double angle)
            {
                x = V.x * Sin(angle / 2);
                y = V.y * Sin(angle / 2);
                z = V.z * Sin(angle / 2);
                w = Cos(angle / 2);
            }
            public Quaternion(Vector V)
            {
                Vector vector = V.Normalized();
                if (Round(vector.Abs(), accuracy) == 0.0) { x = 0; y = 0; z = 0; w = 1; }
                else
                {
                    x = vector.x; y = vector.y; z = vector.z; w = 0;
                }
            }

            public Quaternion Mult(Quaternion b)
            {
                Quaternion a = this;
                Quaternion res = new Quaternion();
                res.w = a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z;
                res.x = a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y;
                res.y = a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x;
                res.z = a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w;
                return res;
            }
            public static Quaternion operator *(Quaternion Q1, Quaternion Q2)
            {
                return Q1.Mult(Q2);
            }

            public Quaternion Add(Quaternion quaternion)
            {
                return new Quaternion(x + quaternion.x, y + quaternion.y, z + quaternion.z, w + quaternion.w);
            }
            public static Quaternion operator +(Quaternion V1, Quaternion V2)
            {
                return V1.Add(V2);
            }
            public Quaternion Normalized()
            {
                double radius = Abs();
                if (radius != 0.0) return new Quaternion(x / radius, y / radius, z / radius, w / radius);
                else return new Quaternion(0, 0, 0, 1);
            }

            /// <summary>
            /// модуль выатерниона
            /// </summary>
            /// <returns></returns>
            public double Abs()
            {
                return Sqrt(x * x + y * y + z * z + w * w);
            }

            /// <summary>
            /// обратный кватернион
            /// </summary>
            /// <returns></returns>
            public Quaternion Reverse()
            {
                double c = Pow(Abs(), 2);
                return new Quaternion(-1 * x / c, -1 * y / c, -1 * z / c, w / c);
            }

        }

        [DebuggerDisplay("X={x}; Y={y}; Z={z}")]
        private struct Vector
        {
            public double x, y, z; // Вектор

            public Vector(double X, double Y, double Z)
            {
                x = X; y = Y; z = Z;
            }
            public Vector(Quaternion Q)
            {
                Vector V = new Vector();
                V.x = Q.x; V.y = Q.y; V.z = Q.z;
                this = V.Normalized();
            }

            /// <summary>
            /// Нормализация вектора, после которой модуль вектора равен единице
            /// </summary>
            /// <returns></returns>
            public Vector Normalized()
            {
                double radius = Abs();
                if (radius == 0.0) return new Vector(0, 0, 0);
                else return new Vector(x / radius, y / radius, z / radius);
            }

            public Vector Reverse()
            {
                return new Vector(-1 * x, -1 * y, -1 * z);
            }

            public double Abs()
            {
                return Sqrt(x * x + y * y + z * z);
            }

            public Vector Add(Vector vector)
            {
                return new Vector(x + vector.x, y + vector.y, z + vector.z);
            }
            public static Vector operator +(Vector V1, Vector V2)
            {
                return V1.Add(V2);
            }

            public Vector Minus(Vector vector)
            {
                return new Vector(x - vector.x, y - vector.y, z - vector.z);
            }
            public static Vector operator -(Vector V1, Vector V2)
            {
                return V1.Minus(V2);
            }

            public Vector MultToScalar(double sc)
            {
                return new Vector(x * sc, y * sc, z * sc);
            }
            public static Vector operator *(Vector V, double sc)
            {
                return V.MultToScalar(sc);
            }


            public static Vector Cross(Vector A, Vector B)
            {
                Vector res = new Vector();
                res.x = A.y * B.z - B.y * A.z;
                res.y = A.z * B.x - B.z * A.x;
                res.z = A.x * B.y - B.x * A.y;
                return res;
            }
            /// <summary>
            /// Векторное произведение векторов
            /// </summary>
            /// <param name="V1"></param>
            /// <param name="V2"></param>
            /// <returns></returns>
            public static Vector operator *(Vector V1, Vector V2)
            {
                return Cross(V1, V2);
            }
            public static double Dot(Vector A, Vector B)
            {
                return A.x * B.x + A.y * B.y + A.z * B.z; ;
            }
            /// <summary>
            /// скалярное произведение векторов
            /// </summary>
            /// <param name="V1"></param>
            /// <param name="V2"></param>
            /// <returns></returns>
            public static double operator |(Vector V1, Vector V2)
            {
                return Dot(V1, V2);
            }

            public static bool operator ==(Vector V1, Vector V2)
            {
                if ((Round(V1.x, accuracy) == Round(V2.x, accuracy)) &&
                    (Round(V1.y, accuracy) == Round(V2.y, accuracy)) &&
                    (Round(V1.z, accuracy) == Round(V2.z, accuracy))
                    )
                    return true;
                else return false;
            }
            public static bool operator !=(Vector V1, Vector V2)
            {
                if (V1 == V2) return false;
                else return true;
            }


            public static Vector Cros(Vector V1, Vector V2)
            {
                return V1.Add(V2);
            }

            public override string ToString()
            {
                return $"X ={Round(x, 1)};\t Y ={Round(y, 1)};\t Z ={Round(z, 1)}";
            }
        };

        [DebuggerDisplay("V= [{v.x}; {v.y}; {v.z}]\n Q= [ {q.x}; {q.y}; {q.z}; w={q.w}]")]
        private struct VectorQuaternion
        {
            public Vector v;
            public Quaternion q;

            public VectorQuaternion(Vector V, Quaternion Q)
            {
                v = V;
                q = Q;
            }

            public VectorQuaternion Mult(VectorQuaternion B)
            {
                VectorQuaternion A = this;
                VectorQuaternion res = new VectorQuaternion();
                Quaternion Q_B = new Quaternion(B.v);
                Quaternion Q_t = A.q * Q_B * A.q.Reverse();

                double an = A.q.Normalized().w;
                double angle = Acos(an) * 2;
                double angle2 = angle * 180 / PI;

                res.v = A.v + KinematicMath.RotationOfVector(B.v, new Vector(A.q), angle) * B.v.Abs();
                res.q = A.q * B.q;

                return res;
            }
            public static VectorQuaternion operator *(VectorQuaternion VQ1, VectorQuaternion VQ2)
            {
                return VQ1.Mult(VQ2);
            }
        }
        private static class KinematicMath
        {
            public static double AngleBetweenVectors(Vector V1, Vector V2, bool debug = false)
            {
                Vector V1n = V1.Normalized();
                Vector V2n = V2.Normalized();
                Vector N = (V2 * V1).Normalized();
                if (debug) Debug.WriteLine("N:\t" + N.ToString() + "\t\tV1n:\t" + V1n.ToString() + "\t\tV2n:\t" + V2n.ToString());

                return -1 * Atan2(Vector.Dot(N, Vector.Cross(V1n, V2n)), Vector.Dot(V1n, V2n));
            }
            public static double AngleBetweenVectors(Vector V1, Vector V2, Vector N, bool debug = false)
            {
                Vector V1n = V1.Normalized();
                Vector V2n = V2.Normalized();

                double angle = Atan2(Vector.Dot(N, Vector.Cross(V1n, V2n)), Vector.Dot(V1n, V2n));
                if (debug) Debug.WriteLine("N:\t" + N.ToString() + "\t\tV1n:\t" + V1n.ToString() + "\t\tV2n:\t" + V2n.ToString());
                double n = (V1.x * V2.x + V1.y * V2.y + V1.z * V2.z) >= 0 ? 1 : -1;
                return -1 * n * angle;
            }
            public static Vector RotationOfVector(Vector vector, Vector axis, double angle)
            {
                Quaternion Q_X2 = new Quaternion(vector);
                Quaternion Q_Z2 = new Quaternion(axis, angle);
                return new Vector(Q_Z2 * Q_X2 * Q_Z2.Reverse());
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="yaw">yaw (Z)</param>
            /// <param name="pitch">pitch (Y)</param>
            /// <param name="roll">roll (X)</param>
            /// <returns></returns>
            public static Quaternion toQuaternion(double yaw, double pitch, double roll)
            {
                // Abbreviations for the various angular functions
                double cy = Cos(yaw * 0.5);
                double sy = Sin(yaw * 0.5);
                double cp = Cos(pitch * 0.5);
                double sp = Sin(pitch * 0.5);
                double cr = Cos(roll * 0.5);
                double sr = Sin(roll * 0.5);

                Quaternion q = new Quaternion();
                q.w = cy * cp * cr + sy * sp * sr;
                q.x = cy * cp * sr - sy * sp * cr;
                q.y = sy * cp * sr + cy * sp * cr;
                q.z = sy * cp * cr - cy * sp * sr;
                return q;
            }

        }


    }
}
