using System;
using NLog;
using NWN.API;
using NWN.API.Events;
using NWN.Services;


namespace Services.Wildshape
{
    // The "ServiceBinding" attribute indicates this class should be created on start, and available to other classes as a dependency.
    // You can also bind yourself to an interface or base class. The system also supports multiple bindings.
    [ServiceBinding(typeof(Shifter))]
    public class Shifter
    {
        // Gets the server log. By default, this reports to "nwm.log"
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        // This function will be called as if the same script was called by a toolset event, or by another script.
        // Script name must be <= 16 characters similar to the toolset.
        // This function must always return void, or a bool in the case of a conditional.
        // The NwObject parameter is optional, but if defined, must always be a single parameter of the NWObject type.
        /*
        Define a method (OnScriptCalled) to be called when the NwScript "test_nwscript" would be called by the game.
        */
        
        [ScriptHandler("nw_s2_wildshape")]
        [ScriptHandler("x2_s2_gwildshp")]
        private void OnShift(CallInfo callInfo)
        {
            if (callInfo.TryGetEvent(out SpellEvents.OnSpellCast onCast))
            {
                Log.Info($"spell script cralled by {onCast}");
            }
        }
    }
}