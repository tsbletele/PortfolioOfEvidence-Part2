﻿namespace PortfolioOfEvidence
{
    class RecipeApp
    {
        static void Main(string[] args)
        {
            List<Recipe> recipes = new List<Recipe>();
            bool exit = false;

            while (!exit) //While the bool value is still false the program should continue running.
            {
                Console.WriteLine("Select an option: ");
                Console.WriteLine("1. Display recipe list");
                Console.WriteLine("2. Enter recipe details");
                Console.WriteLine("3. Scale recipe");
                Console.WriteLine("4. Reset quantity");
                Console.WriteLine("5. Clear all data");
                Console.WriteLine("6. Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice) //A swith is created for the user to choose between the different options in the program.
                {
                    case 1:
                        Console.Clear();
                        DisplayRecipeList(recipes);
                        break;
                    case 2:
                        Console.Clear();
                        Recipe newRecipe = new Recipe();
                        newRecipe.EnterRecipeDetails(); //For every case, a method is called from the Recipe Class.
                        recipes.Add(newRecipe);
                        break;
                    case 3:
                        Console.Clear();
                        ScaleRecipe(recipes);
                        break;
                    case 4:
                        Console.Clear();
                        ResetQuantities(recipes);
                        break;
                    case 5:
                        Console.Clear();
                        ClearAllData(recipes);
                        break;
                    case 6:
                        exit = true; //If case 6 is chosen then the bool value is made true and the program closes.
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid choice. Please enter a number from 1 to 7."); //If the users choice is invalid they will have to put a valid choice to run a task on the program.
                        break;

                }
            }
        }

        static void DisplayRecipeList(List<Recipe> recipes)
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes found.");
                return;
            }

            Console.WriteLine("Select a recipe: ");

            recipes = recipes.OrderBy(r => r.Name).ToList();

            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            int choice = int.Parse(Console.ReadLine());

            if (choice >= 1 && choice <= recipes.Count)
            {
                recipes[choice - 1].DisplayRecipeDetails();
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        static void ScaleRecipe(List<Recipe> recipes)
        {
            Console.WriteLine("Select a recipe to scale: ");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            int recipeChoice = int.Parse(Console.ReadLine()) - 1;

            if (recipeChoice >= 0 && recipeChoice < recipes.Count)
            {
                Console.WriteLine("Enter the factor you would like to scale the recipe by (e.g., 0.5, 2, 3): ");
                double factor = double.Parse(Console.ReadLine());
                recipes[recipeChoice].ScaleRecipe(factor);
            }
            else
            {
                Console.WriteLine("Invalid recipe choice.");
            }
        }

        static void ResetQuantities(List<Recipe> recipes)
        {
            Console.WriteLine("Select a recipe to reset quantities: ");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            int recipeChoice = int.Parse(Console.ReadLine()) - 1;

            if (recipeChoice >= 0 && recipeChoice < recipes.Count)
            {
                recipes[recipeChoice].ResetQuantity();
            }
            else
            {
                Console.WriteLine("Invalid recipe choice.");
            }
        }

        static void ClearAllData(List<Recipe> recipes)
        {
            Console.WriteLine("Are you sure you would like to clear all the data? (Yes/No)");
            string confirm = Console.ReadLine();

            if (confirm.ToLower() == "yes")
            {
                recipes.Clear();
                Console.WriteLine("All recipes have been removed from the system.");
            }
            else
            {
                Console.WriteLine("All recipes will remain on the system.");
            }
        }
    }

        public class Recipe
        {
            public string Name { get; set; }
            public List<Ingredient> Ingredients { get; set; }
            public List<string> Steps { get; set; }
            private List<double> originalQuantities;
            private int recipeCount = 0; //Recipe counter variable is created to keep track of the amount of recpies on the system.

            public delegate void CalorieNotification(int totalCalories);
            public event CalorieNotification OnCalorieExceeded;

            public Recipe()
            {
                Ingredients = new List<Ingredient>();
                Steps = new List<string>();
                originalQuantities = new List<double>();
                OnCalorieExceeded += NotifyCalorieExceeded;
            }

            public void EnterRecipeDetails()
            {
                Console.WriteLine("Enter the name of the recipe: ");
                Name = Console.ReadLine();

                Console.WriteLine("Enter the total number of ingredients in the recipe: ");
                int numIngredients = int.Parse(Console.ReadLine());

                for (int i = 0; i < numIngredients; i++)
                {
                    Ingredient ingredient = new Ingredient();

                    Console.WriteLine("Enter the name of ingredient " + (i + 1) + ": ");
                    ingredient.Name = Console.ReadLine();

                    Console.WriteLine("Enter the unit of measurement for the ingredient: ");
                    ingredient.MeasurementUnit = Console.ReadLine();

                    Console.WriteLine("Enter the quantity of the ingredient. (Corresponding to the unit of measurement): ");
                    ingredient.Quantity = double.Parse(Console.ReadLine());

                    Console.WriteLine("Enter the number of calories for the ingredient: ");
                    ingredient.Calories = int.Parse(Console.ReadLine());

                    Console.WriteLine("Select the food group of the ingredient: ");
                    Console.WriteLine("1. Starchy Foods");
                    Console.WriteLine("2. Fruits & Vegetables");
                    Console.WriteLine("3. Dry beans, Peas, Lentils & Soya");
                    Console.WriteLine("4. Chicken, Fish, Meat & Eggs");
                    Console.WriteLine("5. Milk & Dairy Products");
                    Console.WriteLine("6. Fats & Oils");
                    Console.WriteLine("7. Water");

                    int groupChoice = int.Parse(Console.ReadLine());
                    switch (groupChoice)
                    {
                        case 1:
                            ingredient.FoodGroup = "Starchy Foods";
                            break;

                        case 2:
                            ingredient.FoodGroup = "Fruits & Vegetables";
                            break;

                        case 3:
                            ingredient.FoodGroup = "Dry Beans, Peas, Lentils & Soya";
                            break;

                        case 4:
                            ingredient.FoodGroup = "Chicken, Meat, Fish & Eggs";
                            break;

                        case 5:
                            ingredient.FoodGroup = "Milk & Dairy Products";
                            break;

                        case 6:
                            ingredient.FoodGroup = "Fats & Oils";
                            break;

                        case 7:
                            ingredient.FoodGroup = "Water";
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Setting to 'Unknown'.");
                            ingredient.FoodGroup = "Unknown";
                            break;
                    }

                    Ingredients.Add(ingredient);
                    originalQuantities.Add(ingredient.Quantity);
                }

                Console.WriteLine("Enter the total number of steps for the recipe: ");
                int numSteps = int.Parse(Console.ReadLine());

                for (int i = 0; i < numSteps; i++)
                {
                    Console.WriteLine("Enter details for STEP " + (i + 1) + ": ");
                    Steps.Add(Console.ReadLine());
                }

                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("Recipe details have been stored successfully.");
                Console.WriteLine("---------------------------------------------");

                recipeCount++;
            }

            public void ScaleRecipe(double factor)
            {
                for (int i = 0; i < Ingredients.Count; i++)
                {
                    Ingredients[i].Quantity *= factor;
                }

                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("The recipe has been scaled by " + factor + ".");
                Console.WriteLine("---------------------------------------------");
            }

            public void DisplayRecipeDetails()
            {

                Console.WriteLine("---------------------------------------------");
                Console.WriteLine(Name.ToUpper() + ": ");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("INGREDIENTS: ");

            foreach(var ingredient in Ingredients)
            {
                Console.ForegroundColor = ConsoleColor.Green; // Use of advanced features. Changes colour of the console text.
                Console.WriteLine($"{ingredient.Quantity} {ingredient.MeasurementUnit} of {ingredient.Name} ({ingredient.Calories} cal - {ingredient.FoodGroup})");
                Console.ForegroundColor = ConsoleColor.White;
            }
                Console.ForegroundColor = ConsoleColor.Green; // Use of advanced features. Changes colour of the console text.
                Console.WriteLine($"{Ingredients[recipeCount - 1].Quantity} {Ingredients[recipeCount - 1].MeasurementUnit} of {Ingredients[recipeCount - 1].Name} ({Ingredients[recipeCount - 1].Calories} cal - {Ingredients[recipeCount - 1].FoodGroup})");
                Console.ForegroundColor = ConsoleColor.White;


                Console.WriteLine();
                Console.WriteLine("STEPS: ");

                for (int i = 0; i < Steps.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Step {i + 1}. {Steps[i]}");
                    Console.ForegroundColor = ConsoleColor.White; //Reverts to original text colour for the rest of the program.
                }

                int totalCalories = calculateTotalCalories();
                if (totalCalories > 300)
                {
                    OnCalorieExceeded?.Invoke(totalCalories);
                }
                else
                {
                    Console.WriteLine($"Total Calories: {totalCalories}");
                }
            }

            public int calculateTotalCalories()
            {
                return Ingredients.Sum(ingredient => ingredient.Calories);
            }

            private void NotifyCalorieExceeded(int totalCalories)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("WARNING: Total calories exceed 300!");
                Console.ResetColor();
                Console.WriteLine($"Total Calories: {totalCalories}");
            }

            public void ResetQuantity()
            {
                for (int i = 0; i < Ingredients.Count; i++)
                {
                    Ingredients[i].Quantity = originalQuantities[i];
                }

                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("Quantity has been reset to initial value.");
                Console.WriteLine("---------------------------------------------");
            }

            public void ClearData()
            {
                Ingredients.Clear();
                Steps.Clear();
                originalQuantities.Clear();
                recipeCount = 0;
            }
        }

        public class Ingredient
        {
            public string Name { get; set; }
            public double Quantity { get; set; }
            public string MeasurementUnit { get; set; }
            public int Calories { get; set; }
            public string FoodGroup { get; set; }
        }
}

