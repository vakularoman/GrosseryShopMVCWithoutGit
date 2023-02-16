namespace AquaPlayground.Services.Interfaces
{
    using AquaPlayground.Models;
    using AquaPlayground.ViewModels;

    public interface IUserService
    {
        public Task<PagingListViewModel<UserGetViewModel>> GetUsersInfo(int page, int count);

        public Task<PagingListViewModel<UserGetViewModel>> GetUsersInfoBySubstring(string substring, int page, int count);

        public Task EditUserRolesAsync(User user, List<string> roles);

        public Task<UserRoleEditViewModel> GetUserWithRolesAsync(User user);
    }
}
