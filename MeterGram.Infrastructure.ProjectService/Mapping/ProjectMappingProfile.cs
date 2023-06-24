using AutoMapper;
using MeterGram.Domain.Models;
using MeterGram.Infrastructure.ProjectService.Models;

namespace MeterGram.Infrastructure.ProjectService.Mapping;

public class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<ProjectResponseModel, Project>();
    }
}