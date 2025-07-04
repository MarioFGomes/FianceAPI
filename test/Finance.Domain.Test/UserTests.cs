using System;
using Finance.Domain.Entities;
using Finance.Domain.Enum;
using Xunit;

namespace Finance.Domain.Test;

public class UserTests
{
    [Fact]
    public void User_Can_Be_Created_And_Properties_Set()
    {
        var user = new User
        {
            Name = "Test User",
            Email = "test@example.com",
            Password = "123456",
            UserStatus = UserStatus.Enabled
        };
        Assert.Equal("Test User", user.Name);
        Assert.Equal("test@example.com", user.Email);
        Assert.Equal("123456", user.Password);
        Assert.Equal(UserStatus.Enabled, user.UserStatus);
        Assert.NotEqual(Guid.Empty, user.Id);
    }
} 