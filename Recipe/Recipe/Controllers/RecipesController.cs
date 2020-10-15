using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipe.Models;

namespace Recipe.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly recipesContext _context;

        public RecipesController(recipesContext context)
        {
            _context = context;
        }

        // GET: api/Recipes

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeTable>>> GetRecipes()
        {
            var wholeRecipe = _context.RecipeTables.Include(recipe => recipe.Ingredients).Include(recipe => recipe.Steps).ToListAsync();
            return await wholeRecipe;
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeTable>> GetRecipe(int id)
        {
            var recipe = await _context.RecipeTables.Include(r => r.Ingredients).Include(r => r.Steps).FirstOrDefaultAsync(r => r.RecipeTableId == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        // PUT: api/Recipes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, RecipeTable recipe)
        {
            if (id != recipe.RecipeTableId)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
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

        // POST: api/Recipes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RecipeTable>> PostRecipe(RecipeTable recipe)
        {

            _context.RecipeTables.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipe", new { id = recipe.RecipeTableId }, recipe);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RecipeTable>> DeleteRecipe(int id)
        {
            var recipe = await _context.RecipeTables.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.RecipeTables.Remove(recipe);
            await _context.SaveChangesAsync();

            return recipe;
        }

        private bool RecipeExists(int id)
        {
            return _context.RecipeTables.Any(e => e.RecipeTableId == id);
        }
    }
}
