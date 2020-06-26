using PointSpase;
using System;
using System.Diagnostics;

namespace KinematicTask
{
    public static class TaskDecision
    {
        public static readonly double L_01 = 80;
        public static readonly double L_11 = 20;
        public static readonly double L_2 = 106;
        public static readonly double L_40 = 26;
        public static readonly double L_41 = 135;
        public static readonly double L_4 = Math.Sqrt(L_40 * L_40 + L_41 * L_41);
        public static readonly double L_56 = 175;
        private const int             accuracy = 6; //точность--количество точек после запятой

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
                -1 * dec.AnglA * Math.PI * 2 / 180.0, 
                -1 * dec.AnglB * Math.PI * 2 / 180.0,
                -1 * dec.AnglC * Math.PI * 2 / 180.0);

            Quaternion Q_7 = Q_S * Q_p;

            Vector V_p = new Vector(dec.DecX, dec.DecY, dec.DecZ);
            Vector V_7 = V_p;
            Vector V_75 = (new Vector(Q_7.Reverse())) * L_56;
            Vector V_05 = V_7 + V_75;
            double FI_1 = Math.Atan2(V_05.y, V_05.x);

            Vector V_02 = new Vector(L_11 * Math.Cos(FI_1), L_11 * Math.Sin(FI_1), L_01);
            Vector V_25 = V_05 - V_02;
            Vector V_M = new Vector(V_05.x, V_05.y, V_02.z);
            Vector V_X2 = V_M - V_02;
            double ALFA_1 = KinematicMath.AngleBetweenVectors(V_X2, V_25);
            double BETTA_1 = Math.Atan2(L_41, L_40);
            double BETTA_2 = Math.Atan2(L_40, L_41);
            double L_25 = V_25.Abs();
            double ALFA_2 = Math.Acos((L_2 * L_2 + L_25 * L_25 - L_4 * L_4) / (2 * L_2 * L_25));
            double ALFA_3 = Math.Acos((L_2 * L_2 + L_4 * L_4 - L_25 * L_25) / (2 * L_2 * L_4));
            double ALFA_5 = Math.Acos((L_4 * L_4 + L_25 * L_25 - L_2 * L_2) / (2 * L_4 * L_25));
            double FI_2 = ALFA_1 + ALFA_2;
            double FI_3 = BETTA_1 + ALFA_3 - Math.PI;

            Vector Z_2 = (V_X2 * V_02).Normalized();
            Vector X_2 = V_X2.Normalized();
            Vector V_23 = KinematicMath.RotationOfVector(X_2, Z_2, FI_2) * L_2;

            Vector V_3 = V_23 + V_02;
            Vector X_3 = V_23.Normalized();
            Vector V_Z40 = KinematicMath.RotationOfVector(X_3, Z_2, FI_3) * L_40;

            Vector V_4  = V_3  + V_Z40;
            Vector V_47 = V_7  - V_4;
            Vector V_45 = V_47 + V_75;
            Vector X_5 = V_45.Normalized();
            Vector Z_7 = V_75.Reverse().Normalized();
            Vector Z_5;
            if (X_5 == Z_7) Z_5 = Z_2;
            else Z_5 = (V_45 * V_75).Normalized();

            double FI_4 = KinematicMath.AngleBetweenVectors(Z_2, Z_5.Reverse());
            double FI_5 = KinematicMath.AngleBetweenVectors(X_5, Z_7);
            Vector Y_7 = (Z_5 * (new Vector(0, 0, -1))).Normalized();
            double FI_6 = KinematicMath.AngleBetweenVectors(Z_5, Y_7);

