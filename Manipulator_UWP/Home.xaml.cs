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
using KinematicModeling;
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
        private ProgramConfig programConfig = ProgramConfig.Instance;
        StorageFile pointListFile;
        FileSavePicker savePicker;
        FileOpenPicker openPicker;

        public Home()
        {
            this.InitializeComponent();
            serialPort = ManipulatorSerialPort.Instance;
            if (programConfig.FilePozition != null)
            {
                pointListFile = programConfig.FilePozition;
                filePozition.Text = pointListFile.Path;
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PointListView.Text = "";
            foreach (Point p in PointList) PointListView.Text += p.ToString() + "\n"; //выводит список точекPointListView

            //Настройки кнопки запуска\останова передачи 
            if (StartExecution_status) ExecutionButton.Content = "\uE769";
            else ExecutionButton.Content = "\uE768";


        }

        private void ExecutionButton_Click(object sender, RoutedEventArgs e)
        {

            CommonFunction.ExecutionProcess3(ExecutionButtonChange);
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



        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (pointListFile == null)
                {
                    //вызываем окно для выбора папки
                    savePicker = new FileSavePicker();
                    savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                    savePicker.FileTypeChoices.Add("Manipulator Points", new List<string>() { ".man" });
                    savePicker.SuggestedFileName = "New Points";
                    savePicker.CommitButtonText = "Сохранить";

                    pointListFile = await savePicker.PickSaveFileAsync();
                    filePozition.Text = pointListFile.Path;
                    programConfig.FilePozition = pointListFile;
                }

                if (pointListFile != null) await FileIO.WriteTextAsync(pointListFile, PointList.GetJsonConvertString());
                else CommonConsoleWrite("Не удалось сохранить файл", Colors.Red);
            }
            catch(Exception ex)
            {
                CommonConsoleWrite(ex.Message);
            }
        }

        private async void OpenDirectorry_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.CommitButtonText = "Открыть";
            openPicker.FileTypeFilter.Add(".man");
            pointListFile = await openPicker.PickSingleFileAsync();
            if (pointListFile == null) return;
            filePozition.Text = pointListFile.Path;
            programConfig.FilePozition = pointListFile;
            LoadListButton_Click(sender,  e);
        }

        private async void LoadListButton_Click(object sender, RoutedEventArgs e)
        {

            if (pointListFile != null)
            {
                PointList.Clear();
                PointList.SetJsonConvertPoint(await FileIO.ReadTextAsync(pointListFile));
                Point.ChangeNumPoints(PointList.Count);
                PointListView.Text = "";
                foreach (Point p in PointList) PointListView.Text += p.ToString()+"\n"; //выводит список точек

                FullPointList.Clear();
                Passing.pastPoint = PointList[0];
                //проходимся по всем точкам и выролняем их отправку 
                foreach (Point point in PointList)
                {
                    if (point.MovementType == MovementTypes.РТР)
                    {
                        FullPointList.AddRange(Passing.PassingAlgoritm(Passing.pastPoint, point));
                        Passing.pastPoint = point;
                    }
                    else if (point.MovementType == MovementTypes.LIN)
                    {
                        Dec startDec = TaskDecision.PointToDec(Passing.pastPoint);
                        Dec endDec = TaskDecision.PointToDec(point);
                        FullPointList.AddRange(Passing.PassingAlgoritm(startDec, endDec));
                        Passing.pastPoint = point;
                    }
                }
            }
        }

        private void cycleStatusCheck_Toggled(object sender, RoutedEventArgs e)
        {
            if(cycleStatusCheck.IsOn)   CycleStatus = true; 
            else                        CycleStatus = false; 
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Point.ChangeNumPoints(0);
            PointList.Clear();
            FullPointList.Clear();
            PointListView.Text = "";
        }
    }
}
