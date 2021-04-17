using NLog;

using NWN.API;
using NWN.Services;

namespace Services.Client
{
    [ServiceBinding(typeof(Leave))]
    public class Leave
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public Leave() => NwModule.Instance.OnClientLeave += leave =>
        {
            leave.Player.DeathLog();
            leave.Player.PrintLogout();
        };
    }
}