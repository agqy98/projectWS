using Project_WS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace Project_WS.Controllers
{
    public class HomeController : Controller
    {

        AppDbContext _Db = new AppDbContext();

        public ActionResult Index()
        {
            try
            {
                List<Staff> staffList = _Db.staffs.ToList();
                return View(staffList);
            }
            catch (Exception ex)
            {
                // Log or print the exception details
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }

        [HttpGet]
        public ActionResult AddStaff()
        {
            return View();
        }

        // POST action to handle form submission
        [HttpPost]
        public async Task<ActionResult> AddStaff(Staff staff)
        {
            if (ModelState.IsValid)
            {
                // Add the new entity to the DbSet
                _Db.staffs.Add(staff);

                // Save changes to the database asynchronously
                await _Db.SaveChangesAsync();

                // Redirect to another page or return a different view
                return RedirectToAction("Index");
            }

            // If the model is not valid, redisplay the form with validation errors
            return View(staff);
        }
        [HttpGet]
        public ActionResult EditStaff(int id)
        {
            // Check if staff exists
            Staff staff = _Db.staffs.Find(id);

            if (staff == null)
            {
                return HttpNotFound();
            }

            return View(staff);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> EditStaff(int id, Staff updatedStaff)
        {
            // Check if the model is valid
            if (ModelState.IsValid)
            {
                // Check if staff exists
                Staff existingStaff = _Db.staffs.Find(id);
                if (existingStaff == null)
                {
                    return HttpNotFound();
                }

                existingStaff.FirstName = updatedStaff.FirstName;
                existingStaff.LastName = updatedStaff.LastName;
                existingStaff.Dob = updatedStaff.Dob;

                // Save changes to the database asynchronously
                await _Db.SaveChangesAsync();

                // Redirect to another page
                return RedirectToAction("Index");
            }

            // If the model is not valid, redisplay the form with validation errors
            return View(updatedStaff);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteStaff(int id)
        {
            Staff staff = await _Db.staffs.FindAsync(id);

            // Check if staff exists
            if (staff == null)
            {
                return HttpNotFound();
            }

            _Db.staffs.Remove(staff);
            await _Db.SaveChangesAsync();

            // Return a JSON result indicating success
            return Json(new { success = true, message = "Staff deleted successfully" });
        }
    }
}