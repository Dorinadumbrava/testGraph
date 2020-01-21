using ElasticGraph.Models;

namespace ElasticGraph.Services.Interfaces
{
    public interface IIngredientService
    {
        void AddIngredient(IngredientModel ingredient);
    }
}