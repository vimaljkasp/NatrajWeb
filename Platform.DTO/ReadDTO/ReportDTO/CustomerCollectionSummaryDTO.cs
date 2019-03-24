using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class CustomerCollectionSummaryDTO
    {
        public int CustomerId { get; set; }
   

        public List<CustomerCollectionSummaryDtlDTO> customerCollectionSummaryDtlDTOList { get; set; }

    }

    public class CustomerCollectionSummaryDtlDTO
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime CollectionDate { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string Shift { get; set; }
        public string MiilkType { get; set; }
    }
}
