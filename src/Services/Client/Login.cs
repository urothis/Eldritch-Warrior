using NLog;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

namespace Services.Client
{
    [ServiceBinding(typeof(Login))]
    public class Login
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public Login() => NwModule.Instance.OnClientEnter += enter =>
        {

        };
    }
}