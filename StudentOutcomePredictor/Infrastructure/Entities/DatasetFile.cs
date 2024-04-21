// ReSharper disable All
namespace Infrastructure.Entities;

public class DatasetFile
{
	public int Id { get; set; }

	public string Name { get; set; }

	public byte[] Content { get; set; }

	public DateTime Created { get; set; }

	public ICollection<PredictionHistory> PredictionHistories { get; set; }
}
