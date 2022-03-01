using ExtendedBlitz.Models.WoTBlitz.Personal_data.Statistic;
using ExtendedBlitz.Models.WoTBlitz.Personal_data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System;
using ExtendedBlitz.ViewModels;

namespace ExtendedBlitz.Models.WoTBlitz
{
    internal static class Calculate
    {
        /// <summary>
        /// Отнимает данные loop из constant
        /// </summary>
        /// <param name="constant">Не изменяемое значение данных игрока</param>
        /// <param name="loop">Обновляемое значение данных игрока</param>
        /// <returns></returns>
        internal static Player SubtractLoopOfConstant(Player constant, Player loop)
        {
            var player = new Player();

            player.status = loop.status;
            player.meta = loop.meta;
            player.data = new Data()
            {
                 account = new Account()
                 {
                     statistics = new Statistics()
                     {
                         clan = new Clan()
                         {
                             spotted = loop.data.account.statistics.clan.spotted - constant.data.account.statistics.clan.spotted,
                             max_frags_tank_id = loop.data.account.statistics.clan.max_frags_tank_id,
                             hits = loop.data.account.statistics.clan.hits - constant.data.account.statistics.clan.hits,
                             frags = loop.data.account.statistics.clan.frags - constant.data.account.statistics.clan.frags,
                             max_xp = loop.data.account.statistics.clan.max_xp - constant.data.account.statistics.clan.max_xp,
                             max_xp_tank_id = loop.data.account.statistics.clan.max_xp,
                             wins = loop.data.account.statistics.clan.wins - constant.data.account.statistics.clan.wins,
                             losses = loop.data.account.statistics.clan.losses - constant.data.account.statistics.clan.losses,
                             capture_points = loop.data.account.statistics.clan.capture_points - constant.data.account.statistics.clan.capture_points,
                             battles = loop.data.account.statistics.clan.battles - constant.data.account.statistics.clan.battles,
                             damage_dealt = loop.data.account.statistics.clan.damage_dealt - constant.data.account.statistics.clan.damage_dealt,
                             damage_received = loop.data.account.statistics.clan.damage_received - constant.data.account.statistics.clan.damage_received,
                             max_frags = loop.data.account.statistics.clan.max_frags,
                             shots = loop.data.account.statistics.clan.shots - constant.data.account.statistics.clan.shots,
                             frags8p = loop.data.account.statistics.clan.frags8p - constant.data.account.statistics.clan.frags8p,
                             xp = loop.data.account.statistics.clan.xp - constant.data.account.statistics.clan.xp,
                             win_and_survived = loop.data.account.statistics.clan.win_and_survived - constant.data.account.statistics.clan.win_and_survived,
                             survived_battles = loop.data.account.statistics.clan.survived_battles - constant.data.account.statistics.clan.survived_battles,
                             dropped_capture_points = loop.data.account.statistics.clan.dropped_capture_points - constant.data.account.statistics.clan.dropped_capture_points,
                         },
                         rating = new Rating()
                         {
                             spotted = loop.data.account.statistics.rating.spotted - constant.data.account.statistics.rating.spotted,
                             calibration_battles_left = loop.data.account.statistics.rating.calibration_battles_left - constant.data.account.statistics.rating.calibration_battles_left,
                             hits = loop.data.account.statistics.rating.hits - constant.data.account.statistics.rating.hits,
                             frags = loop.data.account.statistics.rating.frags - constant.data.account.statistics.rating.frags,
                             recalibration_start_time = loop.data.account.statistics.rating.recalibration_start_time,
                             mm_rating = loop.data.account.statistics.rating.mm_rating - constant.data.account.statistics.rating.mm_rating,
                             wins = loop.data.account.statistics.rating.wins - constant.data.account.statistics.rating.wins,
                             losses = loop.data.account.statistics.rating.losses - constant.data.account.statistics.rating.losses,
                             is_recalibration = loop.data.account.statistics.rating.is_recalibration,
                             capture_points = loop.data.account.statistics.rating.capture_points - constant.data.account.statistics.rating.capture_points,
                             battles = loop.data.account.statistics.rating.battles - constant.data.account.statistics.rating.battles,
                             current_season = loop.data.account.statistics.rating.current_season - constant.data.account.statistics.rating.current_season,
                             damage_dealt = loop.data.account.statistics.rating.damage_dealt - constant.data.account.statistics.rating.damage_dealt,
                             damage_received = loop.data.account.statistics.rating.damage_received - constant.data.account.statistics.rating.damage_received,
                             shots = loop.data.account.statistics.rating.shots - constant.data.account.statistics.rating.shots,
                             frags8p = loop.data.account.statistics.rating.frags8p - constant.data.account.statistics.rating.frags8p,
                             xp = loop.data.account.statistics.rating.xp - constant.data.account.statistics.rating.xp,
                             win_and_survived = loop.data.account.statistics.rating.win_and_survived - constant.data.account.statistics.rating.win_and_survived,
                             survived_battles = loop.data.account.statistics.rating.survived_battles - constant.data.account.statistics.rating.survived_battles,
                             dropped_capture_points = loop.data.account.statistics.rating.dropped_capture_points - constant.data.account.statistics.rating.dropped_capture_points,
                         },
                         all = new All()
                         {
                             spotted = loop.data.account.statistics.all.spotted - constant.data.account.statistics.all.spotted,
                             max_frags_tank_id = loop.data.account.statistics.all.max_frags_tank_id,
                             hits = loop.data.account.statistics.all.hits - constant.data.account.statistics.all.hits,
                             frags = loop.data.account.statistics.all.frags - constant.data.account.statistics.all.frags,
                             max_xp = loop.data.account.statistics.all.max_xp - constant.data.account.statistics.all.max_xp,
                             max_xp_tank_id = loop.data.account.statistics.all.max_xp,
                             wins = loop.data.account.statistics.all.wins - constant.data.account.statistics.all.wins,
                             losses = loop.data.account.statistics.all.losses - constant.data.account.statistics.all.losses,
                             capture_points = loop.data.account.statistics.all.capture_points - constant.data.account.statistics.all.capture_points,
                             battles = loop.data.account.statistics.all.battles - constant.data.account.statistics.all.battles,
                             damage_dealt = loop.data.account.statistics.all.damage_dealt - constant.data.account.statistics.all.damage_dealt,
                             damage_received = loop.data.account.statistics.all.damage_received - constant.data.account.statistics.all.damage_received,
                             max_frags = loop.data.account.statistics.all.max_frags,
                             shots = loop.data.account.statistics.all.shots - constant.data.account.statistics.all.shots,
                             frags8p = loop.data.account.statistics.all.frags8p - constant.data.account.statistics.all.frags8p,
                             xp = loop.data.account.statistics.all.xp - constant.data.account.statistics.all.xp,
                             win_and_survived = loop.data.account.statistics.all.win_and_survived - constant.data.account.statistics.all.win_and_survived,
                             survived_battles = loop.data.account.statistics.all.survived_battles - constant.data.account.statistics.all.survived_battles,
                             dropped_capture_points = loop.data.account.statistics.all.dropped_capture_points - constant.data.account.statistics.all.dropped_capture_points,
                         },

                     },
                     account_id = loop.data.account.account_id,
                     created_at = loop.data.account.created_at,
                     updated_at = loop.data.account.updated_at,
                     @private = new Private()
                     {
                         restrictions = new Restrictions()
                         {
                             //chat_ban_time = loop.data.account.@private.restrictions.chat_ban_time,
                         },
                         //gold = loop.data.account.@private.gold - constant.data.account.@private.gold,
                         //free_xp = loop.data.account.@private.free_xp - constant.data.account.@private.free_xp,
                         //ban_time = loop.data.account.@private.free_xp,
                         //is_premium = loop.data.account.@private.is_premium,
                         //credits = loop.data.account.@private.credits - constant.data.account.@private.credits,
                         //premium_expires_at = loop.data.account.@private.premium_expires_at,
                         //battle_life_time = loop.data.account.@private.battle_life_time,
                         //ban_info = loop.data.account.@private.ban_info,
                     },
                     last_battle_time = loop.data.account.last_battle_time,
                     nickname = loop.data.account.nickname,
                 }
            };

            return player;
        }

