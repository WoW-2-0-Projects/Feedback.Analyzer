using AutoMapper;
using Feedback.Analyzer.Application.Products.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Products.Mappers;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}