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

        public MainWindowViewModel()
        {

        }
    }
}