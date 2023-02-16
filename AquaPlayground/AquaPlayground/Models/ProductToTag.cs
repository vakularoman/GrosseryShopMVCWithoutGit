namespace AquaPlayground.Models
{
    public class ProductToTag
    {
        #region [main properties]
        public long ProductToTagId { get; set; }

        public long ProductId { get; set; }

        public long TagId { get; set; }
        #endregion

        #region [navigation properties]
        public virtual Tag Tag { get; set; }

        public virtual Product Product { get; set; }
        #endregion
    }
}
