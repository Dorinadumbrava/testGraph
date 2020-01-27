using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticGraph.GraphQl;
using ElasticGraph.Models;
using ElasticGraph.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElasticGraph.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ElasticController : Controller
    {
        private readonly IIngredientService ingredientService;
        private readonly IRecepyGraphClient _recepyGraphClient;

        public ElasticController(IIngredientService ingredientService, IRecepyGraphClient recepyGraphClient)
        {
            this.ingredientService = ingredientService;
            _recepyGraphClient = recepyGraphClient;
        }
        [HttpGet]
        public IActionResult GetIngredients()
        {
            return View();
        }

        [HttpGet("/products")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _recepyGraphClient.GetAllRecepies());
        }

        [HttpPost]
        public IActionResult AddIngredients(List<IngredientModel> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                ingredientService.AddIngredient(ingredient);
            }

            return Ok(ingredients.Count);
        }
    }
}