using System.Data;
using Bede.Go.Contracts.Interfaces;

namespace Bede.Go.Contracts
{
    public class Winner : IIdentifiable
    {
        public int Id { get; set; }

        public int GameId { get; set; } 
        public int PlayerId { get; set; }
    }
}