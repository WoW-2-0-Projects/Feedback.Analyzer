using AutoMapper;
using Feedback.Analyzer.Application.Clients.Models;
using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.CustomerFeedbacks.Mappers;

/// <summary>
/// Mapper profile for mapping between <see cref="CustomerFeedback"/> entities and <see cref="CustomerFeedbackDto"/> data transfer objects.
/// </summary>
public class CustomerFeedbackMapper : Profile
{
    public CustomerFeedbackMapper()
    {
        CreateMap<CustomerFeedback, CustomerFeedbackDto>().ReverseMap();
    }
}