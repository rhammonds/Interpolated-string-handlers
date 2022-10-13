using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpolated_string_handlers.InterpolatedStringHandler
{
    internal class ProgramIssues
    {
        static void Main(string[] args)
        {
            x(1);
            x(0);
            var arg1 = "1";
            var arg2 = "2";
            var arg3 = "3";

            //error not caught
            //more args than positions in template. Not sure of the intent
            var s = string.Format("{0}", arg1, arg2); 
            s = string.Format("{0} {2}", arg1, arg2, arg3);
            s = string.Format("{0} {0}", arg1, arg2);

            //exception thrown
            s = string.Format("{0} {1}", arg1); //less args than positions in template, argument 1 missing
            s = string.Format("{1} {2}", arg1, arg2); //argument 2 missing
            s = string.Format("{abcd}", arg1, arg2); //bad format string

        }

        private void x(NonZeroInteger x)
        {

        }
    }
}
