using AutoMapper;
using Domain.Entities;
using App.ViewModels.UserModels;
using App.Dtos.Profile;
using App.ViewModels.Post;

namespace App.MappingProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            //CreateMap<User, UserViewModel>().ReverseMap();

            CreateMap<Post, PostHandlerViewModel>().ReverseMap();

            CreateMap<Post, PostViewModel>().ReverseMap();

            CreateMap<AuthenticationRequest, UserLoginViewModel>()
                .ForMember(m => m.HasError, opt => opt.Ignore())
                .ForMember(m => m.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegistrationRequest, UserHandlerViewModel>()
                .ForMember(m => m.HasError, opt => opt.Ignore())
                .ForMember(m => m.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(m => m.HasError, opt => opt.Ignore())
                .ForMember(m => m.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(m => m.HasError, opt => opt.Ignore())
                .ForMember(m => m.Error, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
