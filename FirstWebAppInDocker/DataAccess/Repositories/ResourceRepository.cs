using FirstWebAppInDocker.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstWebAppInDocker.DataAccess.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly ApplicationDbContext _db;

        public ResourceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Resource>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Resources.AsNoTracking().OrderByDescending(r => r.CreatedAt).ToListAsync(cancellationToken);
        }

        public async Task<Resource?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _db.Resources.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<Resource> AddAsync(Resource resource, CancellationToken cancellationToken = default)
        {
            _db.Resources.Add(resource);
            await _db.SaveChangesAsync(cancellationToken);
            return resource;
        }

        public async Task<Resource> UpdateAsync(Resource resource, CancellationToken cancellationToken = default)
        {
            _db.Resources.Update(resource);
            await _db.SaveChangesAsync(cancellationToken);
            return resource;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var existing = await _db.Resources.FindAsync(new object[] { id }, cancellationToken);
            if (existing == null) return false;
            _db.Resources.Remove(existing);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _db.SaveChangesAsync(cancellationToken);
        }
    }
}
