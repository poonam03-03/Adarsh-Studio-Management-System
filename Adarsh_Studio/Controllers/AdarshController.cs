using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using Adarsh_Studio.App_Code;
using Adarsh_Studio.Models;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;

namespace Adarsh_Studio.Controllers
{

    public class AdarshController : Controller
    {

        AdarshSdbContext db = new AdarshSdbContext();
        EmailSender email = new EmailSender();
        FileManager fileobj = new FileManager();
        string ResponseMsg = string.Empty;
        string Response = string.Empty;
        private IConfiguration configuration;
        private IFormFile formFile;

        public AdarshController(IConfiguration conf)
        {
            configuration = conf;
            //Initialize FileManager

        }

        [NonAction]

        public void BindCityDDL()
        {
            List<CityMaster> listcm = db.CityMasters.OrderBy(x => x.CityName).ToList();
            List<SelectListItem> slitem = new List<SelectListItem>();
            foreach (CityMaster city in listcm)
            {
                SelectListItem sl = new SelectListItem(city.CityName, city.CityId.ToString());
                slitem.Add(sl);
            }
            ViewBag.City = slitem;

        }

        private void BindService()
        {
            var service = db.ServiceMasters.Select(s => new { s.ServiceId, s.ServiceType }).ToList();
            ViewData["Service"] = new SelectList(service, "ServiceId", "ServiceType");
            BindCityDDL();
        }
        public IActionResult Booking()
        {

            BindService();
            BindCityDDL();
            return View();
        }

        public JsonResult GetServiceDetails(int serviceId)
        {
            var service = db.ServiceMasters.Where(s => s.ServiceId == serviceId).Select(s => new { s.ServiceType, s.Category, s.Budget, s.DiscountedRate, s.Inclusions, s.Exclusions }).FirstOrDefault();
            return Json(service);
        }

