using Exiled.Events.EventArgs.Player;
using LightPlugin.Features;
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
            Plugin.CheckTeam.checkTeam(player);

            Plugin.TeamDamage.RemoveTeam(player);
        }

        [PluginEvent(ServerEventType.PlayerLeft)]
        public void OnPlayerLeft(PlayerAPI player)
        {
            Plugin.CheckTeam.checkTeam(player);

            Plugin.TeamDamage.RemoveTeam(player);
        }

        [PluginEvent(ServerEventType.PlayerSpawn)]
        public void OnPlayerSpawn(PlayerAPI player)
        {
           Plugin.CheckTeam.addTeam(player);
        }

        [PluginEvent(ServerEventType.PlayerDamage)]
        public void onPlayerDamage(HurtingEventArgs args)
        {
           Plugin.TeamDamage.onAttack(args);
        }
    }
}
