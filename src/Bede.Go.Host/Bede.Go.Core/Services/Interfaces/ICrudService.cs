using System.Collections.Generic;
using System.Threading.Tasks;
using Bede.Go.Contracts.Interfaces;

namespace Bede.Go.Core.Services.Interfaces
{
    public interface ICrudService<TType> where TType : IIdentifiable
    {
        Task Create<TTType>(TType entity);
        Task<TType> Read<TType>(long id);
        Task<IEnumerable<TType>> Read(long[] id);
        Task Update<TType>(TType entity);
        Task Delete<TType>(long id);
    }
}