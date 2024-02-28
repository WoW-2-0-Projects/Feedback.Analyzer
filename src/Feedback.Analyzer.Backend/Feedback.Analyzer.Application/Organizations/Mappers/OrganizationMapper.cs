using AutoMapper;
using Feedback.Analyzer.Api.Models.DTOs;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Organizations.Mappers;

public class OrganizationMapper : Profile
{
    public OrganizationMapper()
    {
        CreateMap<Organization, OrganizationDto>().ReverseMap();
    }
}