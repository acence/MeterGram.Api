﻿using AutoMapper;
using MeterGram.Domain.Models;
using MeterGram.Infrastructure.CourseService.Models;

namespace MeterGram.Infrastructure.CourseService.Mapping;

public class CourseMappingProfile : Profile
{
    public CourseMappingProfile()
    {
        CreateMap<CourseResponseModel, Course>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CourseName));
    }
}