using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Interpolated_string_handlers
{
    public static class Numbers
    {
        public static void Write(PlaceholderInterpolatedStringHandler builder)
        {
            Write('#', builder);
        }

        public static void Write(char placeholder, [InterpolatedStringHandlerArgument("placeholder")] PlaceholderInterpolatedStringHandler builder)
        {
            Console.WriteLine(builder.GetFormattedText());
        }

        public static string Range<T>(T[] args, [InterpolatedStringHandlerArgument("args")] RangeInterpolatedStringHandler<T> handler)
        {
            return handler.ToString();
        }
    }
}
