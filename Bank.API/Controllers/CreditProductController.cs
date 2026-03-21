using Bank.Application.Dto.Request;
using Bank.Application.Ports.CredictProduct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditProductsController : ControllerBase
    {
        private readonly ICreateCredictProductUseCase _createCredictProductUseCase;

        public CreditProductsController(ICreateCredictProductUseCase createCredictProductUseCase)
        {
            _createCredictProductUseCase = createCredictProductUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCredictProductRequest request)
        {
            await _createCredictProductUseCase.ExecuteAsyn(request);
            return Ok();
        }

    }
}
