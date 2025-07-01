using Finance.Application.Service.Cryptography;
using Finance.Communication.Request;
using Finance.Communication.Response;
using Finance.Domain.Repositories;
using Finance.Domain.Security.Token;
using Finance.Exception.Exceptions;
using Finance.Exception;
using Finance.Application.Validators;

namespace Finance.Application.UseCase.Auth.SingIn;
public class SingInUseCase : ISingInUseCase {
    private readonly PasswordEncryptor _PasswordEncryptor;
    private readonly IUserRepository _userRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    public SingInUseCase(PasswordEncryptor PasswordEncryptor, IUserRepository userRepository, IAccessTokenGenerator accessTokenGenerator) {
        _PasswordEncryptor = PasswordEncryptor;
        _userRepository = userRepository;
        _accessTokenGenerator = accessTokenGenerator;
    }
    public async Task<ApiResponse<SingInResponse>> Execute(SingInRequest request) {

        Validar(request);

        var PasswordHash = _PasswordEncryptor.Encrypt(request.Password);

        var user = await _userRepository.GetOneAsync(e => e.Email == request.Email && e.Password== PasswordHash);

        if (user is null) throw new ResourceNotFoundException(ResourceMessage.InvalidLogin);
     
        var token = _accessTokenGenerator.Generate(user.Id);

        return new ApiResponse<SingInResponse> {
            Message = ResourceMessage.LoginSuccess,
            Success = true,
            Data = new SingInResponse { Name = user.Name, Token = token }
        };
    }

    private static void Validar(SingInRequest request) {
        var validar = new SingInValidator();
        var result = validar.Validate(request);

        if (!result.IsValid) {
            var messageError = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ExceptionValidatorError(messageError);
        }
    }
}
