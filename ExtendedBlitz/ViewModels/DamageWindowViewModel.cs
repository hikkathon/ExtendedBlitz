using ExtendedBlitz.ViewModels.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

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

        public DamageWindowViewModel()
        {

        }
    }
}
