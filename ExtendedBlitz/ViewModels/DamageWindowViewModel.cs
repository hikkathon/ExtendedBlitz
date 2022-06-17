using ExtendedBlitz.ViewModels.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;
using ExtendedBlitz.Services;
using System.Windows;

namespace ExtendedBlitz.ViewModels
{
    internal class DamageWindowViewModel : ViewModelBase
    {
        #region Damage : Нанесенный урон

        private int _damage;

        /// <summary> Нанесенный урон </summary>
        public int Damage
        {
            get => _damage;
            set => Set(ref _damage, value);
        }

        #endregion

        #region DamageBlocked : Заблокированный урон

        private int _damage_blocked;

        /// <summary> Заблокированный урон </summary>
        public int DamageBlocked
        {
            get => _damage_blocked;
            set => Set(ref _damage_blocked, value);
        }

        #endregion

        #region Health : Прочность танка

        private int _health;

        /// <summary> Здоровье игрока </summary>
        public int Health
        {
            get => _health;
            set => Set(ref _health, value);
        }

        #endregion

        #region VisibilityDamage : Если активна то открыть окно статистики

        private Visibility _visibilityDamage = Visibility.Hidden;

        /// <summary>Окно статистики</summary>
        public Visibility VisibilityDamage
        {
            get => _visibilityDamage;
            set => Set(ref _visibilityDamage, value);
        }

        #endregion

        public DamageWindowViewModel()
        {
            MemoryScanner MemoryScanner = new MemoryScanner("wotblitz.exe");

            try
            {
                MemoryScanner.GetBaseAddress();
            }
            catch (Exception exc)
            {
                MessageBox.Show($"Что бы использовать <Дамаг панель> запустите World of Tanks Blitz", $"Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
