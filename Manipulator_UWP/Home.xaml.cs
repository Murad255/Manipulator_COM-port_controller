using ManipulatorSerialInterfase;
using PointSpase;
using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static Manipulator_UWP.CommonFunction;
// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Manipulator_UWP
{


    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Home : Page
    {
        ManipulatorSerialPort serialPort;
        StorageFile pointListFile;
        FileSavePicker savePicker;
        FileOpenPicker openPicker;

        public Home()
        {
            this.InitializeComponent();
            serialPort = ManipulatorSerialPort.Instance;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PointListView.Text = "";
            foreach (Point p in PointList) PointListView.Text += p.ToString() + "\n"; //выводит список точекPointListView

            if (StartExecution_status) ExecutionButton.Content = "\uE769";
            else ExecutionButton.Content = "\uE768";
        }

        private void ExecutionButton_Click(object sender, RoutedEventArgs e)
        {

            CommonFunction.ExecutionProcess(ExecutionButtonChange, true);
        }

        async void ExecutionButtonChange()
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (StartExecution_status) ExecutionButton.Content = "\uE769";
                else ExecutionButton.Content = "\uE768";
            }
            );
        }

        private void cycleStatusCheck_Unchecked(object sender, RoutedEventArgs e) { CycleStatus = false; }
        private void cycleStatusCheck_Checked(object sender, RoutedEventArgs e) { CycleStatus = true; }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (pointListFile == null) 
            {
                savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                savePicker.FileTypeChoices.Add("Manipulator Points", new List<string>() { ".man" });
                savePicker.SuggestedFileName = "New Points";
                savePicker.CommitButtonText = "Сохранить";

                pointListFile = await savePicker.PickSaveFileAsync();
                filePozition.Text = pointListFile.Path;
            }

            if (pointListFile != null) await FileIO.WriteTextAsync(pointListFile, PointList.GetJsonConvertString());
            else CommonConsoleWrite("Не удалось сохранить файл", Colors.Red);
        }

        private async void OpenDirectorry_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.CommitButtonText = "Открыть";
            openPicker.FileTypeFilter.Add(".man");
            pointListFile = await openPicker.PickSingleFileAsync();
            filePozition.Text = pointListFile.Path;
        }

        private async void LoadListButton_Click(object sender, RoutedEventArgs e)
        {

            if (pointListFile != null)
            {
                PointList.Clear();
                PointList.SetJsonConvertPoint(await FileIO.ReadTextAsync(pointListFile));
                PointListView.Text = "";
                foreach (Point p in PointList) PointListView.Text += p.ToString()+"\n"; //выводит список точек
            }
        }
    }
}
