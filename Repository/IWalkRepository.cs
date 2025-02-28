using Microsoft.AspNetCore.Mvc;
using NewZelandAPI.Models.Domain;
using NewZelandAPI.Models.DTO;

namespace NewZelandAPI.Repository
{
    public interface IWalkRepository
    {
        public Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
               string? sortOn = null, bool? sortQuery = true, int pageNumber = 1, int pageSize = 1000 );
        public  Task<Walk> CreateWalkAsync(Walk walk);
        public Task<Walk> GetWalkByIdAsync(Guid id);
        public Task<Walk> UpdateAsync(Guid id, Walk walk);
        public Task<Walk> DeleteAsync(Guid id);

    }
}
