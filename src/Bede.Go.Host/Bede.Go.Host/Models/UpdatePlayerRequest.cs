using System.ComponentModel.DataAnnotations;

namespace Bede.Go.Host.Models
{
    public class UpdatePlayerRequest : GetGamesRequest
    {
        [Required]
        public int GameId { get; set; }
    }
}