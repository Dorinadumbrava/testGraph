using ElasticGraph.GraphQL.Types;
using ElasticGraph.Models;
using ElasticGraph.Services.Interfaces;
using Nest;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ElasticGraph.Services
{
    public class RecepyService : IRecepyService
    {
        private readonly IElasticService elasticService;
        private ElasticClient client;
        private IEnumerable<RecepyModel> _recepies;

        public RecepyService(IElasticService elasticService)
        {
            this.elasticService = elasticService;
            client = elasticService.GetClient();
            elasticService.CreateIndex(new RecepyModel(), "testindex");
            AddRecepies();
        }

        public IEnumerable<IngredientModel> GetIngredientsByID(int id)
        {
            var searchResponse = client.Get<RecepyModel>(id);
            return searchResponse.Source.Ingredients;
        }

        public IEnumerable<IngredientModel> GetLocalById(int id)
        {
            return _recepies.Where(x => x.Id == id).Select(i => i.Ingredients).FirstOrDefault();
        }

        private void AddRecepies()
        {
            var recepies = new List<RecepyModel>
            {
                new RecepyModel
                {
                    Id =1,
                    Name = "Chicken Salad",
                    Type = RecepyTypeEnum.Omnivore,
                    Ingredients = new List<IngredientModel>()
                    {
                        new IngredientModel{Id = 1,  Name = "Chicken" },
                        new IngredientModel{Id = 2,  Name = "Lettuce" },
                        new IngredientModel{Id = 3,  Name = "Mayo" }
                    }
                },
                new RecepyModel
                {
                    Id =0,
                    Name = "Tomato Soup",
                    Type = RecepyTypeEnum.Vegan,
                    Ingredients = new List<IngredientModel>()
                    {
                        new IngredientModel{Id = 4,  Name = "Tomato" },
                        new IngredientModel{Id = 5,  Name = "Pepper" },
                        new IngredientModel{Id = 6,  Name = "Stock" }
                    }
                }
            };
            _recepies = recepies;
            var searchResponse = client.IndexMany(recepies, "testindex");
        }

        public IEnumerable<RecepyModel> GetAllRecepies()
        {
            var searchResponse = client.Search<RecepyModel>(s => s.Query(q => q.MatchAll()));
            return searchResponse.Documents.ToList();
        }

        public async Task<ILookup<int, IngredientModel>> GetIngredientsById(IEnumerable<int> ids, CancellationToken cancellationToken)
        {
            List<long> longIds = ids.Select(i => (long)(int)i).ToList();

            var response = await client.MultiGetAsync(m => m.GetMany<RecepyModel>(longIds));
            return response.SourceMany<RecepyModel>(longIds).SelectMany(kvp => kvp.Ingredients,
                (kvp, v) => new { k = kvp.Id, v }).ToLookup(kvp => kvp.k, kvp => kvp.v);
        }

        public RecepyModel GetById(int id)
        {
            var searchResponse = client.Get<RecepyModel>(id);
            return searchResponse.Source;
        }

        public async Task<RecepyModel> AddRecepy(RecepyModel recepy)
        {
            await client.IndexAsync(recepy, i => i.Index("testindex"));
            return recepy;
        }
    }
}