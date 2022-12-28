using BankTransactions.Models;
using BankTransactions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;

namespace BankTransactions.DBTables
{
    public class TransactionTable
    {
        private readonly TransactionDbContext _context;

        public TransactionTable(TransactionDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> getAllTransactions()
        {
            var transactionList = await _context.Transactions.ToListAsync();
            return (transactionList);
        }

        public JsonResult getFilteredTransctions(string name) 
        { 
            var transactions =  _context.Transactions.FromSqlRaw($"SELECT * FROM Transactions WHERE BeneficiaryName LIKE '%{name}%'");
            return new JsonResult(new { transactions });
        }

        public Transaction getTransactionByID (int id)
        {
            var transaction = _context.Transactions.Find(id);

            return transaction;
        }

        public async Task<Transaction> createTransaction(Transaction transaction)
        {
            transaction.Date = DateTime.Now;
            _context.Add(transaction);
            await _context.SaveChangesAsync();
            return (transaction);
            
        }

        public async Task<Transaction> updateTransaction(Transaction transaction) {
            _context.Update(transaction);
            await _context.SaveChangesAsync();
            return (transaction);
        }

        public async Task<Transaction> deleteTransactionById(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }
    }
}
