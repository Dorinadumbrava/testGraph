using ElasticGraph.Models;
using ElasticGraph.Services.Interfaces;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace ElasticGraph.GraphQL.Types
{
    public class Recepy : ObjectGraphType<RecepyModel>
    {
        public Recepy(IRecepyService recepyService, IDataLoaderContextAccessor dataLoaderContextAccessor)
        {
            Field(t => t.Id);
            Field(t => t.Name);
            Field<RecepyTypeEnumType>("Type", "RecepyType");
            Field<ListGraphType<Ingredient>>("ingredients",
                resolve: context =>
                {
                    var loader = dataLoaderContextAccessor.Context.GetOrAddCollectionBatchLoader<int, IngredientModel>(
                        "GetById", recepyService.GetIngredientsById);
                    return loader.LoadAsync(context.Source.Id);
                }) ;
        }
    }

}