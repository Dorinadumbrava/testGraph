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
    }
}