namespace Infrastructure.Entities;

public class EconomicIndicator
{
	public int EconomicIndicatorId { get; set; }
	public int StudentId { get; set; }
	public float UnemploymentRate { get; set; }
	public float InflationRate { get; set; }
	public float Gdp { get; set; }

	public Student Student { get; set; }
}
