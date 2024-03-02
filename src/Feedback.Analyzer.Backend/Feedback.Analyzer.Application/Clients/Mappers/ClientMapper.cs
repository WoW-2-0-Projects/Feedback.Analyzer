using AutoMapper;
using Feedback.Analyzer.Application.Clients.Models;
using Feedback.Analyzer.Application.Common.Identity.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Clients.Mappers;

public class ClientMapper : Profile
{
    public ClientMapper()
    {
        CreateMap<Client, ClientDto>().ReverseMap();
        CreateMap<Client, SignUpDetails>().ReverseMap();
        CreateMap<Client, SignInDetails>().ReverseMap();
    }
}