using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Bede.Go.Contracts;
using Dapper;

namespace Bede.Go.Core.Services
{
    public class GameCrudService :ICrudService<Game>
    {
        private readonly IDbConnection _connection = new SqlConnection(ConfigurationManager.AppSettings["BedeGoConnectionString"]);
        
        public async Task Create(Game entity)
        {
            const string createGameSql = "INSERT INTO [Games] (Name, StartTime, PrizePot, EntryFee, CurrencyCode)" +
                                         "VALUES (@GameName, @GameStartTime, @GamePrizePot, @GameEntryFee, @GameCurrencyCode)" +
                                         "SELECT @@SCOPE_IDENTITY";
            var createGameParameters = new
            {
                GameName = entity.Name,
                GameStartTime = entity.StartTime,
                GamePrizePot = entity.PrizePot,
                GameEntryGee = entity.Entryfee,
                GameEntryFee = entity.CurrencyCode
            };
            var createGameCommand = new CommandDefinition(createGameSql, createGameParameters);
            var gameId = (await _connection.QueryAsync<int>(createGameCommand)).Single();

            const string createLocationsSql = "INSERT INTO [Locations] (Longitiude, Latitude, Accuracy, GameId)" +
                                              "VALUES (@LocationLongitiude, @LocationLatitude, @LocationAccuracy)";
            foreach (var location in entity.Locations)
            {
                var locationParameters = new
                {
                    LocationLongitiude = location.Longitude,
                    LocationLatitude = location.Latitude,
                    LocationAccuracy = location.Accuracy,
                    GameId = gameId
                };
                var createLocationCommand = new CommandDefinition(createLocationsSql, locationParameters);
                await _connection.QueryAsync(createLocationCommand);
            }
        }

        public async Task<Game> Read(long id)
        {
            const string getGameSql = "SELECT * FROM [Games] WHERE [Id] = @GameId";
            var getGameParameters = new
            {
                GameId = id
            };
            var getGameCommand = new CommandDefinition(getGameSql, getGameParameters);
            var game = (await _connection.QueryAsync<Game>(getGameCommand)).Single();
            var parentGameParameters = new
            {
                ParentGameId = game.Id
            };
            
            const string getLocationsSql = "SELECT * FROM [Locations] WHERE [GameId] = @ParentGameId";
            var getLocationsCommand = new CommandDefinition(getLocationsSql, parentGameParameters);
            IEnumerable<Location> locations = await _connection.QueryAsync<Location>(getLocationsCommand);
            game.Locations = locations;
            
            const string getPlayersSql = "SELECT * FROM [Players] WHERE [GameId] = @ParentGameId";
            var getPlayersCommand = new CommandDefinition(getPlayersSql, parentGameParameters);
            IEnumerable<Player> players = await _connection.QueryAsync<Player>(getPlayersCommand);
            game.Players = players;

            return game;
        }

        public async Task<IQueryable<Game>> Read(long[] ids)
        {
            List<Game> games = new List<Game>();
            foreach (var id in ids)
            {
                games.Add(await Read(id));
            }
            return games.AsQueryable();
        }
    
        public async Task Update(Game entity)
        {
            const string updateGameSql = "UPDATE [Games]" +
                                         "SET Name = @GameName, StartTime = @GameStartTime, PrizePot = @GamePrizePot, EntryFee = @GameEntryFee, CurrencyCode = @GameCurrencyCode" +
                                         "WHERE [Id] = @GameId";
            var updateGameParameters = new
            {
                GameName = entity.Name,
                GameStartTime = entity.StartTime,
                GamePrizePot = entity.PrizePot,
                GameEntryGee = entity.Entryfee,
                GameEntryFee = entity.CurrencyCode,
                GameId = entity.Id
            };
            var updateGameCommand = new CommandDefinition(updateGameSql, updateGameParameters);
            await _connection.QueryAsync(updateGameCommand);
            
            foreach (var location in entity.Locations)
            {
                const string updateLocationIfNotExistsSql = "If NOT EXISTS (SELECT * [Locations] WHERE" +
                                                                "[GameId] = @ParentGameId AND" +
                                                                "[Longitiude] = @LocationLongitude AND" +
                                                                "[Latitiude] = @LocationLatitude)" +
                                                            "BEGIN" +
                                                                "INSERT INTO [Locations] (Longitude, Latitude, GameId)" +
                                                                "VALUES (@LocationLongitude, @LocationLatitude, @ParentGameId)" +
                                                            "END";
                var updateLocationParameters = new
                {
                    LocationLongitude = location.Longitude,
                    LocationLatitiude = location.Latitude,
                    ParentGameId = entity.Id
                };
                var updateLocationCommand = new CommandDefinition(updateLocationIfNotExistsSql, updateLocationParameters);
                await _connection.QueryAsync(updateGameCommand);
                
            }
            
            foreach (var player in entity.Players)
            {
                const string updatePlayerIfNotExistsSql = "If NOT EXISTS (SELECT * [Player] WHERE" +
                                                              "[GameId] = @ParentGameId AND" +
                                                              "[Email] = @PlayerEmail)" +
                                                          "BEGIN" +
                                                              "INSERT INTO [Players] (Email, GameId)" +
                                                              "VALUES (@PlayerEmail, @ParentGameId)" +
                                                          "END";
                var updatePlayerParameters = new
                {
                    PlayerEmail = player.Email,
                    ParentGameId = entity.Id
                };
                var updatePlayerCommand = new CommandDefinition(updatePlayerIfNotExistsSql, updatePlayerParameters);
                await _connection.QueryAsync(updatePlayerCommand);
            }

        }

        public Task Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IQueryable<Game>> Query()
        {
            const string getAllGamesSql = "SELECT * from [Games]";
            var getAllGamesCommand = new CommandDefinition(getAllGamesSql);
            IEnumerable<Game> games = await _connection.QueryAsync<Game>(getAllGamesCommand);
            
            foreach (var game in games)
            {
                var parentGameParameters = new
                {
                    ParentGameId = game.Id
                };

                const string getLocationsSql = "SELECT * FROM [Locations] WHERE [GameId] = @ParentGameId";
                var getLocationsCommand = new CommandDefinition(getLocationsSql, parentGameParameters);
                IEnumerable<Location> locations = await _connection.QueryAsync<Location>(getLocationsCommand);
                game.Locations = locations;

                const string getPlayersSql = "SELECT * FROM [Players] WHERE [GameId] = @ParentGameId";
                var getPlayersCommand = new CommandDefinition(getPlayersSql, parentGameParameters);
                IEnumerable<Player> players = await _connection.QueryAsync<Player>(getPlayersCommand);
                game.Players = players;
            }
            return games.AsQueryable();
        }
    }
}