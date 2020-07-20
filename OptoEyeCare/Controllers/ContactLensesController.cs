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
    public class ContactLensesController : Controller
    {     
        // GET: ContactLenses
        
        public ActionResult Index()
        {
            List<SelectListItem> frameList = GetFrameValue();
            List<SelectListItem> lenseList = GetLenses();
            List<SelectListItem> cylList = GetCYL();
            List<SelectListItem> sphList = GetSPH();
            List<SelectListItem> axisList = Getaxis();
            List<SelectListItem> aditionList = Getaddition();
            List<SelectListItem> visionList = Getvision();

            ViewBag.frameList = frameList;
            ViewBag.lenseList = lenseList;
            ViewBag.cylList = cylList;
            ViewBag.sphList = sphList;
            ViewBag.axisList = axisList;
            ViewBag.aditionList = aditionList;
            ViewBag.visionList = visionList;
            return View(frameList);
        }

        private static List<SelectListItem> GetFrameValue()
        {
              OptoEyeCareEntities entities = new OptoEyeCareEntities();
              List<SelectListItem> frameList = (from p in entities.frame.AsEnumerable()
                                                where p.flag == 1
                                                orderby p.frameName ascending
                                                select new SelectListItem
                                                 {
                                                     Text = p.frameName,
                                                     Value = p.Id.ToString()
                                                 }).ToList();


            //Add Default Item at First Position.
            frameList.Insert(0, new SelectListItem { Text = "Select", Value = "0" });
            return frameList;
        }

        private static List<SelectListItem> GetLenses()
        {
            OptoEyeCareEntities entities = new OptoEyeCareEntities();
            List<SelectListItem> lenseList = (from p in entities.lenseDetails.AsEnumerable()
                                              where p.flag ==1
                                              orderby p.lenseName ascending
                                              select new SelectListItem
                                              {
                                                  Text = p.lenseName,
                                                  Value = p.Id.ToString()
                                              }).ToList();


            //Add Default Item at First Position.
            lenseList.Insert(0, new SelectListItem { Text = "Select", Value = "0" });
            return lenseList;
        }

        private static List<SelectListItem> GetCYL()
        {
            OptoEyeCareEntities entities = new OptoEyeCareEntities();
            List<SelectListItem> cylList = (from p in entities.CYL.AsEnumerable()
                                            where p.flag == 1
                                            orderby p.CYL1 ascending
                                              select new SelectListItem
                                              {
                                                  Text = p.CYL1,
                                                  Value = p.Id.ToString()
                                              }).ToList();


            //Add Default Item at First Position.
            cylList.Insert(0, new SelectListItem { Text = "Select", Value = "0" });
            return cylList;
        }

        private static List<SelectListItem> GetSPH()
        {
            OptoEyeCareEntities entities = new OptoEyeCareEntities();
            List<SelectListItem> sphList = (from p in entities.SPH.AsEnumerable()
                                            where p.flag == 1
                                            orderby p.SPHValue ascending
                                            select new SelectListItem
                                            {
                                                Text = p.SPHValue,
                                                Value = p.Id.ToString()
                                            }).ToList();


            //Add Default Item at First Position.
            sphList.Insert(0, new SelectListItem { Text = "Select", Value = "0" });
            return sphList;
        }

        private static List<SelectListItem> Getaxis()
        {
            OptoEyeCareEntities entities = new OptoEyeCareEntities();
            List<SelectListItem> axisList = (from p in entities.axis.AsEnumerable()
                                             where p.flag == 1
                                             orderby p.axisvalue ascending
                                            select new SelectListItem
                                            {
                                                Text = p.axisvalue,
                                                Value = p.Id.ToString()
                                            }).ToList();           
            axisList.Insert(0, new SelectListItem { Text = "Select", Value = "0" });
            return axisList;
        }
        private static List<SelectListItem> Getaddition()
        {
            OptoEyeCareEntities entities = new OptoEyeCareEntities();
            List<SelectListItem> additionList = (from p in entities.addition.AsEnumerable()
                                                 where p.flag == 1
                                                 orderby p.additionValue ascending
                                             select new SelectListItem
                                             {
                                                 Text = p.additionValue,
                                                 Value = p.Id.ToString()
                                             }).ToList();
            additionList.Insert(0, new SelectListItem { Text = "Select", Value = "0" });
            return additionList;
        }

        private static List<SelectListItem> Getvision()
        {
            OptoEyeCareEntities entities = new OptoEyeCareEntities();
            List<SelectListItem> visionList = (from p in entities.vision.AsEnumerable()
                                               where p.flag == 1
                                               orderby p.visionValue ascending
                                                 select new SelectListItem
                                                 {
                                                     Text = p.visionValue,
                                                     Value = p.Id.ToString()
                                                 }).ToList();
            visionList.Insert(0, new SelectListItem { Text = "Select", Value = "0" });
            return visionList;
        }

        [HttpPost]
        public ActionResult SaveContactLenses(contactLenses _contactLenses)
        {
            using (var context = new OptoEyeCareEntities())
            {
                tblContactLenses tcl = new tblContactLenses()
                {
                customerId = _contactLenses.customerId,
                title = _contactLenses.title,
                name = _contactLenses.name,
                mobno = _contactLenses.mobno,
                email = _contactLenses.email,
                address = _contactLenses.address,
                orderDate = _contactLenses.orderDate,
                DeliveryDate = _contactLenses.DeliveryDate,
                DeliveredOn = _contactLenses.DeliveredOn,
                RefNo = _contactLenses.RefNo,
                Examinedy = _contactLenses.Examinedy,
                rightDistanceSPH = _contactLenses.rightDistanceSPH,
                rightDistanceCYL = _contactLenses.rightDistanceCYL,
                rightDistanceAxis = _contactLenses.rightDistanceAxis,
                rightAddition = _contactLenses.rightAddition,                
                leftDistanceSPH = _contactLenses.leftDistanceSPH,
                leftDistanceCYL = _contactLenses.leftDistanceCYL,
                leftDistanceAxis = _contactLenses.leftDistanceAxis,
                leftAddition = _contactLenses.leftAddition,               
                totalPayment = _contactLenses.totalPayment,
                advancedPayment = _contactLenses.advancedPayment,
                BalancePayment = _contactLenses.BalancePayment,
                frame = _contactLenses.frame,
                lenses = _contactLenses.lenses,
                remarks = _contactLenses.remarks,
                createdBy = Convert.ToInt32(Session["UserId"]),
                createdDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                flag = Convert.ToBoolean(1),
                vision= _contactLenses.vision,
                leftvision= _contactLenses.leftvision,
                entryType=_contactLenses.entryType,
                IGST=_contactLenses.IGST,
                TotalPrice=_contactLenses.TotalPrice
                };
               

                context.tblContactLenses.Add(tcl);
                int result = context.SaveChanges();
                if (result > 0)
                {
                    Thread.Sleep(2000);
                    return Json(new { success = true });
                }
                else
                {
                    Thread.Sleep(2000);
                    return Json(new { success = false });
                }
                
                //return Json(new { success = true });
            }
            
        }
    }
}