namespace AquaPlayground.Models
{
    public class ProductType
    {
        #region [main properties]
        public long ProductTypeId { get; set; }

        public long ProductCategoryId { get; set; }

        public string Name { get; set; }
        #endregion

        #region [navigation properties]
        public virtual ProductCategory ProductCategory { get; set; }

        public virtual List<Product> Products { get; set; }
        #endregion
    }
}
