using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Recipe.Models;

namespace Recipe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly recipesContext _context;
        private Helper _helper = new Helper();

        public UsersController(recipesContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetUser(int id)
        {
            var userRecipe = from user in _context.Users
                             where user.UserId == id
                             join recipeTable in _context.RecipeTables on user.UserId equals recipeTable.UserId
                             join ingredients in _context.Ingredients on recipeTable.RecipeTableId equals ingredients.RecipeTableId
                             join steps in _context.Steps on recipeTable.RecipeTableId equals steps.StepId
                             select recipeTable;
            var userRecipeObj = (object)userRecipe;

            if (userRecipe == null)
            {
                return NotFound();
            }

            return userRecipeObj;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
        
            var userCheck = _context.Users.Where(u => u.Email == user.Email).FirstOrDefault();
            var encriptedPassword = _helper.EncryptPlainTextToCipherText(user.UserPassword);
            user.UserPassword = encriptedPassword;

            if (userCheck == null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest();
            }

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
