namespace DataVault.Infrastructure.Data.Repositories.Models;

using Microsoft.EntityFrameworkCore;

public partial class ApplicationContext : DbContext
{
	public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
	{
	}

	public virtual DbSet<Person> Person => Set<Person>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Person>(entity =>
		{
			entity.HasIndex(e => e.Name).IsUnique();
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}