using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace ExpenseTracker.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ExpenseController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Expense> objList = _db.Expenses;
            foreach (var item in objList)
            {
                item.Expensetype = _db.ExpenseType.FirstOrDefault(x=>x.Id==item.ExpenseTypeId);
            }
            return View(objList);
        }

        //Get-Create
        public IActionResult Create()
        {
            //IEnumerable<SelectListItem> TypeDropDown = _db.ExpenseType.Select(i => new SelectListItem
            //{
            //    Text=i.Name,
            //    Value=i.Id.ToString()
            //});

            //ViewBag.TypeDropDown = TypeDropDown;    

            ExpenseVM expenseVM = new ExpenseVM()
            {
                expense=new Expense(),
                TypeDropDown= _db.ExpenseType.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })


        };
            return View(expenseVM);
        }
        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseVM obj)
        {
            if (ModelState.IsValid)
            {
               // obj.ExpenseTypeId = 1;
                _db.Expenses.Add(obj.expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        //Get: Delete
        public IActionResult Delete(int Id)
        {
            var obj = _db.Expenses.Find(Id);

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Id)
        {
            var obj = _db.Expenses.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Expenses.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        //Get: update
        public IActionResult Update(int? Id)
        {
            ExpenseVM expenseVM = new ExpenseVM()
            {
                expense = new Expense(),
                TypeDropDown = _db.ExpenseType.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };



            if (Id == null ||Id==0)
            {
                return NotFound();
            }
            expenseVM.expense = _db.Expenses.Find(Id);
            if (expenseVM.expense == null)
            {
                return NotFound();
            }
            return View(expenseVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseVM obj)
        {
            if(ModelState.IsValid)
            {
                _db.Expenses.Update(obj.expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
           
        }
    }
}
