using GraphQL.Types;
using MovieTracker.GQL.Queries;

namespace MovieTracker.GQL.Schemas;

public class RoleSchema : Schema
{
    public RoleSchema(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<RoleQuery>();
    }
}