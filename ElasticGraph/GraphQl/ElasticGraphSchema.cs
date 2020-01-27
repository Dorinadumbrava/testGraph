using ElasticGraph.GraphQl;
using GraphQL;
using GraphQL.Types;

namespace ElasticGraph.GraphQL
{
    public class ElasticGraphSchema : Schema
    {
        public ElasticGraphSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<ElasticGraphQuerry>();
            Mutation = resolver.Resolve<ElasticGraphMutation>();
        }
    }
}