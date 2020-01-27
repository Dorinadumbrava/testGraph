using ElasticGraph.Models;
using GraphQL.Types;

namespace ElasticGraph.GraphQL.Types
{
    public class Ingredient : ObjectGraphType<IngredientModel>
    {
        public Ingredient()
        {
            Field(t => t.Id);
            Field(t => t.Name);
            Field(t => t.Callories);
        }
    }
}