using FirstWebAppInDocker.Models.Entities;
namespace FirstWebAppInDocker.DataAccess.Repositories
{
    public interface IResourceRepository
    {
        Task<IEnumerable<Resource>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Resource?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Resource> AddAsync(Resource resource, CancellationToken cancellationToken = default);
        Task<Resource> UpdateAsync(Resource resource, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
