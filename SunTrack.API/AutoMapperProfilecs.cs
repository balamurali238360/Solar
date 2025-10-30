using AutoMapper;
using SunTrack.API.Data.Models;
using SunTrack.API.ViewModels.Leads;

namespace SunTrack.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           CreateMap<LeadWithCustomerVM, Customer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
            .ForMember(dest => dest.EmailId, opt => opt.MapFrom(src => src.EmailId))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.Now)) 
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(_ => 1))              
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())                 
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());

            CreateMap<LeadWithCustomerVM, Lead>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore()) 
                .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source))
                .ForMember(dest => dest.LeadNo, opt => opt.MapFrom(src => src.LeadNo))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.FollowUpDate, opt => opt.MapFrom(src => src.FollowUpDate ?? DateTime.Now))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(_ => 1))
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());
        }
    }
}
