using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace LightPlugin.Features
{
    public sealed class CheckTeam
    {
        public void checkTeam(DyingEventArgs args)
        {
            var team = args.Player.Role.Team;

            var survivors = Player.List.Where(player => player.Role.Team == team && player != args.Player).ToList();

            if (survivors.Count != 1) return;

            survivors.First().Broadcast(6, $"Вы последний из команды {team}");
        }
    }
}
