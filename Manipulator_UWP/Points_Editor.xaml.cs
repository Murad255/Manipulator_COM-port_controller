using KinematicTask;
using ManipulatorSerialInterfase;
using PointSpase;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Core;
//using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static Manipulator_UWP.CommonFunction;


namespace Manipulator_UWP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Points_Editor : Page
    {
        private ManipulatorSerialPort serialPort;
        private ProgramConfig programConfig = ProgramConfig.Instance;
        private static  System.Timers.Timer loopTimer;
        private bool    clicFlag;

        public Points_Editor()
        {
            InitializeComponent();
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

            var DecSystemList = new List<string>();
            if (CommonPoint.CanA > 0) { }
            DecSystemList.Add("Обобщенные координаты");
            DecSystemList.Add("Декардовые координаты");
            foreach (string DecSystem in DecSystemList)
            {
                CBoxCoodSystem.Items.Add(DecSystem);
            }
            CBoxCoodSystem.SelectedIndex = (int)CoordSystem.PointSystem;
            control = new PointControl();
            control.SetPointsEditor(this);
            EditorUpdate();

            loopTimer = new System.Timers.Timer();
            loopTimer.Interval = 500;
            loopTimer.Enabled = false;
            loopTimer.AutoReset = true;

        }
        /// <summary>
        /// Обновляет значения окна в соответствии со значениями CommonPoint
        /// </summary>
        private void EditorUpdate()
        {
            if (CBoxCoodSystem.SelectedIndex == (int)CoordSystem.PointSystem)
            {
                control = new PointControl();
                control.SetPointsEditor(this);
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
            else if (CBoxCoodSystem.SelectedIndex == (int)CoordSystem.DecSystem)
            {
                control = new DecControl();
                control.SetPointsEditor(this);
                tBoxA.Text = CommonDec.DecX.ToString();
                tBoxB.Text = CommonDec.DecY.ToString();
                tBoxC.Text = CommonDec.DecZ.ToString();
                tBoxD.Text = CommonDec.AnglA.ToString();
                tBoxE.Text = CommonDec.AnglB.ToString();
                tBoxF.Text = CommonDec.AnglC.ToString();
                tBoxTime.Text = CommonDec.Time.ToString();

               // CommonPoint = TaskDecision.DecToPoint(CommonDec);
                pbCanal_A.Value = CommonPoint.CanA;
                pbCanal_B.Value = CommonPoint.CanB;
                pbCanal_C.Value = CommonPoint.CanC;
                pbCanal_D.Value = CommonPoint.CanD;
                pbCanal_E.Value = CommonPoint.CanE;
                pbCanal_F.Value = CommonPoint.CanF;

                CommonConsoleWrite( $"A: {Math.Round(CommonPoint.CanA, 1)}\tB: {Math.Round(CommonPoint.CanB, 1)}\tC: {Math.Round(CommonPoint.CanC, 1)}\t" +
                                    $"D: {Math.Round(CommonPoint.CanD, 1)}\tE: {Math.Round(CommonPoint.CanE, 1)}\tF: {Math.Round(CommonPoint.CanF, 1)}");
            }
        }

        /// <summary>
        /// Переводит фокус на textBox
        /// </summary>
        /// <param name="textBox"></param>
        private void ClickHandler(TextBox textBox)
        {
            clicFlag = false;
            textBox.Focus(FocusState.Programmatic);
        }

        /// <summary>
        /// обработка нажатий кнопок для управления в обобщённых и декардовых координатах
        /// </summary>
        private interface IControl
        {
            void ScrollFunction(char ch, float addValue);

            /// <summary>
            /// нужно для доступа к функциям родительского класса
            /// </summary>
            /// <param name="points_Editor"></param>
            void SetPointsEditor(Points_Editor points_Editor);
        }

        private class PointControl : IControl
        {

            private Points_Editor PointsEditor;

            public void SetPointsEditor(Points_Editor points_Editor)
            {
                PointsEditor = points_Editor;
            }
            public async void ScrollFunction(char ch, float addValue)
            {
                PointsEditor.clicFlag = true;
                try
                {
                    //serialWrite(ch + trackBar.Value.ToString() + 'z');
                    if (!(PointsEditor.serialPort.IsOpen && PointsEditor.serialPort.CtsHolding))
                        throw new Exception("COM порт закрыт");
                    CommonPoint[ch] = CommonPoint[ch] + addValue;
                    PointsEditor.EditorUpdate();
                    PointsEditor.serialPort.Write(CommonPoint);
                    await Task.Run(async () =>
                    {
                        if (PointsEditor.clicFlag) Thread.Sleep(300);
                        while (PointsEditor.clicFlag)
                        {

                            await PointsEditor.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                            {
                                try
                                {
                                    CommonPoint[ch] += addValue;
                                    PointsEditor.serialPort.Write(CommonPoint);
                                    PointsEditor.EditorUpdate();
                                }
                                catch (MaxValueException e) { CommonConsoleWrite(e.Message, Colors.Red); CommonPoint[ch] -= addValue; }
                                catch (MinValueException e) { CommonConsoleWrite(e.Message, Colors.Red); CommonPoint[ch] -= addValue; }
                                catch (Exception e) { CommonConsoleWrite("PointControl::ScrollFunction:\t" + e.Message, Colors.Red); }
                            });
                            Thread.Sleep(50);
                        }

                    });

                }
                catch (MaxValueException e) { CommonConsoleWrite(e.Message, Colors.Red); CommonPoint[ch] -= addValue; }
                catch (MinValueException e) { CommonConsoleWrite(e.Message, Colors.Red); CommonPoint[ch] -= addValue; }
                catch (Exception e) { CommonConsoleWrite("PointControl::ScrollFunction:\t" + e.Message, Colors.Red); }

            }

        }

        private class DecControl : IControl
        {
            private Points_Editor PointsEditor;
            public void SetPointsEditor(Points_Editor points_Editor)
            {
                PointsEditor = points_Editor;
            }

            public async void ScrollFunction(char ch, float addValue)
            {
                PointsEditor.clicFlag = true;
                try
                {
                    CommonDec[ch] += addValue;
                    CommonPoint = KinematicTask.TaskDecision.DecToPoint(CommonDec);
                    PointsEditor.EditorUpdate();
                    PointsEditor.serialPort.Write(CommonPoint);
                    await Task.Run(async () =>
                    {


                        if (PointsEditor.clicFlag) Thread.Sleep(300);
                        while (PointsEditor.clicFlag)
                        {

                            await PointsEditor.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                            {
                                try
                                {
                                    CommonDec[ch] += addValue;
                                    CommonPoint = KinematicTask.TaskDecision.DecToPoint(CommonDec);
                                    PointsEditor.serialPort.Write(CommonPoint);
                                    PointsEditor.EditorUpdate();
                                }
                                catch (MaxValueException e) { CommonConsoleWrite( e.Message, Colors.Red); CommonDec[ch] -= addValue; }
                                catch (MinValueException e) { CommonConsoleWrite( e.Message, Colors.Red); CommonDec[ch] -= addValue; }
                                catch (Exception e) { CommonConsoleWrite("DecControl::ScrollFunction:\t" + e.Message, Colors.Red); }
                            });
                            Thread.Sleep(50);
                        }

                    });

                }
                catch (MaxValueException e) { CommonConsoleWrite( e.Message, Colors.Red); CommonDec[ch] -= addValue; }
                catch (MinValueException e) { CommonConsoleWrite( e.Message, Colors.Red); CommonDec[ch] -= addValue; }
                catch (Exception e) { CommonConsoleWrite("DecControl::ScrollFunction:\t" + e.Message, Colors.Red); }

            }
        }

        IControl control;

        private void ChangeChanalValueClick(object sender, RoutedEventArgs e) => ClickHandler(tBoxA);
        private void ChangeChanalValuePointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e) => ClickHandler(tBoxA);

        private void chanal_A_minus_GotFocus(object sender, RoutedEventArgs e) => control.ScrollFunction('a', -programConfig.StepChangevalue);
        private void chanal_A_plus_GotFocus(object sender, RoutedEventArgs e) => control.ScrollFunction('a', programConfig.StepChangevalue);

        #region настройки остальных кнопок 
        private void chanal_B_minus_GotFocus(object sender, RoutedEventArgs e) => control.ScrollFunction('b', -programConfig.StepChangevalue);
        private void chanal_B_plus_GotFocus(object sender, RoutedEventArgs e) => control.ScrollFunction('b', programConfig.StepChangevalue);

        private void chanal_C_minus_GotFocus(object sender, RoutedEventArgs e) => control.ScrollFunction('c', -programConfig.StepChangevalue);
        private void chanal_C_plus_GotFocus(object sender, RoutedEventArgs e) => control.ScrollFunction('c', programConfig.StepChangevalue);

        private void chanal_D_minus_GotFocus(object sender, RoutedEventArgs e) => control.ScrollFunction('d', -programConfig.StepChangevalue);
        private void chanal_D_plus_GotFocus(object sender, RoutedEventArgs e) => control.ScrollFunction('d', programConfig.StepChangevalue);

        private void chanal_E_minus_GotFocus(object sender, RoutedEventArgs e) => control.ScrollFunction('e', -programConfig.StepChangevalue);
        private void chanal_E_plus_GotFocus(object sender, RoutedEventArgs e) => control.ScrollFunction('e', programConfig.StepChangevalue);
  
        private void chanal_F_minus_GotFocus(object sender, RoutedEventArgs e) => control.ScrollFunction('f', -programConfig.StepChangevalue);
        private void chanal_F_plus_GotFocus(object sender, RoutedEventArgs e) => control.ScrollFunction('f', programConfig.StepChangevalue);

        private void chanal_TIME_minus_GotFocus(object sender, RoutedEventArgs e) => control.ScrollFunction('t', -programConfig.StepChangevalue * 20);
        private void chanal_TIME_plus_GotFocus(object sender, RoutedEventArgs e) => control.ScrollFunction('t', programConfig.StepChangevalue * 20);
        

        #endregion
        private void Saveutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CommonPoint.Time < 249) throw new Exception("Задержка меньше 250 мс.\n");
                PointList.Add(Point.equivalent(CommonPoint));

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
                Point homePoint  = new Point(0, 135, 0, 0, 60, 0, programConfig.MaxGripValue, 500);
                CommonPoint = homePoint;
                serialPort.Write(homePoint);
                EditorUpdate();
            }

        }

        private void GripButton_Click(object sender, RoutedEventArgs e)
        {
            if (CommonPoint.CanGrab == programConfig.MinGripValue)
            {
                CommonPoint['g'] = programConfig.MaxGripValue;
                GripButton.Content = "Захват";
            }
            else
            {
                CommonPoint['g'] = programConfig.MinGripValue;
                GripButton.Content = "Разжать";

            }
            serialPort.Write(CommonPoint);
            EditorUpdate();
        }

        enum CoordSystem { PointSystem=0, DecSystem=1 }
        private void CBoxCoodSystem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (control == null) return;

            if (CBoxCoodSystem.SelectedIndex == (int)CoordSystem.PointSystem)
            {
                //pbCanal_A.Minimum = Point.MinPoint.CanA;
                //pbCanal_A.Maximum = Point.MaxPoint.CanA;
                //pbCanal_B.Minimum = Point.MinPoint.CanB;
                //pbCanal_B.Maximum = Point.MaxPoint.CanB;
                //pbCanal_C.Minimum = Point.MinPoint.CanC;
                //pbCanal_C.Maximum = Point.MaxPoint.CanC;
                //pbCanal_D.Minimum = Point.MinPoint.CanD;
                //pbCanal_D.Maximum = Point.MaxPoint.CanD;
                //pbCanal_E.Minimum = Point.MinPoint.CanE;
                //pbCanal_E.Maximum = Point.MaxPoint.CanE;
                //pbCanal_F.Minimum = Point.MinPoint.CanF;
                //pbCanal_F.Maximum = Point.MaxPoint.CanF;

                tBlock1.Text = "Канал А";
                tBlock2.Text = "Канал B";
                tBlock3.Text = "Канал C";
                tBlock4.Text = "Канал D";
                tBlock5.Text = "Канал E";
                tBlock6.Text = "Канал F";

                if(CommonDec!=null)
                    CommonPoint = KinematicTask.TaskDecision.DecToPoint(CommonDec);

                EditorUpdate();
            }
            else if (CBoxCoodSystem.SelectedIndex == (int)CoordSystem.DecSystem)
            {
                //pbCanal_A.Minimum = Dec.MinDec.DecX;
                //pbCanal_A.Maximum = Dec.MaxDec.DecX;
                //pbCanal_B.Minimum = Dec.MinDec.DecY;
                //pbCanal_B.Maximum = Dec.MaxDec.DecY;
                //pbCanal_C.Minimum = Dec.MinDec.DecZ;
                //pbCanal_C.Maximum = Dec.MaxDec.DecZ;
                //pbCanal_D.Minimum = Dec.MinDec.AnglA;
                //pbCanal_D.Maximum = Dec.MaxDec.AnglA;
                //pbCanal_E.Minimum = Dec.MinDec.AnglB;
                //pbCanal_E.Maximum = Dec.MaxDec.AnglB;
                //pbCanal_F.Minimum = Dec.MinDec.AnglC;
                //pbCanal_F.Maximum = Dec.MaxDec.AnglC;

                tBlock1.Text = "Ось X";
                tBlock2.Text = "Ось Y";
                tBlock3.Text = "Ось Z";
                tBlock4.Text = "Угол A";
                tBlock5.Text = "Угол B";
                tBlock6.Text = "Угол C";

                CommonDec = KinematicTask.TaskDecision.PointToDec(CommonPoint);
                EditorUpdate();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CBoxCoodSystem.SelectedItem = 0;
        }






    }
}
