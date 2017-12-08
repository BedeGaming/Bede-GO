namespace Bede.Go.Host.Models
{
    public class UpdatePlayerLocationRequest : GetGamesRequest
    {
        public int GameId { get; set; }
    }
}