namespace AquaPlayground.Repository
{
    using AquaPlayground.Models;
    using AquaPlayground.Repository.Interfaces;

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AquaPlaygroundContext context)
            : base(context)
        {
        }
    }
}