        internal static StatSession GetStatBattleSession(ICollection<Battle> battles)
        {
            var statSession = new StatSession();

            foreach (var battle in battles)
            {
                statSession.Wins += battle.Player.data.account.statistics.all.wins;
                statSession.Battles += battle.Player.data.account.statistics.all.battles;
                statSession.Frags += battle.Player.data.account.statistics.all.frags;
                statSession.Survived_battles += battle.Player.data.account.statistics.all.survived_battles;
                statSession.Hits += battle.Player.data.account.statistics.all.hits;
                statSession.Shots += battle.Player.data.account.statistics.all.shots;
                statSession.Deaths += battle.Player.data.account.statistics.all.battles - battle.Player.data.account.statistics.all.survived_battles;
                statSession.Damage_dealt += battle.Player.data.account.statistics.all.damage_dealt;
                statSession.Damage_received += battle.Player.data.account.statistics.all.damage_received;
                statSession.Spotted += battle.Player.data.account.statistics.all.spotted;
                statSession.Dropped_capture_points += battle.Player.data.account.statistics.all.dropped_capture_points;
                statSession.Capture_points += battle.Player.data.account.statistics.all.capture_points;
            }

            return statSession;
        }

        internal static string AverageStatSession(StatSession statSession)
        {
            string stat =
                $"Побед/Боёв:\t\t{statSession.Wins} ({statSession.Battles})\t({Math.Round((float)statSession.Wins / (float)statSession.Battles * 100.0f, 2)}%)" +
                $"\nУничтожил:\t\t{statSession.Frags}\t({Math.Round((float)statSession.Frags / (float)statSession.Battles, 2)})" +
                $"\nУничтожен:\t\t{statSession.Deaths}\t({Math.Round((float)statSession.Deaths / (float)statSession.Battles * 100.0f, 2)}%)" +
                $"\nВыстрелов/Попаданий:\t{statSession.Shots}/{statSession.Hits}\t({Math.Round((float)statSession.Hits / statSession.Shots * 100.0f, 2)}%)" +
                $"\nНанесённый урон:\t{statSession.Damage_dealt}\t({Math.Round((float)statSession.Damage_dealt / (float)statSession.Battles, 0)})" +
                $"\nПолученный урон:\t{statSession.Damage_received}\t({Math.Round((float)statSession.Damage_received / (float)statSession.Battles, 0)})" +
                $"\nОбнаружил:\t\t{statSession.Spotted}\t({Math.Round((float)statSession.Spotted / (float)statSession.Battles, 2)})" +
                $"\nОчки защиты базы:\t{statSession.Dropped_capture_points}\t({Math.Round((float)statSession.Dropped_capture_points / (float)statSession.Battles, 2)})" +
                $"\nОчки захваты базы:\t{statSession.Capture_points}\t({Math.Round((float)statSession.Capture_points / (float)statSession.Battles, 2)})";
            return stat;
        }
    }
}
