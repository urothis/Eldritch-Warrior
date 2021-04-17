using NLog;
using NWN.API;
using NWN.Services;

namespace Services.Client
{
    [ServiceBinding(typeof(Enter))]
    public class Enter
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public Enter() => NwModule.Instance.OnClientEnter += enter =>
        {
            if (enter.Player.ClientCheckName(enter.Player.Name)) return;

            if (enter.Player.IsDM)
            {
                enter.Player.ValidateDM();
                return;
            }

            enter.Player.WelcomeMessage();
            enter.Player.RestoreHitPoints();
        };
    }
}