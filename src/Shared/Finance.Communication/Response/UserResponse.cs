﻿using Finance.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Communication.Response; 
public class UserResponse {
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserStatus UserStatus { get; set; }
    public DateTime CreatedAt { get; set; }
}
