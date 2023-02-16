namespace AquaPlayground.DbSetUp
{
    using System.Threading.Tasks;
    using AquaPlayground.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class AppDbInitializer
    {
        public static async Task SeedRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            var context = serviceScope.ServiceProvider.GetService<AquaPlaygroundContext>();
            context.Database.EnsureCreated();

            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            if (!await roleManager.RoleExistsAsync(AppRoles.Admin))
            {
                await roleManager.CreateAsync(new Role(AppRoles.Admin));
            }

            if (!await roleManager.RoleExistsAsync(AppRoles.Editor))
            {
                await roleManager.CreateAsync(new Role(AppRoles.Editor));
            }

            if (!await roleManager.RoleExistsAsync(AppRoles.User))
            {
                await roleManager.CreateAsync(new Role(AppRoles.User));
            }
        }

        public static async Task SeedAdminAsync(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
            string adminUserEmail = "admin@sb.com";
            string adminName = "admin";

            var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
            if (adminUser == null)
            {
                var newAdminUser = new User()
                {
                    UserName = adminName,
                    Email = adminUserEmail,
                };

                await userManager.CreateAsync(newAdminUser, "Admin_Pass1");
                await userManager.AddToRoleAsync(newAdminUser, AppRoles.Admin);
            }
        }

        public static async Task SeedDataAsync(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            var context = serviceScope.ServiceProvider.GetService<AquaPlaygroundContext>();

            if (!context.Manufacturers.Any())
            {
                await context.Manufacturers.AddRangeAsync(new List<Manufacturer>
                {
                    new Manufacturer { Name = "Syberry", Adress = "Inst Kulturi" },
                    new Manufacturer { Name = "Agro", Adress = "Vilage" },
                    new Manufacturer { Name = "Milk fabrik", Adress = "Vilage" },
                });
            }

            if (!context.Tags.Any())
            {
                await context.Tags.AddRangeAsync(new List<Tag>
                {
                    new Tag { Name = "For Syberry" },
                    new Tag { Name = "For .Net developers" },
                    new Tag { Name = "For children" },
                });
            }

            if (!context.ProductCategories.Any())
            {
                await context.ProductCategories.AddRangeAsync(new List<ProductCategory>
                {
                    new ProductCategory { Name = "Milk" },
                    new ProductCategory { Name = "Water" },
                    new ProductCategory { Name = "Fruit" },
                });
            }

            if (!context.ProductTypes.Any())
            {
                await context.ProductTypes.AddRangeAsync(new List<ProductType>
                {
                    new ProductType { Name = "Milk special", ProductCategoryId = 1 },
                    new ProductType { Name = "Milk regular", ProductCategoryId = 1 },
                    new ProductType { Name = "Water from rudnik", ProductCategoryId = 2 },
                    new ProductType { Name = "Casual water", ProductCategoryId = 2 },
                    new ProductType { Name = "Lemon", ProductCategoryId = 3 },
                    new ProductType { Name = "Apple", ProductCategoryId = 3 },
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
