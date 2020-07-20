using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OptoEyeCare.Models;
using System.Threading;

namespace OptoEyeCare.Controllers
{
    [Authorize]
    public class additionController : Controller
    {
        private OptoEyeCareEntities db = new OptoEyeCareEntities();
        // GET: addition
        public ActionResult Index()
        {
            var query = from s in db.addition
                        where s.flag == 1
                        orderby s.createdDate descending
                        select s;
            var result = query.Select(s => s);
            ViewBag.Data = result;
            return View(result.ToList());
        }

        [HttpPost]
        public ActionResult SaveAddition(additionClass additionData)
        {
            using (var context = new OptoEyeCareEntities())
            {
                addition addition = new addition()
                {
                    additionValue = additionData.additionValue,
                    createdBy = Convert.ToInt32(Session["UserId"]),
                    createdDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    flag = Convert.ToInt32(1)
                };
                context.addition.Add(addition);
                context.SaveChanges();
                Thread.Sleep(1000);
                return Json(new { success = true });
            }
        }

        [HttpPost]
        public ActionResult updateAdditionData(addition Data)
        {
            using (OptoEyeCareEntities entities = new OptoEyeCareEntities())
            {
                addition update = (from c in entities.addition
                               where c.Id == Data.Id
                               select c).FirstOrDefault();
                update.additionValue = Data.additionValue;
                entities.SaveChanges();
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult deleteData(int Id)
        {
            using (OptoEyeCareEntities entities = new OptoEyeCareEntities())
            {
                addition addition = (from c in entities.addition
                             where c.Id == Id
                             select c).FirstOrDefault();
                entities.addition.Remove(addition);
                entities.SaveChanges();
            }

            return Json(new { success = true });
        }
    }
}