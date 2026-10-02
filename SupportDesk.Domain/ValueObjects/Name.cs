namespace SupportDesk.Domain.ValueObjects;

public record Name
{
    public string Value { get; }

	public Name(string value, int minLength = 2)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			throw new ArgumentException("Name is required", nameof(value));
		}

		var normalizedValue = value.Trim();

		if (normalizedValue.Length < minLength)
		{
			throw new ArgumentException($"Name must contain at least {minLength} characters.", nameof(value));
		}

		Value = normalizedValue;
	}

	public override string ToString() => Value;
}
