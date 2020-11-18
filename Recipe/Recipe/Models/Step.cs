using System;
using System.Collections.Generic;

namespace Recipe.Models
{
    public partial class Step
    {
        public int StepId { get; set; }
        public int? RecipeTableId { get; set; }
        public int StepNumber { get; set; }
        public string StepDescription { get; set; }

        public virtual RecipeTable RecipeTable { get; set; }
    }
}
