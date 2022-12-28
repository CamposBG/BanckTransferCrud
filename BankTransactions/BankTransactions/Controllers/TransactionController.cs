using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankTransactions.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Primitives;
using System.Globalization;
using BankTransactions.Services;

namespace BankTransactions.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionService _transactionService;

        // TransactionDbContext is passed as parameter through dependency injection
        public TransactionController(TransactionDbContext context,TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
              List<Transaction> transaction = await _transactionService.GetTransactions();
              return View(transaction);
        }


        // GET: Transaction/Filter
        public  IActionResult Filter()
        {
            var name = Request.Query["name"].ToString();
            var transactions = _transactionService.getTransactionsByName(name);
            return Json(new { transactions });
        }

        // GET: Transaction/AddOrEdit
        public IActionResult AddOrEdit(int id = 0)
        {
            if(id == 0)
                return View(_transactionService.createTransaction());
            else
                return View(_transactionService.getTransactionByID(id));

        }

        // POST: Transaction/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,AccountNumber,BeneficiaryName,BanckName,SWIFTCode,Amount,Date")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var response = await _transactionService.createOrUpdateTransaction(transaction);
                if (response == "update")
                {
                     return  RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction("Index");
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _transactionService.deleteTransaction(id); 
            return RedirectToAction(nameof(Index));
        }
    }
}
