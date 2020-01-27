using GraphQL.Types;

namespace ElasticGraph.GraphQl.Types
{
    public class RecepyInputType : InputObjectGraphType
    {
        public RecepyInputType()
        {
            Name = "recepyInput";
            Field<NonNullGraphType<IntGraphType>>("id");
            Field<NonNullGraphType<StringGraphType>>("name");
        }
    }
}