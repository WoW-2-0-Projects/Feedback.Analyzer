using AutoMapper;
using Feedback.Analyzer.Api.Models.DTOs;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Api.Mappers;

/// <summary>
/// Defines mapping configurations between the Organization and Organization classes.
/// </summary>
public class OrganizationMapper : Profile
{
    /// <summary>
    /// Initializes the mapping configuration for Organization and Organization.
    /// </summary>
    public OrganizationMapper()
    {
        CreateMap<Organization, OrganizationDto>().ReverseMap();
    }
}