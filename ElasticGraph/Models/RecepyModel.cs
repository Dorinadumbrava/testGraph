using ElasticGraph.GraphQL.Types;
using System.Collections.Generic;

namespace ElasticGraph.Models
{
    public class RecepyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<IngredientModel> Ingredients { get; set; }
        public RecepyTypeEnum Type { get; set; }
    }
}