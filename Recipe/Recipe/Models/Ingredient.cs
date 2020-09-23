using System;
using System.Collections.Generic;

namespace Recipe.Models
{
    public partial class Ingredient
    {
        public int IngredientId { get; set; }
        public int? RecipeId { get; set; }
        public string IngredientName { get; set; }
        public string IngredientAmount { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
