using GraphQL;
using GraphQL.DataLoader;
using GraphQL.DI;
using GraphQL.Server;
using GraphQL.SystemTextJson;
using GraphQL.Types;

namespace MovieTracker.Extensions;

public static class GraphQlBuilderExtensions
{
    public static IGraphQLBuilder BuildGqlService<T>(this IGraphQLBuilder gqlBuilder, bool isDevelopment) 
        where T : Schema
    {
        return gqlBuilder
            .ConfigureExecutionOptions(options =>
            {
                options.EnableMetrics = isDevelopment;
                var logger = options.RequestServices?.GetRequiredService<ILogger<Program>>();
                options.UnhandledExceptionDelegate = ctx =>
                {
                    logger?.LogError("{Error} occurred", ctx.OriginalException.Message);
                    return Task.CompletedTask;
                };
            })
            .AddHttpMiddleware<T>()
            .AddDefaultEndpointSelectorPolicy()
            .AddSystemTextJson()
            .AddErrorInfoProvider(opt => 
                opt.ExposeExceptionStackTrace = isDevelopment)
            .AddWebSockets()
            .AddDataLoader()
            .AddGraphTypes();
    }
}