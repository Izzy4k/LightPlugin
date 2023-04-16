using Exiled.Events.EventArgs.Player;
using PlayerAPI = Exiled.API.Features.Player;
namespace LightPlugin.Api.Events
{
    public static class Player
    {
        public delegate void OnPlayerSpawnHandler(PlayerAPI player);

        public static event OnPlayerSpawnHandler OnPlayerSpawn;

        public delegate void OnPlayerDeathHandler(PlayerAPI player);

        public static event OnPlayerDeathHandler OnPlayerDeath;

        public delegate void onPlayerLeftHandler(PlayerAPI player);

        public static event onPlayerLeftHandler OnPlayerLeft;

        public static void InvokePlayerSpawn(SpawnedEventArgs args)
        {
            OnPlayerSpawn?.Invoke(args.Player);
        }

        public static void InvokePlayerDeath(DiedEventArgs args)
        {
            OnPlayerDeath?.Invoke(args.Player);
        }

        public static void InvokePlayerLeft(LeftEventArgs args)
        {
            OnPlayerLeft?.Invoke(args.Player);
        }
    }
}
