namespace AquaPlayground.Repository
{
    using AquaPlayground.Models;
    using AquaPlayground.Repository.Interfaces;

    public class ManufacturerRepository : RepositoryBase<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(AquaPlaygroundContext context)
            : base(context)
        {
        }
    }
}
