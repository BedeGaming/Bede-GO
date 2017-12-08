using Bede.Go.Contracts.Interfaces;

namespace Bede.Go.Contracts
{
    public class GameResult : IIdentifiable
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int LocationId { get; set; }
    }
}