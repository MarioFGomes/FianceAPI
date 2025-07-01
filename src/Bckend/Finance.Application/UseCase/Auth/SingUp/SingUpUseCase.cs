using Finance.Application.Mappers;
using Finance.Application.Service.Cryptography;
using Finance.Application.Validator;
using Finance.Communication.Request;
using Finance.Communication.Response;
using Finance.Domain.Repositories;
using Finance.Domain.Security.Token;
using Finance.Exception;
using Finance.Exception.Exceptions;

namespace Finance.Application.UseCase.Auth.SingUp; 
public class SingUpUseCase : ISingUpUseCase {
    private readonly PasswordEncryptor _PasswordEncryptor;
    private readonly IUserRepository _userRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    public SingUpUseCase(PasswordEncryptor PasswordEncryptor,IUserRepository userRepository, IAccessTokenGenerator accessTokenGenerator)
    {
        _PasswordEncryptor = PasswordEncryptor;
        _userRepository = userRepository;
        _accessTokenGenerator = accessTokenGenerator;
    }
    public async Task<ApiResponse<SingUpResponse>> Execute(SingUpRequest request) {

        Validar(request);
       
        var userExists = await _userRepository.AnyAsync(e=>e.Email==request.Email);
        
        if (userExists) throw new UserAlreadyExistsException(ResourceMessage.UserAlreadyExists);
        
        request.Password = _PasswordEncryptor.Encrypt(request.Password);

        var user= request.ToUserDomin();
         
       await _userRepository.AddOneAsync(user);
       
        var token = _accessTokenGenerator.Generate(user.Id);
        
        return new ApiResponse<SingUpResponse> {
            Message = ResourceMessage.AccountCreated,
            Success = true,
            Data = new SingUpResponse { Nome= user.Name,Token= token }
        };
    }

    private static void Validar(SingUpRequest request) {
        var validar = new SingUpValidator();
        var result = validar.Validate(request);

        if (!result.IsValid) {
            var messageError = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ExceptionValidatorError(messageError);
        }
    }
}
