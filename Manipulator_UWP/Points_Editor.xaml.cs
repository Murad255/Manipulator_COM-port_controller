using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
//using Windows.Foundation;
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
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI;
using System.Timers;
using Timer = System.Threading.Timer;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Manipulator_UWP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Points_Editor : Page
    {
        ManipulatorSerialPort serialPort;
        private ProgramConfig programConfig = ProgramConfig.Instance;

        private static System.Timers.Timer loopTimer;
        volatile bool clicFlag;
        public Points_Editor()
        {
            this.InitializeComponent();
            serialPort = ManipulatorSerialPort.Instance;

            pbCanal_A.Minimum = Point.MinPoint.CanA;
            pbCanal_A.Maximum = Point.MaxPoint.CanA;
            pbCanal_B.Minimum = Point.MinPoint.CanB;
            pbCanal_B.Maximum = Point.MaxPoint.CanB;
            pbCanal_C.Minimum = Point.MinPoint.CanC;
            pbCanal_C.Maximum = Point.MaxPoint.CanC;
            pbCanal_D.Minimum = Point.MinPoint.CanD;
            pbCanal_D.Maximum = Point.MaxPoint.CanD;
            pbCanal_E.Minimum = Point.MinPoint.CanE;
            pbCanal_E.Maximum = Point.MaxPoint.CanE;
            pbCanal_F.Minimum = Point.MinPoint.CanF;
            pbCanal_F.Maximum = Point.MaxPoint.CanF;
            EditorUpdate();

            loopTimer = new System.Timers.Timer();
            loopTimer.Interval = 500;
            loopTimer.Enabled = false;
          ///  loopTimer.Elapsed += loopTimerEvent;
            loopTimer.AutoReset = true;
            //form button


         
        }
        /// <summary>
        /// Обновляет значения окна в сооб=тветствии со значениями CommonPoint
        /// </summary>
        private void EditorUpdate()
        {
            tBoxA.Text = CommonPoint.CanA.ToString();
            tBoxB.Text = CommonPoint.CanB.ToString();
            tBoxC.Text = CommonPoint.CanC.ToString();
            tBoxD.Text = CommonPoint.CanD.ToString();
            tBoxE.Text = CommonPoint.CanE.ToString();
            tBoxF.Text = CommonPoint.CanF.ToString();
            tBoxTime.Text = CommonPoint.Time.ToString();

            pbCanal_A.Value = CommonPoint.CanA;
            pbCanal_B.Value = CommonPoint.CanB;
            pbCanal_C.Value = CommonPoint.CanC;
            pbCanal_D.Value = CommonPoint.CanD;
            pbCanal_E.Value = CommonPoint.CanE;
            pbCanal_F.Value = CommonPoint.CanF;
        }
        private async void ScrollFunction(char ch, float addValue)
        {
            clicFlag = true;
            try
            {
                //serialWrite(ch + trackBar.Value.ToString() + 'z');
                CommonPoint[ch] = (float)(CommonPoint[ch] + addValue);
                serialPort.Write(CommonPoint);
                EditorUpdate();
                await Task.Run(async ()=> {


                    if (clicFlag) Thread.Sleep(300);
                    while (clicFlag)
                    {

                        await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                        try{
                                CommonPoint[ch] +=  addValue ;
                                serialPort.Write(CommonPoint);
                                EditorUpdate();
                            }
                            catch (MaxValueException e) { CommonConsoleWrite(e.Message, Colors.Red); CommonPoint[ch] -= addValue; }
                            catch (MinValueException e) { CommonConsoleWrite(e.Message, Colors.Red); CommonPoint[ch] -= addValue; }
                            catch (Exception e) { CommonConsoleWrite(e.Message, Colors.Red); }
                        });
                        Thread.Sleep(50);
                    }
                    
                });

            }
            catch (MaxValueException e) { CommonConsoleWrite(e.Message, Colors.Red); CommonPoint[ch] -= addValue; }
            catch (MinValueException e) { CommonConsoleWrite(e.Message, Colors.Red); CommonPoint[ch] -= addValue; }
            catch (Exception e) { CommonConsoleWrite(e.Message, Colors.Red); }

        }

        private void ClickHandler(TextBox textBox)
        {
            clicFlag = false;
            textBox.Focus(FocusState.Programmatic);
        }
 

        private float degreeChangeValue = (float)0.5;

        private void chanal_A_minus_GotFocus(object sender, RoutedEventArgs e)=>    ScrollFunction('a', -degreeChangeValue);
        private void chanal_A_minus_Click(object sender, RoutedEventArgs e) =>      ClickHandler(tBoxA);
        private void chanal_A_plus_GotFocus(object sender, RoutedEventArgs e) =>    ScrollFunction('a', degreeChangeValue);
        private void chanal_A_plus_Click(object sender, RoutedEventArgs e) =>       ClickHandler(tBoxA);

        private void chanal_B_minus_GotFocus(object sender, RoutedEventArgs e) =>   ScrollFunction('b', -degreeChangeValue);
        private void chanal_B_plus_GotFocus(object sender, RoutedEventArgs e) =>    ScrollFunction('b', degreeChangeValue);
        private void chanal_B_minus_Click(object sender, RoutedEventArgs e) =>      ClickHandler(tBoxB);
        private void chanal_B_plus_Click(object sender, RoutedEventArgs e) =>       ClickHandler(tBoxB);

        private void chanal_C_minus_GotFocus(object sender, RoutedEventArgs e) =>   ScrollFunction('c', -degreeChangeValue);
        private void chanal_C_plus_GotFocus(object sender, RoutedEventArgs e) =>    ScrollFunction('c', degreeChangeValue);
        private void chanal_C_minus_Click(object sender, RoutedEventArgs e) =>      ClickHandler(tBoxC);
        private void chanal_C_plus_Click(object sender, RoutedEventArgs e) =>       ClickHandler(tBoxC);


        private void chanal_D_minus_GotFocus(object sender, RoutedEventArgs e) =>   ScrollFunction('d', -degreeChangeValue);
        private void chanal_D_plus_GotFocus(object sender, RoutedEventArgs e) =>    ScrollFunction('d', degreeChangeValue);
        private void chanal_D_minus_Click(object sender, RoutedEventArgs e) =>      ClickHandler(tBoxD);
        private void chanal_D_plus_Click(object sender, RoutedEventArgs e) =>       ClickHandler(tBoxD);


        private void chanal_E_minus_GotFocus(object sender, RoutedEventArgs e) =>   ScrollFunction('e',-degreeChangeValue);
        private void chanal_E_plus_GotFocus(object sender, RoutedEventArgs e) =>    ScrollFunction('e', degreeChangeValue);
        private void chanal_E_minus_Click(object sender, RoutedEventArgs e) =>      ClickHandler(tBoxE);
        private void chanal_E_plus_Click(object sender, RoutedEventArgs e) =>       ClickHandler(tBoxE);

        private void chanal_F_minus_GotFocus(object sender, RoutedEventArgs e) =>   ScrollFunction('f', -degreeChangeValue);
        private void chanal_F_plus_GotFocus(object sender, RoutedEventArgs e) =>    ScrollFunction('f', degreeChangeValue);
        private void chanal_F_minus_Click(object sender, RoutedEventArgs e) =>      ClickHandler(tBoxF);
        private void chanal_F_plus_Click(object sender, RoutedEventArgs e) =>       ClickHandler(tBoxF);

        private void chanal_TIME_minus_Click(object sender, RoutedEventArgs e) => ScrollFunction('t', -degreeChangeValue);
        private void chanal_TIME_plus_Click(object sender, RoutedEventArgs e) => ScrollFunction('t', degreeChangeValue);
        private void chanal_TIME_minus_GotFocus(object sender, RoutedEventArgs e) => ClickHandler(tBoxTime);
        private void chanal_TIME_plus_GotFocus(object sender, RoutedEventArgs e) => ClickHandler(tBoxTime);


        private void chanal_A_minus_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void Saveutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CommonPoint.Time < 249) throw new Exception("Задержка меньше 250 мс.\n");
                PointList.Add( Point.equivalent(CommonPoint));

            }
            catch (FormatException fe)
            {
                CommonConsoleWrite(fe.Message);
            }
            catch (Exception sve)
            {
                CommonConsoleWrite(sve.Message);
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            if (serialPort.IsOpen)
            {
                Point homePoint = new Point(0, 45, 87, 0, 240, 0,500);
                CommonPoint = homePoint;
                serialPort.Write(homePoint);
                EditorUpdate();
            }

        }
        
        private void GripButton_Click(object sender, RoutedEventArgs e)
        {
            if (CommonPoint.CanGrab == programConfig.MinGripValue)
            {
                CommonPoint['g'] = (float)programConfig.MaxGripValue;
                GripButton.Content = "Захват";
            }
            else
            {
                CommonPoint['g'] = (float)programConfig.MinGripValue;
                GripButton.Content = "Разжать";

            }
            serialPort.Write(CommonPoint);
            EditorUpdate();
        }
    }
}
