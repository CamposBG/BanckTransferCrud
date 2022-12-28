using BankTransactions.DBTables;
using BankTransactions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BankTransactions.Services
{
    public class TransactionService
    {
        private readonly TransactionTable _transactionTable;

        public TransactionService(TransactionTable transactionTable)
        {
            _transactionTable = transactionTable;
        }


        public async Task<List<Transaction>> GetTransactions()
        {
            var transaction = await _transactionTable.getAllTransactions();
            //ToDo validações entidades 
            return transaction;

        }

        public  JsonResult getTransactionsByName(string name)
        {
            var transactions = _transactionTable.getFilteredTransctions(name);
            return new JsonResult( transactions );
        }

        public Transaction createTransaction()
        {
            return (new Transaction());
        }

        public Transaction getTransactionByID(int id)
        {
            return (_transactionTable.getTransactionByID(id)); 
        }

        public async Task<String> createOrUpdateTransaction(Transaction transaction)
        {
            if (transaction.TransactionId == 0)
            {
                var newTransaction = await  _transactionTable.createTransaction(transaction);
                return "add";
            }
            else
            {
                var updatedTransaction = await  _transactionTable.updateTransaction(transaction);
                return "update";

            }               
        }

        public async Task<String> deleteTransaction(int id)
        {
            var response = await _transactionTable.deleteTransactionById(id);
            return "success";
        }
    }
}

