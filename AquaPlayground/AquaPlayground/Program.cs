namespace AquaPlayground
{
    using AquaPlayground.DbSetUp;
    using AquaPlayground.Mapper;
    using AquaPlayground.Models;
    using AquaPlayground.Services;
    using AquaPlayground.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Serilog;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AquaPlaygroundContext>(options =>
                options.UseSqlServer(connection));

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            builder.Services.AddAutoMapper(typeof(AppMappingProfile));

            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<ISavingImageService, ImgurImageService>();
            builder.Services.AddScoped<IEmailService, GoogleEmailService>();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IAccountServiece, AccountService>();
            builder.Services.AddTransient<ICartProductService, CartProductService>();
            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddTransient<IUserService, UserService>();

            builder.Services.Configure<EmailConfigModel>(builder.Configuration.GetSection("GoogleEmailSettings"));

            builder.Services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<AquaPlaygroundContext>()
            .AddDefaultTokenProviders();

            builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(5);
            });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            await AppDbInitializer.SeedRolesAsync(app);
            await AppDbInitializer.SeedAdminAsync(app);
            await AppDbInitializer.SeedDataAsync(app);

            app.UseExceptionHandler("/Error/500");
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Product}/{action=Index}/{id?}");

            app.Run();
        }
    }
}