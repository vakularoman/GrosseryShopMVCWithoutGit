namespace AquaPlayground.Models
{
    public class Nutrition
    {
        #region [main properties]
        public long NutritionId { get; set; }

        public long ProductId { get; set; }

        public int Calories { get; set; }

        public int Protein { get; set; }

        public int Fat { get; set; }

        public int Carb { get; set; }
        #endregion

        #region [navigation properties]
        public virtual Product Product { get; set; }
        #endregion
    }
}
