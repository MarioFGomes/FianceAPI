using Finance.Communication.Response;
using Finance.Domain.Repositories;
using InvalidOperationException = Finance.Exception.Exceptions.InvalidOperationException;


namespace Finance.Application.UseCase.Wallet.CheckBalance;
public class CheckBalance : ICheckBalance {
    private readonly IWalletRepository _walletRepository;
    public CheckBalance(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }
    public async Task<ApiResponse<WalletResponse>> Execute(Guid UserId,string currency) 
    {
        if(!_checkCurrencyIsvalid(currency)) throw new InvalidOperationException("Operação Invalida");

        var wallet= await _walletRepository.GetOneAsync(e=>e.UserId==UserId && e.currency==currency);
        
        if(wallet is null) throw new InvalidOperationException("Operação Invalida");

        return new ApiResponse<WalletResponse> {
            Success = true,
            Message = "Consulta realizada com sucesso",
            Data = new WalletResponse { Balance = wallet.Balance, currency = wallet.currency }
        };


    }

    private bool _checkCurrencyIsvalid(string currency) 
    {
         string[] AcceptedCurrencies = new[] { "USD", "EUR", "AOA", "BRL" };
         if(AcceptedCurrencies.Contains(currency)) return true;
         return false;
    }
}
