using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpolated_string_handlers.InterpolatedStringHandler
{
    internal class ProgramListUserInfo
    {
        static void Main(string[] args)
        {
            
            var userList = new List<User> { new User { FirstName = "Luke", LastName = "Skywalker" }, new User { FirstName = "Darth", LastName = "Vader" } };
            var users = Write($"List of Star Wars people: {userList: lf}").ToString();
            Console.WriteLine(users);
        }

        public static string Write(UserInterpolatedStringHandler handler)
        {
            return handler.GetFormattedText();
        }
    }
}
