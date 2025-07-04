using Finance.Domain.Enum;
using Finance.Domain.Repositories;
using InvalidOperationException = Finance.Exception.Exceptions.InvalidOperationException;

namespace Finance.Application.UseCase.Wallet.Disabled;
public class DisabledWallet : IDisabledWallet {
    private readonly IWalletRepository _walletRepository;
    public DisabledWallet(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }
    public async Task<bool> Execute(Guid UserId,string currency) 
    {
        if (!_CurrencyIsvalid(currency)) throw new InvalidOperationException("Operação Invalida");
        var wallet = await _walletRepository.GetOneAsync(e => e.UserId == UserId && e.currency == currency);
        if (wallet is null) throw new InvalidOperationException("A Wallet não foi encontrada");
        wallet.WalletStatus = WalletStatus.Disabled;
        await _walletRepository.ReplaceOneAsync(e=>e.Id==wallet.Id,wallet);
        return true;
    }

    private bool _CurrencyIsvalid(string currency) {
        string[] AcceptedCurrencies = new[] { "USD", "EUR", "AOA", "BRL" };
        if (AcceptedCurrencies.Contains(currency)) return true;
        return false;
    }
}
