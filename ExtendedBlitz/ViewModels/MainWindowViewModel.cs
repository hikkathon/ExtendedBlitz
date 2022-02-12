using ExtendedBlitz.Infrastructure.Commands;
using ExtendedBlitz.ViewModels.Base;
using System.Windows.Input;
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
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
            MinimizedWindowCommand = new RelayCommand(OnMinimizedWindowCommandExecuted, CanMinimizedWindowCommandExecuted);
            MaximizedWindowCommand = new RelayCommand(OnMaximizedWindowCommandExecuted, CanMaximizedWindowCommandExecuted);
            NormalWindowCommand = new RelayCommand(OnNormalWindowCommandExecuted, CanNormalWindowCommandExecuted);

            #endregion
        }
    }
}