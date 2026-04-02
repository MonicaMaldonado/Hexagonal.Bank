using Bank.Application.Dto.Request;
using Bank.Application.Ports.CredictProductUseCase;
using Bank.Application.Results;
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

    public async Task<Result<CreditProduct>> ExecuteAsyn(CreateCredictProductRequest request)
    {

        if (request == null)
            return Result<CreditProduct>.Fail("Request cannot be null", 400);

        var credictProduct = CreditProduct.Create(
            name: request.Name,
            description: request.Description,
            minimumAmount: Money.Create(request.Currency, request.MinimumAmount),
            maximumAmount: Money.Create(request.Currency, request.MaximumAmount),
            minimumTerm: request.MinimumTerm,
            maximumTerm: request.MaximumTerm,
            annualInterestRate: InterestRate.Create(request.AnnualInterestRate)
            );

        var result = await _creditProductRepository.AddAsync(credictProduct);
        await _unitOfWork.SaveChangeAsync();

        return Result<CreditProduct>.Ok("Producto crediticio registrado exitosamente",result); ;
    }
}
