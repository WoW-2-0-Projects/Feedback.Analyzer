﻿using AutoMapper;
using Feedback.Analyzer.Application.Common.PromptsHistory.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Common.PromptsHistory.Mappers;

public class PromptsExecutionHistoriesMapper : Profile
{
    public PromptsExecutionHistoriesMapper()
    {
        CreateMap<PromptsExecutionHistoryDto, PromptExecutionHistory>()
            .ForPath(dest => dest.ExecutionDuration, opt => opt.MapFrom(src => TimeSpan.FromMilliseconds(src.ExecutionDurationInMilliseconds)));

        CreateMap<PromptExecutionHistory, PromptsExecutionHistoryDto>()
            .ForPath(dest => dest.ExecutionDurationInMilliseconds, opt => opt.MapFrom(src => src.ExecutionDuration.Milliseconds));
    }
}