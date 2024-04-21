using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
	public static void AddEnumTable<TEntity, TEnum>(this ModelBuilder modelBuilder)
		where TEntity : class, IListTable<TEnum>, new()
		where TEnum : struct, Enum
	{
		modelBuilder.Entity<TEntity>().HasKey(_ => _.Id);
		modelBuilder.Entity<TEntity>().Property(_ => _.Id).HasConversion<int>();
		modelBuilder.Entity<TEntity>().Property(_ => _.Name).IsRequired();
		modelBuilder.Entity<TEntity>().HasData(Enum.GetValues<TEnum>().Select(_ => new TEntity { Id = _, Name = _.ToString() }));
	}
}
