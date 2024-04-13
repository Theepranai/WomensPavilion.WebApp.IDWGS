using AutoMapper;
using PixelPlusMedia.Application.Features.Contents.Commands;
using PixelPlusMedia.Application.Features.Contents.Commands.CreateContent;
using PixelPlusMedia.Application.Features.Contents.Commands.UpdateContent;
using PixelPlusMedia.Application.Features.Contents.Queries;
using PixelPlusMedia.Domain.Entities;

namespace PixelPlusMedia.Application.Profiles;
public class ContentProfile : Profile
{
    public ContentProfile()
    {
        CreateMap<Content, ContentDto>().ReverseMap();

        CreateMap<Content, CreateContentCommand>().ReverseMap();
        CreateMap<Content, UpdateContentCommand>().ReverseMap();

        CreateMap<Content, ContentVm>().ReverseMap();
        CreateMap<Content, ContentVmResponse>().ReverseMap();
    }
}
