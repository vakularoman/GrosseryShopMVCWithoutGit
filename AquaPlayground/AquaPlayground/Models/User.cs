namespace AquaPlayground.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser<long>
    {
        #region [main properties]
        public string? AvatarPath { get; set; }

        public DateTime? ResetPasswordGenerationTime { get; set; }
        #endregion

        #region [navigation properties]
        public virtual List<UserToPaymentSystem> UsersToPaymentSystems { get; set; }

        public virtual List<Order> Orders { get; set; }

        public virtual List<FavoriteProduct> FavoriteProducts { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<CartProduct> CartProducts { get; set; }
        #endregion
    }
}
