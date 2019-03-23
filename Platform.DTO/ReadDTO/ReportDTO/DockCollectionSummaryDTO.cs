using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class DockCollectionSummaryDTO
    {

        public DateTime CollectionFromDate { get; set; }
        public DateTime CollectionToDate { get; set; }
        public string CollectionFromShift { get; set; }
        public string CollectionToShift { get; set; }
        public string MilkType { get; set; }
        public List<DockCollectionSummaryListDTO> dockCollectionSummaryListDTO { get; set; }




    }

    public class DockCollectionSummaryListDTO
    {
        public DateTime CollectionDate { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal Amount { get; set; }
        public decimal Commission { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalVLC { get; set; }
        public string Shift { get; set; }
        public decimal RejectedQuantity { get; set; }
        public decimal AvgFAT { get; set; }
        public decimal AvgCLR { get; set; }
        public decimal AvgRatePerUnit { get; set; }
        public string MiilkType { get; set; }
    }

}
