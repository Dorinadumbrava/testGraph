using ElasticGraph.GraphQL.Types;
using ElasticGraph.Services.Interfaces;
using GraphQL.Types;

namespace ElasticGraph.GraphQL
{
    public class ElasticGraphQuerry : ObjectGraphType
    {
        public ElasticGraphQuerry(IIngredientService ingredientService, IRecepyService recepyService)
        {
            Field<ListGraphType<Ingredient>>(
                "ingredients",
                resolve: context => ingredientService.GetAll());

            Field<ListGraphType<Recepy>>(
                "recepies",
                resolve: context=> recepyService.GetAllRecepies());

            Field<Recepy>(
                "recepy",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                { Name = "id" }),
                resolve: context =>
                 {
                     var id = context.GetArgument<int>("id");
                     return recepyService.GetById(id);
                 });
        }
    }
}