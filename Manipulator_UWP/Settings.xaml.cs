using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using ManipulatorSerialInterfase;
using PointSpase;
using static Manipulator_UWP.CommonFunction;
// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Manipulator_UWP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Settings : Page
    {
        ManipulatorSerialPort serialPort;
        private ProgramConfig programConfig = ProgramConfig.Instance;


        public Settings()
        {
            this.InitializeComponent();

            serialPort = ManipulatorSerialPort.Instance;

            var SpeedNamesList = new List<string>();
            SpeedNamesList.Add("300");
            SpeedNamesList.Add("1200");
            SpeedNamesList.Add("2400");
            SpeedNamesList.Add("4800");
            SpeedNamesList.Add("9600");
            SpeedNamesList.Add("19200");
            SpeedNamesList.Add("38400");
            SpeedNamesList.Add("57600");
            SpeedNamesList.Add("74880");
            SpeedNamesList.Add("115200");
            SpeedNamesList.Add("230400");
            SpeedNamesList.Add("250000");
            foreach (string speedName in SpeedNamesList)  
            {
                SpeedSeetCombo.Items.Add(speedName);
            }

        }

        private void SpeedSeetCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (serialPort.IsOpen) serialPort.BaudRate = Convert.ToInt32( SpeedSeetCombo.SelectedItem);

            programConfig.SpeedComboCount= SpeedSeetCombo.SelectedIndex ;
            programConfig.Speed = Convert.ToInt32(SpeedSeetCombo.SelectedItem);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // SpeedSeetCombo.Text =  programConfig.Speed.ToString();
            SpeedSeetCombo.SelectedIndex = programConfig.SpeedComboCount;
            MinGripValueTBox.Text = programConfig.MinGripValue.ToString();
            MaxGripValueTBox.Text = programConfig.MaxGripValue.ToString();

        }



        private void SaveSetingsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int min = Convert.ToInt32(MinGripValueTBox.Text);
                int max = Convert.ToInt32(MaxGripValueTBox.Text);
                if (min < 0 || min > 180 || max < 0 || max > 180) throw new Exception("Выход за перделы! значения должны быть в пределах от 0 до 180");
                else
                {
                    programConfig.MinGripValue = min;
                    programConfig.MaxGripValue = max;
                }
            }
            catch (Exception ex)
            {
                CommonConsoleWrite(ex.Message);
            }
        }
    }
}
