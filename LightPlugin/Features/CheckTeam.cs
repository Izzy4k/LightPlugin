using System.Collections.Generic;
using Exiled.API.Features;
using PlayerRoles;

namespace LightPlugin.Features
{
    public sealed class CheckTeam
    {
        private readonly Dictionary<int, Team> AliveTeam = new Dictionary<int, Team>();
        public void checkTeam(Player player)
        {
            if (IsPersonWasAlive(player))
            {
                var target = AliveTeam[player.Id];

                RemovePersonAlive(player);

                List<int> RemainingAlive = new List<int>();

                foreach (var itemPlayer in AliveTeam)
                {
                    if (itemPlayer.Value == target)
                    {
                        RemainingAlive.Add(itemPlayer.Key);
                    }
                }

                if(RemainingAlive.Count == 1)
                {
                    var LastPlayer = Player.Get(RemainingAlive[0]);
                    
                    LastPlayer.Broadcast(6,$"Вы последний человек из команды {LastPlayer.RoleManager.CurrentRole.Team}");
                }
            }
        }

        private void AddAliveTeam(Player player)
        {
            AliveTeam.Add(player.Id, player.RoleManager.CurrentRole.Team);
        }

        public void addTeam(Player player)
        {
            if (IsPersonWasAlive(player))
            {
                RemovePersonAlive(player);
            }

            AddAliveTeam(player);
        }
       
        private bool IsPersonWasAlive(Player player) => AliveTeam.ContainsKey(player.Id);

        private void RemovePersonAlive(Player player) => AliveTeam.Remove(player.Id);
    }
}
