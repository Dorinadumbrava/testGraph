using Nest;

namespace ElasticGraph.Services.Interfaces
{
    public interface IElasticService
    {
        ElasticClient GetClient();
    }
}