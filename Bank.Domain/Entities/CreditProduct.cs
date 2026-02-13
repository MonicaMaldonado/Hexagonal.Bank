using Bank.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Entities
{
    public class CreditProduct : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Money MinimumAmount { get; private set; }
        public Money MaximumAmount { get; private set; }
        public int MinimumTerm { get; private set; }
        public int MaximumTerm { get; private set; }
        public InterestRate AnnualInterestRate {  get; private set; }

        private CreditProduct() {  }

        private CreditProduct(string name, string description, Money minimumAmount, Money maximumAmount, int minimunTerm, int maximumTerm, InterestRate annualInterestRate)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("Name cannot be null or empty",nameof(name));

            if (string.IsNullOrEmpty(description)) throw new ArgumentNullException("Description cannot be null or empty", nameof(description));

            if (minimumAmount == null) throw new ArgumentNullException("MinimumAmount cannot be null", nameof(minimumAmount));
            if (maximumAmount == null) throw new ArgumentNullException("MaximumAmount cannot be null", nameof(maximumAmount));

            if (minimunTerm <= 0) throw new ArgumentOutOfRangeException("MinimumTerm must be greater than zero",nameof(minimunTerm));
            if (maximumTerm <= 0) throw new ArgumentOutOfRangeException("MaximumTerm must be greater than zero", nameof(maximumTerm));

            if (annualInterestRate == null) throw new ArgumentNullException("AnnualInterestRate cannot be null",nameof(annualInterestRate));

            Name = name;
            Description = description;
            MinimumAmount = minimumAmount;
            MaximumAmount = maximumAmount;
            MaximumTerm = minimunTerm;
            MaximumTerm = maximumTerm;
            AnnualInterestRate = annualInterestRate;
        }

        public static CreditProduct Create(string name, string description, Money minimumAmount, Money maximumAmount, int minimumTerm, int maximumTerm, InterestRate annualInterestRate)
        { 
            return new CreditProduct(name, description, minimumAmount, maximumAmount, minimumTerm, maximumTerm, annualInterestRate);
        }
    }
}
