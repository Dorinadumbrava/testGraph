using ElasticGraph.Models;
using GraphQL.Client;
using GraphQL.Common.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticGraph.GraphQl
{
    public class RecepyGraphClient : IRecepyGraphClient
    {
        private readonly GraphQLClient _client;

        public RecepyGraphClient(GraphQLClient client)
        {
            _client = client;
        }

        public async Task<List<RecepyModel>> GetAllRecepies()
        {
            var query = new GraphQLRequest
            {
                Query = @"query recepiesQuery{
                    recepies{id name type
                    ingredients{ id name callories}}}"
            };
            var response = await _client.PostAsync(query);
            return response.GetDataFieldAs<List<RecepyModel>>("recepies");
        }

        public async Task<RecepyModel> GetRecepy(int id)
        {
            var query = new GraphQLRequest
            {
                Query = @"query recepyQuery($recepyId: ID){
                    recepy(id: $recepyId){id name type
                    ingredients{ id name callories}}}",
                Variables = new { recepyId = id }
            };
            var response = await _client.PostAsync(query);
            return response.GetDataFieldAs<RecepyModel>("recepy");
        }
    }
}