using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Bede.Go.Contracts;
using Dapper;

namespace Bede.Go.Core.Services
{
    public class WinnersCrudService : ICrudService<Winner>
    {
        private readonly IDbConnection _connection = new SqlConnection(ConfigurationManager.AppSettings["BedeGoConnectionString"]);

        public async Task Create(Winner entity)
        {
            const string createWinnerSql = "INSERT INTO [Winners] (GameId, PlayerId)" +
                                           "VALUES (@ParentGameId, @ParentPlayerId)" +
                                           "SELECT @@SCOPE_IDENTITY";
            var createWinnerParameters = new
            {
                ParentGameId = entity.GameId,
                ParentPlayerId = entity.PlayerId
            };
            var createWinnerCommand = new CommandDefinition(createWinnerSql, createWinnerParameters);
            await _connection.QueryAsync(createWinnerCommand);
        }
        
        public Task<Winner> Read(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IQueryable<Winner>> Read(long[] ids)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Winner entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IQueryable<Winner>> Query()
        {
            const string readAllWinnersSql = "SELECT * FROM [Winners]";
            var readAllWinnersCommand = new CommandDefinition(readAllWinnersSql);
            var winners = await _connection.QueryAsync<Winner>(readAllWinnersCommand);
            return winners.AsQueryable();
        }
    }
}