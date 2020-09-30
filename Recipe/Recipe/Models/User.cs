using System;
using System.Collections.Generic;

namespace Recipe.Models
{
    public partial class User
    {
        public User()
        {
            RecipeTables = new HashSet<RecipeTable>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }

        public virtual ICollection<RecipeTable> RecipeTables { get; set; }
    }
}
