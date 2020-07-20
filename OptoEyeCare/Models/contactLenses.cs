using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OptoEyeCare.Models
{
    public class contactLenses
    {
        public int Id { get; set; }
        public string customerId { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public string mobno { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string orderDate { get; set; }
        public string DeliveryDate { get; set; }
        public string DeliveredOn { get; set; }
        public string RefNo { get; set; }
        public string Examinedy { get; set; }
        public string rightDistanceSPH { get; set; }
        public string rightDistanceCYL { get; set; }
        public string rightDistanceAxis { get; set; }
        public string rightAddition { get; set; }
        public string rightPD { get; set; }
        public string leftDistanceSPH { get; set; }
        public string leftDistanceCYL { get; set; }
        public string leftDistanceAxis { get; set; }
        public string leftAddition { get; set; }
        public string leftPD { get; set; }
        public decimal totalPayment { get; set; }
        public decimal advancedPayment { get; set; }
        public decimal BalancePayment { get; set; }
        public int frame { get; set; }
        public int lenses { get; set; }
        public string remarks { get; set; }
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int updatedBy { get; set; }
        public DateTime updtedDate { get; set; }
        public int flag { get; set; }
        public string entryType { get; set; }
        public string vision { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<decimal> CGST { get; set; }
        public Nullable<decimal> SGST { get; set; }
        public Nullable<decimal> IGST { get; set; }
        public string leftvision { get; set; }
    }
}