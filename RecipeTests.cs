namespace PortfolioOfEvidence.nUnitTests
{
    public class RecipeTests
    {

        [Test]
        public void calculateTotalCalories_Test()
        {
            // Arrange
            var recipe = new Recipe
            {
                Ingredients = new List<Ingredient>
                {
                    new Ingredient { Name = "Flour", Quantity = 2, MeasurementUnit = "cups", Calories = 100 },
                    new Ingredient { Name = "Sugar", Quantity = 1, MeasurementUnit = "cup", Calories = 200 },
                    new Ingredient { Name = "Butter", Quantity = 0.5, MeasurementUnit = "cup", Calories = 400 }
                }
            };

            // Act
            int totalCalories = recipe.calculateTotalCalories();

            // Assert
            Assert.AreEqual(700, totalCalories);
        }
    }
}