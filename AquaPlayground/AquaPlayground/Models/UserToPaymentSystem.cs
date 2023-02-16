namespace AquaPlayground.Models
{
    public class UserToPaymentSystem
    {
        #region [main properties]
        public long UserToPaymentSystemId { get; set; }

        public long PaymentSystemId { get; set; }

        public long UserId { get; set; }
        #endregion

        #region [navigation properties]
        public virtual PaymentSystem PaymentSystem { get; set; }

        public virtual User User { get; set; }
        #endregion
    }
}
