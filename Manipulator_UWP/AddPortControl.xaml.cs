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

// Документацию по шаблону элемента "Пользовательский элемент управления" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234236

namespace Manipulator_UWP
{
    public sealed partial class AddPortControl : UserControl
    {
     /// <summary>
     /// флаг статуса ввода номера. Равен true, если номер введён
     /// </summary>
        public bool AddFlag
        {
            set; get;
        }
        /// <summary>
        /// имя введённого порта
        /// </summary>
        public string PortName 
        {
            get; set;
        }
        public AddPortControl()
        {
            this.InitializeComponent();
            AddFlag = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddFlag = true;
            PortName ="COM"+ NamePortTextBox.Text.ToString();
        }
    }
}
