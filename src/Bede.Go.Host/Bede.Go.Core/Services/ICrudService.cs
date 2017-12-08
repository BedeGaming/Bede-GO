using System.Linq;
using System.Threading.Tasks;
using Bede.Go.Contracts.Interfaces;

namespace Bede.Go.Core.Services
{
    public interface ICrudService<IType> where IType : IIdentifiable
    {
        Task Create(IType entity);
        Task<IType> Read(long id);
        Task<IQueryable<IType>> Read(long[] ids);
        Task Update(IType entity);
        Task Delete(long id);
        Task<IQueryable<IType>> Query();
    }
}