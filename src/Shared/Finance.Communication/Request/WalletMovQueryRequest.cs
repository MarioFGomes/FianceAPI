using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Communication.Request; 
public class WalletMovQueryRequest {
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string currency { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
