using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servo_Manipulator_COM
{
    public sealed class ProgramConfig
    {
        private static readonly Object s_lock = new Object();
        private static ProgramConfig instance = null;

        private ProgramConfig()
        {
        }

        public static ProgramConfig Instance
        {
            get
            {
                if (instance != null) return instance;
                Monitor.Enter(s_lock);
                ProgramConfig temp = new ProgramConfig();
                Interlocked.Exchange(ref instance, temp);
                Monitor.Exit(s_lock);
                return instance;
            }
        }




    }

}