using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.ValueObjects
{
    public record LoanTerm
    {
        public int Months { get; set; }
        
        private LoanTerm() {  }

        private LoanTerm(int months)
        {
            if (months < 0) throw new ArgumentOutOfRangeException("Months must be greater than zero",nameof(months));

            if (months > 360) throw new ArgumentOutOfRangeException("Months must less or equal to 360", nameof(months));

            Months = months;
        }

        public static LoanTerm Create(int months)
        {
            return new LoanTerm(months);    
        }

        public (int Years, int MonthsReminder) ToYearAndMonths() 
        {
            return (Months / 12, Months % 12);
        }
    }
}
