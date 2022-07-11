namespace Warships.Infrastructure.Wrappers
{
    public class ConsoleWrapper : IConsole
    {
        public void WriteLine(string? input = null)
        {
            Console.WriteLine(input);
        }

        public string? ReadLine()
        {
            return Console.ReadLine();
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}