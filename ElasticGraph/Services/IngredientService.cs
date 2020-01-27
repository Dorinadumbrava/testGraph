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
            elasticService.CreateIndex<IngredientModel>(new IngredientModel(), "testindex");
        }

        public void AddIngredient(IngredientModel ingredient)
        {
            var result = client.Index(ingredient, i=>i.Index("testindex"));
        }

        public IEnumerable<IngredientModel> GetAll()
        {
            var searchResponse = client.Search<IngredientModel>(s => s.Query(q => q.MatchAll()));
            return searchResponse.Documents.ToList();
        }




    }
}
