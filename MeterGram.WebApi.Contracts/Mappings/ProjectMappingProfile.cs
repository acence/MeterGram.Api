using AutoMapper;
using MeterGram.Domain.Models;
using MeterGram.WebApi.Contracts.Responses.Course;

namespace MeterGram.WebApi.Contracts.Mappings;

public class CourseMappingProfile : Profile
{
    public CourseMappingProfile()
    {
        CreateMap<Course, CourseResponse>();
    }
}