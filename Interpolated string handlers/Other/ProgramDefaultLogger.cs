using Interpolated_string_handlers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Interpolated_string_handlers
{
    internal class ProgramDefaultLogger
    {
        static void Main(string[] args)
        {
            //Setup ILogger
            using var defaultLoggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddJsonConsole(

                    options =>
                    {
                        options.TimestampFormat = "hh:mm:ss ";
                        options.IncludeScopes = true;
                        options.JsonWriterOptions = new JsonWriterOptions()
                        {
                            Indented = true
                        };
                    });
                builder.SetMinimumLevel(LogLevel.Debug); //Minimum Logging level
            });
            var defaultLogger = defaultLoggerFactory.CreateLogger<ProgramDefaultLogger>();

            //Setup data
            var user = new User { FirstName = "Mary", LastName = "Jane" };
            var userList = new List<User> { new User { FirstName = "John", LastName = "Smith" }, new User { FirstName = "Bob", LastName = "Woods" }, new User { FirstName = "Greg", LastName = "Gaines" } };
           

            //interpolation not supported in ILogger.
            defaultLogger.LogDebug($"#1 The User List is {userList}"); 
            Console.WriteLine();

            //composite formatting
            defaultLogger.LogDebug("#2 The User List is {userList}", userList); 
            Console.WriteLine();

            //structured interpolation
            defaultLogger.LogStructured(LogLevel.Information, $"#3 The User List is {userList}");
            Console.WriteLine();

            //non-structured interpolation - handler must support formatting the object type
            defaultLogger.LogNonStructured(LogLevel.Information, $"#4 The User List is {user}");  
            // Console.WriteLine();
        }
    }
}
