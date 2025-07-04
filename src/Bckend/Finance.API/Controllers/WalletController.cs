using Finance.API.Filter;
using Finance.Application.UseCase.Transaction.Create;
using Finance.Application.UseCase.Wallet.CheckBalance;
using Finance.Application.UseCase.Wallet.Create;
using Finance.Application.UseCase.Wallet.Credite;
using Finance.Application.UseCase.Wallet.Debit;
using Finance.Application.UseCase.Wallet.Disabled;
using Finance.Application.UseCase.Wallet.Enabled;
using Finance.Application.UseCase.Wallet.Fetch;
using Finance.Communication.Request;
using Finance.Communication.Response;
using Finance.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers;

public class WalletController: FinanceController {

    [HttpPost]
    [Route("Create")]
    [ProducesResponseType(typeof(WalletResponse), StatusCodes.Status201Created)]
    [ServiceFilter(typeof(AuthenticatedUser))]
    public async Task<IActionResult> Create([FromServices] ICreateWalletUseCase useCase, [FromBody] WalletRequest request) {
       
        var user = HttpContext.Items["User"] as User;

        if (user is null) return Unauthorized();
       
        var result = await useCase.Execute(request, user.Id);

        return Created(string.Empty, result);
    }

    [HttpPost("deposit")]
    [ProducesResponseType(typeof(WalletMovimentResponse), StatusCodes.Status200OK)]
    [ServiceFilter(typeof(AuthenticatedUser))]
    public async Task<IActionResult> deposit([FromServices] ICrediteUseCase useCase, [FromBody] WalletMovimentRequest request) {

        var user = HttpContext.Items["User"] as User;

        if (user is null) return Unauthorized();

        var result = await useCase.Execute(request, user.Id);

        return Ok(result);
    }

    [HttpPost("withdraw")]
    [ProducesResponseType(typeof(WalletMovimentResponse), StatusCodes.Status200OK)]
    [ServiceFilter(typeof(AuthenticatedUser))]
    public async Task<IActionResult> withdraw([FromServices] IDebitUseCase useCase, [FromBody] WalletMovimentRequest request) {

        var user = HttpContext.Items["User"] as User;

        if (user is null) return Unauthorized();

        var result = await useCase.Execute(request, user.Id);

        return Ok(result);
    }

    [HttpPost]
    [Route("Transfer")]
    [ProducesResponseType(typeof(TransactionResponse), StatusCodes.Status200OK)]
    [ServiceFilter(typeof(AuthenticatedUser))]
    public async Task<IActionResult> Transfer([FromServices] ICreateTransitionUseCase useCase, [FromBody] TransactionRequest request) {
        
        var user = HttpContext.Items["User"] as User;

        if (user is null) return Unauthorized();

        var result = await useCase.Execute(request, user.Id);

        return Ok(result);
    }

    [HttpGet("Movements")]
    [ServiceFilter(typeof(AuthenticatedUser))]
    public async Task<IActionResult> Movements(
        [FromServices] IFetchWalletMov useCase,
        [FromQuery] string currency,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10) {

        var user = HttpContext.Items["User"] as User;

        if (user is null) return Unauthorized();

        var request = new WalletMovQueryRequest {
            currency=currency,
            StartDate = startDate,
            EndDate = endDate,
            Page = page,
            PageSize = pageSize
        };
        var result = await useCase.Execute(request,user.Id);
        return Ok(result);
    }

    [HttpGet("CheckBalance")]
    [ProducesResponseType(typeof(WalletResponse), StatusCodes.Status200OK)]
    [ServiceFilter(typeof(AuthenticatedUser))]
    public async Task<IActionResult> CheckBalance([FromServices] ICheckBalance useCase, [FromQuery] string currency) {

        var user = HttpContext.Items["User"] as User;

        if (user is null) return Unauthorized();

        var result = await useCase.Execute(user.Id, currency);

        return Ok(new { result.Success, result.Message, data=new { result.Data.Balance,result.Data.currency} });
    }

    [HttpPut("Disabled")]
    [ServiceFilter(typeof(AuthenticatedUser))]
    public async Task<IActionResult> Disabled([FromServices] IDisabledWallet useCase, [FromQuery] string currency) {

        var user = HttpContext.Items["User"] as User;

        if (user is null) return Unauthorized();

        var result = await useCase.Execute(user.Id,currency);

        return NoContent();
    }

    [HttpPut("Enabled")]
    [ServiceFilter(typeof(AuthenticatedUser))]
    public async Task<IActionResult> Enabled([FromServices] IEnableddWallet useCase, [FromQuery] string currency) {

        var user = HttpContext.Items["User"] as User;

        if (user is null) return Unauthorized();

        var result = await useCase.Execute(user.Id,currency);

        return NoContent();
    }
}
