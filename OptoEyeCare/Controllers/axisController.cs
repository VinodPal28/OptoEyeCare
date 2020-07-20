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
    public class axisController : Controller
    {
        private OptoEyeCareEntities db = new OptoEyeCareEntities();
        // GET: axis
        public ActionResult Index()
        {
            var query = from s in db.axis
                        where s.flag == 1
                        orderby s.createdDate descending
                        select s;
            var result = query.Select(s => s);
            ViewBag.Data = result;
            return View(result.ToList());
        }

        [HttpPost]
        public ActionResult SaveAxis(axisClass axisData)
        {
            using (var context = new OptoEyeCareEntities())
            {
                axis axis = new axis()
                {
                    axisvalue = axisData.axisvalue,
                    createdBy = Convert.ToInt32(Session["UserId"]),
                    createdDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    flag = Convert.ToInt32(1)
                };
                context.axis.Add(axis);
                context.SaveChanges();
                Thread.Sleep(1000);
                return Json(new { success = true });
            }
        }

        [HttpPost]
        public ActionResult updateAxisData(axisClass Data)
        {
            using (OptoEyeCareEntities entities = new OptoEyeCareEntities())
            {
                axis update = (from c in entities.axis
                               where c.Id == Data.Id
                              select c).FirstOrDefault();
                update.axisvalue = Data.axisvalue;
                entities.SaveChanges();
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult deleteData(int Id)
        {
            using (OptoEyeCareEntities entities = new OptoEyeCareEntities())
            {
                axis axis = (from c in entities.axis
                            where c.Id == Id
                           select c).FirstOrDefault();
                entities.axis.Remove(axis);
                entities.SaveChanges();
            }

            return Json(new { success = true });
        }
    }
}