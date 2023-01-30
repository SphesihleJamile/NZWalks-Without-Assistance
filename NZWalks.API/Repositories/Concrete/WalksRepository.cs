using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Abstract;

namespace NZWalks.API.Repositories.Concrete
{
    public class WalksRepository : IWalksRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalksRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Walk> AddWalkAsync(Walk walk)
        {
            try
            {
                walk.Id = Guid.NewGuid();
                await nZWalksDbContext.Walk.AddAsync(walk);
                await nZWalksDbContext.SaveChangesAsync();
                return walk;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            try
            {
                var walk = await GetWalkAsync(id);
                if (walk == null) return null;
                nZWalksDbContext.Walk.Remove(walk);
                await nZWalksDbContext.SaveChangesAsync();
                return walk;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Walk>> GetAllWalkAsync()
        {
            try
            {
                return await nZWalksDbContext.Walk.ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<Walk> GetWalkAsync(Guid id)
        {
            try
            {
                var walk = await nZWalksDbContext.Walk.FindAsync(id);
                if (walk == null)
                    return null;
                return walk;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            try
            {
                var existingWalk = await GetWalkAsync(id);
                if (existingWalk == null)
                    return null;
                existingWalk.Name = walk.Name;
                existingWalk.Length = walk.Length;
                existingWalk.RegionId = walk.RegionId;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                await nZWalksDbContext.SaveChangesAsync();
                return existingWalk;
            }
            catch
            {
                return null;
            }
        }
    }
}
