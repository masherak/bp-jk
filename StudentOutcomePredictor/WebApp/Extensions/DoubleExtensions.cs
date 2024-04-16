namespace WebApp.Extensions;

public static class DoubleExtensions
{
	public static double Round(this double value, int decimals = 2)
	{
		return Math.Round(value, decimals);
	}
}