        [HttpPost]
        public IActionResult Booking(BookingMaster bm)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    bm.CreatedOn = DateTime.Now;
                    db.BookingMasters.Add(bm);
                    ResponseMsg = "Service Booked Successfully";
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ResponseMsg = "Unable to Book Service" + ex.Message;
                }


            }
            var service = db.ServiceMasters.Select(s => new { s.ServiceId, s.ServiceType }).ToList();
            ViewData["Service"] = new SelectList(service, "ServiceId", "ServiceType");
            ViewBag.savebooking = ResponseMsg;
            BindCityDDL();
            BindService();
            return View();
        }

        

        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string adminId, string adminPass)
        {
            // Check if the admin exists
            var admin = db.LoginMasters.FirstOrDefault(a => a.AdminId == adminId);

            if (admin == null)
            {
                return Json(new { success = false, message = "Invalid Admin ID or Password!" });
            }
            else
            {
                if (admin.IsBlocked == true)
                {
                    return Json(new { success = false, message = "Your account is blocked." });
                }
                else
                {

                    // Validate password (simple example; consider hashing your passwords in a real application)
                    if (admin.AdminPass != adminPass)
                    {
                        return Json(new { success = false, message = "Invalid Admin ID or Password!" });
                    }
                    else

                    {
                        // If login is successful, update login data
                        admin.LoginCount = (admin.LoginCount ?? 0) + 1;
                        admin.LastLoginDt = DateTime.Now;
                        admin.UpdatedOn = DateTime.Now;

                        db.SaveChanges();
                        HttpContext.Session.SetString("aid",admin.AdminId);

                        return Json(new { success = true, message = "Login successful!", redirectUrl = "/Admin/Welcome" });
                    }

                }
            }
        }


        //forgotpassword

        public JsonResult ForgotPassword(string adminId)
        {
            try
            {
                var admin = db.LoginMasters.FirstOrDefault(a => a.AdminId == adminId);
                if (admin == null)
                {
                    return Json(new { success = false, message = "Admin ID not found!" });
                }

                // Generate a verification code (this can be a random string or number)
                string verificationCode = Guid.NewGuid().ToString().Substring(0, 6);  // Example: 6-character code

                // Store the verification code and expiry time in the database
                admin.VerificationCode = verificationCode;
                admin.VerificationCodeExpiry = DateTime.Now.AddMinutes(15);  // Expire in 15 minutes
                db.SaveChanges();

                // Send the email using the EmailSender class
                EmailSender emailSender = new EmailSender();
                string subject = "Password Reset Verification Code";
                string message = $"Your verification code for password reset is:" +verificationCode;
                bool emailSent = emailSender.SendEmailNow(admin.AdminId, subject, message);

                if (emailSent)
                {
                    return Json(new { success = true, message = "Verification code sent to your email!" });
                }
                else
                {
                    return Json(new { success = false, message = "Error sending verification code!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }


        public JsonResult VerifyPassword(string adminId, string verificationCode)
        {
            try
            {
                var admin = db.LoginMasters.FirstOrDefault(a => a.AdminId == adminId);
                if (admin == null)
                {
                    return Json(new { success = false, message = "Admin ID not found!" });
                }

                // Check if the verification code matches and hasn't expired
                if (admin.VerificationCode == verificationCode && admin.VerificationCodeExpiry > DateTime.Now)
                {
                    return Json(new { success = true, message = "Verification code is valid!" });
                }
                else
                {
                    return Json(new { success = false, message = "Invalid or expired verification code!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }


        public JsonResult ResetPassword(string adminId, string adminPass, string verificationCode)
        {
            try
            {
                var admin = db.LoginMasters.FirstOrDefault(a => a.AdminId == adminId);
                if (admin == null)
                {
                    return Json(new { success = false, message = "Admin ID not found!" });
                }

                // Check if the verification code matches
                if (admin.VerificationCode == verificationCode && admin.VerificationCodeExpiry > DateTime.Now)
                {
                    // Reset the password 
                    admin.AdminPass = adminPass; 
                    db.SaveChanges();
                    // Clear the verification code and its expiry time after a successful password reset
                    admin.VerificationCode = null;
                    admin.VerificationCodeExpiry = null;

                    db.SaveChanges();

                    return Json(new { success = true, message = "Password reset successfully!" });
                }
                else
                {
                    return Json(new { success = false, message = "Invalid or expired verification code!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }



        [HttpPost]
        public JsonResult Subscribe(string emailId)
        {
            if (string.IsNullOrEmpty(emailId))
            {
                return Json(new { success = false, message = "Email is required." });
            }

            // Check if email already exists in the database
            bool emailExists = db.SubcribeMasters.Any(s => s.EmailId == emailId); // Assuming 'Subscriptions' is your table

            if (emailExists)
            {
                return Json(new { success = false, message = "This email is already subscribed." });
            }

            // Add the new subscription to the database
            var newSubscription = new SubcribeMaster
            {
                EmailId = emailId,
                CreatedOn = DateTime.Now
            };

            db.SubcribeMasters.Add(newSubscription);
            db.SaveChanges();

            return Json(new { success = true, message = "Subscription successful!" });
        }



        [HttpPost]
        public JsonResult SaveEnquiryUsingAJAX([FromBody] EnquiryMaster em)
        {
            try
            {
                em.CreatedOn = DateTime.Now;
                db.EnquiryMasters.Add(em);
                db.SaveChanges();
                ResponseMsg = "Enquiry Saved Successfully";
                string msg = "Thanks for your enquiry. we will contact you soon.";
                email.SendEmailNow(em.EmailId, "Greeting from Adarsh Studio", msg);
            }
            catch
            {
                ResponseMsg = "Sorry! Unable to save Enquiry Message.Please Try again.";
            }
            return Json(ResponseMsg);
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult PreWedding()
        {
            return View();
        }

        public IActionResult Wedding()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }

        public IActionResult Service()
        {
            var services = db.ServiceMasters.Include(s => s.ServicePicMasters).ToList();
            return View(services); // Pass services to the view

             // Pass the ServiceMaster object to the view
     
        }

        public IActionResult About()
        {
            IEnumerable <StaffMaster> stf= db.StaffMasters.ToList();
            return View(stf);
        }

        public IActionResult Developer()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Feedback()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Feedback(FeedbackMaster fm)
        {
            try
            {
                fm.CreatedOn = DateTime.Now;
                db.FeedbackMasters.Add(fm);
                ResponseMsg = "Feedback Saved Successfully";
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ResponseMsg = "Unable to Book Service" + ex.Message;
            }
            return View();
        }
        public IActionResult Ceremony()
        {
            return View();
        }

        public IActionResult Candid()
        {
            return View();
        }


        public IActionResult Package()
        {
            List<PackageMaster> lst = db.PackageMasters.ToList();
            return View(lst);
          
        }

       



    }
}







       

    


        // public IActionResult Login(string username, string password)
        //{
        //  LoginMaster lm = new LoginMaster();
        // lm.EnrollmentNo = UserId;
        //Encrypting Password
        //Cryptography cd= new Cryptography();
        //lm.AdminPass = cg.EncryptMyData(password);
        // lm.UserType = "USER";
        //Verifying userid and password from Database
        //  LoginMaster lmdb = db.LoginMasters.Where(x => x.EnrollmentNo == lm.EnrollmentNo && x.UserPass == lm.UserPass && x.UserType == lm.UserType).SingleDefault();
        //if(lmdb== null) {
        //   {
        //       Message = "Invalid Userid or Password. Please Try Again.";
        //  }
        //  else
        //  {
        //    if(lmdb.IsBlocked==true)
        //   {
        //      Message = "This account is suspended/blocked.";
        //  }
        // else
        // {
        //Setting Login Log
        // lmdb.LoginCount = lmdb.LoginCount + 1;
        //lmdb.LastLogin= DataTime.Now;
        //db.Entry(lmdb);
        //db.SaveChanges();
        //session -- it has key and value.
        //HttpContext.Session.SetString("uid", lmdb.EnrollmentNo);
        //  return RedirectToAction("Welcome", "User");
        //session
        //   return RedirectToAction("Welcome", "User");
        // }
        // }
        // ViewBag.res = Message;
        //  return View();
        //}



       



