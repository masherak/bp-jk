using Infrastructure.Enums;
using Infrastructure.Interfaces;

namespace Infrastructure.Entities;

public class PipelineType : IListTable<PipelineTypeEnum>
{
	public PipelineTypeEnum Id { get; set; }

	public string Name { get; set; }

	public ICollection<PredictionHistory> PredictionHistories { get; set; }
}
