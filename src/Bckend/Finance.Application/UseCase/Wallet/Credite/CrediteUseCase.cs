using Finance.Application.Validators;
using Finance.Communication.Request;
using Finance.Communication.Response;
using Finance.Domain.Enum;
using Finance.Domain.Repositories;
using Finance.Exception;
using Finance.Exception.Exceptions;
using InvalidOperationException = Finance.Exception.Exceptions.InvalidOperationException;

namespace Finance.Application.UseCase.Wallet.Credite;
public class CrediteUseCase : ICrediteUseCase {
    
    private readonly IWalletRepository _walletRepository;
    
    public CrediteUseCase(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
        
    }
    public async Task<ApiResponse<WalletMovimentResponse>> Execute(WalletMovimentRequest request,Guid userId) 
    {
        Validar(request);
        _walletRepository.BeginTransaction();

        try {
            var wallet = await _walletRepository.GetOneAsync(e => e.UserId == userId && e.currency==request.currency);
            if(wallet is null) throw new InvalidOperationException("Conta bloqueada ou desbilitada");
            wallet.Credit(request.Amount, request.Description,request.currency);
            await _walletRepository.ReplaceOneAsync(e => e.Id == wallet.Id, wallet);
            _walletRepository.CommitTransaction();
            return new ApiResponse<WalletMovimentResponse> { 
                Success = true,
                Message=ResourceMessage.SuccessfullyAddedValue,
                Data=new WalletMovimentResponse { Amount=request.Amount,BalanceAfterMovement = wallet.Balance,currency=request.currency, WalletId = wallet.Id, MovimentType = MovimentType.CREDIT,Description = request.Description,CreatedAt= wallet .CreatedAt}
            };
        } catch {
           _walletRepository.BeginTransaction();
            throw;
        }
    }

    private static void Validar(WalletMovimentRequest request) {
        var validar = new WalletMovimentValidator();
        var result = validar.Validate(request);

        if (!result.IsValid) {
            var messageError = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ExceptionValidatorError(messageError);
        }
    }
}
