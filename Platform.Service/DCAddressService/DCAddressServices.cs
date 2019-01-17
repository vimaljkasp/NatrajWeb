﻿using Platform.DTO;
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
    public class DCAddressServices : IDCAddressService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        public List<DCAddressDTO> GetAllDCAddresses()
        {
            List<DCAddressDTO> dCAddressList = new List<DCAddressDTO>();
            var dCAddresses = unitOfWork.DCAddressRepository.GetAll();
            if (dCAddresses != null)
            {
                foreach (var dCAddress in dCAddresses)
                {
                    dCAddressList.Add(DCAddressConvertor.ConvertToDCAddressDTO(dCAddress));
                }

            }

            return dCAddressList;

        }

    
        public ResponseDTO GetDefaultDCAddressByDCId(int dcId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            DCAddressDTO dCAddressDto = null;
            var dCAddress = unitOfWork.DCAddressRepository.GetDefaultAddressByDCId(dcId);
            if (dCAddress != null)
            {
                dCAddressDto = DCAddressConvertor.ConvertToDCAddressDTO(dCAddress);
                responseDTO.Status = true;
                responseDTO.Message = "DC Address Details For Distribution Center";
                responseDTO.Data = dCAddressDto;
            }
            else
            {
                responseDTO.Status = false;
                responseDTO.Message = String.Format("DC Address Details with DC ID {0} not found", dcId);
                responseDTO.Data = new object();
            }
            return responseDTO;
        }



        public ResponseDTO AddDCAddress(DCAddressDTO dCAddressDto)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            DCAddress dCAddress = new DCAddress();
            var exisitingdCAddress = unitOfWork.DCAddressRepository.GetDefaultAddressByDCId(dCAddressDto.DCId);
            if (exisitingdCAddress != null)
                throw new PlatformModuleException(string.Format("DC Address Details Already Exist For DC Id {0}", dCAddressDto.DCId));

            dCAddress.DCAddressId = unitOfWork.DashboardRepository.NextNumberGenerator("DCAddress");
            DCAddressConvertor.ConvertToDCAddressEntity(ref dCAddress, dCAddressDto, false);
            dCAddress.IsDeleted = false;
          
            unitOfWork.DCAddressRepository.Add(dCAddress);
            unitOfWork.SaveChanges();
            dCAddressDto = DCAddressConvertor.ConvertToDCAddressDTO(dCAddress);
            responseDTO.Status = true;
            responseDTO.Message = "Customer Bank Added Successfully";
            responseDTO.Data = dCAddressDto;
            return responseDTO;
        }


        public void CheckForExisitngDCAddress(int dcId)
        {
            var dCAddress = unitOfWork.DCAddressRepository.GetDefaultAddressByDCId(dcId);
            if (dCAddress != null)
                throw new PlatformModuleException("Customer Bank Details Already Exist with given Customer");

        }

        public ResponseDTO UpdateDCAddress(DCAddressDTO dCAddressDto)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            var dCAddress = unitOfWork.DCAddressRepository.GetDefaultAddressByDCId(dCAddressDto.DCId);
            if (dCAddress == null)
                return AddDCAddress(dCAddressDto);

            DCAddressConvertor.ConvertToDCAddressEntity(ref dCAddress, dCAddressDto, true);
           
            //    dCAddress.ModifiedBy = unitOfWork.VLCRepository.GetEmployeeNameByVLCId(dCAddress.Customer.VLCId.GetValueOrDefault());

            unitOfWork.DCAddressRepository.Update(dCAddress);
            unitOfWork.SaveChanges();
            dCAddressDto = DCAddressConvertor.ConvertToDCAddressDTO(dCAddress);
            responseDTO.Status = true;
            responseDTO.Message = "DC Address Updated Successfully";
            responseDTO.Data = dCAddressDto;
            return responseDTO;
        }

        public ResponseDTO DeleteDCAddress(int id)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            UnitOfWork unitOfWork = new UnitOfWork();
            //get dCAddress
            var dCAddress = unitOfWork.DCAddressRepository.GetDefaultAddressByDCId(id);
         
            dCAddress.IsDeleted = true;
            unitOfWork.DCAddressRepository.Update(dCAddress);
            unitOfWork.SaveChanges();
            DCAddressDTO dCAddressDTO = DCAddressConvertor.ConvertToDCAddressDTO(dCAddress);
            responseDTO.Status = true;
            responseDTO.Message = "DC Address Deleted Successfully";
            responseDTO.Data = dCAddressDTO;
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
