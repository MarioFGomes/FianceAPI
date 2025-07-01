
using Finance.Communication.Request;
using Finance.Domain.Entities;

namespace Finance.Application.Mappers; 
public static  class UserMappers {
    
    public static User ToUserDomin(this SingUpRequest singUpRequest) 
    {
        return new User {
            Name = singUpRequest.Name,
            Email = singUpRequest.Email,
            Password = singUpRequest.Password,
        };
    }
}
