using Finance.Communication.Request;
using Finance.Domain.Entities;

namespace Finance.Application.Mappers; 
public static class WalletMapper {

    public static Wallet ToWalletDomain(this WalletRequest walletRequest, Guid UserId) {

        return new Wallet {
            UserId = UserId,
            Balance = walletRequest.Balance,
            currency=walletRequest.currency
        };

    }

    //public static Wallet ToWalletCreditDomain(this WalletMovimentRequest walletRequest) {

    //    return new Wallet {
    //        Balance = walletRequest.Balance,
    //        currency= walletRequest.currency,
    //    };

    //}
}
