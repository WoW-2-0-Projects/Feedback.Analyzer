using AutoMapper;
using Feedback.Analyzer.Application.Common.Identity.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Clients.Mappers;

public class IdentityTokenMapper : Profile
{
    public IdentityTokenMapper()
    {
        CreateMap<AccessToken, AccessTokenDto>();

        CreateMap<(AccessToken AccessToken, RefreshToken RefreshToken), IdentityTokenDto>()
            .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.AccessToken.Token))
            .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken.Token));
    }
}