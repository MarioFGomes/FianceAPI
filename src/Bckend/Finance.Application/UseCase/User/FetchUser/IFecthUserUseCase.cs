using Finance.Communication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.UseCase.User.FetchUser; 
public interface IFecthUserUseCase {
    Task<ApiResponse<UserResponse>> Execute(string search);
}
