using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class VLCExpenseSummaryDTO
    {
       
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime FromDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime ToDate { get; set; }

        public List<VLCExpenseSummaryDetailDTO> vlcExpenseSummaryDTOList { get; set; }




    }

    public class VLCExpenseSummaryDetailDTO
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime ExpenseDate { get; set; }
        public int VLCId { get; set; }
        public string VLCName { get; set; }
        public string ExpenseReason{get; set; }
        public decimal CRAmount { get; set; }
        public decimal DRAmount { get; set; }
        public string Comments { get; set; }
    }

}
