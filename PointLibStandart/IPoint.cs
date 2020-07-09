using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using KinematicModeling;

namespace PointSpase
{
    public abstract class PointParam
    {
        protected static int numPoints = 0; //общее количество созданных точек 

        protected int numPoint;
        protected float canGrab;
        protected long time; 
        protected PassingStrategy contextStrategy;

        /// <summary>
        /// индекс точки
        /// </summary>
        public int NumPoint { get { return numPoint; } }

        /// <summary>
        /// значение канала рабочего органа
        /// </summary>
        public float CanGrab  { get { return canGrab; } }

        /// <summary>
        /// время перехода к данной точки из прошлой
        /// </summary>
        public long Time { get { return time; } }

        /// <summary>
        /// закон интерполяции (линейный или синусоидальный)
        /// </summary>
        public PassingStrategy PointPassingStrategy
        {
            get { return contextStrategy ; }
            set { contextStrategy = value; }
        }



    }

}
