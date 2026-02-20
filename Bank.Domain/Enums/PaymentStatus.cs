using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Enums
{
    public enum PaymentStatus
    {
        Pending,
        Paid,
        PartiallyPaid,
        Overdue
    }
}
