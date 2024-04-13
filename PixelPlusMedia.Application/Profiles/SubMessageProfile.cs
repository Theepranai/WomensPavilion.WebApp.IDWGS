using AutoMapper;
using PixelPlusMedia.Application.Features.Contents.Commands;
using PixelPlusMedia.Application.Features.SubMessages;
using PixelPlusMedia.Application.Features.SubMessages.Commands.CreateSubMessage;
using PixelPlusMedia.Application.Features.SubMessages.Commands.DeleteSubMessage;
using PixelPlusMedia.Application.Features.SubMessages.Commands.UpdateSubMessage;
using PixelPlusMedia.Domain.Entities;

namespace PixelPlusMedia.Application.Profiles
{
    public class SubMessageProfile : Profile
    {
        public SubMessageProfile()
        {
            CreateMap<SubMessage, SubMessageDto>().ReverseMap();
            CreateMap<SubMessage, CreateSubMessageCommand>().ReverseMap();
            CreateMap<SubMessage, UpdateSubMessageCommand>().ReverseMap(); //.ForAllMembers(m => m.Condition((source, target, sourceValue, targetValue) => sourceValue != null));
            CreateMap<SubMessage, DeleteSubMessageCommand>().ReverseMap();
        }
    }
}
