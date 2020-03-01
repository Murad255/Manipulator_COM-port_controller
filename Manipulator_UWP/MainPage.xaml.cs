using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.SerialCommunication;
using Windows.Devices.Enumeration;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using System.Windows.Input;

using System.IO.Ports;
using System.Threading;
using ManipulatorSerialInterfase;
using Windows.UI.Core;
using Windows.Devices.Bluetooth;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;
using Windows.Security.Credentials.UI;
using Windows.Security.Credentials;
using static Manipulator_UWP.CommonFunction;
using PointSpase;
using KinematicModeling;
// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Manipulator_UWP
{

    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
       // string deviceId;
        List<string> portNamesList ;
        ManipulatorSerialPort serialPort;
        private System.ComponentModel.IContainer components = null;
        Task send;                      //поток для приняти данных
        //Task execution;                 //поток для отправки коллекции точек


        public MainPage()
        {
            try {
                this.InitializeComponent();
                ConsoleWrite("Hello word ");

                /////////////Serial port initialize////////////
                serialPort = ManipulatorSerialPort.Instance;
                this.components = new System.ComponentModel.Container();
                SerialPort sr = this.serialPort;
                sr = new System.IO.Ports.SerialPort(this.components);

                this.serialPort.BaudRate = 115200;
               // this.serialPort.WriteTimeout = 50;
                this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);

                CommonFunction.SetsendMessage(ConsoleWrite);    //для доступа к консоли другим Page
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

        string RX_Message="";
        int RX_countSumbol = 0;
        private  void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                while (0 != sp.BytesToRead) serialPort.RX_Data.Enqueue((char)sp.ReadByte());

                if (send.IsCompleted)
                    send = Task.Run(new Action(() => {
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
                        catch (Exception te) { ConsoleWrite(te.ToString(),Colors.Red); }

                    }));
            }
            catch (Exception ex)
            {
                ConsoleWrite(ex.ToString(), Colors.Red);
            }
            
        }

        private long  lineCount= 0;
        public async void ConsoleWrite(string message, Color colors)
        {
            
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                lineCount++;
                TextBlock printTextBlock = new TextBlock();
                printTextBlock.Foreground = new SolidColorBrush(colors);
                printTextBlock.Text = lineCount.ToString() + "|\t" + message;
                printTextBlock.FontSize = 20;
                Console.Children.Insert(0, printTextBlock);
            });

            //var transform = element.TransformToVisual((UIElement)scrollViewer.Content);
            //var position = transform.TransformPoint(new Point(0, 0));
            //ConsoleScrollPanel.ChangeView(null, abosulatePosition.Y, null, true);
        }

        private async void ConsoleWrite(string message)
        {
            
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
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
               if(debug) ConsoleWrite(e.ToString(), Colors.Red);
            }
            finally
            {
                if (portNamesList != null)
                {
                    comboSelectPort.Items.Clear();
                    foreach (string portName in portNamesList)  //заполняем список портов
                    {
                        comboSelectPort.Items.Add(portName);
                    }
                }
                comboSelectPort.Items.Add("COM5");
                comboSelectPort.Items.Add("Добавить другой");
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

        bool HamburgerMenuFlag = false;
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


        private async void ConectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    ConectButton.IsEnabled = false;
                    serialPort.PortName = ((string)comboSelectPort.SelectedItem);

                    await Task.Run(() =>
                    {
                        try
                        {
                            //  serialPort.BaudRate = programConfig.Speed;
                            serialPort.Open();
                            Point homePoint = new Point(0, 45, 87, 0, 240, 0, 500);
                            CommonPoint = homePoint;
                            serialPort.Write(homePoint);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            ConsoleWrite("COM порт занят.", Colors.Red);
                        }
                        catch (Exception soe)
                        {
                            ConsoleWrite(soe.ToString(), Colors.Red);
                        }
                        finally
                        {
                            if (serialPort.IsOpen)
                            {
                                ConsoleWrite("подключено к " + serialPort.PortName, Colors.Green);
                            }
                        }
                    });

                    send = new Task(() => { });
                    send.Start();
                    Execution.Start();
                }
                else
                {
                    Point homePoint = new Point(0, 0, 8, 0, 245, 0);
                    CommonPoint = homePoint;
                    serialPort.Write(homePoint);

                    serialPort.Close();
                    ConsoleWrite("Отключено от " + serialPort.PortName, Colors.Green);

                }
            }
            catch (IOException)
            {
                ConsoleWrite("Повторное подключение");
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
            catch (Exception ce)
            {
                ConsoleWrite(ce.ToString(), Colors.Red);
            }
            finally 
            {
                ConectButton.IsEnabled = true;
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
                ConsoleWrite($"{AddWindow.PortName} добавлен", Colors.Green);

            }
        }
    }
}
