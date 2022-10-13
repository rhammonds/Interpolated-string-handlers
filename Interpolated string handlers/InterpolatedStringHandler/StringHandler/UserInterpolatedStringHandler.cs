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
    public readonly struct UserInterpolatedStringHandler
    {

        private StringBuilder builder { get; }

        public UserInterpolatedStringHandler(int literalLength, int formattedCount)
            => (builder) = (new StringBuilder());

        public void AppendLiteral(string s) => builder.Append(s);
        
        public void AppendFormatted(IEnumerable<User> t)
        {
            var enumerator = t.GetEnumerator();
            while (enumerator.MoveNext())
            {
                builder.Append("User: ");
                builder.Append(enumerator.Current?.ToString());
                builder.AppendLine();
            }
        }
        public void AppendFormatted(IEnumerable<User> t, string format)
        {
            var enumerator = t.GetEnumerator();
            while (enumerator.MoveNext())
            {
                foreach (var c in format)
                {
                    if(c=='f')
                    {
                        builder.Append("First Name: ");
                        builder.Append(enumerator.Current.FirstName);
                        builder.Append(", ");
                    }
                    if (c == 'l')
                    {
                        builder.Append("Last Name: ");
                        builder.Append(enumerator.Current.LastName);
                        builder.Append(", ");
                    }
                }

                builder.AppendLine();
            }
        }
        internal string GetFormattedText() => builder.ToString();

    }
}
