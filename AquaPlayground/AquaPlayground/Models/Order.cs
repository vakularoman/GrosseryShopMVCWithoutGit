namespace AquaPlayground.Models
{
    public class Order
    {
        #region [main properties]
        public long OrderId { get; set; }

        public long UserId { get; set; }

        public DateTime CreationTime { get; set; }

        public double TotalSpent { get; set; }
        #endregion

        #region [navigation properties]
        public virtual User User { get; set; }

        public virtual List<OrderProduct> OrderProducts { get; set; }
        #endregion
    }
}
