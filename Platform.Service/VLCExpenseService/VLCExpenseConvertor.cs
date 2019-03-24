using Platform.DTO;
using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class VLCExpenseConvertor
    {
        public static VLCExpenseDTO ConvertToVLCExpenseDTO(VLCExpenseDetail vLCExpenseDetail)
        {
            VLCExpenseDTO vLCExpenseDTO = new VLCExpenseDTO();
            if (vLCExpenseDetail != null)
            {
                vLCExpenseDTO.VLCExpenseId = vLCExpenseDetail.VLCExpenseId;
                vLCExpenseDTO.CreatedBy = vLCExpenseDetail.CreatedBy;
                vLCExpenseDTO.CreatedDate = vLCExpenseDetail.CreatedDate;
                vLCExpenseDTO.VLCId = vLCExpenseDetail.VLCId;
                vLCExpenseDTO.ExpenseReason= vLCExpenseDetail.ExpenseReason;
                vLCExpenseDTO.VLCExpenseId = vLCExpenseDetail.VLCExpenseId;
                vLCExpenseDTO.ModifiedDate = vLCExpenseDetail.ModifiedDate;
                vLCExpenseDTO.ModifiedBy = vLCExpenseDetail.ModifiedBy;
                vLCExpenseDTO.ModifiedDate = vLCExpenseDetail.ModifiedDate;
                vLCExpenseDTO.ExpenseComments = vLCExpenseDetail.ExpenseComments;
                vLCExpenseDTO.PaymentCrAmount = vLCExpenseDetail.PaymentCrAmount.GetValueOrDefault();
                vLCExpenseDTO.ExpenseDate = vLCExpenseDetail.ExpenseDate;
                vLCExpenseDTO.PaymentDrAmount = vLCExpenseDetail.PaymentDrAmount.GetValueOrDefault();
            }


            return vLCExpenseDTO;
        }

        public static void ConvertToVLCExpenseDetailEntity(ref VLCExpenseDetail vLCExpenseDetail, VLCExpenseDTO vLCExpenseDTO, bool isUpdate)
        {
            vLCExpenseDetail.VLCId = vLCExpenseDTO.VLCId;
            if (string.IsNullOrWhiteSpace(vLCExpenseDTO.ExpenseComments) == false)
                vLCExpenseDetail.ExpenseComments = vLCExpenseDTO.ExpenseComments;
          
        }
    }
}
