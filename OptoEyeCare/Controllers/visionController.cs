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
    public class visionController : Controller
    {
        private OptoEyeCareEntities db = new OptoEyeCareEntities();
        // GET: vision
        public ActionResult Index()
        {
            var query = from s in db.vision
                        where s.flag == 1
                        orderby s.createdDate descending
                        select s;
            var result = query.Select(s => s);
            ViewBag.Data = result;
            return View(result.ToList());
        }

        [HttpPost]
        public ActionResult SaveVision(vision visionData)
        {
            using (var context = new OptoEyeCareEntities())
            {
                vision vision = new vision()
                {
                    visionValue = visionData.visionValue,
                    createdBy = Convert.ToInt32(Session["UserId"]),
                    createdDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    flag = Convert.ToInt32(1)
                };
                context.vision.Add(vision);
                context.SaveChanges();
                Thread.Sleep(1000);
                return Json(new { success = true });
            }
        }

        [HttpPost]
        public ActionResult updateVisionData(visionClass Data)
        {
            using (OptoEyeCareEntities entities = new OptoEyeCareEntities())
            {
                vision update = (from c in entities.vision
                                 where c.Id == Data.Id
                                   select c).FirstOrDefault();
                update.visionValue = Data.visionValue;
                entities.SaveChanges();
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult deleteData(int Id)
        {
            using (OptoEyeCareEntities entities = new OptoEyeCareEntities())
            {
                vision vision = (from c in entities.vision
                                   where c.Id == Id
                                     select c).FirstOrDefault();
                entities.vision.Remove(vision);
                entities.SaveChanges();
            }

            return Json(new { success = true });
        }
    }
}