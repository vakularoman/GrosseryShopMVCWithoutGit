namespace AquaPlayground.Models
{
    public class Comment
    {
        #region [main properties]
        public long CommentId { get; set; }

        public long UserId { get; set; }

        public long ProductId { get; set; }

        public string Text { get; set; }

        public DateTime CreationTime { get; set; }
        #endregion

        #region [navigation properties]
        public virtual User User { get; set; }

        public virtual Product Product { get; set; }
        #endregion
    }
}
