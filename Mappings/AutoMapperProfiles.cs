using AutoMapper;
using NewZelandAPI.Models.Domain;
using NewZelandAPI.Models.DTO;
using System.Runtime;

namespace NewZelandAPI.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionReqDto, Region>().ReverseMap();
            CreateMap<UpdateRegionDTO, Region>().ReverseMap();
            CreateMap<AddWalkReqDTO, Walk>().ReverseMap();
            CreateMap<UpdateWalkDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
           // CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            //CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();

        }
    }
}
