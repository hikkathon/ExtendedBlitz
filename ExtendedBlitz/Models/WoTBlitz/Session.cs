using ExtendedBlitz.Models.WoTBlitz.Personal_data;
using System.Collections.Generic;
using ExtendedBlitz.Services;

namespace ExtendedBlitz.Models.WoTBlitz
{
    internal class Battle
    {
        public int Id { get; set; }
        public string Status { get; set; } // победа/поражение
        public long Time { get; set; }
        public Player Player { get; set; }

        public string BattleTime
        {
            get { return DateTimeHelper.ParseUnixTimestamp(Time).ToString("dd.MM.yyyy hh:mm:ss"); }
        }
    }

    internal class Session
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public long Time { get; set; }
        public ICollection<Battle> Battles { get; set; }
        public StatSession StatSession { get; set; }

        public string SessionTime
        {
            get { return DateTimeHelper.ParseUnixTimestamp(Time).ToString("dd.MM.yyyy hh:mm:ss"); }
        }
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
        public string Average { get; set; }

        public string WinRate
        {
            get { return $"{System.Math.Round((float)Wins / (float)Battles * 100.0f, 0)}%"; }
        }
    }
}