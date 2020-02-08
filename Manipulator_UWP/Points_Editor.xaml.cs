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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Manipulator_UWP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Points_Editor : Page
    {
        ManipulatorSerialPort serialPort;

        public Points_Editor()
        {
            this.InitializeComponent();
            serialPort = ManipulatorSerialPort.Instance;

        }

        private void chanal_A_minus_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void chanal_A_plus_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
