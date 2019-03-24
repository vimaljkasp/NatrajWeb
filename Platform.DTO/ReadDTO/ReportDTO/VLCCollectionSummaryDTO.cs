using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class VLCCollectionSummaryDTO
    {
        public int VLCId { get; set; }
        
        public List<VLCCollectionSummaryDtlDTO> vLCCollectionSummaryDtlDTOList { get; set; }

    }

    public class VLCCollectionSummaryDtlDTO
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime CollectionDate { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalCustomer { get; set; }
        public string Shift { get; set; }
    }
}
