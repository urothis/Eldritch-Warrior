using System;
using System.Collections.Generic;

using NLog;

using NWN.API;
using NWN.API.Constants;
using NWN.Services;
using NWNX.API.Events;
using NWNX.Services;

namespace Services.Examine
{
    [ServiceBinding(typeof(ExamineObject))]
    public class ExamineObject
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ExamineObject(NWNXEventService nWNX) => nWNX.Subscribe<ExamineEvents.OnExamineObjectBefore>(OnExamineObjectBefore);

        public static void OnExamineObjectBefore(ExamineEvents.OnExamineObjectBefore onExamineObject)
        {
            logger.Info("HELLO onExamineObject");
        }
    }
}