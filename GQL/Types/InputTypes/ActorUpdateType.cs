using GraphQL.Types;

namespace MovieTracker.GQL.Types.InputTypes;

public class ActorUpdateType : InputObjectGraphType
{
    public ActorUpdateType()
    {
        Name = "actorInput";
        Field<NonNullGraphType<StringGraphType>>("firstName");
        Field<NonNullGraphType<StringGraphType>>("lastName");
        Field<ByteGraphType>("score");
    }
}