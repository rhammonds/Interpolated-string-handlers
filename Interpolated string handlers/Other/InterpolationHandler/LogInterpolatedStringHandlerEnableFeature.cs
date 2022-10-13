using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using System.Text;

namespace Interpolated_string_handlers
{
    [InterpolatedStringHandler]
    public ref struct LogInterpolatedStringHandlerEnableFeature
    {
        private readonly bool enabled; //reduce overhead as needed
        readonly StringBuilder builder;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="literalLength">String length</param>
        /// <param name="formattedCount">Number of interpolated values</param>
        /// <param name="logger"></param>
        /// <param name="logLevel"></param>
        public LogInterpolatedStringHandlerEnableFeature(int literalLength, int formattedCount, Logger logger, LogLevel logLevel)
        {
            enabled = logLevel>= logger.EnabledLevel   ;
            builder = new (literalLength);
        }

        public void AppendLiteral(string s)
        {
            if (!enabled) return; 
            builder.Append(s);
        }
        public void AppendFormatted<T>(T t)
        {
            if (!enabled) return;  
            builder.Append(t?.ToString());
        }

        public void AppendFormatted<T>(T t, string format) where T : IFormattable
        {
            if (!enabled) return;
            builder.Append(t?.ToString(format, null));
        }
      
        internal string GetFormattedText() => builder.ToString();
    }
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
