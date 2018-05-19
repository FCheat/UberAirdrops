using System.Collections.Generic;
using Rocket.API;

namespace Uber_Airdrops
{
    public class AirdropCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public string Name
        {
            get { return "adrop"; }
        }

        public string Help
        {
            get { return "Sends an airdrop"; }
        }

        public string Syntax
        {
            get { return "/adrop"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            Uber_AirdropsMain.Instance.ForceADrop = true;
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>
                {
                  "uber.adrop"
                };
            }
        }
    }
}
