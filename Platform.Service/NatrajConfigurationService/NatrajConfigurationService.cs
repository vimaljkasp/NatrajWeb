using Platform.DTO;
using Platform.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class NatrajConfigurationService : INatrajConfigurationService, IDisposable
    {

        public UnitOfWork unitOfWork = new UnitOfWork();

        public ResponseDTO GetAllNatarjConfigurations()
        {
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Status = true;
            responseDTO.Message = "All Natraj Configurations";
            List<NatrajConfigurationDTO> natrajConfigurationDTOList = new List<NatrajConfigurationDTO>();
            var natrajConfigurationList = unitOfWork.ConfigurationRepository.GetAll();
            if (natrajConfigurationList != null && natrajConfigurationList.Count() > 0)
            {
                foreach (var natrajConfiguration in natrajConfigurationList)
                {
                    natrajConfigurationDTOList.Add(NatrajConfigurationConvertor.ConvertToNatrajConfigurationDTO(natrajConfiguration));
               }
            }
            responseDTO.Data = natrajConfigurationDTOList;
            return responseDTO;
        }


        public ResponseDTO UpdateNatrajConfiguration(NatrajConfigurationDTO natrajConfigurationDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Status = true;
            responseDTO.Message = "Configuration Updated Successfully";
            var natrajConfiguration = unitOfWork.ConfigurationRepository.GetById(natrajConfigurationDTO.ConfigurationId);
                if(natrajConfiguration !=null && string.IsNullOrWhiteSpace(natrajConfigurationDTO.Value)==false)
            {
                natrajConfiguration.DataVal = natrajConfigurationDTO.Value;
            }
            unitOfWork.ConfigurationRepository.Update(natrajConfiguration);
            unitOfWork.SaveChanges();
            responseDTO.Data = this.GetAllNatarjConfigurations();
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
