using Bank.Domain.Enums;
using Bank.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid LoanId { get; set; }
        public Loan Loan { get; set; } //Propiedad de navegacion
        public int Number { get; set; }
        public DateTime DueDate { get; set; }
        public Money Amount { get; set; }
        public Money Interest { get; set; }
        public Money Total => Amount + Interest;
        public PaymentStatus Status { get; set; }
        public DateTime? PaymentDate { get; set; }
        public Money PaidAmount { get; set; } = Money.Create("USD",0);

        private Payment() {}

        private Payment(Loan loan, int number, DateTime dueDate, Money amount, Money interest)
        {
            if (loan == null)   throw new ArgumentException(nameof(loan),"Loan cannot be null");

            if (amount == null) throw new ArgumentNullException(nameof(amount),"Amount cannot be null");

            if (interest == null)  throw new ArgumentException(nameof(interest),"Interest cannot be null");

            LoanId = loan.Id;
            Loan = loan;
            Number = number;
            DueDate = dueDate;
            Amount = amount;
            Interest = interest;
            Status = PaymentStatus.Pending;
        }

        public void ApplyPayment(Money paidAmount, DateTime paymentDate)
        {
            if (paidAmount == null) throw new ArgumentException("Paid amount cannot br null",nameof(paidAmount));
            if (paymentDate == default) throw new ArgumentNullException("PaymentDate must be a valid date", nameof(paymentDate));

            PaidAmount += paidAmount;
            PaymentDate = paymentDate;

            var paid = PaidAmount.Amount ?? 0;
            var total = Total.Amount ?? 0;

            if (paid >= total)
                Status = PaymentStatus.Paid;
            else if (paid > 0 && paid < total)
                Status = PaymentStatus.PartiallyPaid;

        }
    }
}
