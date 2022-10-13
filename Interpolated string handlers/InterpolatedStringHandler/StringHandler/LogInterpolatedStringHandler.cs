using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using System.Text;

namespace Interpolated_string_handlers
{
    [InterpolatedStringHandler]
    public ref struct LogInterpolatedStringHandler
    {
        public readonly bool IsEnabled; //reduce overhead as needed
        private readonly StringBuilder builder = null!;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="literalLength">String length</param>
        /// <param name="formattedCount">Number of interpolated values</param>
        /// <param name="logger"></param>
        /// <param name="logLevel"></param>
        public LogInterpolatedStringHandler(int literalLength, int formattedCount, ILogger logger, LogLevel logLevel)
        {
            IsEnabled = logger.IsEnabled(logLevel); //get enabled differently
            if (!IsEnabled) return; 
            builder = new(literalLength);
        }

        public void AppendLiteral(string s)
        {
            if (!IsEnabled) return; 
            builder.Append(s);
        }
        public void AppendFormatted<T>(T t)
        {
            if (!IsEnabled) return;  
            builder.Append(t?.ToString());
        }

        public void AppendFormatted<T>(T t, string format) where T : IFormattable
        {
            if (!IsEnabled) return;
            builder.Append(t?.ToString(format, null));
        }
      
        internal string GetFormattedText() => builder.ToString();
    }
}
