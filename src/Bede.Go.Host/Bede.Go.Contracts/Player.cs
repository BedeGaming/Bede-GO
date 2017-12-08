using Bede.Go.Contracts.Interfaces;

namespace Bede.Go.Contracts
{
    public class Player : IIdentifiable
    {
        public int Id { get; set; }
        public string Email  { get; set; }
        public int GameId { get; set; }
    }
}