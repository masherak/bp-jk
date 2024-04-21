using Microsoft.ML.Data;

namespace PredictorApp.Sources;

public class InMemoryMultiStreamSource(byte[] data) : IMultiStreamSource
{
	private readonly MemoryStream _stream = new(data);

	public int Count => 1;

	public string GetPathOrNull(int index) => null;

	public Stream Open(int index)
	{
		if (index == 0)
		{
			_stream.Position = 0;

			return _stream;
		}

		throw new ArgumentOutOfRangeException(nameof(index));
	}

	public TextReader OpenTextReader(int index)
	{
		if (index == 0)
		{
			_stream.Position = 0;

			return new StreamReader(_stream, leaveOpen: true);
		}

		throw new ArgumentOutOfRangeException(nameof(index));
	}
}
