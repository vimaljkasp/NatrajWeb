using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class VLCCustomerCollectionDTO
    {

        public int VLCMilkCollectionId { get; set; }

        public DateTime CollectionDateTime { get; set; }

        public int CustomerId { get; set; }

        public int CustomerCodeId { get; set; }

        public string CustomerName { get; set; }

        public string Shift { get; set; }

        public Decimal TotalQuantity { get; set; }

        public Decimal TotalAmount {get; set;}

        public List<VLCCustomerCollectionDtlDTO> vLCCustomerCollectionDtlDTOList;

        public VLCCustomerCollectionDTO()
        {
            vLCCustomerCollectionDtlDTOList = new List<VLCCustomerCollectionDtlDTO>();
        }
    }
    public class VLCCustomerCollectionDtlDTO
    {
        public int VLCMilkCollectionDtlId { get; set; }
        public Decimal Fat { get; set; }

        public Decimal CLR { get; set; }

        public Decimal Quantity { get; set; }

        public Decimal Amount { get; set; }

        public string ProductName { get; set; }
    }
}
