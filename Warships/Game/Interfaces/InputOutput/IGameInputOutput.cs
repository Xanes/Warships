namespace Warships.Game.Interfaces.InputOutput
{
    public interface IGameInputOutput
    {
        public void RenderMessage(string message);

        string? AskForInput(string? question = null);
    }
}