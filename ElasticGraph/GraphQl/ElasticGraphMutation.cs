using ElasticGraph.GraphQl.Types;
using ElasticGraph.GraphQL.Types;
using ElasticGraph.Models;
using ElasticGraph.Services.Interfaces;
using GraphQL.Types;

namespace ElasticGraph.GraphQl
{
    public class ElasticGraphMutation : ObjectGraphType
    {
        public ElasticGraphMutation(IRecepyService recepyService)
        {
            FieldAsync<Recepy>(
                "addRecepy",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<RecepyInputType>> { Name = "recepy" }),
                resolve: async context =>
                 {
                     var recepy = context.GetArgument<RecepyModel>("recepy");
                     return await context.TryAsyncResolve(
                         async c => await recepyService.AddRecepy(recepy));
                 });
        }
    }
}