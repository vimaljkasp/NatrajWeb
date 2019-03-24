using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Platform.DTO
{
    public class DockCollectionSummaryDetailByVLCDTO
    {
        public int VLCId { get; set; }
        public string VLCName { get; set; }
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

            public DateTime CollectionFromDate { get; set; }
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

            public DateTime CollectionToDate { get; set; }

        public List<DockCollectionSummaryDetailByVLCListDTO> dockCollectionSummaryDetailByVLCListDTO { get; set; }

    }

    public class DockCollectionSummaryDetailByVLCListDTO
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime CollectionDate { get; set; }
        public ReportMilkTypeEnum  MilkType { get; set; }
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
