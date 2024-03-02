using AutoMapper;
using Feedback.Analyzer.Application.Common.Identity.Models;

namespace Feedback.Analyzer.Api.Mappers;

public class IdentityTokenMapper : Profile
{
    public IdentityTokenMapper()
    {
        CreateMap<Feedback.Analyzer.Domain.Entities.AccessToken, AccessTokenDto>();

        CreateMap<(Feedback.Analyzer.Domain.Entities.AccessToken AccessToken, Feedback.Analyzer.Domain.Entities.RefreshToken RefreshToken), IdentityTokenDto>()
            .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.AccessToken.Token))
            .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken.Token));
    }
}