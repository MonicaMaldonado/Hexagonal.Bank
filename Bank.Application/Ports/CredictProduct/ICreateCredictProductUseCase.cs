using Bank.Application.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Ports.CredictProduct
{
    public interface ICreateCredictProductUseCase
    {
        Task ExecuteAsyn(CreateCredictProductRequest request);
    }
}
