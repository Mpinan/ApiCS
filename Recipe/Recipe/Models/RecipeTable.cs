using System;
using System.Collections.Generic;

namespace Recipe.Models
{
    public partial class RecipeTable
    {
        public RecipeTable()
        {
            Ingredients = new HashSet<Ingredient>();
            Steps = new HashSet<Step>();
        }

        public int RecipeTableId { get; set; }
        public int UserId { get; set; }
        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Step> Steps { get; set; }
    }
}
