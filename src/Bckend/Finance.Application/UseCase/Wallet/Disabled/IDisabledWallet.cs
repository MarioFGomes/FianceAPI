using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.UseCase.Wallet.Disabled; 
public interface IDisabledWallet {
    Task<bool> Execute(Guid UserId, string currency);
}
