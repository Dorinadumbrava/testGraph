using Nest;

namespace ElasticGraph.Services.Interfaces
{
    public interface IElasticService
    {
        ElasticClient GetClient();
        bool IndexExists(string index);
        void CreateIndex<T>(T entity, string index) where T : class;
    }
}