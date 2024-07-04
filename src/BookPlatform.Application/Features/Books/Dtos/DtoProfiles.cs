using AutoMapper;
using BookPlatform.Application.Features.Books.Commands.Create;
using BookPlatform.Application.Features.Books.Commands.Update;
using BookPlatform.Domain;

namespace BookPlatform.Application.Features.Books.Dtos;

public sealed class DtoProfiles : Profile
{
    public DtoProfiles()
    {
        CreateMap<CreateBookCommandRequest, CreateBookDto>();
        CreateMap<CreateBookDto, Book>();
        
        CreateMap<Book, GetBookDto>().ReverseMap();

        CreateMap<UpdateBookCommandRequest, UpdateBookDto>();
        CreateMap<UpdateBookDto, Book>();
    }
}