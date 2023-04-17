using System;
using System.Collections.Generic;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
namespace LightPlugin.Features
{
    public sealed class TeamDamage
    {
        private readonly Dictionary<int, float> teamDamage = new Dictionary<int, float>();

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

                if (IsTeamDamager(ev.Attacker)) teamDamage.Add(ev.Attacker.Id, 0f);

                if (teamDamage[ev.Attacker.Id] >= 500f)
                {
                    ev.IsAllowed = false;
                    ev.Attacker.Broadcast(5, "Вы превысели норму урона по союзным классам.");

                    return;
                }

                teamDamage[ev.Attacker.Id] += ev.Amount;
            }
            catch (Exception e)
            {
                Log.Error($"Error {e.Message}");
            }
        }

        private bool IsTeamDamager(Player attaker) => teamDamage.ContainsKey(attaker.Id);

        public void RemoveTeam(Player player)
        {
            if (!IsTeamDamager(player)) return;

            teamDamage.Remove(player.Id);
        }
    }
}
