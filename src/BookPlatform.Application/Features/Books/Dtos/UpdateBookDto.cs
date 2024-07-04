using BookPlatform.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace BookPlatform.Application.Features.Books.Dtos;

public sealed record UpdateBookDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Isbn { get; set; }
    public int Pages { get; set; }
    public string Genre { get; set; }
    public DateTime PublishedDate { get; set; }
    public string Description { get; set; }
    public ShelfLocation ShelfLocation { get; set; }
    public IFormFile? Picture { get; set; }
}