using Microsoft.AspNetCore.Mvc;
using Expense_Tracker.Models;
using Expense_Tracker.Services;
using System.Threading.Tasks;
using Expense_Tracker.Services.Interfaces;

namespace Expense_Tracker.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ICategoryService _categoryService;

        public TransactionController(ITransactionService transactionService, ICategoryService categoryService)
        {
            _transactionService = transactionService;
            _categoryService = categoryService;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return View(transactions);
        }

        // GET: Transaction/AddOrEdit
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            await PopulateCategoriesAsync();
            if (id == 0)
            {
                return View(new Transaction());
            }
            else
            {
                var transaction = await _transactionService.GetTransactionByIdAsync(id);
                if (transaction == null)
                {
                    return NotFound();
                }
                return View(transaction);
            }
        }

        // POST: Transaction/AddOrEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,CategoryId,Amount,Note,Date")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                if (transaction.TransactionId == 0)
                {
                    await _transactionService.AddTransactionAsync(transaction);
                }
                else
                {
                    await _transactionService.UpdateTransactionAsync(transaction);
                }
                return RedirectToAction(nameof(Index));
            }
            await PopulateCategoriesAsync();
            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _transactionService.DeleteTransactionAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        private async Task PopulateCategoriesAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var categoryList = categories.ToList();
            categoryList.Insert(0, new Category { CategoryId = 0, Title = "Choose a Category" });
            ViewBag.Categories = categoryList;
        }
    }
}
