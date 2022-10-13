using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpolated_string_handlers
{
    public record User : IFormattable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string ToString()
        {
            return string.Format("{0} {1} ", FirstName, LastName);
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if(format == null) return string.Format("{0} {1}", FirstName, LastName);
            switch (format.ToLower())
            {
                // John Doe
                case "f":
                    return string.Format("{0} {1}", FirstName, LastName);
                // Doe, John
                case "lf":
                    return string.Format("{0}, {1}", LastName, FirstName);
                // 123456: John Doe

                default:
                    return ToString();
            }
        }
    }
}
