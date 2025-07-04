using Finance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Service.LoggedUser; 
public interface ILoggedUser {
    Task<User?> RecoverUser();
}
