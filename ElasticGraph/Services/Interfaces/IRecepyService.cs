using ElasticGraph.GraphQL.Types;
using ElasticGraph.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ElasticGraph.Services.Interfaces
{
    public interface IRecepyService
    {
        IEnumerable<RecepyModel> GetAllRecepies();
        IEnumerable<IngredientModel> GetIngredientsByID(int id);
        IEnumerable<IngredientModel> GetLocalById(int id);
        Task<ILookup<int, IngredientModel>> GetIngredientsById(IEnumerable<int> ids, CancellationToken cancellationToken);
        RecepyModel GetById(int id);
        Task<RecepyModel> AddRecepy(RecepyModel recepy);
    }
}