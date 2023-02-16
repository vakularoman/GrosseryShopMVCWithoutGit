namespace AquaPlayground.Models
{
    public class Product
    {
        #region [main properties]
        public long ProductId { get; set; }

        public long ProductTypeId { get; set; }

        public long ManufacturerId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public double Price { get; set; }

        public int Count { get; set; }
        #endregion

        #region [navigation properties]
        public virtual Nutrition Nutrition { get; set; }

        public virtual ProductType ProductType { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual List<ProductToTag> ProductTags { get; set; }

        public virtual List<OrderProduct> OrderProducts { get; set; }

        public virtual List<FavoriteProduct> FavoriteProducts { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<CartProduct> CartProducts { get; set; }
        #endregion
    }
}
