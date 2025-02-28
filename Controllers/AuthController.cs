using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NewZelandAPI.Models.DTO;
using NewZelandAPI.Repository;

namespace NewZelandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {

            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username

            };
           var newUser =  await userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if (newUser.Succeeded)
            {
                // add roles to this user
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    newUser = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
                    if (newUser.Succeeded)
                    {
                        return Ok("User was registered");
                    }

                }

            }
            return BadRequest("Something went wrong");
            
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginReqDTO loginReqDTO)
        {
            var user = await userManager.FindByEmailAsync(loginReqDTO.Username);

            if (user != null) 
            {
              var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginReqDTO.Password);

                if (checkPasswordResult)
                {
                    // get roles for this user 
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {

                        // create token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LogInResponseDTO
                        {
                            JWT = jwtToken
                        };
                        return Ok(jwtToken);

                    }
                }
                
            }
            return BadRequest("Username does not exist");
        }
    }
}
