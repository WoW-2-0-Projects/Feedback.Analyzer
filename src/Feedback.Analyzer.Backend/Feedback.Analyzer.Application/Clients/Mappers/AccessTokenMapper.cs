using AutoMapper;
using Feedback.Analyzer.Application.Common.Identity.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Clients.Mappers;


public class AccessTokenMapper : Profile
{
    public AccessTokenMapper()
    {
        CreateMap<AccessToken, AccessTokenDto>().ReverseMap();
    }
}
