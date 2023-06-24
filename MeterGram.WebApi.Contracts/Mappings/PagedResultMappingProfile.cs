using AutoMapper;
using MeterGram.Core.Models;
using MeterGram.WebApi.Contracts.Responses;

namespace MeterGram.WebApi.Contracts.Mappings;

public class PagedResultMappingProfile : Profile
{
    public PagedResultMappingProfile()
    {
        CreateMap(typeof(PagedResult<>), typeof(PagedResponse<>));
    }
}