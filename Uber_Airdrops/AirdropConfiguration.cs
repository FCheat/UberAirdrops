using Rocket.API;

namespace Uber_Airdrops
{
    public class AirdropConfiguration : IRocketPluginConfiguration
    {
        public bool RegularAirdropsEnabled;
        public float AirdropTimeMinimum;
        public float AirdropTimeMaximum;

        public bool MassDropsEnabled;
        public double MassDropTime;
        public double MassDropAnnounceTime;
        public int MassDropPlayersRequired;
        public int MassDropPlayersSendAnnounce;

        public string RegularDropAnnounceColor;
        public string MassDropAnnounceColor;
        public string MassDropWarnColor;

        public void LoadDefaults()
        {
            RegularAirdropsEnabled = true;
            AirdropTimeMinimum = 360;   //6 Minutes
            AirdropTimeMaximum = 840;   //14 Minutes

            MassDropsEnabled = true;
            MassDropTime = 3600;        //1 Hours
            MassDropAnnounceTime = 480; //8 Minutes
            MassDropPlayersRequired = 20;
            MassDropPlayersSendAnnounce = 16;

            RegularDropAnnounceColor = "magenta";
            MassDropAnnounceColor = "cyan";
            MassDropWarnColor = "cyan";
        }
    }
}
