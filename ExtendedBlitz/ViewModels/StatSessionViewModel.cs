using ExtendedBlitz.ViewModels.Base;

namespace ExtendedBlitz.ViewModels
{
    internal class StatSessionViewModel : ViewModelBase
    {
        #region Заголовок окна

        private string _stats = "Extended Blitz";

        /// <summary>Заголовок окна</summary>
        public string Stats
        {
            get => _stats;
            set => Set(ref _stats, value);
        }

        #endregion

        public StatSessionViewModel()
        {

        }
    }
}
