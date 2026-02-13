using Bank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.ValueObjects
{
    public record InterestRate
    {
        public decimal Value { get; set; }
        public decimal AsPercentaje => Value * 100;

        private InterestRate() { }
        private InterestRate(decimal value)
        {
            if (value < 1) throw new ArgumentException("Interest rate must be greater or equal to 1", nameof(value));
            value = Value;
        }

        public static InterestRate Create(decimal value)
        {
            return new InterestRate(value);
        }

        public Money CalculateInterest(Money amount, int months) {

            decimal interest = (amount.Amount ?? 0) * Value * months / 12;
            return Money.Create(amount.Currency, interest);
        }
    }
}
