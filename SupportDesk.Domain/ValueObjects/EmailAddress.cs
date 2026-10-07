using System.Net.Mail;

namespace SupportDesk.Domain.ValueObjects;

public record EmailAddress
{
    public string Value { get; }

	public EmailAddress(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			throw new ArgumentException("E-postadress måste anges.");
		}

		var normalizedValue = value.Trim().ToLowerInvariant();

		if (!MailAddress.TryCreate(normalizedValue, out var emailAddress) || !string.Equals(emailAddress.Address, normalizedValue, StringComparison.Ordinal))
		{
			throw new ArgumentException("E-postadressen måste vara giltig.");
		}

		Value = normalizedValue;
	}

	public override string ToString() => Value;
}

