using Finance.Communication.Request;
using Finance.Communication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.UseCase.Auth.SingIn; 
public interface ISingInUseCase {
    public Task<ApiResponse<SingInResponse>> Execute(SingInRequest request);
}
