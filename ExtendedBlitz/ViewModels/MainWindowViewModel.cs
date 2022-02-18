using ExtendedBlitz.Infrastructure.Commands;
using ExtendedBlitz.ViewModels.Base;
using ExtendedBlitz.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Threading;
using System.Net.Http;
using System.Windows;

namespace ExtendedBlitz.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Заголовок окна

        private string _title = "Extended Blitz";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        #endregion

        #region Status : string - Статус программы

        private string _status = "Undefined";

        /// <summary>Статус программы</summary>
        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        #endregion

        #region WindowState - Состояние окна

        private WindowState _curWindowState = WindowState.Normal;

        /// <summary> Свернуть окно программы </summary>
        public WindowState CurWindowState
        {
            get => _curWindowState;
            set => Set(ref _curWindowState, value);
        }

        #endregion

        #region Команды

        #region CloseApplicationCommand

        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecuted(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region MinimizedWindowCommand

        public ICommand MinimizedWindowCommand { get; }
        private bool CanMinimizedWindowCommandExecuted(object p) => true;
        private void OnMinimizedWindowCommandExecuted(object p)
        {
            CurWindowState = WindowState.Minimized;
        }

        #endregion

        #region MaximizedWindowCommand

        public ICommand MaximizedWindowCommand { get; }
        private bool CanMaximizedWindowCommandExecuted(object p) => true;
        private void OnMaximizedWindowCommandExecuted(object p)
        {
            CurWindowState = WindowState.Maximized;
        }

        #endregion

        #region NormalWindowCommand

        public ICommand NormalWindowCommand { get; }
        private bool CanNormalWindowCommandExecuted(object p) => true;
        private void OnNormalWindowCommandExecuted(object p)
        {
            CurWindowState = WindowState.Normal;

            Status = "Стартую!";

            var tcs = new CancellationTokenSource();
            var token = tcs.Token;
            Task task = new Task(async () =>
            {
                string application_id = "07ac358d831595916aca265c2f14750c";
                string account_id = "71941826";
                string region = "ru";

                using (var client = new HttpClient())
                {
                    int i = 0;
                    var data_service = new DataService(client, application_id, account_id, region);
                    while (!token.IsCancellationRequested)
                    {
                        var player = data_service.GetData();
                        Status = $"{player.data.account.nickname}({i++})";
                        await Task.Delay(500);
                    }
                }
            }, token);

            task.Start();
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
            MinimizedWindowCommand  = new RelayCommand(OnMinimizedWindowCommandExecuted, CanMinimizedWindowCommandExecuted);
            MaximizedWindowCommand  = new RelayCommand(OnMaximizedWindowCommandExecuted, CanMaximizedWindowCommandExecuted);
            NormalWindowCommand     = new RelayCommand(OnNormalWindowCommandExecuted, CanNormalWindowCommandExecuted);

            #endregion
        }
    }
}