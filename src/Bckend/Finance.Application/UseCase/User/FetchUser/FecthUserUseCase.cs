using Finance.Application.Mappers;
using Finance.Communication.Response;
using Finance.Domain.Repositories;
using Finance.Exception.Exceptions;
using InvalidOperationException = Finance.Exception.Exceptions.InvalidOperationException;

namespace Finance.Application.UseCase.User.FetchUser;
public class FecthUserUseCase : IFecthUserUseCase {
    private readonly IUserRepository _userRepository;
    public FecthUserUseCase(IUserRepository userRepository)
    {
        _userRepository= userRepository;
    }
    public async Task<ApiResponse<UserResponse>> Execute(string search) {
        if(string.IsNullOrEmpty(search)) throw new InvalidOperationException("Operação Invalida");
        var user=await _userRepository.GetOneAsync(e=>e.Name.Contains(search) || e.Email.Equals(search));
        if (user is null) throw new ResourceNotFoundException("Utilizador não encontrado");
        return new ApiResponse<UserResponse> { 
            Success = true,
            Message="Utilizador encontrado",
            Data= user.ToUserResponse()
        };
    }
}
