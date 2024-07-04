using AutoMapper;
using BookPlatform.Infrastructure.Persistence.EntityFramework;

namespace BookPlatform.Application.Common.Services;

public class BaseService
{
    public required IUnitOfWork UnitOfWork { get; init; }
    public required IEfRepository EfRepository { get; init; }
    public required IMapper Mapper { get; init; }

    public required ICurrentUserService CurrentUser { get; init; }
}