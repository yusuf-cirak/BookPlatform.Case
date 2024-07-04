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

        CreateMap<BookNote, GetBookNoteDto>()
            .ForMember(s=>s.BookNoteId, opt=>opt.MapFrom(d=>d.Id))
            .ForMember(s=>s.BookId, opt=>opt.MapFrom(d=>d.BookId))
            .ForMember(s=>s.Note, opt=>opt.MapFrom(d=>d.Note))
            .ForMember(s=>s.ShareType, opt=>opt.MapFrom(d=>d.ShareType))
            .ReverseMap();

        CreateMap<UpdateBookNoteDto, BookNote>();
    }
}