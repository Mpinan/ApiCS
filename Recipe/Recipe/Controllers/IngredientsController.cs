using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipe.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recipe.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {

        private readonly recipesContext _context;

        public IngredientsController(recipesContext context)
        {
            _context = context;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
        {
            return await _context.Ingredients.ToListAsync();
        }

        // GET api/ingredients/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetIngredient(int id)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(u => u.IngredientId == id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return ingredient;
        }

        // POST api/Ingredients
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Ingredient>> PostIngredient(Ingredient ingredient)
        {

            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngredient", new { id = ingredient.IngredientId }, ingredient);
        }

        // PUT/UPDATE api/Ingredients/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient(int id, Ingredient ingredient)
        {
            if (id != ingredient.IngredientId)
            {
                return BadRequest();
            }

            _context.Entry(ingredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/Ingredients/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ingredient>> DeleteIngredient(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return ingredient;
        }

        private bool IngredientExists(int id)
        {
            return _context.Ingredients.Any(e => e.IngredientId == id);
        }
    }
}
