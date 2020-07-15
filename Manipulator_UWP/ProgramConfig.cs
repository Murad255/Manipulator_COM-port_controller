using KinematicModeling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

namespace Manipulator_UWP
{
    internal class ProgramConfig
    {

        private static readonly Object s_lock = new Object();
        private static ProgramConfig instance = null;

        private ProgramConfig()
        {
            strategy = new SinPassingStrategy();
            portNum = new int();
            speed = new int();
            filePozition = null;
            portName = new List<string>();
            speedComboCount = 9;
        }

        public static ProgramConfig Instance
        {
            get
            {
                if (instance != null) return instance;
                else
                {
                    Monitor.Enter(s_lock);
                    ProgramConfig temp = new ProgramConfig();
                    Interlocked.Exchange(ref instance, temp);
                    Monitor.Exit(s_lock);
                    instance.Load();
                    return instance;
                }
            }
        }


        private int speed;
        private int speedComboCount;
        private int portNum;
        private StorageFile filePozition;
        public PassingStrategy strategy;
        private List<string> portName;
        private int minGripValue;
        private int maxGripValue;
        private float stepChangevalue;
        public List<string> PortName { get { return portName; } }

        public void AddPortName(string name)
        {
            if (name != null) portName.Add(name);
            Save();
        }
        public void ClearPortName()
        {
            portName.Clear();
            Save();
        }


        public int Speed
        {
            get { return Instance.speed; }
            set
            {
                Instance.speed = value;
                Save();
            }
        }

        public int MinGripValue
        {
            get { return Instance.minGripValue; }
            set
            {
                Instance.minGripValue = value;
                Save();
            }
        }


        public float StepChangevalue
        {
            get { return Instance.stepChangevalue; }
            set
            {
                Instance.stepChangevalue = value;
                Save();
            }
        }
        public int MaxGripValue
        {
            get { return Instance.maxGripValue; }
            set
            {
                Instance.maxGripValue = value;
                Save();
            }
        }
        public int SpeedComboCount
        {
            get { return Instance.speedComboCount; }
            set
            {
                Instance.speedComboCount = value;
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

        public StorageFile FilePozition
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

        private async void Load()
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile configFile = await localFolder.GetFileAsync("programConfig.progc");

                var data = await FileIO.ReadTextAsync(configFile);
                ProgramConfig programConfig = JsonConvert.DeserializeObject<ProgramConfig>(data);
                if (programConfig == null) programConfig = new ProgramConfig();
            }
            catch (FileNotFoundException)
            {
                ProgramConfig programConfig = ProgramConfig.Instance;
                programConfig.Speed = 9600;
                programConfig.PortNum = 0;
                programConfig.MaxGripValue = 180;
                programConfig.MinGripValue = 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        private bool saveProcessFlag = false;
        private async void Save()
        {
            try
            {
                if (saveProcessFlag) await Task.Run(() => { while (saveProcessFlag) { } });
                saveProcessFlag = true;
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile configFile = await localFolder.CreateFileAsync("programConfig.progc",
                                                CreationCollisionOption.ReplaceExisting);
                ProgramConfig prog = ProgramConfig.Instance;

                await FileIO.WriteTextAsync(configFile, JsonConvert.SerializeObject(prog));
                saveProcessFlag = false;

            }
            catch (Exception e)
            {
                // CommonFunction.CommonConsoleWrite(e.Message);
            }
        }
    }
}
