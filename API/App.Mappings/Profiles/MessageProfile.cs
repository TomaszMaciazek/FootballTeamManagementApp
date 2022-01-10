using App.Model.Dtos;
using App.Model.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Mappings.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, SimpleMessageDto>();
            CreateMap<MessageTransmission, MessageDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Message.Id))
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => src.Message.Sender))
                .ForMember(dest => dest.Recipients, opt => opt.MapFrom(src => src.Message.Recipients.Select(x => x.Recipient)))
                .ForMember(dest => dest.Topic, opt => opt.MapFrom(src => src.Message.Topic))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Message.Content))
                .ForMember(dest => dest.SendDate, opt => opt.MapFrom(src => src.Message.SendDate))
                .ForMember(dest => dest.MailboxType, opt => opt.MapFrom(src => src.MailboxType));
        }
    }
}
