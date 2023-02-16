namespace AquaPlayground.Models
{
    public class CartProduct
    {
        #region [main properties]
        public long CartProductId { get; set; }

        public long UserId { get; set; }

        public long ProductId { get; set; }

        public int Count { get; set; }
        #endregion

        #region [navigation properties]
        public virtual User User { get; set; }

        public virtual Product Product { get; set; }
        #endregion
    }
}
