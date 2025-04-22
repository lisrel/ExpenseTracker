using System.Diagnostics;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ExpenseTrackerDbContext _context;

        public HomeController(ILogger<HomeController> logger, ExpenseTrackerDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            var allExpenses = _context.Expenses.ToList();
            return View(allExpenses);
        }

        public IActionResult CreateEditExpense(int? id)
        {

            if(id != null)
            {
                var expenseInDb = _context.Expenses.SingleOrDefault(x => x.Id == id);

                if (expenseInDb == null)
                {
                    // Optionally, log the issue or show an error message
                    return NotFound(); // Return a 404 response if the record doesn't exist
                }

              //  return Json(expenseInDb);
                return View(expenseInDb);

            }
            else
            {
                return View();

            }

        }


        public IActionResult CreateEditExpenseForm(Expense model)
        {

            if (model.Id == 0) {
                _context.Expenses.Add(model);

            }
            else
            {
                _context.Expenses.Update(model);
            }

             _context.SaveChanges();
            return RedirectToAction("Expenses");
        }

        public IActionResult DeleteExpense(int id)
        {
            var expenseInDb = _context.Expenses.SingleOrDefault(x => x.Id == id);
            _context.Expenses.Remove(expenseInDb);
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
