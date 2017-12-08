using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Bede.Go.Contracts;
using Dapper;

namespace Bede.Go.Core.Services
{
    public class GameResultCrudService : ICrudService<GameResult>
    {
        private readonly IDbConnection _connection = new SqlConnection(ConfigurationManager.AppSettings["BedeGoConnectionString"]);

        public async Task Create(GameResult entity)
        {
            const string createGameResultSql = "INSERT INTO [GameResults] (GameId, LocationId)" +
                                               "VALUES (@ParentGameId, @ParentLocationId)";
            var createGameResultParameters = new
            {
                ParentGameId = entity.GameId,
                ParentLocationId = entity.LocationId
            };
            var createGameResultCommand = new CommandDefinition(createGameResultSql, createGameResultParameters);
            await _connection.QueryAsync(createGameResultCommand);
        }

        public Task<GameResult> Read(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IQueryable<GameResult>> Read(long[] ids)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(GameResult entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IQueryable<GameResult>> Query()
        {
            const string getAllGameResultsSql = "SELECT * FROM [GameResults]";
            var getAllGameResultsCommand = new CommandDefinition(getAllGameResultsSql);
            var gameResults = await _connection.QueryAsync<GameResult>(getAllGameResultsCommand);
            return gameResults.AsQueryable();
        }
    }
}