using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OptoEyeCare.Models;
using System.Threading;
using System.Collections;

namespace OptoEyeCare.Controllers
{
    [Authorize]
    public class frameController : Controller
    {
        private OptoEyeCareEntities db = new OptoEyeCareEntities();
        // GET: frame       
        public ActionResult Index()
        {
            var query = from s in db.frame
                        where s.flag == 1
                        orderby s.createdDate descending
                        select s;
            var result = query.Select(s => s);
            ViewBag.Data = result;
            return View(result.ToList());
           
        }

        //[HttpPost]
        //public JsonResult GetData(int pageIndex)
        //{
        //    OptoEyeCareEntities entities = new OptoEyeCareEntities();
        //    frameModel modelData = new frameModel();
        //    modelData.PageIndex = pageIndex;
        //    modelData.PageSize = 5;
        //    modelData.RecordCount = entities.frame.Count();
        //    int startIndex = (pageIndex - 1) * modelData.PageSize;

        //    var query = (from s in db.frame
        //                where s.flag == 1
        //                orderby s.createdDate descending
        //                select s).OrderBy(s => s.createdDate)
        //                        .Skip(startIndex)
        //                        .Take(modelData.PageSize).ToList();
        //    modelData.frameData = query.ConvertAll(x => new frames
        //    {
        //        Id = Convert.ToInt32(x.Id),
        //        frameName = x.frameName
        //    });
           
        //    return Json(modelData);           
        //}

        [HttpPost]
        public ActionResult frame(frames objframe)
        {
            using (var context = new OptoEyeCareEntities())
            {
                frame _frame = new frame()
                {
                    frameName = objframe.frameName,
                    createdBy = Convert.ToInt32(Session["UserId"]),
                    createdDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    flag = Convert.ToInt32(1)
                };
                context.frame.Add(_frame);
                context.SaveChanges();             
                Thread.Sleep(2000);
                return Json(new { success = true });
            }           
        }

        [HttpPost]
        public ActionResult updateData(frames Data)
        {
            using (OptoEyeCareEntities entities = new OptoEyeCareEntities())
            {
                frame update = (from c in entities.frame
                                            where c.Id == Data.Id
                                            select c).FirstOrDefault();
                update.frameName = Data.frameName;              
                entities.SaveChanges();
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult deleteData(int Id)
        {
            using (OptoEyeCareEntities entities = new OptoEyeCareEntities())
            {
                frame frameId = (from c in entities.frame
                                     where c.Id == Id
                                 select c).FirstOrDefault();
                entities.frame.Remove(frameId);
                entities.SaveChanges();
            }

            return Json(new { success = true });
        }


    }
}