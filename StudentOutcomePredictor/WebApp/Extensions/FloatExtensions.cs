namespace WebApp.Extensions;

public static class FloatExtensions
{
	public static float Round(this float value, int decimals = 2)
	{
		return (float)((double)value).Round(decimals);
	}
}
