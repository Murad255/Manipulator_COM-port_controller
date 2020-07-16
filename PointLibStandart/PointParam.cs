using KinematicModeling;

namespace PointSpase
{
    public enum MovementTypes { РТР, LIN }
    public abstract class PointParam
    {
        protected static int numPoints = 0; //общее количество созданных точек 

        protected int numPoint;
        protected float canGrab;
        protected long time;
        protected PassingStrategy contextStrategy;
     
        public static void ChangeNumPoints(int num) { numPoints = num; }
        /// <summary>
        /// индекс точки
        /// </summary>
        public int NumPoint { set { numPoint = value; } get { return numPoint; } }

        /// <summary>
        /// значение канала рабочего органа
        /// </summary>
        public float CanGrab
        {
            set
            {
                if (value > 180.0) throw new MaxValueException($"Значение подвижности на канале G больше предельного!\t {value} > 180");
                if (value < 0.0) throw new MinValueException($"Значение подвижности на канале G меньше предельного!\t {value} < 0");
                canGrab = value;
            }
            get { return canGrab; }
        }

        /// <summary>
        /// время перехода к данной точки из прошлой
        /// </summary>
        public long Time
        {
            set
            {
                if (value < 0) throw new MinValueException($"Значение Time меньше предельного!\t {value} < 0");
                this.time = value;
            }
            get { return time; }
        }

        /// <summary>
        /// закон интерполяции (линейный или синусоидальный)
        /// </summary>
        public PassingStrategy PointPassingStrategy { set; get; }
        public MovementTypes MovementType { set; get; }
    }

}
