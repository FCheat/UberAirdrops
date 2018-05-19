using System.Collections.Generic;
using Rocket.API;
using System.Globalization;
using UnityEngine;

namespace Uber_Airdrops
{
    public class MassAirdropCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public string Name
        {
            get { return "mdrop"; }
        }

        public string Help
        {
            get { return "Sends a mass airdrop"; }
        }

        public string Syntax
        {
            get { return "/adrop <drops>"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length > 0)
            {
                if (command[0] != "")
                    Uber_AirdropsMain.Instance.MassDrops = int.Parse(command[0], CultureInfo.InvariantCulture);
            }
            else
                Uber_AirdropsMain.Instance.MassDrops = Random.Range(5, 8);

            Rocket.Unturned.Chat.UnturnedChat.Say(Uber_AirdropsMain.Instance.Translate("bigdrop"), Uber_AirdropsMain.Instance.BigdropC);
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>
                {
                  "uber.massdrop"
                };
            }
        }
    }
}
