using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KhumaloCraft.Models
{
    public class AboutUs
    {
        
    
        public class ContactUsModel : PageModel
        {
            public bool HasData = false;
            public string firstName = "";
            public string lastName = "";
            public string eMail = "";
            public string mEssage = "";

            public void OnGet()
            {
            }

            public void OnPost()
            {
                HasData = true;
                firstName = Request.Form["firstname"];
                lastName = Request.Form["lastname"];
                eMail = Request.Form["email"];
                mEssage = Request.Form["message"];
            }
        }
    }

}

