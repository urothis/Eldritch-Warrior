using NLog;

using NWN.API;
using NWN.Services;

namespace Services.Client
{
    [ServiceBinding(typeof(Login))]
    public class Login
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public Login() => NwModule.Instance.OnClientEnter += enter =>
        {
            if (enter.Player.ClientCheckName(enter.Player.Name)) return;
        };
    }
}