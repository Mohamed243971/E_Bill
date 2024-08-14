using E_Bill.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing.Text;
using System.Text.Json;

namespace E_Bill.Controllers
{

    public class BillController : Controller
    {
        private BillContext Db;
        public BillController()
        {
            Db = new BillContext();
        }
        public IActionResult Index()
        {
            return RedirectToAction("ViewBills");
        }

        [HttpGet]
        public IActionResult AddForm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBillDb(Bill bill, string serializedItems)
        {
            if (ModelState.IsValid)
            {
                // Deserialize the items
                var items = JsonConvert.DeserializeObject<List<Item>>(serializedItems);

                // Set the Bill's Items
                bill.Items = items;
                //update the bill amount
                bill.Amount = (int)bill.Items.Sum(item => item.Price * item.Quantity);
                // Add the Bill to the database
                Db.Bills.Add(bill);
                Db.SaveChanges();

                return RedirectToAction("ViewBills");
            }

            return View("AddForm", bill);
        }


        // Method to remove an item from a bill
        [HttpPost]
        public IActionResult RemoveItem(int billId, int itemId)
        {
            var bill = Db.Bills.Include(b => b.Items).SingleOrDefault(b => b.Id == billId);
            if (bill == null) return NotFound();

            var item = bill.Items.SingleOrDefault(i => i.Id == itemId);
            if (item == null) return NotFound();

            bill.Items.Remove(item);
            Db.Items.Remove(item);
            Db.SaveChanges();

            // Update the bill amount after item removal
            bill.Amount = (int)bill.Items.Sum(i => i.Price * i.Quantity);
            Db.SaveChanges();

            return RedirectToAction("Details", new { id = billId });
        }

        public IActionResult ViewBills()
        {
            var bills = Db.Bills.ToList();
            return View(bills);
        }

        public IActionResult Details(int id)
        {
            Bill bill = Db.Bills.Include(b => b.Items).SingleOrDefault(b => b.Id == id);
            if (bill == null)
            {
                return NotFound();
            }
            return View(bill);
        }

        public IActionResult EditForm(int id)
        {
            Bill bill= Db.Bills.SingleOrDefault(b => b.Id == id);
            return View(bill);
        }

        public IActionResult UpdateData(int id, Bill bil)
        {
            Bill bill = Db.Bills.SingleOrDefault(b => b.Id == id);
            if (bill == null) { return NotFound(); }
            bill.CustomerName = bil.CustomerName;
            bill.CustomerAddress = bil.CustomerAddress;
            bill.CustomerPhone = bil.CustomerPhone;
            bill.Amount = bil.Amount;
            bill.Items = bil.Items;

            Db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            Bill bill = Db.Bills.SingleOrDefault(b => b.Id == id);
            if (bill == null) { return NotFound(); }
            Db.Bills.Remove(bill);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        
    }
}
