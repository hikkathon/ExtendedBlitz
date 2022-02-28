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
        public StatSession StatSession { get; set; }
    }

    internal class StatSession
    {
        public int Wins { get; set; }
        public int Battles { get; set; }
        public int Frags { get; set; }
        public int Survived_battles { get; set; }
        public int Hits { get; set; }
        public int Shots { get; set; }
        public int Deaths { get; set; }
        public int Damage_dealt { get; set; }
        public int Damage_received { get; set; }
        public int Spotted { get; set; }
        public int Dropped_capture_points { get; set; }
        public int Capture_points { get; set; }
    }
}