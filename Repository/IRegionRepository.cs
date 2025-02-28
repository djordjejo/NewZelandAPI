using NewZelandAPI.Models.Domain;

namespace NewZelandAPI.Repository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsyn();
        Task<Region?> GetRegionAsyin(Guid id);
        Task<Region?> UpdateRegionAsyin(Guid id, Region region);
        Task<Region?> DeleteRegionAsyin(Guid id);
        Task<Region> AddRegionAsyin(Region region);
    }
}
