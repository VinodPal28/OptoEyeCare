using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OptoEyeCare.Models;
using System.Web.Security;
using System.Threading;

namespace OptoEyeCare.Controllers
{
    
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login()
        {           
            return View();
        }
        [HttpPost]
        public ActionResult Login(users objLoginViewModel)
        {
            using (var context = new OptoEyeCareEntities())
            {
               
                var userDetails = context.mst_User.Where(x => x.Email_Id == objLoginViewModel.EmailId && x.Password == objLoginViewModel.Password).FirstOrDefault();
                if (userDetails !=null)
                {
                    if(userDetails.isActive==true && userDetails.flag == 1)
                    {
                        FormsAuthentication.SetAuthCookie(Convert.ToString(userDetails.Id), false);
                        Session["UserId"] = userDetails.Id;
                        Session["UserName"] = userDetails.UserName;
                        ViewBag.username = userDetails.UserName;
                        return Json(new { success = true, url = Url.Action("Index", "Dashboard") });
                    }
                    else 
                    {
                        return Json(new { success = false, status="NotActive"});
                    }
                  
                }
                else
                {                    
                    return Json(new { success = false });                  
                }
            }             
        }
        [HttpPost]
        public ActionResult Registration(users registrationModel)
        {
            using (var context = new OptoEyeCareEntities())
            {
                var userDetails = context.mst_User.Where(x => x.Email_Id == registrationModel.EmailId).FirstOrDefault();
                if (userDetails != null)
                {
                    Thread.Sleep(2000);
                    return Json(new { success = true, email="exist" });
                }
                else
                {
                    mst_User reg = new mst_User()
                    {
                        UserName = registrationModel.UserName,
                        Email_Id = registrationModel.EmailId,
                        Password = registrationModel.Password,
                        createdBy = Convert.ToInt32(1),
                        createdDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                        isActive = Convert.ToBoolean(0),
                        flag = Convert.ToInt32(0)
                    };
                    context.mst_User.Add(reg);
                    context.SaveChanges();
                    Thread.Sleep(2000);
                    return Json(new { success = true, email = "NotExist" });
                }
                    
                
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }
    }
}