using AutoMapper;
using BookPlatform.Application.Features.Books.Commands.Create;
using BookPlatform.Domain;

namespace BookPlatform.Application.Features.BookNotes.Dtos;

public sealed class DtoProfiles : Profile
{
    public DtoProfiles()
    {
        CreateMap<CreateBookCommandRequest, CreateBookNoteDto>();
        CreateMap<CreateBookNoteDto, BookNote>();

        CreateMap<BookNote, GetBookNoteDto>().ReverseMap();

        CreateMap<UpdateBookNoteDto, BookNote>();
    }
}