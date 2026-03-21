using Bank.Application.Dto.Request;
using Bank.Application.Ports.CredictProduct;
using Bank.Domain.Entities;
using Bank.Domain.Ports.Repositories;
using Bank.Domain.Ports.Services;
using Bank.Domain.ValueObjects;


namespace Bank.Application.UseCases.CreditProductUseCase;

public class CreateCreditProductUseCase : ICreateCredictProductUseCase
{
    private readonly ICreditProductRepository _creditProductRepository; 
    private readonly IUnitOfWork _unitOfWork;

    public CreateCreditProductUseCase(ICreditProductRepository creditProductRepository, IUnitOfWork unitOfWork)
    {
        _creditProductRepository = creditProductRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsyn(CreateCredictProductRequest request)
    {
        var credictProduct = CreditProduct.Create(
            name: request.Name,
            description: request.Description,
            minimumAmount: Money.Create(request.Currency, request.MinimumAmount),
            maximumAmount: Money.Create(request.Currency, request.MaximumAmount),
            minimumTerm: request.MinimumTerm,
            maximumTerm: request.MaximumTerm,
            annualInterestRate: InterestRate.Create(request.AnnualInterestRate)
            );

        await _creditProductRepository.AddAsync(credictProduct);
        await _unitOfWork.SaveChangeAsync();
    }
}
