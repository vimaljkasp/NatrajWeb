using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class DockMilkCollectionDTO
    {

        public DockMilkCollectionDTO()
        {
            CollectionDateTime = DateTime.Now;
            dockMilkCollectionList = new List<DockMilkCollectionDtlDTO>() {
                 new DockMilkCollectionDtlDTO{
                      CLR=0,
                       Comments="",
                        FAT=0,
                         ProductId=0,
                          Quantity=0,
                           TotalCan=0,
                            TotalRejectedCan=0
                 }

            };
        }
        public int DockMilkCollectionId { get; set; }
        public int VLCId { get; set; }
        public string VLCName { get; set; }
        public int DockMilkId { get; set; }
        public Nullable<int> TotalCan { get; set; }
        public Nullable<int> TotalRejectedCan { get; set; }
        public Nullable<decimal> TotalQuantity { get; set; }
        public Nullable<decimal> RejectedQuantity { get; set; }
        public decimal Amount { get; set; }
        public Nullable<decimal> Commission { get; set; }
        public decimal TotalAmount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CollectionDateTime { get; set; }
        public string CollectionShift { get; set; }
        public ShiftEnum ShiftId { get; set; }
        public string BillNumber { get; set; }
        public string ReceiverName { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<DockMilkCollectionDtlDTO> dockMilkCollectionList { get; set; }


    }
}
