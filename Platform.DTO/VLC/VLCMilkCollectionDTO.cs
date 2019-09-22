using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class VLCMilkCollectionDTO
    {
        public VLCMilkCollectionDTO()
        {
            vLCMilkCollectionDtlDTOList = new List<VLCMilkCollectionDtlDTO>()
            {
                 new VLCMilkCollectionDtlDTO {
                      Amount=0,
                      CLR=0,
                      FAT=0,
                      ProductId=0,
                      ProductName=String.Empty,
                      Quantity=0,
                      RatePerUnit=0,
                      VLCMilkCollectionDtlId=0,
                      VLCMilkCollectionId =0
                 }
            };
        }

        public int VLCMilkCollectionId { get; set; }
        [DisplayName("VLC")]
        public int VLCId { get; set; }
        [DisplayName("Customer")]
        public int CustomerId { get; set; }
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }
        [DisplayName("VLC Name")]
        public string VLCName { get; set; }
        [DisplayName("Total Qty")]
        public Nullable<decimal> TotalQuantity { get; set; }
        [DisplayName("Total Amt")]
        public Nullable<decimal> TotalAmount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Collection Date")]
        public DateTime CollectionDateTime { get; set; }
        [DisplayName("Shift")]
        public string CollectionShift { get; set; }
        [DisplayName("Shift")]
        public ShiftEnum ShiftId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<VLCMilkCollectionDtlDTO> vLCMilkCollectionDtlDTOList { get; set; }

    }
}
