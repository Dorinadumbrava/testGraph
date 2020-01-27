using ElasticGraph.Models;
using System.Collections.Generic;

namespace ElasticGraph.Services.Interfaces
{
    public interface IIngredientService
    {
        void AddIngredient(IngredientModel ingredient);
        IEnumerable<IngredientModel> GetAll();
    }
}