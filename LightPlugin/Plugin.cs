using Exiled.API.Features;
using LightPlugin.Features;
using PlayerAPI = Exiled.Events.Handlers.Player;
using PlayerMyAPI = LightPlugin.Api.Events.Player;
using Exiled.Events.EventArgs.Player;
using Exiled.API.Enums;
using System;

namespace LightPlugin
{
    public sealed class Plugin : Plugin<Config>
    {

        private static readonly Lazy<TeamDamage> LazyTeamDamage = new Lazy<TeamDamage>(() => new TeamDamage());

        public static TeamDamage TeamDamage { get { return LazyTeamDamage.Value; } }

        private static readonly Lazy<CheckTeam> LazyCheckTeam = new Lazy<CheckTeam>(() => new CheckTeam());

        public static CheckTeam CheckTeam => LazyCheckTeam.Value;

        private Events.Internal.Player player;

        public override void OnEnabled()
        {
            Initialization();
            RegisterPlayerEvents();
            RegisterEvents();
            base.OnEnabled();
        }

        private void RegisterPlayerEvents()
        {
            PlayerAPI.Spawned += PlayerMyAPI.InvokePlayerSpawn;
            PlayerAPI.Died += PlayerMyAPI.InvokePlayerDeath;
            PlayerAPI.Left += PlayerMyAPI.InvokePlayerLeft;
        }

        private void RegisterEvents()
        {
            PlayerMyAPI.OnPlayerSpawn += player.OnPlayerSpawn;
            PlayerMyAPI.OnPlayerLeft += player.OnPlayerLeft;
            PlayerMyAPI.OnPlayerDeath += player.OnPlayerDeath;

            PlayerAPI.Hurting += player.onPlayerDamage;
            PlayerAPI.Shooting += onShooting;

        }

        private void UnRegisterPlayerEvents()
        {
            PlayerAPI.Spawned -= PlayerMyAPI.InvokePlayerSpawn;
            PlayerAPI.Died -= PlayerMyAPI.InvokePlayerDeath;
            PlayerAPI.Left -= PlayerMyAPI.InvokePlayerLeft;
        }

        private void UnRegisterEvents()
        {
            PlayerMyAPI.OnPlayerSpawn -= player.OnPlayerSpawn;
            PlayerMyAPI.OnPlayerLeft -= player.OnPlayerLeft;
            PlayerMyAPI.OnPlayerDeath -= player.OnPlayerDeath;

            PlayerAPI.Hurting -= player.onPlayerDamage;
            PlayerAPI.Shooting -= onShooting;

        }

        private void Initialization()
        {
            player = new Events.Internal.Player();

        }

        private void UnInitialization()
        {
            player = null;
        }

        public override void OnDisabled()
        {
            UnRegisterPlayerEvents();
            UnRegisterEvents();
            UnInitialization();
            base.OnDisabled();
        }

        private void onShooting(ShootingEventArgs args)
        {
            if (args.Player.CurrentItem.Type != ItemType.GunCOM15) return;

            args.IsAllowed = false;

            args.Player.ThrowGrenade(ProjectileType.Flashbang);
        }
    }
}