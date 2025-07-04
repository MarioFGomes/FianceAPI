using Finance.Domain.Entities;
using Finance.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.DataAcess.Seeds; 
public class SeedData {

    public static void Seed(ModelBuilder modelBuilder) {
        
        var user1 = new User { 
            Id = Guid.NewGuid(), 
            Name = "Elisa Ferreira", 
            Email = "elisa@example.com", 
            Password = BCrypt.Net.BCrypt.HashPassword("123456"), 
            CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow 
        };

        var user2 = new User { 
            Id = Guid.NewGuid(), 
            Name = "Pedro Castro", 
            Email = "pedro@example.com", 
            Password = BCrypt.Net.BCrypt.HashPassword("123456"), 
            CreatedAt = DateTime.UtcNow, 
            UpdatedAt = DateTime.UtcNow 
        };


        modelBuilder.Entity<User>().HasData(user1, user2);

        var wallet1 = new Wallet { 
            Id = Guid.NewGuid(), 
            UserId = user1.Id, 
            Balance = 500m,
            currency= "BRL",
            CreatedAt = DateTime.UtcNow, 
            UpdatedAt = DateTime.UtcNow 
        };
        var wallet2 = new Wallet { 
            Id = Guid.NewGuid(), 
            UserId = user2.Id, 
            Balance = 1000m,
            currency = "BRL",
            CreatedAt = DateTime.UtcNow, 
            UpdatedAt = DateTime.UtcNow 
        };
        
        modelBuilder.Entity<Wallet>().HasData(wallet1, wallet2);

        
        var transaction = new Transaction {
            Id = Guid.NewGuid(),
            SenderWalletId = wallet1.Id,
            ReceiverWalletId = wallet2.Id,
            Amount = 200m,
            Description = "Pagamento por serviço",
            CreatedAt = DateTime.UtcNow
        };

        modelBuilder.Entity<Transaction>().HasData(transaction);

        
        var walletTransaction1 = new WalletMovement {
            Id = Guid.NewGuid(),
            WalletId = wallet1.Id,
            MovimentType = MovimentType.DEBIT,
            Amount = 200m,
            currency = "BRL",
            TransactionId = transaction.Id,
            Description = "Transferência para Maria",
            CreatedAt = DateTime.UtcNow
        };

        var walletTransaction2 = new WalletMovement {
            Id = Guid.NewGuid(),
            WalletId = wallet2.Id,
            MovimentType = MovimentType.CREDIT,
            Amount = 200m,
            currency = "BRL",
            TransactionId = transaction.Id,
            Description = "Recebido de João",
            CreatedAt = DateTime.UtcNow
        };

        modelBuilder.Entity<WalletMovement>().HasData(walletTransaction1, walletTransaction2);

    }
}
