namespace Big2.Domain.Games
{
    public class Player
    {
        public Guid ID { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public int Order { get; private set; }

        public bool IsFinished { get; private set; }

        private Player() { }

        public static Player CreateNewOne(string name)
        {
            Player player = new()
            {
                Name = name,
                ID = Guid.NewGuid(),
                IsFinished = false,
                Order = 0,
            };

            return player;
        }

        public void SetOrder(int order)
        {
            Order = order;
        }

        public void SetFinished()
        {
            IsFinished = true;
        }
    }
}
