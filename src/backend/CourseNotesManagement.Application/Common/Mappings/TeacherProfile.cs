using AutoMapper;
using CourseNotesManagement.Application.Features.Teachers.Commands.Update;
using CourseNotesManagement.Application.Features.Teachers.Queries.GetTeacherById;
using CourseNotesManagement.Domain.Entities;

namespace CourseNotesManagement.Application.Common.Mappings;

public class TeacherProfile : Profile
{
    public TeacherProfile()
    {
        CreateMap<Teacher, TeacherDto>();
        CreateMap<CourseAssignment, CourseAssignmentDto>()
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name));
        CreateMap<UpdateTeacherCommand, Teacher>()
            .ForMember(dest => dest.Role, opt => opt.Ignore()); // Role handler'da sabitleniyor!
    }
}