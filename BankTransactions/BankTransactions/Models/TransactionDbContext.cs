using Microsoft.EntityFrameworkCore;

namespace BankTransactions.Models
{
    public class TransactionDbContext:DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options) 
        {}

        // Create Table corresponding to the Transaction model  
        public DbSet<Transaction> Transactions { get; set;} 
    }
}
