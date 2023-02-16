namespace AquaPlayground.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ProductPostViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public IFormFile Image { get; set; }

        [Range(0.01, double.MaxValue)]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Count is required")]
        public int Count { get; set; }

        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Calories is required")]
        public int Calories { get; set; }

        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Protein is required")]
        public int Protein { get; set; }

        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Fat is required")]
        public int Fat { get; set; }

        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Carb is required")]
        public int Carb { get; set; }

        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Carb is required")]
        public long ProductTypeId { get; set; }

        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Carb is required")]
        public long ManufacturerId { get; set; }

        public List<long> TagsIds { get; set; }
    }
}
