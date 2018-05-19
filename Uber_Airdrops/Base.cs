//****//
// THIS PROJECT IS PROVIDED UNDER NO COPYRIGHT OR PROTECTION BY UBER PVP @ FAGGOCHEAT.ORG
//
// SOME DATA HAS BEEN REDACTED OR CHANGED FOR PROTECTION PURPOSES BY FURTHER ADMINISTRATION
//
// "Hope you like it."
//        - Faggo Cheat Team
//****//

using System;
using UnityEngine;
using SDG.Unturned;
using Rocket.Core.Plugins;
using Rocket.API.Collections;
using Rocket.Unturned.Chat;

namespace Uber_Airdrops
{
    public class Uber_AirdropsMain : RocketPlugin<AirdropConfiguration>
    {
        public static Uber_AirdropsMain Instance;

        DateTime? LastAirdrop     = null;
        DateTime? LastMassAirdrop = null;
        DateTime? LastMassDropped = null;
        DateTime? LastMassMessage = null;

        public Color AirdropC, BigdropC, BigdropAnnounceC;

        public int MassDrops = 0;
        double AirdropNextSeconds;
        public bool ForceADrop = false;

        void FixedUpdate()
        {
            //**C_STATE**//
            if (!(State == Rocket.API.PluginState.Loaded && Level.isLoaded))
                return;

            //**DO_DROP**//
            if (ForceADrop)
            {
                SendDrop();
                ForceADrop = false;
                UnturnedChat.Say(Translate("airdrop"), AirdropC);
                return;
            }

            //**S_MASSDROPS**//
            if (MassDrops > 0 && (DateTime.Now - LastMassDropped.Value).TotalSeconds > 3)
            {
                SendDrop();
                MassDrops--;
                LastMassDropped = DateTime.Now;
                return;
            }

            //**VERIFY**//
            if (!Configuration.Instance.RegularAirdropsEnabled && !Configuration.Instance.MassDropsEnabled)
                return;

            //**D_DROP**//
            if ((DateTime.Now - LastAirdrop.Value).TotalSeconds > AirdropNextSeconds)
            {
                SendDrop();
                LastAirdrop = DateTime.Now;
                AirdropNextSeconds = UnityEngine.Random.Range(Configuration.Instance.AirdropTimeMinimum, Configuration.Instance.AirdropTimeMaximum);
                UnturnedChat.Say(Translate("airdrop"), AirdropC);
                return;
            }

            //**R_CHECK**//
            if (Provider.clients.Count > 0)
            {
                //**C_CHECK**//
                if (((DateTime.Now - LastMassAirdrop.Value).TotalSeconds > Configuration.Instance.MassDropTime) && (Provider.clients.Count > (Configuration.Instance.MassDropPlayersRequired - 1)))
                {
                    UnturnedChat.Say(Translate("bigdrop"), BigdropC);
                    LastMassAirdrop = DateTime.Now;
                    MassDrops = UnityEngine.Random.Range(5, 8);
                    return;
                }

                //**C_ANNOUNCE**//
                if (((DateTime.Now - LastMassMessage.Value).TotalSeconds > Configuration.Instance.MassDropAnnounceTime) && ((DateTime.Now - LastMassAirdrop.Value).TotalSeconds > (Configuration.Instance.MassDropTime - 1)) && (Provider.clients.Count > (Configuration.Instance.MassDropPlayersSendAnnounce - 1)) )
                {
                    UnturnedChat.Say(Translate("airannounce", Configuration.Instance.MassDropPlayersRequired), BigdropAnnounceC);
                    LastMassMessage = DateTime.Now;
                }
            }

            //**FORCE_DF**//
            LevelManager.airdropFrequency = 900;
        }

        void SendDrop()
        {
            LevelManager.airdropFrequency = 0;
        }

        protected override void Load()
        {
            Rocket.Core.Logging.Logger.Log("Loading Uber Airdrops..");

            Instance = this;
            Rocket.Core.Logging.Logger.Log("- Instance Set");

            LastAirdrop        = DateTime.Now;
            LastMassAirdrop    = DateTime.Now;
            LastMassDropped    = DateTime.Now;
            LastMassMessage    = DateTime.Now;
            AirdropNextSeconds = UnityEngine.Random.Range(360, 840);
            Rocket.Core.Logging.Logger.Log("- Time Controls Set");

            AirdropC = UnturnedChat.GetColorFromName(Configuration.Instance.RegularDropAnnounceColor, Color.magenta);
            BigdropC = UnturnedChat.GetColorFromName(Configuration.Instance.MassDropAnnounceColor, Color.cyan);
            BigdropAnnounceC = UnturnedChat.GetColorFromName(Configuration.Instance.MassDropWarnColor, Color.cyan);
            Rocket.Core.Logging.Logger.Log("- Color Settings Loaded");

            Rocket.Core.Logging.Logger.Log("Uber Airdrops Loaded\n----------------------------------");
        }

        protected override void Unload()
        {

        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList(){
                    {"airdrop", "Airdrop Incoming!"},
                    {"bigdrop", "Mass Airdrop Incoming!"},
                    {"airannounce", "Mass-Airdrop @ {0} Players!" }
                };
            }
        }
    }
}
