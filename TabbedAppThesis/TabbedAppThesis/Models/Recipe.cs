using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace TabbedAppThesis.Models
{
    public class Recipe
    {
        [BsonId]
        public Guid ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string HowTo { get; set; }
        public int TimeToMake { get; set; }
        public bool IsVegan { get; set; }
        public bool IsVegetarian { get; set; }


        public List<string> IngredientList { get; set; }
    }
}
