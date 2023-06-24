using AutoMapper;
using Metergram.Core.UseCases.Applications.UseCases;
using MeterGram.Domain.Models;
using MeterGram.WebApi.Contracts.Requests.CompanyApplications;
using MeterGram.WebApi.Contracts.Responses.CompanyApplications;

namespace MeterGram.WebApi.Contracts.Mappings;

public class CompanyApplicationMappingProfile : Profile
{
    public CompanyApplicationMappingProfile()
    {
        CreateMap<CompanyApplicationGetRequest, GetAllApplications.Query>();
        CreateMap<CompanyApplicationGetForParticipantRequest, GetAllApplicationsForParticipant.Query>();
        CreateMap<CompanyApplicationCreateRequest, CreateNewApplication.Command>();
        CreateMap<CompanyApplicationCreateRequestParticipant, CreateNewApplication.ParticipantCommand>();

        CreateMap<CompanyApplication, CompanyApplicationResponse>();
        CreateMap<Participant, CompanyApplicationResponseParticipant>();
        CreateMap<Project, CompanyApplicationResponseProject>();
    }
}