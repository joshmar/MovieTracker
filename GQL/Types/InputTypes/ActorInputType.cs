using GraphQL.Types;

namespace MovieTracker.GQL.Types.InputTypes;

public class ActorInputType : InputObjectGraphType
{
    public ActorInputType()
    {
        Name = "actorInput";
        Field<NonNullGraphType<StringGraphType>>("firstName");
        Field<NonNullGraphType<StringGraphType>>("lastName");
        Field<ByteGraphType>("score");
    }    
}