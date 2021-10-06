using App.Model.Dtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;

namespace App.Mappings.Profiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<UserImage, UserImageDto>();

            CreateMap<GroupChatImage, GroupChatImageDto>();
        }
    }
}
