namespace AquaPlayground.Models
{
    public class PaymentSystem
    {
        #region [main properties]
        public long PaymentSystemId { get; set; }

        public string Name { get; set; }
        #endregion

        #region [navigation properties]
        public virtual List<UserToPaymentSystem> UserPaymentSystems { get; set; }
        #endregion
    }
}
