using ElasticGraph.Services.Interfaces;
using Elasticsearch.Net;
using Nest;
using Nest.JsonNetSerializer;
using System;

namespace ElasticGraph.Services
{
    public class ElasticService: IElasticService
    {
        private ElasticClient client;

        public ElasticClient GetClient()
        {
            if (client == null)
            {
                CreateClient();
            }
            return client;
        }

        private void CreateClient()
        {
            var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
            var settings = new ConnectionSettings(pool, sourceSerializer: JsonNetSerializer.Default).DefaultIndex("testindex");
            client = new ElasticClient(settings);
        }

        public bool IndexExists(string index)
        {
            return client.Indices.Exists(index).Exists;
        }

        public void CreateIndex<T>(T entity, string index) where T : class
        {
            if (!IndexExists(index))
            {
                client.Indices.Create(index);
            }
            var response = client.Index(entity,
                s => s.Index(index));

            if (!response.IsValid)
                throw new Exception("Index could not be created" + response.OriginalException.Message);

        }
    }
}