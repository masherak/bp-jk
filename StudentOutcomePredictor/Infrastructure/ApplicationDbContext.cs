using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

	}
}
