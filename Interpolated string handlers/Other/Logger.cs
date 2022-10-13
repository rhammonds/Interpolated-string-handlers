using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Interpolated_string_handlers
{
     public class Logger
    {
        public LogLevel EnabledLevel { get; init; } = LogLevel.Error;

        public string GetMessage(LogLevel level, string msg)
        {
            if (level < EnabledLevel) return null;
            return msg;
        }

        public string GetMessage(LogLevel level, [InterpolatedStringHandlerArgument("", "level")] LogInterpolatedStringHandlerEnableFeature builder)
        {
            //only log certain levels
            if (level < EnabledLevel) return null;
            return builder.GetFormattedText();
        }

        public string GetUserMessage(UserInterpolatedStringHandler handler)
        {
            return handler.GetFormattedText();
        }

    }
}
