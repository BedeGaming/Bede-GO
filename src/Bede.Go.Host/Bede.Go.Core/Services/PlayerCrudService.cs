using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Bede.Go.Contracts;
using Dapper;

namespace Bede.Go.Core.Services
{
    public class PlayerCrudService : ICrudService<Player>
    {
        private readonly IDbConnection _connection = new SqlConnection(ConfigurationManager.AppSettings["BedeGoConnectionString"]);

        public Task Create(Player entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Player> Read(long id)
        {
            const string readPlayerSql = "SELECT * FROM [dbo.Players] Where [Id] = PlayerId";
            var readPlayerParameters = new
            {
                PlayerId = id
            };
            var readPlayerCommand = new CommandDefinition(readPlayerSql, readPlayerParameters);
            return (await _connection.QueryAsync<Player>(readPlayerCommand)).Single();
        }

        public Task<IQueryable<Player>> Read(long[] ids)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Player entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IQueryable<Player>> Query()
        {
            throw new System.NotImplementedException();
        }
    }

}