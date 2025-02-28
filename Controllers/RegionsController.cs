using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewZelandAPI.CostumeActionFilter;
using NewZelandAPI.Data;
using NewZelandAPI.Models.Domain;
using NewZelandAPI.Models.DTO;
using NewZelandAPI.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewZelandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        public RegionsController(NZWalksDbContext DbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            dbContext = DbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        public readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        // Dohvatanje svih regiona iz baze
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            
            var regionDomains = await regionRepository.GetAllAsyn();
          

           return Ok( mapper.Map<List<RegionDTO>>(regionDomains));

        }

        //Dohvatio region preko id
        // url => https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionByID(Guid id)
        {
            // get region domain model from database 
            var regionDomain = await dbContext.Regions.FindAsync(id);

            if (regionDomain == null) return NotFound();

            // map/conver region domain model to region dto 

            return Ok(mapper.Map<RegionDTO>(regionDomain));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateRegon([FromBody] AddRegionReqDto addRegionDTO)
        {
                // prvo pretvaramo dto u domain model 
                var domainModel = mapper.Map<Region>(addRegionDTO);

                //Use domain to create Region
                await regionRepository.AddRegionAsyin(domainModel);

                // pretvaramo nazad domain model u DTO

                var regionDTO = mapper.Map<RegionDTO>(domainModel);
                return CreatedAtAction(nameof(GetRegionByID), new { id = regionDTO.Id }, regionDTO);
            }
           
           

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]

        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {
                // pronalazak regiona koji treba da azuriramo 
                var regionDomainModel = mapper.Map<Region>(updateRegionDTO);
                regionDomainModel = await regionRepository.UpdateRegionAsyin(id, regionDomainModel);

                if (regionDomainModel == null) return NotFound();

                // DTO => domain model

                return Ok(mapper.Map<RegionDTO>(regionDomainModel));
            
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task <IActionResult> DeleteRegion([FromRoute] Guid id)
        {
           var regionDomainModel = regionRepository.DeleteRegionAsyin(id);

            if (regionDomainModel == null) return NotFound();
            

            return Ok();
        }

       

        

    }
}
