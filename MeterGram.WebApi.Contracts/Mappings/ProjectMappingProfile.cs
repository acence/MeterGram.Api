using AutoMapper;
using MeterGram.Domain.Models;
using MeterGram.WebApi.Contracts.Responses.Project;

namespace MeterGram.WebApi.Contracts.Mappings;

public class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<Project, ProjectResponse>();
    }
}