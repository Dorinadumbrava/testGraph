using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ElasticController(IIngredientService ingredientService)
        {
            this.ingredientService = ingredientService;
        }
        [HttpGet]
        public IActionResult GetIngredients()
        {
            return View();
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