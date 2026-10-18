namespace SupportDesk.Domain.ValueObjects;

public record Name
{
    public string Value { get; }

	public Name(string value, int minLength = 2)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			throw new ArgumentException("Namn måste anges.");
		}

		var normalizedValue = value.Trim();

		if (normalizedValue.Length < minLength)
		{
			throw new ArgumentException($"Namnet måste innehålla minst {minLength} tecken.");
		}

		Value = normalizedValue;
	}

	public override string ToString() => Value;
}
