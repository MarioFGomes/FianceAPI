using Finance.Application.Mappers;
using Finance.Application.Validators;
using Finance.Communication.Request;
using Finance.Communication.Response;
using Finance.Domain.Repositories;
using Finance.Exception;
using Finance.Exception.Exceptions;

namespace Finance.Application.UseCase.Wallet.Create;
public class CreateWalletUseCase : ICreateWalletUseCase {
    private readonly IWalletRepository _walletRepository;

    public CreateWalletUseCase(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }
    public async Task<ApiResponse<WalletResponse>> Execute(WalletRequest request, Guid UserId) {
        Validar(request);
        var ExistWalletForThisUser = await _walletRepository.AnyAsync(e => e.UserId ==UserId && e.currency==request.currency);
        if (ExistWalletForThisUser) throw new ResourceAlreadyExistsException(ResourceMessage.WalletAlreadyExists);
        var wallet = request.ToWalletDomain(UserId);
        await _walletRepository.AddOneAsync(wallet);
        return new ApiResponse<WalletResponse> { 
            Success = true,
            Message=ResourceMessage.WalletCreated,
            Data=new WalletResponse {Id=wallet.Id, Balance=wallet.Balance,currency=wallet.currency,UserId=wallet.UserId,WalletStatus=wallet.WalletStatus },
        };
    }

    private static void Validar(WalletRequest request) {
        var validar = new WalletValidator();
        var result = validar.Validate(request);

        if (!result.IsValid) {
            var messageError = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ExceptionValidatorError(messageError);
        }
    }
}
