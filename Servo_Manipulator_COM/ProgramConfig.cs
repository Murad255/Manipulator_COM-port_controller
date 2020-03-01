using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace Servo_Manipulator_COM
{
    public sealed class ProgramConfig
    {
        private static readonly Object s_lock = new Object();
        private static ProgramConfig instance = null;

        private ProgramConfig()
        {
            strategy = new SinPassingStrategy();
            portNum = new int();
            speed = new int();
            filePozition = null;
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

                Load();

                return instance;
            }
        }

        private int speed;
        private int portNum;
        private string filePozition;
        public PassingStrategy strategy;
        public int Speed
        {
            get { return Instance.speed; }
            set
            {
                Instance.speed = value;
                Save();
            }
        }
        public int PortNum
        {
            get { return Instance.portNum; }
            set
            {
                Instance.portNum = value;
                Save();
            }
        }
        public string FilePozition
        {
            get { return Instance.filePozition; }
            set
            {
                Instance.filePozition = value;
                Save();
            }
        }

        public PassingStrategy Strategy
        {
            get { return Instance.strategy; }
            set
            {
                Instance.strategy = value;
                Save();
            }
        }

        private static void Load()
        {
            try
            {
                string Path = $"{Environment.CurrentDirectory}\\programConfig.progc";
                var data = File.ReadAllText(Path);//File.ReadAllText($"{Environment.CurrentDirectory}\\{Path}");
                ProgramConfig p = JsonConvert.DeserializeObject<ProgramConfig>(data);
            }
            catch(FileNotFoundException)
            {
                ProgramConfig p = ProgramConfig.Instance;
                p.Speed = 9600;
                p.PortNum = 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private static void Save()
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