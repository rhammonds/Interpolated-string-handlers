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
using Serilog;
using Serilog.Formatting.Compact;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Interpolated_string_handlers
{
    //https://habr.com/en/post/591171/
    //https://prog.world/structural-logging-and-interpolated-strings-in-c-10/
    //https://messagetemplates.org/
    //https://marketplace.visualstudio.com/items?itemName=Suchiman.SerilogAnalyzer
    internal class ProgramOther
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(SalutionTemplate);                       

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .WriteTo.Console(new CompactJsonFormatter())
                .CreateLogger();

            //using var loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog(dispose: true));
            //var serilogger = loggerFactory.CreateLogger<Program>();

            using var defaultLoggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddJsonConsole(
                    options =>
                    options.JsonWriterOptions = new JsonWriterOptions()
                    {
                        Indented = true
                    });
                builder.SetMinimumLevel(LogLevel.Error);
            });
            var defaultLogger = defaultLoggerFactory.CreateLogger<ProgramOther>();



            //var serviceCollection = new ServiceCollection();
            //ConfigureServices(serviceCollection);
            //var serviceProvider = serviceCollection.BuildServiceProvider();
            //var logger2 = serviceProvider.GetService<ILogger<Program>>();

            var myLogger = new Logger() { EnabledLevel = LogLevel.Error };

            var time = DateTime.Now;
            var name = "John";
            var list = new List<string> { "a", "b", "c" };
            var numbers = Enumerable.Range(0, 10).ToArray();
            var letters = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" }.ToArray();
            var user = new User { FirstName = "John", LastName = "Smith" };
            var userList = new List<User> { new User { FirstName = "John", LastName = "Smith" }, new User { FirstName = "Bob", LastName = "Woods" }, new User { FirstName = "Greg", LastName = "Gaines" } };

            //serilogger.LogInformation($"Hello {name}.");
            //serilogger.Log(LogLevel.Information, $"Hello {name}.");
            //serilogger.Log2(LogLevel.Information, $"Hello {name}.");

            var segment = new Segment(new(1, 2), new(4, 6));
            var length = segment.GetLength();
            //serilogger.LogInformation("The length of segment {@segment} is {length}.", segment, length);

            Log.Information($"#0 The User is {user}");
            Console.WriteLine();
            defaultLogger.LogInformation($"#1 The User is {user}");
            Console.WriteLine();
            defaultLogger.LogInformation("#2 The User is {@user}", user);
            Console.WriteLine();
            defaultLogger.LogStructured(LogLevel.Information, $"#3 The User is {user}");
            Console.WriteLine();
            defaultLogger.LogNonStructured(LogLevel.Information, $"#4 The User is {user}");
            Console.WriteLine();

            myLogger.GetMessage(LogLevel.Information, $"Error Level. CurrentTime: {time:t}. This is an error. It will be printed.");
            //logger.GetMessage(LogLevel.Error, $"Error Level. CurrentTime: {time}. This is an error. It will be printed.");
            myLogger.GetMessage(LogLevel.Information, $"{name}  The time doesn't use formatting.");
            myLogger.GetMessage(LogLevel.Information, $"{name} CurrentTime: {time}. The time doesn't use formatting.");
            myLogger.GetMessage(LogLevel.Warning, "Warning Level. This warning is a string, not an interpolated string expression.");
            myLogger.GetMessage(LogLevel.Warning, $"Call {list} For A Good Time!");
            //logger.LogMessage(LogLevel.Warning, numbers, $"Call ({0..3}) {3..6}-{6..9} For A Good Time!");
            myLogger.GetMessage(LogLevel.Information, $"user is {user} ");

            //logger.LogMessage(LogLevel.Trace, $"Trace Level. CurrentTime: {time}. This won't be printed.");



            Numbers.Write($"Call {numbers:(###) ###-####} For A Good Time!");
            Numbers.Write('x', $"Call {letters:(xxx) xxx-xxxx} For A Good Time!");
            var range = Numbers.Range(numbers, $"Call ({0..3}) {3..6}-{6..10} For A Good Time!");
            Console.WriteLine(range);
            //var users = Write($"List of users {userList}").ToString();
            //The InterpolatedStringHandler can be used to build strings
            var users = Write($"List of users {userList: lf}").ToString();
            Console.WriteLine(users);
            //x.ItIsTrue(DateTime.Now.Day == 30, $"Today is not {30}");




        }

        public static string Write(UserInterpolatedStringHandler handler)
        {
            return handler.GetFormattedText();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure =>
            {
                configure.ClearProviders();
                configure.AddSerilog();
                configure.SetMinimumLevel(LogLevel.Critical);
            }
            )
            .AddTransient<ProgramOther>();

        }
    }

    public record Point(double X, double Y);

    public record Segment(Point Start, Point End)
    {
        public double GetLength()
        {
            var dx = Start.X - End.X;
            var dy = Start.Y - End.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}


