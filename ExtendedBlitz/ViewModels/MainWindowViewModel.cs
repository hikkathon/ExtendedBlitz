using ExtendedBlitz.Models.WoTBlitz.Personal_data.Statistic;
using ExtendedBlitz.Models.WoTBlitz.Personal_data;
using ExtendedBlitz.Infrastructure.Commands;
using System.Collections.ObjectModel;
using ExtendedBlitz.ViewModels.Base;
using ExtendedBlitz.Models.WoTBlitz;
using System.Collections.Generic;
using System.Windows.Threading;
using ExtendedBlitz.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Threading;
using System.Net.Http;
using System.Windows;
using System.Linq;
using System.IO;
using System;
using Newtonsoft.Json;
using ExtendedBlitz.Models;

namespace ExtendedBlitz.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        /* ---------------------------------------------------------------------------------------------------- */

        public ObservableCollection<Session> Sessions { get; }
        public ObservableCollection<Battle> Battles { get; }
        public ObservableCollection<LanguageModel> Languages { get; }

        ICollection<Battle> battles_1;
        ICollection<Session> sessions_1;
        Dispatcher dispatcher;
        DataService dataService;
        public Setting Setting { get; set; }

        #region Setting prop

        #region account_id

        private string _account_id;

        /// <summary>Заголовок окна</summary>
        public string account_id
        {
            get => _account_id;
            set => Set(ref _account_id, value);
        }

        #endregion

        #region access_token

        private string _access_token;

        /// <summary>Заголовок окна</summary>
        public string access_token
        {
            get => _access_token;
            set => Set(ref _access_token, value);
        }

        #endregion

        #endregion

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

        private string _status;

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

        #region Selected session

        private Session _selectSession;
        public Session SelectSession { get => _selectSession; set => Set(ref _selectSession, value); }

        #endregion

        #region Selected languages

        private LanguageModel _selectLanguage;
        public LanguageModel SelectLanguage { get => _selectLanguage; set => Set(ref _selectLanguage, value); }

        #endregion

        #region Average Stats Properties

        #region Средние прказатели за сессию
        private string _averageStatsSession =
            "Побед/Боёв:\t\t0 (0)\t(0.0%)" +
            "\nУничтожил:\t\t0\t(0.0)" +
            "\nУничтожен:\t\t0\t(0.0%)" +
            "\nПопаданий/Выстрелов:\t0/0\t(0.0%)" +
            "\nНанесённый урон:\t0\t(0)" +
            "\nПолученный урон:\t0\t(0)" +
            "\nОбнаружил:\t\t0\t(0)" +
            "\nОчки защиты базы:\t0\t(0)" +
            "\nОчки захваты базы:\t0\t(0)";

        /// <summary>Win/Battles</summary>
        public string AverageStatsSession
        {
            get => _averageStatsSession;
            set => Set(ref _averageStatsSession, value);
        }
        #endregion

        #endregion

        #region IsOpenSession : bool - Флаг

        private bool _isOpenSession;

        /// <summary>Флаг показывает открыта сессия или нет</summary>
        public bool IsOpenSession
        {
            get => _isOpenSession;
            set => Set(ref _isOpenSession, value);
        }

        #endregion

        #region Token : Токен для отмены процесса

        private CancellationTokenSource _cts;

        /// <summary>Токен для отмены скана статистики</summary>
        public CancellationTokenSource CTS
        {
            get => _cts;
            set => Set(ref _cts, value);
        }

        #endregion

        #region Ник игрока

        private string _nickname = "@hikkathon";

        /// <summary>Заголовок окна</summary>
        public string Nickname
        {
            get => _nickname;
            set => Set(ref _nickname, value);
        }

        #endregion

        /* ---------------------------------------------------------------------------------------------------- */

        #region Команды

        #region CloseApplicationCommand

        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecuted(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            dataService.SaveToJson(Sessions);
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

        public ICommand NormalWindowCommand { get; set; }
        private bool CanNormalWindowCommandExecuted(object p) => true;
        private void OnNormalWindowCommandExecuted(object p)
        {
            CurWindowState = WindowState.Normal;
        }

        #endregion

        #region StartSessionCommand

        public ICommand StartSessionCommand { get; }
        private bool CanStartSessionCommandExecuted(object p) => !IsOpenSession;
        private void OnStartSessionCommandExecuted(object p)
        {
            Status = " Стартую!";

            CTS = new CancellationTokenSource();
            var token = CTS.Token;

            Player loop = new Player();
            Player constant = new Player();

            IsOpenSession = true;
            bool IsNewSession = true;

            Task task = new Task(async () =>
            {
                int i = 0;
                int battle_max_index = 0;
                int session_max_index = 0;

                using (var client = new HttpClient())
                {
                    var data_service = new DataService(client, Setting.application_id, Setting.account_id, Setting.language);
                    constant = data_service.GetData();

                    Nickname = $"{constant.data.account.nickname} ";

                    while (IsOpenSession)
                    {
                        loop = data_service.GetData();
                        if (loop.data.account.statistics.all.battles > constant.data.account.statistics.all.battles)
                        {
                            string status = "Undefined";

                            if (loop.data.account.statistics.all.wins > constant.data.account.statistics.all.wins)
                                status = "Победа";
                            else
                                status = "Поражение";

                            #region Добавить бой
                            dispatcher.Invoke(new Action(() =>
                            {
                                battle_max_index = Battles.Count + 1;
                                var new_battle = new Battle
                                {
                                    Id = battle_max_index,
                                    Status = status,
                                    Time = DateTimeHelper.ToUnixTimestamp(DateTime.Now),
                                    Player = Calculate.SubtractLoopOfConstant(constant, loop),
                                };

                                Battles.Add(new_battle);
                            }));
                            #endregion

                            if (IsNewSession)
                            {
                                #region Добавление сессии
                                dispatcher.Invoke(new Action(() =>
                                {
                                    session_max_index = Sessions.Count + 1;
                                    var new_session = new Session
                                    {
                                        Id = session_max_index,
                                        Title = $"Сессия ({session_max_index})",
                                        Time = DateTimeHelper.ToUnixTimestamp(DateTime.Now),
                                        Battles = new ObservableCollection<Battle>(Battles),
                                        StatSession = Calculate.GetStatBattleSession(Battles),
                                    };

                                    Sessions.Add(new_session);
                                }));
                                #endregion

                                IsNewSession = false;
                            }
                            else
                            {
                                #region Добавить бой
                                battle_max_index = Battles.Count + 1;
                                var new_battle = new Battle
                                {
                                    Id = battle_max_index,
                                    Status = status,
                                    Time = DateTimeHelper.ToUnixTimestamp(DateTime.Now),
                                    Player = Calculate.SubtractLoopOfConstant(constant, loop)
                                };
                                #endregion

                                #region Обновить сессию
                                var update_session = new Session
                                {
                                    Id = session_max_index,
                                    Title = $"Сессия ({session_max_index})",
                                    Time = DateTimeHelper.ToUnixTimestamp(DateTime.Now),
                                    Battles = new ObservableCollection<Battle>(Battles),
                                    StatSession = Calculate.GetStatBattleSession(Battles)
                                };
                                #endregion

                                dispatcher.Invoke(new Action(() =>
                                {
                                    Sessions.ElementAt(session_max_index - 1).Battles.Add(new_battle);
                                    Sessions[session_max_index - 1] = update_session;
                                }));

                                SelectSession = Sessions[session_max_index - 1]; // Выбрать последнбб сессию
                            }

                            constant = loop;
                        }

                        Status = $"({i++})";

                        //if (SelectSession != null)
                        //    AverageStatsSession = Calculate.AverageStatSession(SelectSession.StatSession);

                        await Task.Delay(2 * 1000);
                    }
                }
            }, token);
            task.Start();
        }

        #endregion

        #region CloseSessionCommand

        public ICommand CloseSessionCommand { get; }
        private bool CanCloseSessionCommandExecuted(object p) => IsOpenSession;
        private void OnCloseSessionCommandExecuted(object p)
        {
            Status = "Сессия прервана!";
            dataService.SaveToJson(Sessions);
            Battles.Clear();
            IsOpenSession = false;
            CTS.Cancel();
        }

        #endregion

        #region SaveSettingCommand

        public ICommand SaveSettingCommandCommand { get; }
        private bool CanSaveSettingCommandExecuted(object p) => !IsOpenSession;
        private void OnSaveSettingCommandExecuted(object p)
        {
            Setting = new Setting()
            {
                application_id = "07ac358d831595916aca265c2f14750c",
                account_id = account_id,
                access_token = access_token,
                extra = "private.grouped_contacts, statistics.rating",
                language = p.ToString(),
            };
        }

        #endregion

        #endregion

        /* ---------------------------------------------------------------------------------------------------- */

        public MainWindowViewModel()
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            dataService = new DataService();

            Languages = File.Exists("Data\\Languages.json") ? JsonConvert.DeserializeObject<ObservableCollection<LanguageModel>>(File.ReadAllText("Data\\Languages.json")) : new ObservableCollection<LanguageModel>();

            #region Регистрация команд 

            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
            MinimizedWindowCommand = new RelayCommand(OnMinimizedWindowCommandExecuted, CanMinimizedWindowCommandExecuted);
            MaximizedWindowCommand = new RelayCommand(OnMaximizedWindowCommandExecuted, CanMaximizedWindowCommandExecuted);
            NormalWindowCommand = new RelayCommand(OnNormalWindowCommandExecuted, CanNormalWindowCommandExecuted);
            StartSessionCommand = new RelayCommand(OnStartSessionCommandExecuted, CanStartSessionCommandExecuted);
            CloseSessionCommand = new RelayCommand(OnCloseSessionCommandExecuted, CanCloseSessionCommandExecuted);
            SaveSettingCommandCommand = new RelayCommand(OnSaveSettingCommandExecuted, CanSaveSettingCommandExecuted);

            #endregion

            bool test1 = false;
            bool test2 = true;

            Random rnd = new Random();

            if (test1)
            {

                var battles = Enumerable.Range(1, 10).Select(i => new Battle
                {
                    Id = i,
                    Status = rnd.Next(-1, 1) > rnd.Next(-1, 1) ? "Победа" : "Поражение",
                    Player = new Player()
                    {
                        status = "ok",
                        meta = new Meta() { count = 1 },
                        data = new Data()
                        {
                            account = new Account()
                            {
                                statistics = new Statistics()
                                {
                                    all = new All()
                                    {
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
                                    },
                                    clan = new Clan()
                                    {
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
                                    },
                                    rating = new Rating()
                                    {
                                        spotted = rnd.Next(1, 20),
                                        hits = rnd.Next(1, 20),
                                        frags = rnd.Next(1, 20),
                                        wins = rnd.Next(1, 20),
                                        losses = rnd.Next(1, 20),
                                        capture_points = rnd.Next(1, 20),
                                        battles = rnd.Next(1, 20),
                                        damage_dealt = rnd.Next(1, 20),
                                        damage_received = rnd.Next(1, 20),
                                        shots = rnd.Next(1, 20),
                                        frags8p = rnd.Next(1, 20),
                                        xp = rnd.Next(1, 20),
                                        win_and_survived = rnd.Next(1, 20),
                                        survived_battles = rnd.Next(1, 20),
                                        dropped_capture_points = rnd.Next(1, 20),
                                    }
                                },
                                account_id = rnd.Next(1, 2000),
                                created_at = DateTime.Now,
                                updated_at = DateTime.Now,
                                @private = new Private()
                                {

                                },
                                last_battle_time = DateTime.Now,
                                nickname = $"HINCO"
                            }
                        }
                    }
                });

                var sessions = Enumerable.Range(1, 10).Select(i => new Session
                {
                    Id = i,
                    Title = $"Сессия {i}",
                    Time = DateTimeHelper.ToUnixTimestamp(DateTime.Now),
                    Battles = new ObservableCollection<Battle>(battles)
                });

                Sessions = new ObservableCollection<Session>(sessions);

                //DataService ds = new DataService();
                //ds.SaveToJson(Sessions);
            }


            if (test2)
            {
                battles_1 = new ObservableCollection<Battle>();
                sessions_1 = new ObservableCollection<Session>();

                battles_1.Add(new Battle
                {
                    Id = 1,
                    Status = "Победа",
                    Player = new Player
                    {
                        status = "ok",
                        meta = new Meta() { count = 1 },
                        data = new Data()
                        {
                            account = new Account()
                            {
                                statistics = new Statistics()
                                {
                                    all = new All()
                                    {
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
                                    },
                                    clan = new Clan()
                                    {
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
                                    },
                                    rating = new Rating()
                                    {
                                        spotted = rnd.Next(1, 20),
                                        hits = rnd.Next(1, 20),
                                        frags = rnd.Next(1, 20),
                                        wins = rnd.Next(1, 20),
                                        losses = rnd.Next(1, 20),
                                        capture_points = rnd.Next(1, 20),
                                        battles = rnd.Next(1, 20),
                                        damage_dealt = rnd.Next(1, 20),
                                        damage_received = rnd.Next(1, 20),
                                        shots = rnd.Next(1, 20),
                                        frags8p = rnd.Next(1, 20),
                                        xp = rnd.Next(1, 20),
                                        win_and_survived = rnd.Next(1, 20),
                                        survived_battles = rnd.Next(1, 20),
                                        dropped_capture_points = rnd.Next(1, 20),
                                    }
                                },
                                account_id = rnd.Next(1, 2000),
                                created_at = DateTime.Now,
                                updated_at = DateTime.Now,
                                @private = new Private()
                                {

                                },
                                last_battle_time = DateTime.Now,
                                nickname = $"HINCO"
                            }
                        }
                    }
                });

                sessions_1.Add(new Session
                {
                    Id = 1,
                    Title = "Сессия (1)",
                    Time = DateTimeHelper.ToUnixTimestamp(DateTime.Now),
                    Battles = new ObservableCollection<Battle>(battles_1),
                    StatSession = Calculate.GetStatBattleSession(battles_1)
                });


                var fileLocalSessionJson = File.Exists("Data\\Save\\Sessions.json") ? JsonConvert.DeserializeObject<ICollection<Session>>(File.ReadAllText("Data\\Save\\Sessions.json")) : new ObservableCollection<Session>();


                Sessions = new ObservableCollection<Session>(fileLocalSessionJson);
                Battles = new ObservableCollection<Battle>();
            }
        }
    }
}