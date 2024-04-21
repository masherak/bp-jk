namespace Infrastructure.Interfaces;

public interface IListTable<TEnum>
{
	public TEnum Id { get; set; }

	public string Name { get; set; }
}
