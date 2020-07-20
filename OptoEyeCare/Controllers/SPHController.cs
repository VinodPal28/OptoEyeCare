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
    public class SPHController : Controller
    {
        private OptoEyeCareEntities db = new OptoEyeCareEntities();
        // GET: SPH
        public ActionResult Index()
        {
            var query = from s in db.SPH
                        where s.flag == 1
                        orderby s.createdDate descending
                        select s;
            var result = query.Select(s => s);
            ViewBag.Data = result;
            return View(result.ToList());
        }

        [HttpPost]
        public ActionResult SaveSPH(SPHClass SPHData)
        {
            using (var context = new OptoEyeCareEntities())
            {
                SPH sph = new SPH()
                {
                    SPHValue = SPHData.SPHValue,
                    createdBy = Convert.ToInt32(Session["UserId"]),
                    createdDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    flag = Convert.ToInt32(1)
                };
                context.SPH.Add(sph);
                context.SaveChanges();
                Thread.Sleep(1000);
                return Json(new { success = true });
            }
        }
        [HttpPost]
        public ActionResult updateSPHData(SPHClass Data)
        {
            using (OptoEyeCareEntities entities = new OptoEyeCareEntities())
            {
                SPH update = (from c in entities.SPH
                              where c.Id == Data.Id
                              select c).FirstOrDefault();
                update.SPHValue = Data.SPHValue;
                entities.SaveChanges();
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult deleteData(int Id)
        {
            using (OptoEyeCareEntities entities = new OptoEyeCareEntities())
            {
                SPH sph = (from c in entities.SPH
                           where c.Id == Id
                           select c).FirstOrDefault();
                entities.SPH.Remove(sph);
                entities.SaveChanges();
            }

            return Json(new { success = true });
        }

    }
}