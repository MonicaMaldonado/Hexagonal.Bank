using Bank.Domain.Entities;
using Bank.Domain.Ports.Repositories;
using Bank.Domain.ValueObjects;


namespace Bank.Application.Ports.CredictProduct;

public class CreateCreditProductCase
{
    private readonly IBaseRepository<CreditProduct> _repository;

    public CreateCreditProductCase(IBaseRepository<CreditProduct> repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync()
    {
        var creditProduct = CreditProduct.Create(
            name: "Personal loan",
            description: "A personal loan for various needs.",
            minimumAmount: Money.Create("USD", 100),
            maximumAmount: Money.Create("USD", 1000),
            12,
            26,
            annualInterestRate: InterestRate.Create(5.5m)
           );

         await _repository.AddAsync(creditProduct);
    }
}
