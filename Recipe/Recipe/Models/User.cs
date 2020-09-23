using System;
using System.Collections.Generic;

namespace Recipe.Models
{
    public partial class User
    {
        public User()
        {
            Recipes = new HashSet<Recipe>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
