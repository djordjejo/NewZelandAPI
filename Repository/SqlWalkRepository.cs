using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NewZelandAPI.Data;
using NewZelandAPI.Models.Domain;
using NewZelandAPI.Models.DTO;

namespace NewZelandAPI.Repository
{
    public class SqlWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SqlWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateWalkAsync(Walk walk)
        {

           await dbContext.Walks.AddAsync(walk);
           await dbContext.SaveChangesAsync();
           return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null) return null;

            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortOn = null, bool? sortQuery = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                walks = walks.Where(x => x.Name.Contains(filterQuery));
            }

            // sorting
            if (string.IsNullOrWhiteSpace(sortOn) == false && sortQuery == null)
            {
                if (sortOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = sortQuery == true ? walks = walks.OrderBy(x => x.Name) : walks = walks.OrderByDescending(x=> x.Name);
                }
            }
            // pagination


            var skipResults = (pageNumber - 1) * pageSize;


                return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
            
           
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            var walk =  await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null) return null;
            return walk;

        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk Walk)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null) return null;

            existingWalk.Name = Walk.Name;
            existingWalk.Description = Walk.Description;
            existingWalk.LengthInKm = Walk.LengthInKm;
            existingWalk.WalkImageUrl = Walk.WalkImageUrl;
            existingWalk.DifficultyId = Walk.DifficultyId;
            existingWalk.RegionId = Walk.RegionId;
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
