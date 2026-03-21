using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Dto.Request
{
    public class CreateCredictProductRequest
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Currency { get; set; } = default!;
        public decimal MinimumAmount { get; set; }
        public decimal MaximumAmount { get; set; }
        public int MinimumTerm { get; set; }
        public int MaximumTerm { get; set; }
        public decimal AnnualInterestRate { get; set; }
    }
}
