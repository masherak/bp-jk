using Infrastructure.Enums;
using Infrastructure.Interfaces;

namespace Infrastructure.Entities;

public class TrainerType : IListTable<TrainerTypeEnum>
{
	public TrainerTypeEnum Id { get; set; }

	public string Name { get; set; }

	public ICollection<PredictionHistory> PredictionHistories { get; set; }
}
