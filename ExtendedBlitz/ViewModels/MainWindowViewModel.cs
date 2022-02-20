using ExtendedBlitz.Infrastructure.Commands;
using System.Collections.ObjectModel;
using ExtendedBlitz.ViewModels.Base;
using ExtendedBlitz.Models.WoTBlitz;
using ExtendedBlitz.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Threading;
using System.Net.Http;
using System.Windows;
using System.Linq;
using System;

namespace ExtendedBlitz.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        /* ---------------------------------------------------------------------------------------------------- */

        public ObservableCollection<Session> Sessions {get;}

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

        #region Selected battle

        private Session _selectSession;
        public Session SelectSession { get => _selectSession; set => Set(ref _selectSession, value); }

        #endregion

        /* ---------------------------------------------------------------------------------------------------- */

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

        /* ---------------------------------------------------------------------------------------------------- */

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
            MinimizedWindowCommand  = new RelayCommand(OnMinimizedWindowCommandExecuted, CanMinimizedWindowCommandExecuted);
            MaximizedWindowCommand  = new RelayCommand(OnMaximizedWindowCommandExecuted, CanMaximizedWindowCommandExecuted);
            NormalWindowCommand     = new RelayCommand(OnNormalWindowCommandExecuted, CanNormalWindowCommandExecuted);

            #endregion

            Random rnd = new Random();

            var battles = Enumerable.Range(1, 20).Select(i => new Battle
            {
                id = i,
                status = rnd.Next(-1,1) > rnd.Next(-1, 1) ? "Победа" : "Поражение",
                spotted = rnd.Next(1, 20),
                max_frags_tank_id = rnd.Next(1, 20),
                hits = rnd.Next(1, 20),
                frags = rnd.Next(1, 20),
                max_xp = rnd.Next(1, 20),
                max_xp_tank_id = rnd.Next(1, 20),
                wins = rnd.Next(1, 20),
                losses = rnd.Next(1, 20),
                capture_points = rnd.Next(1, 20),
                battles = rnd.Next(1, 20),
                damage_dealt = rnd.Next(1, 20),
                damage_received = rnd.Next(1, 20),
                max_frags = rnd.Next(1, 20),
                shots = rnd.Next(1, 20),
                frags8p = rnd.Next(1, 20),
                xp = rnd.Next(1, 20),
                win_and_survived = rnd.Next(1, 20),
                survived_battles = rnd.Next(1, 20),
                dropped_capture_points = rnd.Next(1, 20),
            });

            var sessions = Enumerable.Range(1, 10).Select(i => new Session
            {
                Id = i,
                Name = $"Сессия {i}",
                TimeSession = DateTime.Now,
                Battles = new ObservableCollection<Battle>(battles)
            });

            Sessions = new ObservableCollection<Session>(sessions);
        }
    }
}