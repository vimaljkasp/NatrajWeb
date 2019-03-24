using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class VLCPaymentStatementDTO
    {
        public int VLCId { get; set; }

        public string VLCName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime PaymentStartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime PaymentEndDate { get; set; }

        public List<VLCPaymentStatmentListDTO> vlcPaymentStatmentListDTO { get; set; }
    }

    public class VLCPaymentStatmentListDTO
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime PaymentDate { get; set; }

        public decimal PaymentCRAmoumt { get; set; }

        public decimal PaymentDRAmoumt { get; set; }

        public string PaymentReceivedBy { get; set; }

        public string PaymentMode { get; set; }

    }
}
