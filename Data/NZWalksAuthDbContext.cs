using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewZelandAPI.Models.Domain;

namespace NewZelandAPI.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerId = "23a5eaec-ba55-4833-9734-3e8101474c33";
            var writerId = "70d3530c-32b4-4592-b45d-d80970597890";

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerId,
                    ConcurrencyStamp = readerId,
                    NormalizedName = "Reader".ToUpper(),
                    Name = "Reader"

                },

                new IdentityRole()
                {
                    Id = writerId,
                    ConcurrencyStamp = writerId,
                    NormalizedName = "Writer".ToUpper(),
                    Name = "Writer"

                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
