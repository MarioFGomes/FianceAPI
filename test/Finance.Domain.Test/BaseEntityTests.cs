using System;
using Finance.Domain.Entities;
using Finance.Domain.Enum;
using Xunit;

namespace Finance.Domain.Test;

public class BaseEntityTests
{
    [Fact]
    public void BaseEntity_Initializes_With_Default_Values()
    {
        var entity = new BaseEntity();
        Assert.NotEqual(Guid.Empty, entity.Id);
        Assert.True((DateTime.UtcNow - entity.CreatedAt).TotalSeconds < 5);
        Assert.True((DateTime.UtcNow - entity.UpdatedAt).TotalSeconds < 5);
        Assert.Equal(BaseStatus.created, entity.Status);
    }
} 