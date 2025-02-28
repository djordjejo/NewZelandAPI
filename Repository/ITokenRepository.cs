using Microsoft.AspNetCore.Identity;

namespace NewZelandAPI.Repository
{
    public interface ITokenRepository
    {
      string CreateJWTToken(IdentityUser user, List<string> roles);
        
    }
}
