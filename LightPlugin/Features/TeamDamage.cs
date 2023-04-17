using System;
using System.Collections.Generic;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
namespace LightPlugin.Features
{
    public sealed class TeamDamage
    {
        private readonly Dictionary<Player, float> teamDamage = new Dictionary<Player, float>();

        public void onAttack(HurtingEventArgs ev)
        {
            try
            {
                var attacker = ev.Attacker;

                var player = ev.Player;

                var attackerTeam = attacker.Role.Team;
                var playerTeam = player.Role.Team;

                var isTeam = attackerTeam.GetSide() == playerTeam.GetSide();

                if (!isTeam) return;

                if (!IsTeamDamager(attacker)) teamDamage.Add(attacker, 0f);

                if (teamDamage[attacker] >= 500f)
                {
                    ev.IsAllowed = false;
                    attacker.Broadcast(5, "Вы превысели норму урона по союзным классам.");

                    return;
                }

                teamDamage[attacker] += ev.Amount;
            }
            catch (Exception e)
            {
                Log.Error($"Error {e.Message}");
            }
        }

        private bool IsTeamDamager(Player attaker) => teamDamage.ContainsKey(attaker);

        public void RemoveTeam(Player player)
        {
            if (!IsTeamDamager(player)) return;

            teamDamage.Remove(player);
        }
    }
}
