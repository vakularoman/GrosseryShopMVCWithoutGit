namespace AquaPlayground.Services.Interfaces
{
    public interface IEmailService
    {
        public Task SendResetPasswordEmailAsync(string to, string link);
    }
}
