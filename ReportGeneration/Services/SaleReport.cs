using System;

namespace ReportGeneration.Services
{
    public class SaleReport
    {
        public int SaleId { get; set; }
        public string ItemName { get; set; }
        public int QuantitySold { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
