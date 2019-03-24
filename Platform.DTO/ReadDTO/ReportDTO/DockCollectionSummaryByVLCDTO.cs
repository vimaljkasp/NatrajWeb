using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class DockCollectionSummaryByVLCDTO
    {
        public int VLCId { get; set; }
        public string VLCName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime CollectionFromDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime CollectionToDate { get; set; }

        public List<DockCollectionSummaryListByVLCDTO> dockCollectionSummaryListByVLCDTO { get; set; }




    }

    public class DockCollectionSummaryListByVLCDTO
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime CollectionDate { get; set; }
        public int TotalCan { get; set; }
        public int TotalRejectedCan { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal RejectedQuantity { get; set; }
        public decimal Amount { get; set; }
        public decimal Commission { get; set; }
        public decimal TotalAmount { get; set; }

        public string Shift { get; set; }
    }

}
