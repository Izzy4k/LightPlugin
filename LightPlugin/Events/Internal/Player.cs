using Exiled.Events.EventArgs.Player;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PlayerAPI = Exiled.API.Features.Player;

namespace LightPlugin.Events.Internal
{
    public sealed class Player
    {

        [PluginEvent(ServerEventType.PlayerDeath)]
        public void OnPlayerDeath(PlayerAPI player)
        {
            Plugin.TeamDamage.RemoveTeam(player);
        }

        [PluginEvent(ServerEventType.PlayerLeft)]
        public void OnPlayerLeft(PlayerAPI player)
        {
            Plugin.TeamDamage.RemoveTeam(player);
        }

        [PluginEvent(ServerEventType.PlayerDamage)]
        public void onPlayerDamage(HurtingEventArgs args)
        {
           Plugin.TeamDamage.onAttack(args);
        }

        [PluginEvent(ServerEventType.PlayerDying)]
        public void onPlayerDying(DyingEventArgs args)
        {
            Plugin.CheckTeam.checkTeam(args);
        }
    }
}
