using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OptoEyeCare.Controllers
{
    public class SMSController : Controller
    {
        // GET: SMS
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadExcelsheet()
        {
            if (Request.Files.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                
                var file = Request.Files[0];
                string body = Request.Form[0];
                sb.Append(body.ToString());
                string filePath = string.Empty;            
                string name = string.Empty;
                string mobileNo = string.Empty;
                int total = 0;
                if (Request.Files != null)
                {
                   
                    string path = Server.MapPath("~/Uploads/Product/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (Directory.Exists(path))
                    {
                        foreach (string filepath in Directory.GetFiles(path))
                        {
                            System.IO.File.Delete(filepath);
                        }
                    }                  
                    filePath = path + Path.GetFileName("SMSUploadSheet_" + DateTime.Now.ToString("ddMMyyyyHHmmssff") + Path.GetExtension(file.FileName));
                    string extension = Path.GetExtension("SMSUploadSheet_" + DateTime.Now.ToString("ddMMyyyyHHmmssff") + Path.GetExtension(file.FileName));
                    file.SaveAs(filePath);
                    string conString = string.Empty;
                    switch (extension)
                    {
                        case ".xls": //Excel 97-03.
                            conString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;IMEX=1\";";
                            break;
                        case ".xlsx": //Excel 07 and above.
                            conString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + filePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";";
                            break;
                    }
            
                    conString = string.Format(conString, filePath);
                    using (OleDbConnection connExcel = new OleDbConnection(conString))
                    {
                        using (OleDbCommand cmdExcel = new OleDbCommand())
                        {
                            using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                            {
                                DataTable dt = new DataTable();
                                cmdExcel.Connection = connExcel;

                                //Get the name of First Sheet.
                                connExcel.Open();
                                DataTable dtExcelSchema;
                                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                connExcel.Close();

                                //Read Data from First Sheet.
                                connExcel.Open();
                                cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                odaExcel.SelectCommand = cmdExcel;
                                odaExcel.Fill(dt);
                                connExcel.Close();

                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        total++;
                                        name = row["Name"].ToString();
                                        mobileNo = row["MobileNo"].ToString();
                                        sb.Replace("[Name]", name.ToString());
                                        if(name != "" && mobileNo != "")
                                        {
                                            sendSMS(name, mobileNo, sb.ToString());
                                        }                                        
                                    }
                                }
                            }
                        }
                    }
                    
                }
                return View("success");
            }
            else
            {
                //return Json("", JsonRequestBehavior.AllowGet);
                return View("error");
            }

        }
        public string sendSMS(string name, string mobno, string body)
        {
            //String message = HttpUtility.UrlEncode("This is your message");
            String message = body;
            using (var wb = new WebClient())
            {
                byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                {"apikey" , "44cVaQOLiUY-3M8KTMIA8HTHzfazTvXgiffqVhZP13"},
                {"numbers" , mobno},
                {"message" , message},
                {"sender" , "TXTLCL"}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                return result;
            }
        }        
    }
}