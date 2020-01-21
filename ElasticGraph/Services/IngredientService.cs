using ElasticGraph.Models;
using ElasticGraph.Services.Interfaces;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticGraph.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IElasticService elasticService;
        private ElasticClient client;

        public IngredientService(IElasticService elasticService)
        {
            this.elasticService = elasticService;
            client = elasticService.GetClient();
            EnsureIndex(client);
        }

        private void EnsureIndex(ElasticClient client)
        {
            var existingIndex = client.Indices;
            if (existingIndex.Exists("testindex").Exists)
            {
                return;
            }

            client.Indices.Create("testindex");
        }

        public void AddIngredient(IngredientModel ingredient)
        {
            var result = client.IndexDocument(ingredient);
        }
    }
}
