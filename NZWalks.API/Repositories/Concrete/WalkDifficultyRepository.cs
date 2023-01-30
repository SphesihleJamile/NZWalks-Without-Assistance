using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Abstract;

namespace NZWalks.API.Repositories.Concrete
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty)
        {
            try
            {
                walkDifficulty.Id = Guid.NewGuid();
                await nZWalksDbContext.WalkDifficulty.AddAsync(walkDifficulty);
                await nZWalksDbContext.SaveChangesAsync();
                return walkDifficulty;
            }
            catch
            {
                return null;
            }
        }

        public async Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id)
        {
            try
            {
                var walkDifficulty = await GetWalkDifficultyAsync(id);
                if (walkDifficulty == null)
                    return null;
                nZWalksDbContext.WalkDifficulty.Remove(walkDifficulty);
                await nZWalksDbContext.SaveChangesAsync();
                return walkDifficulty;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficultiesAsync()
        {
            try
            {
                return await nZWalksDbContext.WalkDifficulty.ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<WalkDifficulty> GetWalkDifficultyAsync(Guid id)
        {
            try
            {
                var walkDifficulty = await nZWalksDbContext.WalkDifficulty.FindAsync(id);
                if (walkDifficulty == null)
                    return null;
                return walkDifficulty;
            }
            catch
            {
                return null;
            }
        }

        public async Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, WalkDifficulty newWalkDifficulty)
        {
            try
            {
                var existnigWalkDifficulty = await GetWalkDifficultyAsync(id);
                if (existnigWalkDifficulty == null)
                    return null;
                existnigWalkDifficulty.Code = newWalkDifficulty.Code;
                await nZWalksDbContext.SaveChangesAsync();
                return existnigWalkDifficulty;
            }
            catch
            {
                return null;
            }
        }
    }
}
