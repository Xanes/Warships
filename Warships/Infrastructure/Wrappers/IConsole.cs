namespace Warships.Infrastructure.Wrappers
{
    public interface IConsole
    {
        string? ReadLine();

        void WriteLine(string? input = null);

        void Clear();
    }
}