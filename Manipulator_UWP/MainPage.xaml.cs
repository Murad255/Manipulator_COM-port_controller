using KinematicModeling;
using ManipulatorSerialInterfase;
using PointSpase;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using static Manipulator_UWP.CommonFunction;
// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Manipulator_UWP
{

    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // string deviceId;
        private List<string> portNamesList;
        private ManipulatorSerialPort serialPort;
        private ProgramConfig programConfig;

        private System.ComponentModel.IContainer components = null;
        private Task send;                      //поток для приняти данных
                                                //Task execution;                 //поток для отправки коллекции точек


        public MainPage()
        {
            try
            {
                InitializeComponent();
                ConsoleWrite("Hello word ");
                programConfig = ProgramConfig.Instance;

                //Serial port initialize
                serialPort = ManipulatorSerialPort.Instance;
                components = new System.ComponentModel.Container();
                SerialPort sr = serialPort;
                sr = new System.IO.Ports.SerialPort(components);
                serialPort.BaudRate = 115200;
                CommonFunction.SendMessage = ConsoleWrite;    //для доступа к консоли другим Page

                // this.serialPort.WriteTimeout = 50;
                serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPort_DataReceived);

                comboSelectPort.Items.Clear();
                GetPortNames();                                 //загрузить список портов в comboSelectPort

                // по умолчанию открываем страницу home.xaml
                myFrame.Navigate(typeof(Home));
                TitleTextBlock.Text = "Главная";

                //passing
                Passing.ContextStrategy = new SinPassingStrategy();
                Passing.SentPointFunction = serialPort.Write;

            }
            catch (Exception e)
            {
                ConsoleWrite(e.ToString(), Colors.Red);
            }
        }

        private string RX_Message = "";
        private int RX_countSumbol = 0;
        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                while (0 != sp.BytesToRead) serialPort.RX_Data.Enqueue((char)sp.ReadByte());

                if (send.IsCompleted)
                    send = Task.Run(new Action(() =>
                    {
                        try
                        {
                            Thread.Sleep(50);     //ожидаем завершения передачи
                            foreach (char c in serialPort.RX_Data)
                            {
                                if ((c != '\n') && RX_countSumbol <= 20)
                                {
                                    RX_Message += c.ToString();
                                    RX_countSumbol++;
                                }

                                else
                                {
                                    ConsoleWrite(RX_Message, Colors.Blue);
                                    RX_Message = "";
                                    RX_countSumbol = 0;
                                }
                            }
                            serialPort.RX_Data = new Queue<char>();
                        }
                        catch (Exception te) { ConsoleWrite(te.ToString(), Colors.Red); }

                    }));
            }
            catch (Exception ex)
            {
                ConsoleWrite(ex.ToString(), Colors.Red);
            }

        }

        private long lineCount = 0;
        public async void ConsoleWrite(string message, Color colors)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                lineCount++;
                TextBlock printTextBlock = new TextBlock();
                printTextBlock.Foreground = new SolidColorBrush(colors);
                printTextBlock.Text = lineCount.ToString() + "|\t" + message;
                printTextBlock.FontSize = 20;
                Console.Children.Insert(0, printTextBlock);
            });
        }

        private async void ConsoleWrite(string message)
        {

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                lineCount++;
                TextBlock printTextBlock = new TextBlock();
                printTextBlock.Text = lineCount.ToString() + "|\t" + message;
                printTextBlock.FontSize = 20;
                Console.Children.Insert(0, printTextBlock); //Add(printTextBlock);

                var transform = Console.TransformToVisual((UIElement)ConsoleScrollPanel.Content);
                var position = transform.TransformPoint(new Windows.Foundation.Point(0, 0));
                ConsoleScrollPanel.ChangeView(null, position.Y, null, true);
            });

        }


        private async void GetPortNames(bool debug = true)
        {
            try
            {
                string aqs = SerialDevice.GetDeviceSelector();
                var deviceCollection = await DeviceInformation.FindAllAsync(aqs);
                portNamesList = new List<string>();
                foreach (var item in deviceCollection)
                {
                    var serialDevice = await SerialDevice.FromIdAsync(item.Id);
                    var portName = serialDevice.PortName;
                    portNamesList.Add(portName);
                }
            }
            catch (Exception e)
            {
                if (debug) ConsoleWrite(e.ToString(), Colors.Red);
            }
            finally
            {
                int portCount = 1;
                if (portNamesList != null)
                {
                    comboSelectPort.Items.Clear();
                    foreach (string portName in portNamesList)  //заполняем список портов
                    {
                        comboSelectPort.Items.Add(portName);
                        portCount++;
                    }
                }
                comboSelectPort.Items.Add("COM5");
                if (programConfig.PortName.Count > 0)
                    foreach (string portName in programConfig.PortName)
                    {
                        comboSelectPort.Items.Add(portName);
                        portCount++;
                    }
                comboSelectPort.Items.Add("Добавить другой");
                if (portCount > programConfig.PortNum) comboSelectPort.SelectedIndex = programConfig.PortNum;

            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (home.IsSelected)
            {
                myFrame.Navigate(typeof(Home));
                TitleTextBlock.Text = "Главная";
                HamburgerButton.Content = "\uE700";
                HamburgerMenuFlag = false;
                mySplitView.IsPaneOpen = !mySplitView.IsPaneOpen;

            }
            else if (share.IsSelected)
            {
                myFrame.Navigate(typeof(Points_Editor));
                TitleTextBlock.Text = "Редактор точек";
                HamburgerButton.Content = "\uE700";
                HamburgerMenuFlag = false;
                mySplitView.IsPaneOpen = !mySplitView.IsPaneOpen;

            }
            else if (settings.IsSelected)
            {
                myFrame.Navigate(typeof(Settings));
                TitleTextBlock.Text = "Настройки";
                HamburgerButton.Content = "\uE700";
                HamburgerMenuFlag = false;
                mySplitView.IsPaneOpen = !mySplitView.IsPaneOpen;

            }
        }

        private bool HamburgerMenuFlag = false;
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            if (HamburgerMenuFlag)
            {
                HamburgerButton.Content = "\uE700";
                HamburgerMenuFlag = false;
            }
            else
            {
                HamburgerButton.Content = "\uE72B";
                HamburgerMenuFlag = true;
            }
            mySplitView.IsPaneOpen = !mySplitView.IsPaneOpen;
        }

        int trying=0; //количество попыток подключения
        private async void ConectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ConectButton.IsEnabled = false;
                if (!(serialPort.IsOpen && serialPort.CtsHolding))
                {
                    ConectButton.Background = new SolidColorBrush(Windows.UI.Colors.Gray);
                    serialPort.PortName = ((string)comboSelectPort.SelectedItem);

                    await Task.Run(async () =>
                    {
                        try
                        {
                            //  serialPort.BaudRate = programConfig.Speed;
                            serialPort.Open();
                            Point homePoint = new Point(0, 90, 0, 0, 0, 0, programConfig.MaxGripValue, 500);
                            CommonPoint = homePoint;
                            serialPort.Write(homePoint);
                            trying = 0;
                            ConsoleWrite("подключено к " + serialPort.PortName, Colors.Green);
                            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                                () =>
                                {
                                    ConectButton.Background = new SolidColorBrush(Windows.UI.Colors.LightGreen);
                                });
                            }
                        catch (UnauthorizedAccessException)
                        {
                            ConsoleWrite("COM порт занят.", Colors.Red);
                        }
                        catch (ArgumentNullException)
                        {
                            ConsoleWrite("Не выбран COM-порт.\nПовторить поиск?", Colors.Red);
                        }
                        catch (IOException)
                        {
                            ++trying;
                            if (trying >= 10)
                            {
                                ConsoleWrite($"Не удалюсь подключиться к {serialPort.PortName}.", Colors.Red);
                                trying = 0;
                                return; 
                            }
                            ConsoleWrite("Повторное подключение",Colors.Orange);
                            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                             () =>                          {
                                 ConectButton_Click(new object(), new RoutedEventArgs());
                             });
                        }
                        catch (Exception soe)
                        {
                            ConsoleWrite(soe.ToString(), Colors.Red);
                        }
                    });

                    send = new Task(() => { });
                    send.Start();
                    // Execution.Start();
                }
                else
                {
                    Point homePoint = new Point(0, 180, -90, 0, 30, 0, programConfig.MaxGripValue, 500);
                    CommonPoint = homePoint;
                    serialPort.Write(homePoint);

                    serialPort.Close();
                    ConsoleWrite("Отключено от " + serialPort.PortName, Colors.Green);
                    ConectButton.Background = new SolidColorBrush(Windows.UI.Colors.Gray); 
                }
            }
            catch (IOException)
            {
                ++trying;
                if (trying >= 10)
                {
                    ConsoleWrite($"Не удалюсь подключиться к {serialPort.PortName}.", Colors.Red);
                    trying = 0;
                    return;
                }
                ConsoleWrite("Повторное подключение", Colors.Orange);
                ConectButton_Click(new object(), new RoutedEventArgs());
            }
            //catch (ArgumentNullException)
            //{
            //    DialogResult dialogResult = MessageBox.Show("Не выбран COM-порт.\nПовторить поиск?",
            //                                                "Ошибка!",
            //                                                MessageBoxButtons.OKCancel,
            //                                                MessageBoxIcon.Error);
            //    if (dialogResult == DialogResult.OK)
            //    {
            //        comboBox.Items.Clear();
            //        int portCount = 0;
            //        foreach (string portName in System.IO.Ports.SerialPort.GetPortNames())
            //        {
            //            comboBox.Items.Add(portName);
            //            portCount++;
            //        }
            //        if (portCount >= 2) comboBox.SelectedIndex = 2;
            //        comboHomeMode.SelectedIndex = 1;

            //        connectButton_Click_1(sender, e);
            //    }
            //}
            catch (InvalidOperationException)
            {
                serialPort.Close();
                ConectButton.Background = new SolidColorBrush(Windows.UI.Colors.Gray);
                ConectButton_Click(new object(), new RoutedEventArgs());
            }
            catch (Exception ce)
            {
                ConsoleWrite(ce.ToString(), Colors.Red);
            }
            finally
            {
               if(trying==0) ConectButton.IsEnabled = true;
            }
        }



        private async void comboSelectPort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((string)comboSelectPort.SelectedItem == "Добавить другой")
            {
                var AddWindow = new AddPortControl();
                Console.Children.Insert(0, AddWindow);
                await Task.Run(() =>
                {
                    while (AddWindow.AddFlag == false) { }
                });
                comboSelectPort.Items.Add(AddWindow.PortName);
                programConfig.AddPortName(AddWindow.PortName);
                ConsoleWrite($"{AddWindow.PortName} добавлен", Colors.Green);
            }
            else programConfig.PortNum = comboSelectPort.SelectedIndex;
        }
    }
}
