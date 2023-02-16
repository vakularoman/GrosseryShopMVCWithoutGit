namespace AquaPlayground.Models
{
    public class OrderProduct
    {
        #region [main properties]
        public long OrderProductId { get; set; }

        public long ProductId { get; set; }

        public long OrderId { get; set; }

        public int Count { get; set; }
        #endregion

        #region [navigation properties]
        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
        #endregion
    }
}
