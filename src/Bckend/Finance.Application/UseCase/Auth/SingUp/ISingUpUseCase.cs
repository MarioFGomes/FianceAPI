using Finance.Communication.Request;
using Finance.Communication.Response;

namespace Finance.Application.UseCase.Auth.SingUp; 
public interface ISingUpUseCase {
   Task<ApiResponse<SingUpResponse>> Execute(SingUpRequest request);
}
