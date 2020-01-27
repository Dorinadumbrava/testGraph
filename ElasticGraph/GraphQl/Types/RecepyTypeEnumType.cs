using ElasticGraph.Models;
using GraphQL.Types;

namespace ElasticGraph.GraphQL.Types
{
    public class RecepyTypeEnumType : EnumerationGraphType<RecepyTypeEnum>
    {
        public RecepyTypeEnumType()
        {
            Name = "Type";
            Description = "Recepy type";
        }
    }
}