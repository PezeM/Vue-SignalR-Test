using System.Collections.Generic;

namespace Server.Models
{
    public class Recipe
    {
        public string DisplayName { get; }
        public string Name { get; }
        public int MakeTime { get; }
        public List<RecipeItem> Ingredients { get; }
        public List<RecipeItem> Results { get; }

        public Recipe(string displayName, string name, int makeTime, List<RecipeItem> ingredients, List<RecipeItem> results)
        {
            DisplayName = displayName;
            Name = name;
            MakeTime = makeTime;
            Ingredients = ingredients;
            Results = results;
        }
    }
}