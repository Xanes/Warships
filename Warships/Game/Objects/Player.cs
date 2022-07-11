namespace Warships.Game.Objects
{
    public class Player
    {
        private List<Field> _choises { get; }
        public IReadOnlyCollection<Field> Choises => _choises;

        public Player()
        {
            _choises = new List<Field>();
        }


        public void AddChoice(Field choice)
        {
            _choises.Add(choice);
        }
    }
}