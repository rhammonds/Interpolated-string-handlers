using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Templates;
using Serilog.Templates.Themes;

namespace Interpolated_string_handlers
{
    internal class ProgramSerilog
    {
        private const string Salutation = "Welcome";
        public const string SalutionTemplate = $"{Salutation} to Interpolation";

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(new ExpressionTemplate("{ {Time:@t, Template:@mt, Rendered:@m, ..@p, SourceContext: undefined()} }\n", null, null, TemplateTheme.Code))
                //.WriteTo.Console(new ExpressionTemplate("{ {Rendered:@m} }\n", null, null, TemplateTheme.Code))
                .CreateLogger();

            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddSerilog(dispose: true);
            });
            var serilogger = loggerFactory.CreateLogger<ProgramSerilog>();

            //Setup data
            var starWarsPeopleList = new List<User> { new User { FirstName = "Luke", LastName = "Skywalker" }, new User { FirstName = "Darth", LastName = "Vader" }, new User { FirstName = "Leia", LastName = "Organa" } };
            starWarsPeopleList.FirstOrDefault(i => i.FirstName == "Luke");
            var title = "List of Star Wars people";
            //Act
            //Log with ILogger using interpolation - interpolation not supported in ILogger.
            serilogger.LogDebug($"#1 {title}: {starWarsPeopleList}");
            Console.WriteLine();

            //Log with ILogger using composite formatting - structured - @ symbol is required to get json structure
            serilogger.LogDebug("#2 {title}: {abc}", title, starWarsPeopleList);
            Console.WriteLine();
            Console.ReadKey();

            //Use InterpolatedStringHandler and structure logging
            serilogger.LogStructured(LogLevel.Debug, $"#3 {title}: {starWarsPeopleList}");
            Console.WriteLine();

            //Use InterpolatedStringHandler and non structure logging - handler must support formatting the object type
            serilogger.LogNonStructured(LogLevel.Debug, $"#4 {title}: {starWarsPeopleList}");
            Console.WriteLine();
            Console.ReadKey();
 
        }


         
    }

}


