using Bank.Application.Dto.Request;
using Bank.Application.Results;
using Bank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Ports.CredictProductUseCase
{
    public interface ICreateCredictProductUseCase
    {
        Task<Result<CreditProduct>> ExecuteAsyn(CreateCredictProductRequest request);
    }
}