            Point point = new Point();
            point[(char)Point.pointEnum.Time] = (float)dec[(char)Dec.decEnum.Time];
            point[(char)Point.pointEnum.Grab] = (float)dec[(char)Dec.decEnum.Grab];
            point['a'] = (float)Math.Round((180.0 / Math.PI * FI_1 - 90), accuracy);
            point['b'] = (float)Math.Round((180.0 / Math.PI * FI_2), accuracy);
            point['c'] = (float)Math.Round(180.0 / Math.PI * FI_3, accuracy);
            point['d'] = (float)Math.Round((180.0 / Math.PI * FI_4), accuracy);
            point['e'] = (float)Math.Round((180.0 / Math.PI * FI_5), accuracy);
            point['f'] = (float)Math.Round((180.0 / Math.PI * FI_6 - 90), accuracy);

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
               new Quaternion(new Vector(0, 0, 1), (point.CanA / 180.0 - 90.0) * Math.PI)
               );
            VectorQuaternion T2 = new VectorQuaternion(
               new Vector(0, L_11, L_01),
               new Quaternion(new Vector(1, 0, 0), point.CanB / 180.0 * Math.PI)
               );
            VectorQuaternion T3 = new VectorQuaternion(
               new Vector(0, L_2, 0),
               new Quaternion(new Vector(1, 0, 0), (point.CanC - 90.0) / 180.0 * Math.PI)
               );
            VectorQuaternion T4 = new VectorQuaternion(
               new Vector(0, L_41, L_40),
               new Quaternion(new Vector(0, 0, 1), point.CanD / 180.0 * Math.PI)
               );
            VectorQuaternion T5 = new VectorQuaternion(
               new Vector(0, 0, 0),
               new Quaternion(new Vector(1, 0, 0), point.CanE / 180.0 * Math.PI)
               );
            VectorQuaternion T6 = new VectorQuaternion(
               new Vector(0, L_56, 0),
               new Quaternion(new Vector(0, 0, 1), point.CanF / 180.0 * Math.PI)
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
                heading = 2 * Math.Atan2(Tp.q.x, Tp.q.w);
                attitude = Math.PI / 2;
                bank = 0;
            }
            if (test < -0.499)
            { // singularity at south pole
                heading = -2 * Math.Atan2(Tp.q.x, Tp.q.w);
                attitude = -Math.PI / 2;
                bank = 0;
            }
            double sqx = Tp.q.x * Tp.q.x;
            double sqy = Tp.q.y * Tp.q.y;
            double sqz = Tp.q.z * Tp.q.z;
            heading = Math.Atan2(2 * Tp.q.y * Tp.q.w - 2 * Tp.q.x * Tp.q.z, 1 - 2 * sqy - 2 * sqz);
            attitude = Math.Asin(2 * test);
            bank = Math.Atan2(2 * Tp.q.x * Tp.q.w - 2 * Tp.q.y * Tp.q.z, 1 - 2 * sqx - 2 * sqz);

            Dec dec = new Dec(Math.Round(Tp.v.x, accuracy),
                            Math.Round(Tp.v.y, accuracy),
                            Math.Round(Tp.v.z, accuracy),
                            Math.Round(-180.0 / Math.PI * heading, accuracy),
                            Math.Round(180.0 / Math.PI * attitude, accuracy),
                            Math.Round(180.0 / Math.PI * bank, accuracy)
                          );

            dec[(char)Dec.decEnum.Time] = point[(char)Point.pointEnum.Time];
            dec[(char)Dec.decEnum.Grab] = point[(char)Point.pointEnum.Grab];

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
                x = V.x * Math.Sin(angle / 2);
                y = V.y * Math.Sin(angle / 2);
                z = V.z * Math.Sin(angle / 2);
                w = Math.Cos(angle / 2);
            }
            public Quaternion(Vector V)
            {
                Vector vector = V.Normalized();
                if (Math.Round(vector.Abs(), accuracy) == 0.0) { x = 0; y = 0; z = 0; w = 1; }
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
                return Math.Sqrt(x * x + y * y + z * z + w * w);
            }

            /// <summary>
            /// обратный кватернион
            /// </summary>
            /// <returns></returns>
            public Quaternion Reverse()
            {
                double c = Math.Pow(Abs(), 2);
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
                return Math.Sqrt(x * x + y * y + z * z);
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

            public Vector MultToVectorVect(Vector B)
            {
                Vector A = this;
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
                return V1.MultToVectorVect(V2);
            }
            public double MultToVectorScal(Vector B)
            {
                Vector A = this;
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
                return V1.MultToVectorScal(V2);
            }

            public static bool operator ==(Vector V1, Vector V2)
            {
                if ((Math.Round(V1.x, accuracy) == Math.Round(V2.x, accuracy)) &&
                    (Math.Round(V1.y, accuracy) == Math.Round(V2.y, accuracy)) &&
                    (Math.Round(V1.z, accuracy) == Math.Round(V2.z, accuracy))
                    )
                    return true;
                else return false;
            }
            public static bool operator !=(Vector V1, Vector V2)
            {
                if (V1 == V2) return false;
                else return true;
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
                double angle = Math.Acos(an) * 2;
                double angle2 = angle * 180 / Math.PI;

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
            public static double AngleBetweenVectors(Vector V1, Vector V2)
            {
                Vector N = V1 * V2;
                return Math.Atan2((V1 * V2).Abs(), V1 | V2) * (((V1 * V2) | N) > 0 ? 1 : -1);
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
                double cy = Math.Cos(yaw * 0.5);
                double sy = Math.Sin(yaw * 0.5);
                double cp = Math.Cos(pitch * 0.5);
                double sp = Math.Sin(pitch * 0.5);
                double cr = Math.Cos(roll * 0.5);
                double sr = Math.Sin(roll * 0.5);

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
