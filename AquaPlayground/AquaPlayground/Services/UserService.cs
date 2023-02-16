namespace AquaPlayground.Services
{
    using System.Linq.Expressions;
    using AquaPlayground.Models;
    using AquaPlayground.Services.Interfaces;
    using AquaPlayground.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork unitOfWork, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<PagingListViewModel<UserGetViewModel>> GetUsersInfo(int page, int count)
        {
            return await GetUsersInfoByPredicate(page, count);
        }

        public async Task<PagingListViewModel<UserGetViewModel>> GetUsersInfoBySubstring(string substring, int page, int count)
        {
            return await GetUsersInfoByPredicate(page, count, x => x.Email.Contains(substring));
        }

        public async Task EditUserRolesAsync(User user, List<string> roles)
        {
            using var transaction = _unitOfWork.CreateTransaction();

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = await _roleManager.Roles.ToListAsync();
            var addedRoles = roles.Except(userRoles);
            var removedRoles = userRoles.Except(roles);

            await _userManager.AddToRolesAsync(user, addedRoles);
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            await transaction.CommitAsync();
        }

        public async Task<UserRoleEditViewModel> GetUserWithRolesAsync(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();
            UserRoleEditViewModel result = new UserRoleEditViewModel
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserRoles = userRoles,
                AllRoles = allRoles,
            };

            return result;
        }

        private async Task<PagingListViewModel<UserGetViewModel>> GetUsersInfoByPredicate(
            int page,
            int count,
            Expression<Func<User, bool>> predicate = null)
        {
            var pagingList = new PagingListViewModel<UserGetViewModel> { CurrentPageNumber = page, RecordsPerPageCount = count };
            var elementsQuery = _unitOfWork.UserRepository.GetAsQueryable();

            if (predicate is not null)
            {
                elementsQuery = elementsQuery.Where(predicate);
            }

            pagingList.Elements = await elementsQuery
                .Select(x => new UserGetViewModel
                {
                    UserId = x.Id,
                    Name = x.UserName,
                    Email = x.Email,
                }).ToListAsync();

            pagingList.TotalPagesCount = await _unitOfWork.UserRepository.CountAsync();

            return pagingList;
        }
    }
}
