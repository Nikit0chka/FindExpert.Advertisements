using API.Endpoints.Advertisements.CreateAdvertisement;
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

        logger.LogInformation("Api services added");
    }
}