using AutoMapper;
using Feedback.Analyzer.Application.Organizations.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Organizations.Mappers;

public class OrganizationMapper : Profile
{
    public OrganizationMapper()
    {
        CreateMap<Organization, OrganizationDto>().ReverseMap();
    }
}