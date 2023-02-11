namespace DataVault.Infrastructure.Data;

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repositories.Models;

public class AppContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
	public ApplicationContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
		const string connectionString = "Filename=DataVault.db";
		optionsBuilder.UseSqlite(connectionString, builder =>
		{
			builder.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
		});
		return new ApplicationContext(optionsBuilder.Options);
	}
}