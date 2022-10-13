using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using System.Text;

namespace Interpolated_string_handlers
{
    [InterpolatedStringHandler]
    public ref struct StructuredLoggingInterpolatedStringHandler
    {
        public bool IsEnabled { get; } //reduce overhead as needed
        private readonly StringBuilder _builder = null!;
        private readonly List<object?> _arguments = null!;        

        /// <param name="literalLength"></param>
        /// <param name="formattedCount"></param>
        /// <param name="logger"></param>
        /// <param name="logLevel"></param>
        /// <param name="isEnabled">optional out parm. Setting to false indicates that the 
        /// handler shouldn't be called at all to process the interpolated string expression</param>
        public StructuredLoggingInterpolatedStringHandler(int literalLength, int formattedCount, ILogger logger, LogLevel logLevel, out bool isEnabled)
        {
            IsEnabled = isEnabled =  logger.IsEnabled(logLevel);
            if (!isEnabled) return;
            _builder = new(literalLength);
            _arguments = new(formattedCount);
        }

        public void AppendLiteral(string s)
        {
            _builder.Append(s.Replace("{", "{{", StringComparison.Ordinal).Replace("}", "}}", StringComparison.Ordinal));
        }

        public void AppendFormatted<T>(T value, [CallerArgumentExpression("value")] string name = "")
        {
            _arguments.Add(value);
            _builder.Append($"{{@{name}}}");
        }

        public (string, object?[]) GetTemplateAndArguments()
        {
            return (_builder.ToString(), _arguments.ToArray());
        }
    }
}
