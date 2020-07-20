using OptoEyeCare.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace OptoEyeCare.Controllers
{
    
    [Authorize]
    public class registrationApproveController : Controller
    {
        Businesslogic Bl = new Businesslogic();
        private OptoEyeCareEntities db = new OptoEyeCareEntities();
        // GET: registrationApprove
        public ActionResult Index()
        {
            var query = from s in db.mst_User
                        where s.flag == 0
                        orderby s.createdDate descending
                        select s;
            var result = query.Select(s => s);
            ViewBag.Data = result;
            return View(result.ToList());
        }

        [HttpPost]
        public ActionResult Approve(int Id)
        {
            using (OptoEyeCareEntities entities = new OptoEyeCareEntities())
            {
                mst_User update = (from c in entities.mst_User
                                       where c.Id == Id
                                       select c).FirstOrDefault();
                update.flag = 1;
                update.isActive = true;
                int result = entities.SaveChanges();
                if (result>0)
                {
                    var query = from s in db.mst_User
                                where s.Id == Id                               
                                select s;
                    var res = query.FirstOrDefault<mst_User>();
                    sendmail(res.Password,res.UserName,res.Email_Id);
                    Thread.Sleep(2000);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
                    
            }
           
        }

        [HttpPost]
        public ActionResult DisApprove(int Id)
        {
            using (OptoEyeCareEntities entities = new OptoEyeCareEntities())
            {
                mst_User update = (from c in entities.mst_User
                                   where c.Id == Id
                                   select c).FirstOrDefault();
                update.flag = 2;
                entities.SaveChanges();
               
                Thread.Sleep(2000);
            }
            return Json(new { success = true });
        }

        #region Send Mail
        private void sendmail(string password, string username, string ToEmail)
        {          
                StringBuilder sb = new StringBuilder();              
                string strSubject =string.Empty;
                var query = from s in db.email_configuration where s.type == "registration" select s;
                var res = query.FirstOrDefault<email_configuration>();               
                if (res !=null)
                {
                    strSubject = res.subject;
                    sb.Append(res.body);
                }
                sb.Replace("[EmailId]", ToEmail);
                sb.Replace("[Name]", username);              
                sb.Replace("[Password]", password);              
                string strFrom = ConfigurationManager.AppSettings["fromMail"].ToString();
                Bl.sendmail(strSubject, sb.ToString(), "", "", strFrom, ToEmail, "", "");                      
        }
        #endregion
    }
}