namespace Bank.Domain.ValueObjects;

public record Money
{
    public string? Currency { get; init; }
    public decimal? Amount { get; init; }

    private Money() { }  //Entity framework necesita un constructor privado vacio

    private Money(string currency, decimal amount)
    {
        if (string.IsNullOrEmpty(currency)) throw new ArgumentNullException("Currency cannot be null or empty", nameof(currency));

        if (amount <= 0) throw new ArgumentOutOfRangeException("Amount cannot be negative or zero", nameof(amount));

        Currency = currency;
        Amount = amount;
    }

    public static Money Create(string currency, decimal amount)
    {
        return new Money(currency, amount);
    }

    //Sobrecarga sobre metodo primitivo, la operacion suma, el operado +
    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency) throw new InvalidOperationException("Cannot add money with different currencies");

        return new Money(a.Currency!, a.Amount!.Value +  b.Amount!.Value);
    }
}
