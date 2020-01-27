using System.Collections.Generic;
using System.Threading.Tasks;
using ElasticGraph.Models;

namespace ElasticGraph.GraphQl
{
    public interface IRecepyGraphClient
    {
        Task<List<RecepyModel>> GetAllRecepies();
        Task<RecepyModel> GetRecepy(int id);
    }
}