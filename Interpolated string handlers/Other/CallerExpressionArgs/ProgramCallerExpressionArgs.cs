using System.Runtime.CompilerServices;

namespace Interpolated_string_handlers
{
    internal class ProgramCallerExpressionArgs
    {
        static void Main(string[] args)
        {
            Evaluate(1512 - 19 * 7, "my message");
            Console.WriteLine();

            var message = "Hello";
            Message(message);
            Console.WriteLine();

            Log("My message");
            Console.WriteLine();
        }

        static void Evaluate(int value, string value2, [CallerArgumentExpression("value")] string? expression = null)
        {
            Console.WriteLine($"{expression} = {value}");
        }

        static void Message(string value, [CallerArgumentExpression("value")] string? expression = null)
        {
            Console.WriteLine($"The value of the variable '{expression}' is '{value}'");
        }

        static void Log(string message, [CallerMemberName] string? callerMemberName = null, [CallerFilePath] string callerFilePath = null)
        {
            Console.WriteLine($"{message} - called from '{callerMemberName}', in file '{callerFilePath}'");
        }

    }
}


