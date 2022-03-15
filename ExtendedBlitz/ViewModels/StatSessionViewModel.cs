using ExtendedBlitz.ViewModels.Base;
using System.Windows;

namespace ExtendedBlitz.ViewModels
{
    internal class StatSessionViewModel : ViewModelBase
    {
        #region Stats : Вывод сессии

        private string _stats = "Extended Blitz";

        /// <summary>Заголовок окна</summary>
        public string Stats
        {
            get => _stats;
            set => Set(ref _stats, value);
        }

        #endregion

        #region VisibilityStats : Если активна то открыть окно статистики

        private Visibility _visibilityStats;

        /// <summary>Окно статистики</summary>
        public Visibility VisibilityStats
        {
            get => _visibilityStats;
            set => Set(ref _visibilityStats, value);
        }

        #endregion

        public StatSessionViewModel()
        {

        }
    }
}
