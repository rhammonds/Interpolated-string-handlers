using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Interpolated_string_handlers
{
    [InterpolatedStringHandler]
    public readonly struct RangeInterpolatedStringHandler<T>
    {
        private StringBuilder Builder { get; }
        private T[] Args { get; }

        public RangeInterpolatedStringHandler(int literalLength, int formattedCount, T[] args)
        {
            (Args, Builder) = (args, new StringBuilder());
        }

        public void AppendFormatted(Range range)
        {
            Builder.Append(string.Concat(Args[range]));
        }

        public void AppendLiteral(string value)
        {
            Builder.Append(value);
        }

        public string ToString() => Builder.ToString();
    }
}
