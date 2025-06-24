using API.Endpoints.Advertisements.Create;
using API.Endpoints.Advertisements.Delete;
using API.Endpoints.Advertisements.Get;
using API.Endpoints.Advertisements.GetMy;
using API.Endpoints.Advertisements.Search;
using API.Endpoints.Advertisements.Update;
using API.Endpoints.Responses.Delete;
using API.Endpoints.Responses.Update;
using FastEndpoints;
using FastEndpoints.Swagger;
using FluentValidation;

namespace API.Extensions;

/// <summary>
///     Service collection extensions
/// </summary>
internal static class ServiceCollectionsExtensions
{
    /// <summary>
    ///     Add api services to service collections
    /// </summary>
    /// <param name="serviceCollection"> Service collection </param>
    /// <param name="logger"> Logger </param>
    public static void AddApiServices(this IServiceCollection serviceCollection, ILogger logger)
    {
        logger.LogInformation("Adding api services...");

        serviceCollection.AddFastEndpoints().SwaggerDocument();
        serviceCollection.AddValidatorsFromAssemblyContaining<CreateAdvertisementValidator>();
        serviceCollection.AddOpenApi();
        serviceCollection.AddApiMappers();

        logger.LogInformation("Api services added");
    }

    private static void AddApiMappers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<CreateAdvertisementErrorMapper>();
        serviceCollection.AddScoped<DeleteAdvertisementErrorMapper>();
        serviceCollection.AddScoped<GetAdvertisementErrorMapper>();
        serviceCollection.AddScoped<UpdateAdvertisementErrorMapper>();
        serviceCollection.AddScoped<SearchAdvertisementErrorMapper>();
        serviceCollection.AddScoped<GetMyAdvertisementErrorMapper>();

        serviceCollection.AddScoped<DeleteResponseErrorMapper>();
        serviceCollection.AddScoped<UpdateResponseErrorMapper>();
    }
}