using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Interpolated_string_handlers
{
    public static partial class LoggerExtensions
    {
        public static void LogStructured(this ILogger logger, LogLevel logLevel, 
            [InterpolatedStringHandlerArgument("logger", "logLevel")] ref StructuredLoggingInterpolatedStringHandler handler)
        {
            if (handler.IsEnabled)
            {
                //this is passing the a template and arguments to the Log method
                var (template, arguments) = handler.GetTemplateAndArguments();
                logger.Log(logLevel, template, arguments);
            }
        }


        public static void LogNonStructured(this ILogger logger, LogLevel level,
            [InterpolatedStringHandlerArgument("logger", "level")] ref LogInterpolatedStringHandler handler)
        {
            //only log certain levels
            if (handler.IsEnabled)
            {
                //this is just using a string builder to combine all the text and pass to the 
                logger.Log(level, handler.GetFormattedText());
            }
        }
    }
}
