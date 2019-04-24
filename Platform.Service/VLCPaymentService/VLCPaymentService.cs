using Platform.DTO;
using Platform.Repository;
using Platform.Sql;
using Platform.Utilities;
using System;
using System.Collections.Generic;


namespace Platform.Service
{
    public class VLCPaymentService : IVLCPaymentService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        public List<VLCPaymentDTO> GetAllVLCPayments()
        {
            List<VLCPaymentDTO> vLCPaymentDetailList = new List<VLCPaymentDTO>();
            var vLCPaymentDetails = unitOfWork.VLCPaymentDetailRepository.GetAll();
            if (vLCPaymentDetails != null)
            {
                foreach (var vLCpayemnt in vLCPaymentDetails)
                {
                    vLCPaymentDetailList.Add(VLCPaymentConvertor.ConvertToVLCPaymentDTO(vLCpayemnt));
                }

            }

            return vLCPaymentDetailList;

        }



        public ResponseDTO GetAllVLCPayementsByVLCId(int vLCId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            List<VLCPaymentDTO> vLCPaymentDetailList = new List<VLCPaymentDTO>();
            var vLCPaymentDetails = unitOfWork.VLCPaymentDetailRepository.GetAllVLCPaymentDetailByVLCId(vLCId);
            if (vLCPaymentDetails != null)
            {
                foreach (var vLCpayemnt in vLCPaymentDetails)
                {
                    vLCPaymentDetailList.Add(VLCPaymentConvertor.ConvertToVLCPaymentDTO(vLCpayemnt));

                }
                responseDTO.Status = true;
                responseDTO.Message = "VLC Payemnts Details For Dock Milk  Collection";
                responseDTO.Data = vLCPaymentDetailList;
            }
            else
            {
                responseDTO.Status = false;
                responseDTO.Message = String.Format("VLC Payemnts Details with VLC ID {0} not found", vLCId);
                responseDTO.Data = new object();
            }
            return responseDTO;
        }



        public ResponseDTO AddVLCPaymentDetail(VLCPaymentDTO vLCPaymentDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            VLCPaymentDetail vLCPaymentDetail;
            if (vLCPaymentDTO.PaymentCrAmount > 0)
            {
                UpdateVLCWalletForOrder(vLCPaymentDTO.VLCId, vLCPaymentDTO.PaymentCrAmount, true);
                vLCPaymentDetail = AddDockCollectionPaymentDetail(vLCPaymentDTO, 0, vLCPaymentDTO.PaymentCrAmount);
            }
            else
            {
                UpdateVLCWalletForOrder(vLCPaymentDTO.VLCId, vLCPaymentDTO.PaymentDrAmount, false);
                vLCPaymentDetail = AddDockCollectionPaymentDetail(vLCPaymentDTO, 0, vLCPaymentDTO.PaymentDrAmount);
            }
            unitOfWork.SaveChanges();
            vLCPaymentDTO = VLCPaymentConvertor.ConvertToVLCPaymentDTO(vLCPaymentDetail);

            responseDTO.Status = true;
            responseDTO.Message = "VLC Payment Detail Added Successfully";
            responseDTO.Data = vLCPaymentDTO;
            return responseDTO;
        }

        public VLCPaymentDetail AddDockCollectionPaymentDetail(VLCPaymentDTO vLCPaymentDTO, int dockCollectionId, decimal paidAmount)
        {
            VLCPaymentDetail vLCPaymentDetail = new VLCPaymentDetail();
            vLCPaymentDetail.VLCPaymentId = unitOfWork.DashboardRepository.NextNumberGenerator("VLCPaymentDetail");
            var vLC = unitOfWork.DistributionCenterRepository.GetById(vLCPaymentDTO.VLCId);
            VLCPaymentConvertor.ConvertToVLCPaymentDetailEntity(ref vLCPaymentDetail, vLCPaymentDTO, false);
            vLCPaymentDetail.DockMilkCollectionId = dockCollectionId;
            vLCPaymentDetail.IsDeleted = false;
            vLCPaymentDetail.CreatedBy = vLCPaymentDetail.ModifiedBy = "Admin";
            vLCPaymentDetail.CreatedDate = vLCPaymentDetail.ModifiedDate = DateTimeHelper.GetISTDateTime();
            if (vLCPaymentDTO.PaymentDate != DateTime.MinValue)
                vLCPaymentDetail.PaymentDate = DateTimeHelper.GetISTDateTime().Date;
            else
                vLCPaymentDetail.PaymentDate = vLCPaymentDTO.PaymentDate;
            vLCPaymentDetail.PaymentCrAmount = paidAmount;
            unitOfWork.VLCPaymentDetailRepository.Add(vLCPaymentDetail);
            return vLCPaymentDetail;
        }

        public void UpdateVLCWalletForOrder(int vLCId, decimal orderAmount, bool isCredit)
        {
            var vLCWallet = unitOfWork.VLCWalletRepository.GetByVLCId(vLCId);
            if (isCredit)
                vLCWallet.WalletBalance -= orderAmount;
            else
                vLCWallet.WalletBalance += orderAmount;
            vLCWallet.AmountDueDate = vLCWallet.AmountDueDate.AddDays(10);
            unitOfWork.VLCWalletRepository.Update(vLCWallet);
        }



        public ResponseDTO UpdateVLCPaymentDetail(VLCPaymentDTO vLCPaymentDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            var vLCPayment = unitOfWork.VLCPaymentDetailRepository.GetPaymentDetailByPaymentId(vLCPaymentDTO.VLCPaymentId);
            if (vLCPayment == null)
                throw new PlatformModuleException("VLC Payment Details not found with VLC Payment Id");

            // return AddVLCPaymentDetail(vLCPaymentDTO);

            VLCPaymentConvertor.ConvertToVLCPaymentDetailEntity(ref vLCPayment, vLCPaymentDTO, true);

            vLCPayment.DockMilkCollectionId = vLCPaymentDTO.DockMilkCollectionId;
            vLCPayment.PaymentCrAmount = vLCPaymentDTO.PaymentCrAmount;
            vLCPayment.PaymentDrAmount = vLCPaymentDTO.PaymentDrAmount;
            vLCPayment.ModifiedBy = "Admin";
            vLCPayment.ModifiedDate = DateTimeHelper.GetISTDateTime();

            unitOfWork.VLCPaymentDetailRepository.Update(vLCPayment);
            unitOfWork.SaveChanges();
            vLCPaymentDTO = VLCPaymentConvertor.ConvertToVLCPaymentDTO(vLCPayment);
            responseDTO.Status = true;
            responseDTO.Message = "VLC Payment Detail Updated Successfully";
            responseDTO.Data = vLCPaymentDTO;
            return responseDTO;
        }

        public ResponseDTO DeleteVLCPaymentDetail(int id)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            UnitOfWork unitOfWork = new UnitOfWork();
            //get vLCAddress
            var vLCPayemnt = unitOfWork.VLCPaymentDetailRepository.GetPaymentDetailByPaymentId(id);

            vLCPayemnt.IsDeleted = true;
            unitOfWork.VLCPaymentDetailRepository.Update(vLCPayemnt);
            unitOfWork.SaveChanges();
            VLCPaymentDTO vLCPaymentDTO = VLCPaymentConvertor.ConvertToVLCPaymentDTO(vLCPayemnt);
            responseDTO.Status = true;
            responseDTO.Message = "VLC Payment Detail Deleted Successfully";
            responseDTO.Data = vLCPaymentDTO;
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
