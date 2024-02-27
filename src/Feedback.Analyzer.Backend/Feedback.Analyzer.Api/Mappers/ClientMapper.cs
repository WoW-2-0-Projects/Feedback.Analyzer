using AutoMapper;
using Feedback.Analyzer.Application.Clients.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Api.Mappers;

/// <summary>
/// Mapper configuration for mapping between <see cref="Client"/> and <see cref="ClientDto"/>.
/// </summary>
public class ClientMapper : Profile
{
    public ClientMapper()
    {
        CreateMap<Client, ClientDto>().ReverseMap();
    }
}