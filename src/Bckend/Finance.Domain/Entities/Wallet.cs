using Finance.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Domain.Entities; 
public class Wallet: BaseEntity {
    public Guid UserId { get; set; }
    public decimal Balance {get;set; }
    public WalletStatus WalletStatus { get; set; } = WalletStatus.Enabled;
}
