using System.Reflection;
using AutoMapper;
using BookPlatform.Application.Common.Abstractions;
using BookPlatform.Application.Common.Behaviors;
using BookPlatform.Application.Common.Services;
using BookPlatform.Application.Features.BookNotes.Services;
using BookPlatform.Application.Features.Books.Services;
using BookPlatform.Application.Features.UserFriends.Services;
using BookPlatform.Infrastructure.Image.Abstractions;
using BookPlatform.Infrastructure.Persistence.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BookPlatform.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();

        services.AddMediatrServices(executingAssembly);

        services.AddAutoMappers(executingAssembly);

        services.AddFluentValidators(executingAssembly);

        services.AddLifetimedServices(executingAssembly);

        services.AddBaseServices();

        services.AddBusinessRuleServices(executingAssembly, ServiceLifetime.Scoped);
    }

    private static void AddFluentValidators(this IServiceCollection services, Assembly assembly)
    {
        services.AddValidatorsFromAssembly(assembly);
    }

    private static void AddMediatrServices(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(SecuredRequestBehavior<,>));
    }

    private static void AddAutoMappers(this IServiceCollection services, Assembly assembly)
    {
        services.AddAutoMapper(assembly);
    }

    private static void AddBusinessRuleServices(this IServiceCollection services, Assembly assembly,
        ServiceLifetime serviceLifetime)
    {
        var businessRuleServices = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseBusinessRules)));

        foreach (var serviceType in businessRuleServices)
        {
            services.Add(new ServiceDescriptor(serviceType, serviceType, serviceLifetime));
        }
    }

    private static void AddBaseServices(this IServiceCollection services)
    {
        services.AddScoped<BaseService>(sp =>
        {
            var unitOfWork = sp.GetRequiredService<IUnitOfWork>();
            var efRepository = sp.GetRequiredService<IEfRepository>();
            var mapper = sp.GetRequiredService<IMapper>();
            var currentUserService = sp.GetRequiredService<ICurrentUserService>();

            return new BaseService()
            {
                CurrentUser = currentUserService,
                UnitOfWork = unitOfWork,
                EfRepository = efRepository,
                Mapper = mapper
            };
        });

        services.AddScoped<IBookService, BookService>(sp =>
        {
            var unitOfWork = sp.GetRequiredService<IUnitOfWork>();
            var efRepository = sp.GetRequiredService<IEfRepository>();
            var mapper = sp.GetRequiredService<IMapper>();
            var currentUserService = sp.GetRequiredService<ICurrentUserService>();
            var imageService = sp.GetRequiredService<IImageService>();

            return new BookService(imageService)
            {
                CurrentUser = currentUserService,
                UnitOfWork = unitOfWork,
                EfRepository = efRepository,
                Mapper = mapper
            };
        });

        services.AddScoped<IUserFriendService, UserFriendService>(sp =>
        {
            var unitOfWork = sp.GetRequiredService<IUnitOfWork>();
            var efRepository = sp.GetRequiredService<IEfRepository>();
            var mapper = sp.GetRequiredService<IMapper>();
            var currentUserService = sp.GetRequiredService<ICurrentUserService>();

            return new UserFriendService()
            {
                CurrentUser = currentUserService,
                UnitOfWork = unitOfWork,
                EfRepository = efRepository,
                Mapper = mapper
            };
        });

        services.AddScoped<IBookNoteService, BookNoteService>(sp =>
        {
            var unitOfWork = sp.GetRequiredService<IUnitOfWork>();
            var efRepository = sp.GetRequiredService<IEfRepository>();
            var mapper = sp.GetRequiredService<IMapper>();
            var currentUserService = sp.GetRequiredService<ICurrentUserService>();

            return new BookNoteService
            {
                CurrentUser = currentUserService,
                UnitOfWork = unitOfWork,
                EfRepository = efRepository,
                Mapper = mapper
            };
        });
    }
}