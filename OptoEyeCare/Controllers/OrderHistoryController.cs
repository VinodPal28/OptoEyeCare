using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OptoEyeCare.Models;
using DataTables.Mvc;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OptoEyeCare.Controllers
{
    public class OrderHistoryController : Controller
    {
        OptoEyeCareEntities DbContext = new OptoEyeCareEntities();
        // GET: OrderHistory       
        public ActionResult orderHistory()
        {
            //using (OptoEyeCareEntities db = new OptoEyeCareEntities())
            //{

            //    var orderDetails= from s in db.tblContactLenses
            //                      where s.flag == true
            //                      orderby s.createdDate descending
            //                      select s;

            //    return Json(new { data = orderDetails.ToList() }, JsonRequestBehavior.AllowGet);
            //}
            return View();
        }

        [HttpGet]
        public JsonResult GetData()
        {
            using (OptoEyeCareEntities db = new OptoEyeCareEntities())
            {
                var orderlist = db.tblContactLenses.OrderBy(a => a.name).ToList();
                return Json(new { data = orderlist }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Get([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, tblContactLenses searchViewModel)
        {          
            IQueryable<tblContactLenses> query = (from s in DbContext.tblContactLenses
                        where s.flag == true
                        orderby s.createdDate descending
                        select s).OrderBy(s => s.createdDate)
                                .Skip(requestModel.Start)
                                .Take(requestModel.Length);

            var totalCount = query.Count();

            // searching and sorting
            query = Search(requestModel, searchViewModel, query);
            var filteredCount = query.Count();
           
            var data = query.Select(tblContactLenses => new
            {
                customerId = tblContactLenses.customerId,
                name = tblContactLenses.name,
                mobno = tblContactLenses.mobno,
                email = tblContactLenses.email,
                address = tblContactLenses.address,
                orderDate = tblContactLenses.orderDate,
                DeliveryDate = tblContactLenses.DeliveryDate,
                TotalPrice = tblContactLenses.TotalPrice
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);

        }


        private IQueryable<tblContactLenses> Search(IDataTablesRequest requestModel, tblContactLenses searchViewModel, IQueryable<tblContactLenses> query)
        {

            // Apply filters
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.customerId.Contains(value) ||
                                         p.name.Contains(value) ||
                                         p.mobno.Contains(value) ||
                                         p.orderDate.Contains(value)
                                   );
            }
               
            var filteredCount = query.Count();

            // Sort
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = String.Empty;

            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != String.Empty ? "," : "";
                orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }

            query = query.OrderBy(orderByString == string.Empty ? "customerId asc" : orderByString);

            return query;

        }

        // Details action  
        public async Task<ActionResult> Details(string id)
        {
            var results = await (from s in DbContext.tblContactLenses 
                                 join f in DbContext.frame on
                                    new { frameid = s.frame } equals new { frameid = (int?)f.Id }
                                    into frm from f in frm.DefaultIfEmpty()
                                 join l in DbContext.lenseDetails on
                                    new { lenseid = s.lenses } equals new { lenseid = (int?)l.Id }
                                    into lnse from l in lnse.DefaultIfEmpty()
                                 where s.customerId == id.ToString()
                                 orderby s.customerId
                                 ascending
                                 select new {
                                     s.customerId,s.title,s.name,s.mobno,s.email,s.address,s.orderDate,s.DeliveryDate,
                                     s.DeliveredOn,s.entryType,
                                     RefNo = s.RefNo != null ? s.RefNo : "0",
                                     Examinedy= s.Examinedy != null ? s.Examinedy : "0",
                                     remarks= s.remarks != null ? s.remarks : "",
                                     IGST= s.IGST != null ? s.IGST : 0,
                                     s.rightDistanceSPH,s.rightDistanceCYL,s.rightDistanceAxis,
                                     s.rightAddition,s.vision,s.leftDistanceSPH,s.leftDistanceCYL,s.leftDistanceAxis,s.leftAddition,s.leftvision,
                                     frameName = f.frameName != null ? f.frameName : "",
                                     lenseName = l.lenseName != null ? l.lenseName : "",
                                     s.totalPayment,s.TotalPrice
                                 }).ToListAsync();            
            var assetVM = MapToViewModel(results.FirstOrDefault());         
            if (Request.IsAjaxRequest())
                return PartialView("_PartialDetails", assetVM);

            return View(assetVM);           
        }

        private object MapToViewModel(object p)
        {
            Type type = p.GetType();
            var customer_Id = type.GetProperty("customerId").GetValue(p, null);          
            var facilitySite = DbContext.tblContactLenses.Where(x => x.customerId == customer_Id.ToString());

            OrderHistory _orderHistory = new OrderHistory()
            {
                customerId = type.GetProperty("customerId").GetValue(p, null).ToString(),
                title = type.GetProperty("title").GetValue(p, null).ToString(),
                name = type.GetProperty("name").GetValue(p, null).ToString(),
                mobno = type.GetProperty("mobno").GetValue(p, null).ToString(),
                email = type.GetProperty("email").GetValue(p, null).ToString(),
                address = type.GetProperty("address").GetValue(p, null).ToString(),
                orderDate = type.GetProperty("orderDate").GetValue(p, null).ToString(),
                DeliveryDate = type.GetProperty("DeliveryDate").GetValue(p, null).ToString(),
                DeliveredOn = type.GetProperty("DeliveredOn").GetValue(p).ToString(),
                RefNo = type.GetProperty("RefNo").GetValue(p).ToString(),
                Examinedy = type.GetProperty("Examinedy").GetValue(p, null).ToString(),
                rightDistanceSPH = type.GetProperty("rightDistanceSPH").GetValue(p, null).ToString(),
                rightDistanceCYL = type.GetProperty("rightDistanceCYL").GetValue(p, null).ToString(),
                rightDistanceAxis = type.GetProperty("rightDistanceAxis").GetValue(p, null).ToString(),
                rightAddition = type.GetProperty("rightAddition").GetValue(p, null).ToString(),
                vision = type.GetProperty("vision").GetValue(p, null).ToString(),
                entryType = type.GetProperty("entryType").GetValue(p, null).ToString(),
                leftDistanceSPH = type.GetProperty("leftDistanceSPH").GetValue(p, null).ToString(),
                leftDistanceCYL = type.GetProperty("leftDistanceCYL").GetValue(p, null).ToString(),
                leftDistanceAxis = type.GetProperty("leftDistanceAxis").GetValue(p, null).ToString(),
                leftAddition = type.GetProperty("leftAddition").GetValue(p, null).ToString(),
                leftvision = type.GetProperty("leftvision").GetValue(p, null).ToString(),
                totalPayment = Convert.ToDecimal(type.GetProperty("totalPayment").GetValue(p, null)),
                IGST = Convert.ToDecimal(type.GetProperty("IGST").GetValue(p, null).ToString()),
                TotalPrice = Convert.ToDecimal(type.GetProperty("TotalPrice").GetValue(p, null).ToString()),
                remarks = type.GetProperty("rightDistanceSPH").GetValue(p, null).ToString(),
                lenseName= type.GetProperty("lenseName").GetValue(p, null).ToString(),
                frameName = type.GetProperty("frameName").GetValue(p, null).ToString(),
            };
            return _orderHistory;
           
        }

        // Delete action 
        public ActionResult Delete(string id)
        {
            var query = DbContext.tblContactLenses.FirstOrDefault(x => x.customerId == id);

            //var assetViewModel = MapToViewModel(query);
            contactLenses _contactLenses = new contactLenses()
            {
                customerId = query.customerId
            };

            if (Request.IsAjaxRequest())
                return PartialView("_DeletePartial", _contactLenses);
            return View(_contactLenses);
        }

        // POST: Asset/Delete/5
        //[HttpPost, ActionName("Delete")]
        [HttpPost]
        public async Task<ActionResult> DeleteAsset(string customerId)
        {

            using (OptoEyeCareEntities entities = new OptoEyeCareEntities())
            {
                tblContactLenses obj = (from c in entities.tblContactLenses
                                        where c.customerId == customerId
                                        select c).FirstOrDefault();


                entities.tblContactLenses.Remove(obj);
                entities.SaveChanges();
            }
            var task = DbContext.SaveChangesAsync();
            await task;

            if (task.Exception != null)
            {               
                return View(Request.IsAjaxRequest() ? "_DeletePartial" : "Delete");
            }

            if (Request.IsAjaxRequest())
            {
                return Content("success");
            }
            return RedirectToRoute("Order");
        }
    }
}