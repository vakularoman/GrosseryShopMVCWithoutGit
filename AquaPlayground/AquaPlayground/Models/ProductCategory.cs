namespace AquaPlayground.Models
{
    public class ProductCategory
    {
        #region [main properties]
        public long ProductCategoryId { get; set; }

        public string Name { get; set; }
        #endregion

        #region [navigation properties]
        public virtual List<ProductType> ProductTypes { get; set; }
        #endregion
    }
}
