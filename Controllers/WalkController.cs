using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NewZelandAPI.CostumeActionFilter;
using NewZelandAPI.Data;
using NewZelandAPI.Models.Domain;
using NewZelandAPI.Models.DTO;
using NewZelandAPI.Repository;

namespace NewZelandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalkController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortOn, [FromQuery] bool? sortQuery, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortOn, sortQuery ?? true);
            return Ok( mapper.Map<List<WalkDTO>>(walksDomainModel));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkReqDTO addWalkReqDTO)
        {
           
                // DTO TO DOMAIN MODEL 
                var walkDomainModel = mapper.Map<Walk>(addWalkReqDTO);
                walkDomainModel = await walkRepository.CreateWalkAsync(walkDomainModel);


                return Ok(mapper.Map<AddWalkReqDTO>(walkDomainModel));
            
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {

            var walkDomainModel = await walkRepository.GetWalkByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }
        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkDto walkDTO)
        {
            
                var walk = mapper.Map<Walk>(walkDTO);
                walk = await walkRepository.UpdateAsync(id, walk);
                if (walk == null) return NotFound();

                return Ok(mapper.Map<WalkDTO>(walk));
            
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.DeleteAsync(id);
            if (walkDomainModel == null) return NotFound();

            return Ok(mapper.Map<WalkDTO>(walkDomainModel));

        }

    }

}
