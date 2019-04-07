using Platform.DTO;
using Platform.Repository;
using Platform.Sql;
using Platform.Utilities;
using System;
using System.Collections.Generic;


namespace Platform.Service
{
    public class VLCExpenseService : IVLCExpenseService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        public List<VLCExpenseDTO> GetAllVLCExpenses()
        {
            List<VLCExpenseDTO> vLCPaymentDetailList = new List<VLCExpenseDTO>();
            var vLCExpenseDetails = unitOfWork.VLCExpenseDetailRepository.GetAll();
            if (vLCExpenseDetails != null)
            {
                foreach (var vLCpayemnt in vLCExpenseDetails)
                {
                    vLCPaymentDetailList.Add(VLCExpenseConvertor.ConvertToVLCExpenseDTO(vLCpayemnt));
                }

            }

            return vLCPaymentDetailList;

        }

        public ResponseDTO GetVLCExpensesById(int vLCExpenseId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            VLCExpenseDTO vLCExpenseList = new VLCExpenseDTO();
            var vLCExpenseDetails = unitOfWork.VLCExpenseDetailRepository.GetExpenseByExpenseId(vLCExpenseId);
            if (vLCExpenseDetails != null)
            {
                vLCExpenseList = VLCExpenseConvertor.ConvertToVLCExpenseDTO(vLCExpenseDetails);
                responseDTO.Status = true;
                responseDTO.Message = "VLC Expense Details ";
                responseDTO.Data = vLCExpenseList;
            }
            else
            {
                responseDTO.Status = false;
                responseDTO.Message = String.Format("VLC Payemnts Details with VLC expense ID {0} not found", vLCExpenseId);
                responseDTO.Data = new object();
            }
            return responseDTO;
        }


        public ResponseDTO GetAllVLCExpensesByVLCId(int vLCId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            List<VLCExpenseDTO> vLCExpenseList = new List<VLCExpenseDTO>();
            var vLCExpenseDetails = unitOfWork.VLCExpenseDetailRepository.GetAllVLCExpenseDetailByVLCId(vLCId);
            if (vLCExpenseDetails != null)
            {
                foreach (var vlcExpense in vLCExpenseDetails)
                {
                    vLCExpenseList.Add(VLCExpenseConvertor.ConvertToVLCExpenseDTO(vlcExpense));

                }
                responseDTO.Status = true;
                responseDTO.Message = "VLC Expense Details ";
                responseDTO.Data = vLCExpenseList;
            }
            else
            {
                responseDTO.Status = false;
                responseDTO.Message = String.Format("VLC Payemnts Details with VLC ID {0} not found", vLCId);
                responseDTO.Data = new object();
            }
            return responseDTO;
        }



        public ResponseDTO AddVLCExpenseDetail(VLCExpenseDTO vLCExpenseDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();

            var vLC = unitOfWork.VLCRepository.GetById(vLCExpenseDTO.VLCId);
            if (vLC != null)
            {
                VLCExpenseDetail vLCExpenseDetail = AddExpense(vLCExpenseDTO);
                vLCExpenseDTO = VLCExpenseConvertor.ConvertToVLCExpenseDTO(vLCExpenseDetail);

                responseDTO.Status = true;
                responseDTO.Message = "VLC Expense Detail Added Successfully";
                responseDTO.Data = vLCExpenseDTO;
                return responseDTO;
            }
            else
            {
                throw new PlatformModuleException("VLC Not Found");
            }

        }

        private VLCExpenseDetail AddExpense(VLCExpenseDTO vLCExpenseDTO)
        {
            VLCExpenseDetail vLCExpenseDetail = new VLCExpenseDetail();
            vLCExpenseDetail.VLCExpenseId = unitOfWork.DashboardRepository.NextNumberGenerator("VLCExpenseDetail");
            vLCExpenseDetail.VLCId = vLCExpenseDTO.VLCId;
            vLCExpenseDetail.ExpenseReason = (int)vLCExpenseDTO.ExpenseReason;
            vLCExpenseDetail.IsDeleted = false;
            vLCExpenseDetail.CreatedBy = vLCExpenseDetail.ModifiedBy = "Admin";
            vLCExpenseDetail.CreatedDate = vLCExpenseDetail.ModifiedDate = DateTimeHelper.GetISTDateTime();
            if (vLCExpenseDetail.ExpenseDate != DateTime.MinValue)
                vLCExpenseDetail.ExpenseDate = DateTimeHelper.GetISTDateTime().Date;
            else
                vLCExpenseDetail.ExpenseDate = vLCExpenseDTO.ExpenseDate;
            unitOfWork.VLCExpenseDetailRepository.Add(vLCExpenseDetail);
            if (vLCExpenseDTO.PaymentCrAmount > 0)
            {
                UpdateVLCWalletForExpense(vLCExpenseDTO.VLCId, vLCExpenseDTO.PaymentCrAmount, true);
                AddVLCExpenseInPaymentDetail(vLCExpenseDTO, vLCExpenseDetail.VLCExpenseId, vLCExpenseDTO.PaymentCrAmount);
            }
            else
            {
                UpdateVLCWalletForExpense(vLCExpenseDTO.VLCId, vLCExpenseDTO.PaymentDrAmount, false);
                AddVLCExpenseInPaymentDetail(vLCExpenseDTO, vLCExpenseDetail.VLCExpenseId, vLCExpenseDTO.PaymentCrAmount);
            }
            unitOfWork.SaveChanges();
            return vLCExpenseDetail;
        }


