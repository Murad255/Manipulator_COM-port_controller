using KinematicModeling;
using ManipulatorSerialInterfase;
using PointSpase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private static System.Timers.Timer loopTimer;
        private bool clicFlag;

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
            DecSystemList.Add("Осевое перемещение");
            DecSystemList.Add("Декартово перемещение");
            foreach (string DecSystem in DecSystemList)
            {
                CBoxCoodSystem.Items.Add(DecSystem);
            }
            CBoxCoodSystem.SelectedIndex = (int)CoordSystem;
            control = new PointControl();
            control.SetPointsEditor(this);
            SetPTP_Checked(null, null);
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
            if (CBoxCoodSystem.SelectedIndex == (int)CoordSystems.PointSystem)
            {
                control = new PointControl();
                control.SetPointsEditor(this);
                tBoxA.Text = Math.Round(CommonPoint.CanA,3).ToString();
                tBoxB.Text = Math.Round(CommonPoint.CanB,3).ToString();
                tBoxC.Text = Math.Round(CommonPoint.CanC,3).ToString();
                tBoxD.Text = Math.Round(CommonPoint.CanD,3).ToString();
                tBoxE.Text = Math.Round(CommonPoint.CanE,3).ToString();
                tBoxF.Text = Math.Round(CommonPoint.CanF,3).ToString();
                tBoxTime.Text = CommonPoint.Time.ToString();

                pbCanal_A.Value = CommonPoint.CanA;
                pbCanal_B.Value = CommonPoint.CanB;
                pbCanal_C.Value = CommonPoint.CanC;
                pbCanal_D.Value = CommonPoint.CanD;
                pbCanal_E.Value = CommonPoint.CanE;
                pbCanal_F.Value = CommonPoint.CanF;
            }
            else if (CBoxCoodSystem.SelectedIndex == (int)CoordSystems.DecSystem)
            {
                control = new DecControl();
                control.SetPointsEditor(this);
                tBoxA.Text = Math.Round(CommonDec.DecX,3).ToString();
                tBoxB.Text = Math.Round(CommonDec.DecY, 3).ToString();
                tBoxC.Text = Math.Round(CommonDec.DecZ, 3).ToString();
                tBoxD.Text = Math.Round(CommonDec.AnglA, 3).ToString();
                tBoxE.Text = Math.Round(CommonDec.AnglB, 3).ToString();
                tBoxF.Text = Math.Round(CommonDec.AnglC, 3).ToString();
                tBoxTime.Text = CommonDec.Time.ToString();

                // CommonPoint = TaskDecision.DecToPoint(CommonDec);
                pbCanal_A.Value = CommonPoint.CanA;
                pbCanal_B.Value = CommonPoint.CanB;
                pbCanal_C.Value = CommonPoint.CanC;
                pbCanal_D.Value = CommonPoint.CanD;
                pbCanal_E.Value = CommonPoint.CanE;
                pbCanal_F.Value = CommonPoint.CanF;
                Dec dec = KinematicModeling.TaskDecision.PointToDec(CommonPoint);

                Debug.WriteLine($"A: {Math.Round(CommonPoint.CanA, 1)}\tB: {Math.Round(CommonPoint.CanB, 1)}\tC: {Math.Round(CommonPoint.CanC, 1)}\t" +
                                    $"D: {Math.Round(CommonPoint.CanD, 1)}\tE: {Math.Round(CommonPoint.CanE, 1)}\tF: {Math.Round(CommonPoint.CanF, 1)}\t\t" +
                                    $"X: {Math.Round(dec.DecX, 1)}\tY: {Math.Round(dec.DecY, 1)}\tZ: {Math.Round(dec.DecZ, 1)}\t" +
                                   $"A(Z): {Math.Round(dec.AnglA, 1)}\tB(Y): {Math.Round(dec.AnglB, 1)}\tC(X): {Math.Round(dec.AnglC, 1)}");

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
                    CommonPoint = TaskDecision.DecToPoint(CommonDec);
                    PointsEditor.EditorUpdate();
                    PointsEditor.serialPort.Write(CommonPoint);
                    await Task.Run(async () =>
                    {
                        if (PointsEditor.clicFlag) Thread.Sleep(300);
                        while (PointsEditor.clicFlag)
                        {
                            try
                            {
                                CommonDec[ch] += addValue;
                                CommonPoint = TaskDecision.DecToPoint(CommonDec);
                                PointsEditor.serialPort.Write(CommonPoint);

                                await PointsEditor.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                    PointsEditor.EditorUpdate());
                                Thread.Sleep(50);
                            }
                            catch (MaxValueException e) { CommonConsoleWrite(e.Message, Colors.Red); CommonDec[ch] -= addValue; }
                            catch (MinValueException e) { CommonConsoleWrite(e.Message, Colors.Red); CommonDec[ch] -= addValue; }
                            catch (Exception e) { CommonConsoleWrite("DecControl::ScrollFunction:\t" + e.Message, Colors.Red); }
                        }
                    });

                }
                catch (MaxValueException e) { CommonConsoleWrite(e.Message, Colors.Red); CommonDec[ch] -= addValue; }
                catch (MinValueException e) { CommonConsoleWrite(e.Message, Colors.Red); CommonDec[ch] -= addValue; }
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
                CommonConsoleWrite(fe.Message, Colors.Red);
            }
            catch (Exception sve)
            {
                CommonConsoleWrite(sve.Message,Colors.Red);
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            if (serialPort.IsOpen)
            {
                CBoxCoodSystem.SelectedIndex = (int)CoordSystems.PointSystem;
                CoordSystem = (int)CoordSystems.PointSystem;
                SetPTP_Checked(null,null);
                Point homePoint = new Point(0, 135, 0, 0, 60, 0, programConfig.MaxGripValue, 500);
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

        private void CBoxCoodSystem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (control == null) return;
            CoordSystem = (CoordSystems)CBoxCoodSystem.SelectedIndex;
            if (CBoxCoodSystem.SelectedIndex == (int)CoordSystems.PointSystem)
            {
                tBlock1.Text = "Канал А";
                tBlock2.Text = "Канал B";
                tBlock3.Text = "Канал C";
                tBlock4.Text = "Канал D";
                tBlock5.Text = "Канал E";
                tBlock6.Text = "Канал F";

                if (CommonDec != null)
                    CommonPoint = TaskDecision.DecToPoint(CommonDec);
                SetPTP_Checked(null,null);
                SetLIN.IsEnabled = false;
                EditorUpdate();
            }
            else if (CBoxCoodSystem.SelectedIndex == (int)CoordSystems.DecSystem)
            {
                tBlock1.Text = "Ось X";
                tBlock2.Text = "Ось Y";
                tBlock3.Text = "Ось Z";
                tBlock4.Text = "Угол A";
                tBlock5.Text = "Угол B";
                tBlock6.Text = "Угол C";
                SetLIN_Checked(null, null);
                SetLIN.IsEnabled = true;
                CommonDec = TaskDecision.PointToDec(CommonPoint);
                EditorUpdate();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CBoxCoodSystem.SelectedIndex= (int)CoordSystem;
        }

        private void SetPTP_Checked(object sender, RoutedEventArgs e)
        {
            CommonPoint.MovementType = MovementTypes.РТР;
            CommonDec.MovementType   = MovementTypes.РТР;
            SetPTP.IsChecked = true;
            if (SetLIN.IsChecked == true) SetLIN.IsChecked = false;
        }

        private void SetLIN_Checked(object sender, RoutedEventArgs e)
        {
            CommonPoint.MovementType = MovementTypes.LIN;
            CommonDec.MovementType   = MovementTypes.LIN;
            SetLIN.IsChecked = true;
            if (SetPTP.IsChecked == true) SetPTP.IsChecked = false;
        }
    }
}
