using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Abstract;

namespace NZWalks.API.Repositories.Concrete
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> AddRegionAsync(Region region)
        {
            try
            {
                region.Id = Guid.NewGuid();
                await nZWalksDbContext.Region.AddAsync(region);
                await nZWalksDbContext.SaveChangesAsync();
                return region;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteAllRegionsAsync()
        {
            try
            {
                nZWalksDbContext.Region.ToList().Clear();
                await nZWalksDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            try
            {
                Region existingRegion = nZWalksDbContext.Region.Where(x => x.Id == id).FirstOrDefault();
                if (existingRegion == null)
                    return null;
                nZWalksDbContext.Region.Remove(existingRegion);
                await nZWalksDbContext.SaveChangesAsync();
                return existingRegion;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Region>> GetAllRegionsAsync()
        {
            try
            {
                return await nZWalksDbContext.Region.ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<Region> GetRegionAsync(Guid id)
        {
            try
            {
                Region region = await nZWalksDbContext.Region.FindAsync(id);
                if (region == null)
                    return null;
                return region;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            try
            {
                var existingRegion = await nZWalksDbContext.Region.FindAsync(id);
                if (existingRegion == null)
                    return null;
                existingRegion.Population = region.Population;
                existingRegion.Area = region.Area;
                existingRegion.Code = region.Code;
                existingRegion.Name = region.Name;
                existingRegion.Lat = region.Lat;
                existingRegion.Long = region.Long;
                await nZWalksDbContext.SaveChangesAsync();
                return existingRegion;
            }
            catch
            {
                return null;
            }
        }
    }
}
