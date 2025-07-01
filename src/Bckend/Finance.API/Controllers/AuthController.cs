using Finance.Application.UseCase.Auth.SingIn;
using Finance.Application.UseCase.Auth.SingUp;
using Finance.Communication.Request;
using Finance.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers; 
public class AuthController: FinanceController {
    
    [HttpPost]
    [Route("SingUp")]
    [ProducesResponseType(typeof(SingUpResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> SingUp([FromServices] ISingUpUseCase useCase, [FromBody] SingUpRequest request) {

        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpPost]
    [Route("SingIn")]
    [ProducesResponseType(typeof(SingInResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SingIn([FromServices] ISingInUseCase useCase, [FromBody] SingInRequest request) {
       
        var result = await useCase.Execute(request);

        return Ok(result);

    }
}
