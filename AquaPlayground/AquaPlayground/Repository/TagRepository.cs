namespace AquaPlayground.Repository
{
    using AquaPlayground.Models;
    using AquaPlayground.Repository.Interfaces;

    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(AquaPlaygroundContext context)
            : base(context)
        {
        }
    }
}
