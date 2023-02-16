namespace AquaPlayground.Models
{
    public class FavoriteProduct
    {
        #region [main properties]
        public long FavoriteProductId { get; set; }

        public long UserId { get; set; }

        public long ProductId { get; set; }
        #endregion

        #region [navigation properties]
        public virtual User User { get; set; }

        public virtual Product Product { get; set; }
        #endregion
    }
}
