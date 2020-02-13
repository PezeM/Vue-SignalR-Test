using System;
using System.Collections.Generic;
using System.Linq;
using Server.Models;

namespace Server
{
    public static class RecipesDatabase
    {
        private static Random _rng = new Random();
        
        public static List<Recipe> Recipes = new List<Recipe>
        {
            new Recipe("Drewniana deska", "woodplank", 3000, 
                new List<RecipeItem>
                {
                    new RecipeItem(ItemDatabase.Items["wood"])
                }, 
                new List<RecipeItem>
                {
                    new RecipeItem(ItemDatabase.Items["woodplank"])
                }),
            new Recipe("Metal", "metal", 3000, 
                new List<RecipeItem>
                {
                    new RecipeItem(ItemDatabase.Items["rock"])
                }, 
                new List<RecipeItem>
                {
                    new RecipeItem(ItemDatabase.Items["metal"])
                }),
            new Recipe("Miedź", "copper", 8000, 
                new List<RecipeItem>
                {
                    new RecipeItem(ItemDatabase.Items["metal"], 2)
                }, 
                new List<RecipeItem>
                {
                    new RecipeItem(ItemDatabase.Items["copper"])
                }),
            new Recipe("Złoto", "gold", 10000, 
                new List<RecipeItem>
                {
                    new RecipeItem(ItemDatabase.Items["metal"], 3),
                    new RecipeItem(ItemDatabase.Items["copper"], 2),
                }, 
                new List<RecipeItem>
                {
                    new RecipeItem(ItemDatabase.Items["gold"])
                })
        };

        public static List<Recipe> GetRandomRecipes(int recipesCount = 3)
        {
            var recipes = new List<Recipe>();
            for (var i = 0; i < recipesCount; i++)
            {
                var foundRecipe = Recipes[_rng.Next(0, Recipes.Count)];
                if (!recipes.Contains(foundRecipe))
                {
                    recipes.Add(foundRecipe);
                }
            }

            return recipes;
        }

        public static Recipe GetRecipe(string name)
        {
            return Recipes.FirstOrDefault(recipe => recipe.Name == name);
        }
    }
}