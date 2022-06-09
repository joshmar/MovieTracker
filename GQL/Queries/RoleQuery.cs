using GraphQL.Types;
using MovieTracker.GQL.Types;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.GQL.Queries;

public sealed class RoleQuery : ObjectGraphType
{
    public RoleQuery(IRoleRepository roleRepository)
    {
        FieldAsync<ListGraphType<RoleType>>("Roles",
            resolve: async fieldContext => await roleRepository.GetAllAsync(fieldContext.CancellationToken));
    }
}