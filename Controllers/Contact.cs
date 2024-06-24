using KhumaloCraft.Models;
using Microsoft.AspNetCore.Mvc;

namespace KhumaloCraft.Controllers
{
    public class Contact : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            var firstName = form["firstname"];
            var lastName = form["lastname"];
            var email = form["email"];
            var message = form["message"];

            // Do something with the data (e.g., save to database)

            // Set the success message
            ViewBag.SuccessMessage = $"Thank you, {firstName} {lastName}, for contacting us! We have received your message and will get back to you soon.";

            // Render the same view with success message
            return View();
        }
    }
}
