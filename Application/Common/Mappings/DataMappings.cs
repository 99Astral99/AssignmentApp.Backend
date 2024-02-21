using AssignmentApp.Application.Responses;
using AssignmentApp.Domain.Entities;
using AutoMapper;

namespace AssignmentApp.Application.Common.Mappings
{
    public class DataMappings : Profile
    {
        public DataMappings()
        {
            CreateMap<AssignmentList, ResponseAssignmentList>();

            CreateMap<Assignment, ResponseAssignment>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(x => x.CurrentStatus.ToString()));

            CreateMap<AssignmentComment, ResponseAssignmentComment>()
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(x => x.Message));
        }
    }
}
