using System;
using System.Collections.Generic;

namespace Recipe.Models
{
    public partial class Step
    {
        public int StepId { get; set; }
        public int? RecipeId { get; set; }
        public string StepDescription { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
