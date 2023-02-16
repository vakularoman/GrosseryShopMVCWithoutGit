namespace AquaPlayground.Services.Interfaces
{
    public interface ISavingImageService
    {
        public Task<string> SaveImage(IFormFile image);
    }
}
