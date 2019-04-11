using Platform.DTO;
using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class VLCPaymentConvertor
    {
        public static VLCPaymentDTO ConvertToVLCPaymentDTO(VLCPaymentDetail vLCPaymentDetail)
        {
            VLCPaymentDTO vLCPaymentDTO = new VLCPaymentDTO();
            if (vLCPaymentDetail != null)
            {

                vLCPaymentDTO.CreatedBy = vLCPaymentDetail.CreatedBy;
                vLCPaymentDTO.CreatedDate = vLCPaymentDetail.CreatedDate;
                vLCPaymentDTO.VLCId = vLCPaymentDetail.VLCId;
                vLCPaymentDTO.DockMilkCollectionId = vLCPaymentDetail.DockMilkCollectionId;
                vLCPaymentDTO.VLCPaymentId = vLCPaymentDetail.VLCPaymentId;
                vLCPaymentDTO.ModifiedDate = vLCPaymentDetail.ModifiedDate;
                vLCPaymentDTO.ModifiedBy = vLCPaymentDetail.ModifiedBy;
                vLCPaymentDTO.ModifiedDate = vLCPaymentDetail.ModifiedDate;
                vLCPaymentDTO.PaymentComments = vLCPaymentDetail.PaymentComments;
                vLCPaymentDTO.PaymentCrAmount = vLCPaymentDetail.PaymentCrAmount.GetValueOrDefault();
                vLCPaymentDTO.PaymentDate = vLCPaymentDetail.PaymentDate;
                vLCPaymentDTO.PaymentDrAmount = vLCPaymentDetail.PaymentDrAmount.GetValueOrDefault();
                PaymentModeEnum paymentMode;
                Enum.TryParse(vLCPaymentDetail.PaymentMode.ToString(),out paymentMode);
                vLCPaymentDTO.PaymentMode = paymentMode;
                vLCPaymentDTO.PaymentReceivedBy = vLCPaymentDetail.PaymentReceivedBy;
            }


            return vLCPaymentDTO;
        }

        public static void ConvertToVLCPaymentDetailEntity(ref VLCPaymentDetail vLCPaymentDetail, VLCPaymentDTO vLCPaymentDTO, bool isUpdate)
        {
            vLCPaymentDetail.VLCId = vLCPaymentDTO.VLCId;
            if (string.IsNullOrWhiteSpace(vLCPaymentDTO.PaymentComments) == false)
                vLCPaymentDetail.PaymentComments = vLCPaymentDTO.PaymentComments;
                vLCPaymentDetail.PaymentMode = (int)vLCPaymentDTO.PaymentMode;
            if (string.IsNullOrWhiteSpace(vLCPaymentDTO.PaymentReceivedBy) == false)
                vLCPaymentDetail.PaymentReceivedBy = vLCPaymentDTO.PaymentReceivedBy;
        }
    }
}
