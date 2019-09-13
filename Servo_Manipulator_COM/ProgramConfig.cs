using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Servo_Manipulator_COM
{
    public sealed class ProgramConfig
    {
        private static readonly Object s_lock = new Object();
        private static ProgramConfig instance = null;

        private ProgramConfig()
        {
            speed = new int();
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

        private int speed;
        private int portNum;
        private string filePozition;

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public int PortNum
        {
            get { return portNum; }
            set { portNum = value; }
        }
        public string FilePozition
        {
            get { return filePozition; }
            set { filePozition = value; }
        }

        public static void Load()
        {
            try
            {
                string Path = $"{Environment.CurrentDirectory}\\programConfig.progc";
                var data = File.ReadAllText(Path);//File.ReadAllText($"{Environment.CurrentDirectory}\\{Path}");
                ProgramConfig.instance = JsonConvert.DeserializeObject<ProgramConfig>(data);
            }
            catch(FileNotFoundException fnfe)
            {
                ProgramConfig.instance.PortNum = 1;
                ProgramConfig.instance.Speed = 9600;
                ProgramConfig.instance.FilePozition = null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void Save()
        {
            try
            {
                string Path = $"{Environment.CurrentDirectory}\\programConfig.progc";
                using (StreamWriter sr = new StreamWriter(Path, false))
                {
                    ProgramConfig prog = ProgramConfig.Instance;
                    sr.WriteLine(JsonConvert.SerializeObject(prog));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

}