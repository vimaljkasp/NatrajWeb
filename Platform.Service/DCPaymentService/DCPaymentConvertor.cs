using Platform.DTO;
using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class DCPaymentConvertor
    {
        public static DCPaymentDTO ConvertToDCPaymentDTO(DCPaymentDetail dCPaymentDetail)
        {
            DCPaymentDTO dCPaymentDTO = new DCPaymentDTO();
            if (dCPaymentDetail != null)
            {
              
                dCPaymentDTO.CreatedBy = dCPaymentDetail.CreatedBy;
                dCPaymentDTO.CreatedDate = dCPaymentDetail.CreatedDate;
                dCPaymentDTO.DCId = dCPaymentDetail.DCId;
                dCPaymentDTO.DCOrderId = dCPaymentDetail.DCOrderId;
                dCPaymentDTO.DCPaymentId = dCPaymentDetail.DCPaymentId;
                dCPaymentDTO.ModifiedDate = dCPaymentDetail.ModifiedDate;
                dCPaymentDTO.ModifiedBy = dCPaymentDetail.ModifiedBy;
                dCPaymentDTO.ModifiedDate = dCPaymentDetail.ModifiedDate;
                dCPaymentDTO.PaymentComments = dCPaymentDetail.PaymentComments;
                dCPaymentDTO.PaymentCrAmount = dCPaymentDetail.PaymentCrAmount.GetValueOrDefault();
                dCPaymentDTO.PaymentDate = dCPaymentDetail.PaymentDate;
                dCPaymentDTO.PaymentDrAmount = dCPaymentDetail.PaymentDrAmount.GetValueOrDefault();
                dCPaymentDTO.PaymentMode = dCPaymentDetail.PaymentMode;
                dCPaymentDTO.PaymentReceivedBy = dCPaymentDetail.PaymentReceivedBy;
            }


            return dCPaymentDTO;
        }

        public static void ConvertToDCPaymentDetailEntity(ref DCPaymentDetail dCPaymentDetail, DCPaymentDTO dCPaymentDTO, bool isUpdate)
        {
            dCPaymentDetail.DCId = dCPaymentDTO.DCId;
            dCPaymentDetail.DCOrderId = dCPaymentDTO.DCOrderId;
            dCPaymentDetail.PaymentComments = dCPaymentDTO.PaymentComments;
            dCPaymentDetail.PaymentCrAmount = dCPaymentDTO.PaymentCrAmount;
            dCPaymentDetail.PaymentDrAmount = dCPaymentDTO.PaymentDrAmount;
            dCPaymentDetail.PaymentMode = dCPaymentDTO.PaymentMode;
            dCPaymentDetail.PaymentReceivedBy = dCPaymentDTO.PaymentReceivedBy;
        }
    }
}
