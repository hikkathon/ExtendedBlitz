using ExtendedBlitz.Models.WoTBlitz.Personal_data;
using System.Collections.Generic;
using System;

namespace ExtendedBlitz.Models.WoTBlitz
{
    internal class Battle
    {
        public int Id { get; set; }
        public string Status { get; set; } // победа/поражение
        public Player Player { get; set; }
    }

    internal class Session
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public ICollection<Battle> Battles { get; set; }
    }
}