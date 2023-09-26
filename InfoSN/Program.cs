using InfoSN.Managers.Abstractions;
using InfoSN.Managers.Implementations;
using InfoSN.Options;
using InfoSN.Repositories.Abstractions;
using InfoSN.Repositories.Implementations;
using InfoSN.Services.Abstractions;
using InfoSN.Services.Implementations;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InfoSN
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddAuthentication().AddCookie("LoginCookie");

			builder.Services.AddOptions<PasswordHasherOptions>().Bind(builder.Configuration.GetSection("PasswordHasher"));

			builder.Services.AddScoped<IAccountService, AccountService>();
			builder.Services.AddScoped<IAccountManager, AccountManager>();

			builder.Services.AddScoped<IUserRepository, UserRepository>();

			builder.Services.AddScoped<IRoleRepository, RoleRepository>();

			builder.Services.AddScoped<IArticleService, ArticleService>();
			builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

			builder.Services.AddScoped<ICookieAuthenticationManager, CookieAuthenticationManager>();

			string? connectionString = builder.Configuration["SecretSQLServerConnectionString"];
			builder.Services.AddTransient<IDbConnection>(db => new SqlConnection(connectionString));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}