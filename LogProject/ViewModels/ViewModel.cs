using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Xamarin.Essentials;

namespace LogProject
{
    public class ViewModel : ViewModelBase
    {
        private readonly string eventTrackerMessage = "User click to generate crash!";
        private readonly ILogService logService;
        private readonly ICloudLogService cloudLogService;

        public ICommand GenerateLog { get; set; }

        public ViewModel(ILogService logService, ICloudLogService cloudLogService)
        {
            this.logService = logService;
            this.cloudLogService = cloudLogService;
            this.GenerateLog = new RelayCommand(GenerateException);
        }

        /// <summary>
        /// Demo exceptions.
        /// </summary>
        private async void GenerateException()
        {
            try
            {
                this.cloudLogService.LogEvent(eventTrackerMessage);
                // Simulate a crash
                throw new Exception();
            }
            catch (Exception ex)
            {
                this.logService.LogInfo("This is not an exception for...");
                this.logService.LogError(ex.ToString());
                this.cloudLogService.LogError(ex);

                System.Diagnostics.Debug.WriteLine($"App folder path :{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}");
                
                var file = Directory.GetFiles(FileSystem.AppDataDirectory, "*.txt").LastOrDefault();                if (!string.IsNullOrEmpty(file))                {                    var logList = new ObservableCollection<string>(File.ReadAllLines(file));                }
            }
        }
    }
}
