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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public Region AddRegion(Region region)
        {
            try
            {
                region.Id = Guid.NewGuid();
                nZWalksDbContext.Region.Add(region);
                nZWalksDbContext.SaveChanges();
                return region;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool DeleteAllRegions()
        {
            try
            {
                nZWalksDbContext.Region.ToList().Clear();
                nZWalksDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Region DeleteRegion(Guid id)
        {
            try
            {
                var region = nZWalksDbContext.Region.Where(x => x.Id == id).FirstOrDefault();
                if (region == null)
                    return null;
                nZWalksDbContext.Region.Remove(region);
                nZWalksDbContext.SaveChanges();
                return region;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            try
            {
                Region existingRegion = await nZWalksDbContext.Region.Where(x => x.Id == id).FirstOrDefaultAsync();
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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Region> GetAllRegions()
        {
            try
            {
                return nZWalksDbContext.Region.ToList();
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Region GetRegion(Guid id)
        {
            try
            {
                var region = nZWalksDbContext.Region.Where(x => x.Id == id).FirstOrDefault();
                if (region == null)
                    return null;
                return region;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public Region UpdateRegion(Guid id, Region region)
        {
            try
            {
                var existingRegion = GetRegion(id);
                if (existingRegion == null)
                    return null;
                existingRegion.Name = region.Name;
                existingRegion.Code = region.Code;
                existingRegion.Area = region.Area;
                existingRegion.Population = region.Population;
                existingRegion.Long = region.Long;
                existingRegion.Lat = region.Lat;
                nZWalksDbContext.SaveChanges();
                return existingRegion;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="region"></param>
        /// <returns></returns>
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
