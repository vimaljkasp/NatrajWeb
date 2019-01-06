using Platform.DTO;
using Platform.Repository;
using Platform.Sql;
using Platform.Utilities.ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class DCPaymentService : IDCPaymentService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        public List<DCPaymentDTO> GetAllDCPayments()
        {
            List<DCPaymentDTO> dCPaymentDetailList = new List<DCPaymentDTO>();
            var dCPaymentDetails = unitOfWork.DCPaymentDetailRepository.GetAll();
            if (dCPaymentDetails != null)
            {
                foreach (var dcpayemnt in dCPaymentDetails)
                {
                    dCPaymentDetailList.Add(DCPaymentConvertor.ConvertToDCPaymentDTO(dcpayemnt));
                }

            }

            return dCPaymentDetailList;

        }

        //public List<DCPaymentDTO> GetDCAddressByPageCount(int? pageNumber, int? count)
        //{
        //    List<DCPaymentDTO> dCAddressList = new List<DCPaymentDTO>();
        //    var dCAddresss = unitOfWork.DCAddressRepository.GetCustomerByCount(pageNumber, count);
        //    if (dCAddresss != null)
        //    {
        //        foreach (var dCAddress in dCAddresss)
        //        {
        //            dCAddressList.Add(DCAddressConvertor.ConvertTodCAddressDto(dCAddress));
        //        }

        //    }

        //    return dCAddressList;

        //}


        public ResponseDTO GetAllDCPayementsByDCId(int dcId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            List<DCPaymentDTO> dCPaymentDetailList = new List<DCPaymentDTO>();
            var dCPaymentDetails = unitOfWork.DCPaymentDetailRepository.GetAllDCPaymentDetailByDCId(dcId);
            if (dCPaymentDetails != null)
            {
                foreach (var dcpayemnt in dCPaymentDetails)
                {
                    dCPaymentDetailList.Add(DCPaymentConvertor.ConvertToDCPaymentDTO(dcpayemnt));
                    
                }
                responseDTO.Status = true;
                responseDTO.Message = "DC Address Details For Distribution Center";
                responseDTO.Data = dCPaymentDetailList;
            }
            else
            {
                responseDTO.Status = false;
                responseDTO.Message = String.Format("DC Address Details with DC ID {0} not found", dcId);
                responseDTO.Data = new object();
            }
            return responseDTO;
        }



        public ResponseDTO AddDCPaymentDetail(DCPaymentDTO dCPaymentDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            DCPaymentDetail dCPaymentDetail = new DCPaymentDetail();

            dCPaymentDetail.DCPaymentId = unitOfWork.DashboardRepository.NextNumberGenerator("DCPaymentDetail");
            DCPaymentConvertor.ConvertToDCPaymentDetailEntity(ref dCPaymentDetail, dCPaymentDTO, false);
            dCPaymentDetail.IsDeleted = false;
            dCPaymentDetail.CreatedBy = dCPaymentDetail.ModifiedBy = "Admin";
            dCPaymentDetail.CreatedDate = dCPaymentDetail.ModifiedDate = DateTime.Now;
            dCPaymentDetail.PaymentDate = DateTime.Now.Date;
            unitOfWork.DCPaymentDetailRepository.Add(dCPaymentDetail);
            unitOfWork.SaveChanges();
            dCPaymentDTO = DCPaymentConvertor.ConvertToDCPaymentDTO(dCPaymentDetail);
            responseDTO.Status = true;
            responseDTO.Message = "DC Payment Detail Added Successfully";
            responseDTO.Data = dCPaymentDTO;
            return responseDTO;
        }


        //public void CheckForExisitngDCAddress(int dcId)
        //{
        //    var dCAddress = unitOfWork.DCAddressRepository.GetDefaultAddressByDCId(dcId);
        //    if (dCAddress != null)
        //        throw new PlatformModuleException("Customer Bank Details Already Exist with given Customer");

        //}

        public ResponseDTO UpdateDCPaymentDetail(DCPaymentDTO dCPaymentDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            var dcPayment = unitOfWork.DCPaymentDetailRepository.GetPaymentDetailByPaymentId(dCPaymentDTO.DCPaymentId);
            if (dcPayment == null)
                return AddDCPaymentDetail(dCPaymentDTO);

            DCPaymentConvertor.ConvertToDCPaymentDetailEntity(ref dcPayment, dCPaymentDTO, true);

            //    dCAddress.ModifiedBy = unitOfWork.VLCRepository.GetEmployeeNameByVLCId(dCAddress.Customer.VLCId.GetValueOrDefault());

            unitOfWork.DCPaymentDetailRepository.Update(dcPayment);
            unitOfWork.SaveChanges();
            dCPaymentDTO = DCPaymentConvertor.ConvertToDCPaymentDTO(dcPayment);
            responseDTO.Status = true;
            responseDTO.Message = "DC Payment Detail Updated Successfully";
            responseDTO.Data = dCPaymentDTO;
            return responseDTO;
        }

        public ResponseDTO DeleteDCPaymentDetail(int id)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            UnitOfWork unitOfWork = new UnitOfWork();
            //get dCAddress
            var dcPayemnt = unitOfWork.DCPaymentDetailRepository.GetPaymentDetailByPaymentId(id);
       
            dcPayemnt.IsDeleted = true;
            unitOfWork.DCPaymentDetailRepository.Update(dcPayemnt);
            unitOfWork.SaveChanges();
            DCPaymentDTO dCPaymentDTO = DCPaymentConvertor.ConvertToDCPaymentDTO(dcPayemnt);
            responseDTO.Status = true;
            responseDTO.Message = "DC Payment Detail Deleted Successfully";
            responseDTO.Data = dCPaymentDTO;
            return responseDTO;

        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (unitOfWork != null)
                {
                    unitOfWork.Dispose();
                    unitOfWork = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