        public VLCPaymentDetail AddVLCExpenseInPaymentDetail(VLCExpenseDTO vLCPaymentDTO, int expenseId, decimal paidAmount)
        {
            VLCPaymentDetail vLCPaymentDetail = new VLCPaymentDetail();
            vLCPaymentDetail.VLCPaymentId = unitOfWork.DashboardRepository.NextNumberGenerator("VLCPaymentDetail");
            var vLC = unitOfWork.DistributionCenterRepository.GetById(vLCPaymentDTO.VLCId);
            vLCPaymentDetail.VLCId = vLCPaymentDTO.VLCId;
            if (string.IsNullOrWhiteSpace(vLCPaymentDTO.ExpenseComments) == false)
                vLCPaymentDetail.PaymentComments = vLCPaymentDTO.ExpenseComments;

            vLCPaymentDetail.VLCExpenseId = expenseId;
            vLCPaymentDetail.IsDeleted = false;
            vLCPaymentDetail.CreatedBy = vLCPaymentDetail.ModifiedBy = "Admin";
            vLCPaymentDetail.CreatedDate = vLCPaymentDetail.ModifiedDate = DateTimeHelper.GetISTDateTime();
            if (vLCPaymentDTO.ExpenseDate != DateTime.MinValue)
                vLCPaymentDetail.PaymentDate = DateTimeHelper.GetISTDateTime().Date;
            else
                vLCPaymentDetail.PaymentDate = vLCPaymentDTO.ExpenseDate;
            vLCPaymentDetail.PaymentCrAmount = paidAmount;
            unitOfWork.VLCPaymentDetailRepository.Add(vLCPaymentDetail);
            return vLCPaymentDetail;
        }

        public void UpdateVLCWalletForExpense(int vLCId, decimal orderAmount, bool isCredit)
        {
            var vLCWallet = unitOfWork.VLCWalletRepository.GetByVLCId(vLCId);
            if (isCredit)
                vLCWallet.WalletBalance -= orderAmount;
            else
                vLCWallet.WalletBalance += orderAmount;
            vLCWallet.AmountDueDate = vLCWallet.AmountDueDate.AddDays(10);
            unitOfWork.VLCWalletRepository.Update(vLCWallet);
        }



        public ResponseDTO UpdateVLCExpenseDetail(VLCExpenseDTO vLCExpenseDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            var vLCExpense = unitOfWork.VLCExpenseDetailRepository.GetExpenseByExpenseId(vLCExpenseDTO.VLCExpenseId);
            if (vLCExpense == null)
                throw new PlatformModuleException("VLC Expense Details not found with VLC Expense Id");

            // return AddVLCPaymentDetail(vLCPaymentDTO);

            VLCExpenseConvertor.ConvertToVLCExpenseDetailEntity(ref vLCExpense, vLCExpenseDTO, true);

            vLCExpense.PaymentCrAmount = vLCExpenseDTO.PaymentCrAmount;
            vLCExpense.PaymentDrAmount = vLCExpenseDTO.PaymentDrAmount;
            vLCExpense.ModifiedBy = "Admin";
            vLCExpense.ModifiedDate = DateTimeHelper.GetISTDateTime();

            unitOfWork.VLCExpenseDetailRepository.Update(vLCExpense);
            unitOfWork.SaveChanges();
            vLCExpenseDTO = VLCExpenseConvertor.ConvertToVLCExpenseDTO(vLCExpense);
            responseDTO.Status = true;
            responseDTO.Message = "VLC Expense Detail Updated Successfully";
            responseDTO.Data = vLCExpenseDTO;
            return responseDTO;
        }

        public ResponseDTO DeleteVLCExpenseDetail(int id)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            UnitOfWork unitOfWork = new UnitOfWork();
            //get vLCAddress
            var vLCExpenseDetail = unitOfWork.VLCExpenseDetailRepository.GetExpenseByExpenseId(id);

            vLCExpenseDetail.IsDeleted = true;
            unitOfWork.VLCExpenseDetailRepository.Update(vLCExpenseDetail);
            unitOfWork.SaveChanges();
            VLCExpenseDTO vLCPaymentDTO = VLCExpenseConvertor.ConvertToVLCExpenseDTO(vLCExpenseDetail);
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

        public ResponseDTO UpdateMachineAndHouseRentExpenseForALLVLC(DateTime expenseMonth)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            var vlcList = unitOfWork.VLCRepository.GetAll();
            foreach (var vlc in vlcList)
            {
                if (vlc.HouseRent.GetValueOrDefault() > 0 || vlc.MachineRent.GetValueOrDefault() > 0)
                {
                    VLCExpenseDTO vLCExpenseDTO = new VLCExpenseDTO();
                    vLCExpenseDTO.VLCId = vlc.VLCId;
                    vLCExpenseDTO.ExpenseDate = expenseMonth;
                    if (vlc.HouseRent.GetValueOrDefault() > 0 && vlc.MachineRent.GetValueOrDefault() > 0)
                    {
                        vLCExpenseDTO.ExpenseReason = VLCExpenseEnum.HouseAndMachineRent;
                        vLCExpenseDTO.PaymentDrAmount = vlc.HouseRent.GetValueOrDefault() + vlc.MachineRent.GetValueOrDefault();
                    }
                    else if (vlc.HouseRent.GetValueOrDefault() > 0)
                    {
                        vLCExpenseDTO.ExpenseReason = VLCExpenseEnum.HouseRent;
                        vLCExpenseDTO.PaymentDrAmount = vlc.HouseRent.GetValueOrDefault();

                    }
                    else
                    {
                        vLCExpenseDTO.ExpenseReason = VLCExpenseEnum.MachineRent;
                        vLCExpenseDTO.PaymentDrAmount = vlc.HouseRent.GetValueOrDefault();
                    }
                }
            }
            responseDTO.Message = "Machine Rent and House Rent Updated for all VLCs";
            responseDTO.Status = true;
            responseDTO.Data = new object();
            return responseDTO;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
