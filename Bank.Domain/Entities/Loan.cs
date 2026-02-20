using Bank.Domain.Enums;
using Bank.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Entities
{
    public class Loan : BaseEntity
    {
        public Money Amount { get; private set; }
        public InterestRate InterestRate { get; private set; }
        public LoanTerm Term { get; private set; }
        public LoanStatus Status { get; private set; }
        public string RejectionReason { get; private set; }
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; } //propiedad de navegacion
        public Money TotalInterest => CalculateTotalInterest();
        public Money TotalAmount => Amount + TotalInterest;

        private readonly List<Payment> _payments = new();
        public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();


        private Loan()  { }

        public static Loan Create(Customer customer, Money amount, InterestRate interestRate, LoanTerm term)
        {
            if (customer == null) throw new ArgumentNullException("Customer cannot be null", nameof(customer));
            if (amount == null) throw new ArgumentNullException(nameof(amount),"Amount cannot be null");
            if (interestRate == null) throw new ArgumentNullException("InterestRate cannot be null", nameof (interestRate));
            if (term == null) throw new ArgumentNullException("Term cannot be null",nameof(term));

            return new Loan
            {
                Customer = customer,
                CustomerId = customer.Id,
                Amount = amount,
                InterestRate = interestRate,
                Term = term,
                Status = LoanStatus.Pending
            };
        }

        public void Approve()
        {
            if (Status != LoanStatus.Pending)  throw new InvalidOperationException("Only pending loans can be aprroved");

            Status = LoanStatus.Approved;
        }

        public void Rejected(string reason)
        {
            if (Status  == LoanStatus.Pending) throw new InvalidOperationException("Only pending loans can be rejected");

            if (string.IsNullOrEmpty(reason)) throw new InvalidOperationException("Rejection reason cannot be null or empty");

            Status = LoanStatus.Rejected;
            RejectionReason = reason;
        }

        private Money CalculateTotalInterest()
        {
            return InterestRate.CalculateInterest(Amount, Term.Months);
        }
    }
}
