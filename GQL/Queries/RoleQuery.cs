using GraphQL.Types;
using MovieTracker.GQL.Types;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.GQL.Queries;

public sealed class RoleQuery : ObjectGraphType
{
    public RoleQuery(IRoleService roleService)
    {
        FieldAsync<ListGraphType<RoleType>>("Roles",
            resolve: async fieldContext => await roleService.GetAllAsync(fieldContext.CancellationToken));
    }
}