using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Abstract
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllRegionsAsync();
        IEnumerable<Region> GetAllRegions();

        Task<Region> GetRegionAsync(Guid id);
        Region GetRegion(Guid id);

        Task<Region> UpdateRegionAsync(Guid id, Region region);
        Region UpdateRegion(Guid id, Region region);

        Task<Region> AddRegionAsync(Region region);
        Region AddRegion(Region region);

        Task<Region> DeleteRegionAsync(Guid id);
        Region DeleteRegion(Guid id);

        Task<bool> DeleteAllRegionsAsync();
        Boolean DeleteAllRegions();
    }
}
