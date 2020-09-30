using System;
using System.Collections.Generic;

namespace Recipe.Models
{
    public partial class Ingredient
    {
        public int IngredientId { get; set; }
        public int? RecipeTableId { get; set; }
        public string IngredientName { get; set; }
        public string IngredientAmount { get; set; }

        public virtual RecipeTable RecipeTable { get; set; }
    }
}
