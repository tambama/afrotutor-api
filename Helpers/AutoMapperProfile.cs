using AutoMapper;
using afrotutor.webapi.Dtos;
using afrotutor.webapi.Entities;

namespace afrotutor.webapi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Class, ClassDto>()
                .ForMember(dest => dest.Tutor, opts => opts.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.LocationName, opts => opts.MapFrom(src => src.Location.Name))
                .ForMember(dest => dest.Latitude, opts => opts.MapFrom(src => src.Location.Latitude))
                .ForMember(dest => dest.Longitude, opts => opts.MapFrom(src => src.Location.Longitude));
            CreateMap<ClassDto, Class>();
            CreateMap<UserClass, UserClassDto>().ReverseMap();
            CreateMap<UserClass, ClassDto>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.ClassId))
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.Class.UserId))
                .ForMember(dest => dest.UserClassId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Tutor, opts => opts.MapFrom(src => src.Class.User.FullName))
                .ForMember(dest => dest.IsFull, opts => opts.MapFrom(src => src.Class.IsFull))
                .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Class.Price))
                .ForMember(dest => dest.Subject, opts => opts.MapFrom(src => src.Class.Subject))
                .ForMember(dest => dest.Topic, opts => opts.MapFrom(src => src.Class.Topic))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Class.Description))
                .ForMember(dest => dest.StartTime, opts => opts.MapFrom(src => src.Class.StartTime))
                .ForMember(dest => dest.EndTime, opts => opts.MapFrom(src => src.Class.EndTime))
                .ForMember(dest => dest.LocationId, opts => opts.MapFrom(src => src.Class.LocationId))
                .ForMember(dest => dest.LocationName, opts => opts.MapFrom(src => src.Class.Location.Name))
                .ForMember(dest => dest.Latitude, opts => opts.MapFrom(src => src.Class.Location.Latitude))
                .ForMember(dest => dest.Longitude, opts => opts.MapFrom(src => src.Class.Location.Longitude))
                .ForMember(dest => dest.Capacity, opts => opts.MapFrom(src => src.Class.Capacity))
                .ForMember(dest => dest.Count, opts => opts.MapFrom(src => src.Class.Count))
                .ForMember(dest => dest.IsCancelled, opts => opts.MapFrom(src => src.Class.IsCancelled));
        }
    }
}