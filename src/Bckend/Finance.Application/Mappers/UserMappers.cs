
using Finance.Communication.Request;
using Finance.Communication.Response;
using Finance.Domain.Entities;

namespace Finance.Application.Mappers; 
public static  class UserMappers {
    
    public static User ToUserDomain(this SingUpRequest singUpRequest) 
    {
        return new User {
            Name = singUpRequest.Name,
            Email = singUpRequest.Email,
            Password = singUpRequest.Password,
        };
    }

    public static UserResponse ToUserResponse(this User user) {
        return new UserResponse {
            Name = user.Name,
            Email = user.Email,
            UserStatus = user.UserStatus,
            CreatedAt=user.CreatedAt
        };
    }
}
