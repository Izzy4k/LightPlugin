using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace LightPlugin.Features
{
    public sealed class TeamDamage
    {
        private readonly Dictionary<int, float> teamDamage = new Dictionary<int, float>();

        public void onAttack(HurtingEventArgs ev)
        {
            try
            {
                if (ev.Attacker.RoleManager.CurrentRole.Team != ev.Player.RoleManager.CurrentRole.Team) return;

                if (IsTeamDamager(ev.Attacker)) teamDamage.Add(ev.Attacker.Id, 0f);

                teamDamage[ev.Attacker.Id] += ev.Amount;

                if (teamDamage[ev.Attacker.Id] >= 500f)
                {
                    ev.IsAllowed = false;
                    ev.Attacker.Broadcast(5, "Вы превысели норму урона по союзным классам.");
                }
            }
            catch(Exception e) {
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
