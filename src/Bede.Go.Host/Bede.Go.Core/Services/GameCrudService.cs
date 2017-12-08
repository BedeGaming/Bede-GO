using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Bede.Go.Contracts;
using Dapper;

namespace Bede.Go.Core.Services
{
    public class GameCrudService :ICrudService<Game>
    {
        private readonly Func<Task<IDbConnection>> _connectionFunc;

        public GameCrudService(Func<Task<IDbConnection>> connectionFunc)
        {
            _connectionFunc = connectionFunc;
        }

        public async Task Create(Game entity)
        {
            const string createGameSql = "INSERT INTO [dbo.Games] (Name, StartTime, PrizePot, EntryFee, CurrencyCode)" +
                                         "VALUES (GameName, GameStartTime, GamePrizePot, GameEntryFee, GameCurrencyCode)" +
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
            int gameId;
            using (var connection = await _connectionFunc())
            {
                gameId = (await connection.QueryAsync<int>(createGameCommand)).Single();
            }

            const string createLocationsSql = "INSERT INTO [dbo.Locations] (Longitiude, Latitude, Accuracy, GameId)" +
                                              "VALUES (LocationLongitiude, LocationLatitude, LocationAccuracy)";
            foreach (var location in entity.Locations)
            {
                var locationParameters = new
                {
                    LocationLongitiude = location.Longitude,
                    LocationLatitude = location.Latitude,
                    LocationAccuracy = location.Accuracy,
                    GameId = gameId
                };
                var createdLocationCommand = new CommandDefinition(createLocationsSql, locationParameters);
                using (var connection = await _connectionFunc())
                {
                    await connection.QueryAsync(createLocationsSql);
                }
            }
        }

        public async Task<Game> Read(long id)
        {
            const string getGameSql = "SELECT * FROM [dbo.Games] WHERE [Id] = GameId";
            var getGameParameters = new
            {
                GameId = id
            };
            var getGameCommand = new CommandDefinition(getGameSql, getGameParameters);
            Game game;
            using (var connection = await _connectionFunc())
            {
                game = (await connection.QueryAsync<Game>(getGameCommand)).Single();
            }
            var parentGameParameters = new
            {
                ParentGameId = game.Id
            };
            
            const string getLocationsSql = "SELECT * FROM [dbo.Locations] WHERE [GameId] = ParentGameId";
            var getLocationsCommand = new CommandDefinition(getLocationsSql, parentGameParameters);
            IEnumerable<Location> locations;
            using (var connection = await _connectionFunc())
            {
                locations = await connection.QueryAsync<Location>(getLocationsCommand);
            }
            game.Locations = locations;
            
            const string getPlayersSql = "SELECT * FROM [dbo.Players] WHERE [GameId] = ParentGameId";
            var getPlayersCommand = new CommandDefinition(getPlayersSql, parentGameParameters);
            IEnumerable<Player> players;
            using (var connection = await _connectionFunc())
            {
                players = await connection.QueryAsync<Player>(getPlayersCommand);
            }
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
            const string updateGameSql = "UPDATE [dbo.Games]" +
                                         "SET Name = GameName, StartTime = GameStartTime, PrizePot = GamePrizePot, EntryFee = GameEntryFee, CurrencyCode = GameCurrencyCode" +
                                         "WHERE [Id] = GameId";
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
            using (var connection = await _connectionFunc())
            {
                await connection.QueryAsync(updateGameCommand);
            }
            
            foreach (var location in entity.Locations)
            {
                const string updateLocationIfNotExistsSql = "If NOT EXISTS (SELECT * [dbo.Locations] WHERE" +
                                                                "[GameId] = ParentGameId AND" +
                                                                "[Longitiude] = LocationLongitude AND" +
                                                                "[Latitiude] = LocationLatitude)" +
                                                            "BEGIN" +
                                                                "INSERT INTO [dbo.Locations] (Longitude, Latitude, GameId)" +
                                                                "VALUES (LocationLongitude, LocationLatitude, ParentGameId)" +
                                                            "END";
                var updateLocationParameters = new
                {
                    LocationLongitude = location.Longitude,
                    LocationLatitiude = location.Latitude,
                    ParentGameId = entity.Id
                };
                var updateLocationCommand = new CommandDefinition(updateLocationIfNotExistsSql, updateLocationParameters);
                using (var connection = await _connectionFunc())
                {
                    await connection.QueryAsync(updateGameCommand);
                }
            }
            
            foreach (var player in entity.Players)
            {
                const string updatePlayerIfNotExistsSql = "If NOT EXISTS (SELECT * [dbo.Player] WHERE" +
                                                              "[GameId] = ParentGameId AND" +
                                                              "[Email] = PlayerEmail)" +
                                                          "BEGIN" +
                                                              "INSERT INTO [dbo.Players] (Email, GameId)" +
                                                              "VALUES (PlayerEmail, ParentGameId)" +
                                                          "END";
                var updatePlayerParameters = new
                {
                    PlayerEmail = player.Email,
                    ParentGameId = entity.Id
                };
                var updatePlayerCommand = new CommandDefinition(updatePlayerIfNotExistsSql, updatePlayerParameters);
                using (var connection = await _connectionFunc())
                {
                    await connection.QueryAsync(updatePlayerCommand);
                }
            }

        }

        public Task Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IQueryable<Game>> Query()
        {
            const string getAllGamesSql = "SELECT * from [dbo.Games]";
            var getAllGamesCommand = new CommandDefinition(getAllGamesSql);
            IEnumerable<Game> games;
            using (var connection = await _connectionFunc())
            {
                games = await connection.QueryAsync<Game>(getAllGamesCommand);
            }

            foreach (var game in games)
            {
                var parentGameParameters = new
                {
                    ParentGameId = game.Id
                };

                const string getLocationsSql = "SELECT * FROM [dbo.Locations] WHERE [GameId] = ParentGameId";
                var getLocationsCommand = new CommandDefinition(getLocationsSql, parentGameParameters);
                IEnumerable<Location> locations;
                using (var connection = await _connectionFunc())
                {
                    locations = await connection.QueryAsync<Location>(getLocationsCommand);
                }
                game.Locations = locations;

                const string getPlayersSql = "SELECT * FROM [dbo.Players] WHERE [GameId] = ParentGameId";
                var getPlayersCommand = new CommandDefinition(getPlayersSql, parentGameParameters);
                IEnumerable<Player> players;
                using (var connection = await _connectionFunc())
                {
                    players = await connection.QueryAsync<Player>(getPlayersCommand);
                }
                game.Players = players;
            }
            return games?.AsQueryable();
        }
    }
}