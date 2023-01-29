using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Abstract
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllRegionsAsync();
        //IEnumerable<Region> GetAllRegions();

        Task<Region> GetRegionAsync(Guid id);
        Task<Region> UpdateRegionAsync(Guid id, Region region);
        Task<Region> AddRegionAsync(Region region);
        Task<Region> DeleteRegionAsync(Guid id);
        Task<bool> DeleteAllRegionsAsync();
    }
}
