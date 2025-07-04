using Finance.Application.UseCase.User.FetchUser;
using Finance.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers; 
public class UserController: FinanceController {

    [HttpGet]
    [Route("fetch/search")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Fetch([FromServices] IFecthUserUseCase useCase, [FromQuery] string search) {
        var resultado = await useCase.Execute(search);
        return Ok(resultado);
    }
}
