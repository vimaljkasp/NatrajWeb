using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class DockCollectionSummaryDetailByVLCDTO
    {
        public int VLCId { get; set; }
        public string VLCName { get; set; }
        public DateTime CollectionFromDate { get; set; }
        public DateTime CollectionToDate { get; set; }

        public List<DockCollectionSummaryDetailByVLCListDTO> dockCollectionSummaryDetailByVLCListDTO { get; set; }

    }

    public class DockCollectionSummaryDetailByVLCListDTO
    {
        public DateTime CollectionDate { get; set; }
        public MilkTypeEnum MilkType { get; set; }
        public decimal Fat { get; set; }
        public decimal CLR { get; set; }

        public int TotalCan { get; set; }
        public int TotalRejectedCan { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal RejectedQuantity { get; set; }
        public decimal RatePerUnit { get; set; }
        public decimal Amount { get; set; }
        public decimal Commission { get; set; }
        public decimal TotalAmount { get; set; }

        public string Shift { get; set; }

    }

}
