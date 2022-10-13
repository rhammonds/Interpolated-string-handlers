using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Interpolated_string_handlers
{
    [InterpolatedStringHandler]
    public readonly struct PlaceholderInterpolatedStringHandler
    {
        private char Placeholder { get; }
        private StringBuilder Builder { get; }

        public PlaceholderInterpolatedStringHandler(int literalLength, int formattedCount, char placeholder = '#')
            => (Placeholder, Builder) = (placeholder, new StringBuilder());

        public void AppendLiteral(string s) => Builder.Append(s);
        internal string GetFormattedText() => Builder.ToString();

        public void AppendFormatted(IEnumerable t, string format)
        {
            var enumerator = t.GetEnumerator();
            foreach (var c in format)
            {
                if (c == Placeholder && enumerator.MoveNext())
                    Builder.Append(enumerator.Current);
                else
                    Builder.Append(c);
            }
        }
    }
}
