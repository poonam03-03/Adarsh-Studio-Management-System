using Adarsh_Studio.App_Code;
using Adarsh_Studio.Models;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Adarsh_Studio.Controllers
{
    [AuthorisedAdmin]
    public class AdminController : Controller
    {
        AdarshSdbContext db = new AdarshSdbContext();
        FileManager fm=new FileManager();
        string msg = string.Empty;

        [NonAction]

        void BindCityDDL()
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

        public IActionResult Welcome()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ManageNotification()
        {

            return View();
        }
        [HttpPost]
        public IActionResult ManageNotification(UpdatesMaster um)
        {
            try
            {
                um.CreatedOn = DateTime.Now;
                db.UpdatesMasters.Add(um);

                msg = "Notification Added Successfully";
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                msg = "Error in adding notification" + ex.Message;
            }

            ViewBag.notify = msg;

            return View();
        }

        public IActionResult ShowNotification()
        {
            IEnumerable<UpdatesMaster> umlist = db.UpdatesMasters.ToList();
            return View(umlist);
        }



        public IActionResult DeleteNotification(int id)
        {
            UpdatesMaster ems = db.UpdatesMasters.Find(id);
            if (ems != null)
            {
                db.Remove(ems);
                db.SaveChanges();
                msg = "Notification Deleted Successfully";
            }
            else
            {
                msg = "Unable  to delete Notification.";
            }
         
            TempData["ndelete"] = msg;
            return RedirectToAction("ShowNotification");
           
        }


        public IActionResult EditNotification(int id)
        {
            UpdatesMaster nf = db.UpdatesMasters.Find(id);
            return View(nf);
        }

        [HttpPost]

        public IActionResult EditNotification(UpdatesMaster em)
        {

            UpdatesMaster dm = db.UpdatesMasters.Find(em.UpdateId);
            if (dm != null)
            {
                dm.UpdateMsg = em.UpdateMsg;
                db.Entry(dm);
                db.SaveChanges();
                msg = "Message Updated Successfully";
            }
            else
                msg = "Unable to Update Message! Try Again Later";
            TempData["uedit"] = msg;
            return RedirectToAction("ShowNotification");
        }





        [HttpGet]
        public ActionResult AddCity()
        {

            BindCityDDL();
            return View();
        }

        [HttpPost]
        public ActionResult AddCity(CityMaster city)
        {
            try
            {
                city.CreatedOn = DateTime.Now;
                db.CityMasters.Add(city);
                db.SaveChanges();
                msg = "City Added Successfully";
            }
            catch (Exception ex)
            {
                msg = "Unable to Add City! Try again Later" +ex.Message;
            }
            ViewBag.addcity = msg;
            BindCityDDL();
            return View();
        }

        public IActionResult ShowCity()
        {
            IEnumerable<CityMaster> emlist = db.CityMasters.ToList();
            BindCityDDL();
            return View(emlist);
        }

        public IActionResult EditCity(int id)
        {
            CityMaster cm = db.CityMasters.Find(id);
            BindCityDDL();
            return View(cm);
        }

        [HttpPost]

        public IActionResult EditCity(CityMaster em)
        {
            
            CityMaster dm = db.CityMasters.Find(em.CityId);
            if (dm != null)
            {
                dm.CityName = em.CityName;
                db.Entry(dm);
                db.SaveChanges();
                msg = "City Name Updated Successfully";
            }
            else
                msg = "City Name Not Updated";
            TempData["cityedit"] = msg;
            BindCityDDL();
            return RedirectToAction("ShowCity");
        }



        public IActionResult DeleteCity(int id)
        {
            CityMaster ems = db.CityMasters.Find(id);
            if (ems != null)
            {
                db.Remove(ems);
                db.SaveChanges();
                msg = "City Deleted Successfully";
            }
            else
            {
                msg = "City to delete Service.";
            }
            BindCityDDL();
            TempData["cdelete"] = msg;
            return RedirectToAction("ShowCity");
           

        }

        public IActionResult AddService()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddService(ServiceMaster ss)
        {
            string msg = string.Empty;
            try
            {
                ss.CreatedOn = DateTime.Now;
                ss.UpdatedOn = DateTime.Now;
                db.ServiceMasters.Add(ss);
                msg = "Service Added Successfully.";
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                msg = "Error in Service adding" + ex.Message;
            }
            ViewBag.Service = msg;
            return View();
        }

        
        public ActionResult EditService(int id)
        {
            var service = db.ServiceMasters.Find(id);
            return View(service); 
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditService(ServiceMaster sm)
        {
            if (ModelState.IsValid)
            {
                var service = db.ServiceMasters.Find(sm.ServiceId);

                if (service != null)
                {
                    service.ServiceType = sm.ServiceType;
                    service.Category = sm.Category;
                    service.Budget = sm.Budget;
                    service.DiscountedRate = sm.DiscountedRate;
                    service.Description = sm.Description;
                    service.Inclusions = sm.Inclusions;
                    service.Exclusions = sm.Exclusions;
                    db.SaveChanges(); 
                    TempData["serviceedit"]= "Service updated successfully!";
                    return RedirectToAction("ShowService"); 
                }
                else
                {
                    TempData["serviceedit"] = "Service not found.";
                    return View(sm);
                }
            }

            // If the model is invalid, return the same view with validation messages
            return View(sm);
        }


        public IActionResult UploadServiceImage(int serviceId)
        {
            var service = db.ServiceMasters.FirstOrDefault(s => s.ServiceId == serviceId);

            if (service == null)
            {
                TempData["ErrorMessage"] = "Service not found.";
                return RedirectToAction("ShowService");
            }

            return View(service);  
        }


        [HttpPost]
        public IActionResult UploadServiceImage(int serviceId, IFormFile file, string remark)
        {
            // Check if the service exists
            var service = db.ServiceMasters.FirstOrDefault(s => s.ServiceId == serviceId);
            if (service == null)
            {
                TempData["ErrorMessage"] = "Service not found!";
                return RedirectToAction("ShowService");
            }

            // Create the FileManager object to handle the file upload
            var fileManager = new FileManager
            {
                FileObject = file // Assign the uploaded file
            };

            // Upload the file
            string uploadResult = fileManager.UploadMyFile();

            if (uploadResult != "SUCCESS")
            {
                TempData["ErrorMessage"] = uploadResult;
                return RedirectToAction("ShowService");
            }

            // After successful file upload, create the ServicePicMaster object
            var servicePic = new ServicePicMaster
            {
                ServiceId = serviceId,
                PicFileName = fileManager.FileName,
                PicFolderName = fileManager.FolderName,
                PicType = fileManager.FileExtension,
                PicSizeInKb = fileManager.FileSizeInKB,
                Remark = remark,
                CreatedOn = DateTime.Now
            };

            // Save the file information in the database
            db.ServicePicMasters.Add(servicePic);
            db.SaveChanges();

            TempData["imageupload"] = "File uploaded and associated with the service successfully.";
            return RedirectToAction("ShowService", new { id = serviceId });
        }

        public IActionResult ShowService()
        {
            IEnumerable<ServiceMaster> slist = db.ServiceMasters.Include(s => s.ServicePicMasters).ToList();
            

            // Ensure that slist is not null and has data before passing to view
            if (slist == null || !slist.Any())
            {
                TempData["ErrorMessage"] = "No services found.";
            }
            return View(slist);
        }

        public IActionResult DeleteService(int id)
        {
            var service = db.ServiceMasters.Find(id);
            if (service != null)
            {
                // Delete associated service images
                var servicePics = db.ServicePicMasters.Where(spm => spm.ServiceId == id).ToList();
                if (servicePics.Any())
                {
                    db.ServicePicMasters.RemoveRange(servicePics);  // Delete associated images
                }

                // Delete the service
                db.ServiceMasters.Remove(service);
                db.SaveChanges();

                TempData["sdelete"] = "Service and associated images deleted successfully.";
            }
            else
            {
                TempData["sdelete"] = "Unable to delete service: service not found.";
            }
            return RedirectToAction("ShowService");
        }

        public IActionResult AddStaff()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStaff(StaffMaster staff, IFormFile file)
        {
           
            if (file != null)
            {
                fm.FileObject = file;
                var uploadResult = fm.UploadMyFile();

                if (uploadResult != "SUCCESS")
                {
                    // If file upload fails, return a BadRequest with the upload error message
                    return BadRequest(uploadResult);
                }

                // 3. Set staff image details
                staff.ImgFileName = fm.FileName;
                staff.ImgFolderName = fm.FolderName;
                staff.ImgType = fm.FileExtension;
                staff.ImgSizeInKb = fm.FileSizeInKB;
            }

            
            staff.CreatedOn = DateTime.Now;

            // 5. Add the new staff record to the database
          db.StaffMasters.Add(staff);

            try
            {
                db.SaveChanges();  
                msg = "Staff Details Added Successfully.";                          // 6. Return success response
                
            }
            catch (Exception ex)
            {
                msg = "Unable to Add This Staff Details." + ex.Message;
            }
            ViewBag.addstaff = msg;
            return View();
        }




        public IActionResult ShowStaff()
        {
            IEnumerable<StaffMaster> st = db.StaffMasters.ToList();
            return View(st);
        }

        public IActionResult DeleteStaff(int id)
        {

            StaffMaster ems = db.StaffMasters.Find(id);
            if (ems != null)
            {
                db.Remove(ems);
                db.SaveChanges();
                msg = "Member Details Deleted Successfully";
            }
            else
            {
                msg = "Unable to delete this Member.";
            }
            TempData["stdelete"] = msg;
            return RedirectToAction("ShowStaff");
        }

        public IActionResult AddPackage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPackage(PackageMaster pm)
        {
            try
            {
                pm.CreatedOn = DateTime.Now;
                db.PackageMasters.Add(pm);
                db.SaveChanges();
                msg = "Package Added Successfully";
            }
            catch
            {
                msg = "Soory! PAckage not add. plase try again later";
            }
            ViewBag.pmsg = msg;
            return View();
        }
        public IActionResult ShowPackage()
        {
            List<PackageMaster> lst = db.PackageMasters.ToList();
            return View(lst);

        }
        public IActionResult EditPackage(int id)
        {
            PackageMaster lst = db.PackageMasters.Find(id);
            return View(lst);
        }
        [HttpPost]
        public IActionResult EditPackage(PackageMaster pm)
        {
            if (ModelState.IsValid)
            {
                PackageMaster dbpm = db.PackageMasters.Find(pm.PackageId);
                if (dbpm == null)
                {
                    return NotFound();
                }
                dbpm.PackageTitle = pm.PackageTitle;
                dbpm.Price = pm.Price;
                dbpm.Detail1 = pm.Detail1;
                dbpm.Detail2 = pm.Detail2;
                dbpm.Detail3 = pm.Detail3;
                dbpm.Detail4 = pm.Detail4;
                db.PackageMasters.Entry(dbpm);
                db.SaveChanges();
                msg = "Record Update successfully";
            }
            else
            {
                msg = "Record Update successfully";
            }
            TempData["pedit"] = msg;

            return RedirectToAction("ShowPackage");

        }
        [HttpGet]
        public IActionResult DeletePackage(int id)
        {
            var package = db.PackageMasters.Find(id);
            if (package == null)
            {
                TempData["pmsg"] = "Package not found!";
                return RedirectToAction("ShowPackage");
            }

            db.PackageMasters.Remove(package);
            db.SaveChanges();

            TempData["pdelete"] = "Package deleted successfully!";
            return RedirectToAction("ShowPackage");
        }




        public IActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string SendTo, string Subject, string Message)
        {
            // Create an instance of the EmailSender class
            EmailSender emailSender = new EmailSender();

            // Call SendEmailNow method and send the email
            bool emailSent = emailSender.SendEmailNow(SendTo, Subject, Message);

            // Return a result based on whether the email was sent successfully
            if (emailSent)
            {
                ViewBag.EMessage = "Email sent successfully!";
            }
            else
            {
                ViewBag.EMessage = "There was an error sending the email.";
            }
            return View();
        
        }




        public IActionResult ShowBooking()
        {
            IEnumerable<BookingMaster> blist= db.BookingMasters.ToList();
            return View(blist);
        }

        public IActionResult DeleteBooking(int id)
        {
            BookingMaster ems = db.BookingMasters.Find(id);
            if (ems != null)
            {
                db.Remove(ems);
                db.SaveChanges();
                msg = "Booking Deleted Successfully";
            }
            else
            {
                msg = "Unable to delete Booking.";
            }
            TempData["bdelete"] = msg;
            return RedirectToAction("ShowBooking");
        }

        [HttpGet]

        public IActionResult ShowFeedback()

        {
            IEnumerable<FeedbackMaster> emlist = db.FeedbackMasters.ToList();
            return View(emlist);


        }
        
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult DeleteFeedback(int id)
        {
            FeedbackMaster ems = db.FeedbackMasters.Find(id);
            if (ems != null)
            {
                db.Remove(ems);
                db.SaveChanges();
                msg = "Feedback Deleted Successfully";
            }
            else
            {
                msg = "Unable to delete Feedback.";
            }
            TempData["fdelete"] = msg;

            return RedirectToAction("ShowFeedback");
        }
        
        public IActionResult ShowEnquiry()
        {
            IEnumerable<EnquiryMaster> enlist=db.EnquiryMasters.ToList();
            return View(enlist);
        }

        public IActionResult ShowSubscription()
        {
            IEnumerable<SubcribeMaster> sblist = db.SubcribeMasters.ToList();
            return View(sblist);
        }


        public IActionResult DeleteSubscription(int id)
        {
            SubcribeMaster ems = db.SubcribeMasters.Find(id);
            if (ems != null)
            {
                db.Remove(ems);
                db.SaveChanges();
                msg = "Subscription Deleted Successfully";
            }
            else
            {
                msg = "Unable to delete Subscription.";
            }
            TempData["sbdelete"] = msg;

            return RedirectToAction("ShowSubscription");
        }



        public IActionResult DeleteEnquiry(int id)
        {
            EnquiryMaster ems = db.EnquiryMasters.Find(id);
            if(ems != null) 
                {
                    db.Remove(ems);
                    db.SaveChanges();
                    msg = "Enquiry Deleted Successfully";
                }
             else
                {
                    msg = "Unable to delete Enquiry.";
                }
                TempData["edelete"]=msg;

            return RedirectToAction("ShowEnquiry");
        }

        



       

        [HttpPost]
        public JsonResult Logout()
        {
            // Clear the session to log the admin out
            HttpContext.Session.Remove("aid");

            
            HttpContext.Session.Clear();

           
            return Json(new { success = true, message = "Logout successful!", redirectUrl = "/Admin/Login" });
        }


    }
}
