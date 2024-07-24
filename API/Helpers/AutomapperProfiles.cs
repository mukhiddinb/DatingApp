using API.DTO;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers;

public class AutomapperProfiles: Profile
{
    public AutomapperProfiles()
    {
        CreateMap<AppUser, MemberDto>().ForMember(x => x.PhotoUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(ph => ph.IsMain)!.Url)).ForMember(x => x.Age, o => o.MapFrom(s => s.DateOfBirth.CalculateAge()));

        CreateMap<Photo, PhotoDto>();
    }
}
