namespace AquaPlayground.Models
{
    public class Tag
    {
        #region [main properties]
        public long TagId { get; set; }

        public string Name { get; set; }
        #endregion

        #region [navigation properties]
        public virtual List<ProductToTag> ProductsToTags { get; set; }
        #endregion
    }
}
