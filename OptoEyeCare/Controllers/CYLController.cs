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
    public class CYLController : Controller
    {
        private OptoEyeCareEntities db = new OptoEyeCareEntities();
        // GET: CYL
        public ActionResult Index()
        {
            var query = from s in db.CYL
                        where s.flag == 1
                        orderby s.createdDate descending
                        select s;
            var result = query.Select(s => s);
            ViewBag.Data = result;
            return View(result.ToList());
        }

        [HttpPost]
        public ActionResult SaveCYL(CYLClass CYLData)
        {
            using (var context = new OptoEyeCareEntities())
            {
                CYL cyl = new CYL()
                {
                    CYL1 = CYLData.CYL,
                    createdBy = Convert.ToInt32(Session["UserId"]),
                    createdDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    flag = Convert.ToInt32(1)
                };
                context.CYL.Add(cyl);
                context.SaveChanges();
                Thread.Sleep(1000);
                return Json(new { success = true });
            }
        }

        [HttpPost]
        public ActionResult updateCYLData(CYLClass Data)
        {
            using (OptoEyeCareEntities entities = new OptoEyeCareEntities())
            {
                CYL update = (from c in entities.CYL
                                where c.Id == Data.Id
                                select c).FirstOrDefault();
                update.CYL1 = Data.CYL;
                entities.SaveChanges();
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult deleteData(int Id)
        {
            using (OptoEyeCareEntities entities = new OptoEyeCareEntities())
            {
                CYL cyl = (from c in entities.CYL
                                 where c.Id == Id
                                 select c).FirstOrDefault();
                entities.CYL.Remove(cyl);
                entities.SaveChanges();
            }

            return Json(new { success = true });
        }
    }
}