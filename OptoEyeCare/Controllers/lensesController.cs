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
    public class lensesController : Controller
    {
        private OptoEyeCareEntities db = new OptoEyeCareEntities();
        // GET: lenses
        public ActionResult Index()
        {
            var query = from s in db.lenseDetails
                        where s.flag == 1
                        orderby s.createdDate descending
                        select s;
            var result = query.Select(s => s);
            ViewBag.LenseData = result;
            return View(result.ToList());
        }

        [HttpPost]
        public ActionResult lense(lenses lenseData)
        {
            using (var context = new OptoEyeCareEntities())
            {
                lenseDetails _lense = new lenseDetails()
                {
                    lenseName = lenseData.lenseName,
                    createdBy = Convert.ToInt32(Session["UserId"]),
                    createdDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    flag = Convert.ToInt32(1)
                };
                context.lenseDetails.Add(_lense);
                context.SaveChanges();
                Thread.Sleep(2000);
                return Json(new { success = true });
            }
        }

        [HttpPost]
        public ActionResult updateLenseData(lenses Data)
        {
            using (OptoEyeCareEntities entities = new OptoEyeCareEntities())
            {
                lenseDetails update = (from c in entities.lenseDetails
                                       where c.Id == Data.Id
                                select c).FirstOrDefault();
                update.lenseName = Data.lenseName;
                entities.SaveChanges();
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult deleteData(int Id)
        {
            using (OptoEyeCareEntities entities = new OptoEyeCareEntities())
            {
                lenseDetails lenseId = (from c in entities.lenseDetails
                                 where c.Id == Id
                                 select c).FirstOrDefault();
                entities.lenseDetails.Remove(lenseId);
                entities.SaveChanges();
            }

            return Json(new { success = true });
        }
    }
}