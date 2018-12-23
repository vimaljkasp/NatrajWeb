using Platform.DTO;
using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class CustomerAgricultureConvertor
    {
        public static CustomerAgricultureDTO ConvertToCustomerAgriCultureDto(CustomerAgriculture customerAgriculture)
        {
            CustomerAgricultureDTO customerAgricultureDto = new CustomerAgricultureDTO();
            if (customerAgriculture != null)
            {
                customerAgricultureDto.CustAgriId = customerAgriculture.CustAgriId;
                customerAgricultureDto.CustomerId = customerAgriculture.CustomerId.GetValueOrDefault();
                customerAgricultureDto.NoOfCow = customerAgriculture.NoOfCow;
                customerAgricultureDto.NoOfBuffelo = customerAgriculture.NoOfBuffelo;
                customerAgricultureDto.AgricultureLand = customerAgriculture.AgricultureLand;
                customerAgricultureDto.MilkProductionQty = customerAgriculture.MilkProductionQty;
                customerAgricultureDto.Comments = customerAgriculture.Comments;
                customerAgricultureDto.CreatedBy = customerAgriculture.CreatedBy;
                customerAgricultureDto.CreatedDate = customerAgriculture.CreatedDate;
                customerAgricultureDto.IsDeleted = customerAgriculture.IsDeleted;
                customerAgricultureDto.ModifiedBy = customerAgriculture.ModifiedBy;
                customerAgricultureDto.ModifiedDate = customerAgriculture.ModifiedDate;
            }
            return customerAgricultureDto;

    }

        public static void ConvertToCustomerAgriCultureEntity(ref CustomerAgriculture customerAgriculture, CustomerAgricultureDTO customerAgricultureDto, bool isUpdate)
        {
            if (isUpdate)
            customerAgriculture.CustAgriId = customerAgricultureDto.CustAgriId;
            customerAgriculture.CustomerId = customerAgricultureDto.CustomerId;
            if(customerAgricultureDto.NoOfCow.GetValueOrDefault()>0)
            customerAgriculture.NoOfCow = customerAgricultureDto.NoOfCow;
            if (customerAgricultureDto.NoOfBuffelo.GetValueOrDefault() > 0)
                customerAgriculture.NoOfBuffelo = customerAgricultureDto.NoOfBuffelo;
            if (customerAgricultureDto.AgricultureLand.GetValueOrDefault() > 0)
                customerAgriculture.AgricultureLand = customerAgricultureDto.AgricultureLand;
            if (customerAgricultureDto.MilkProductionQty.GetValueOrDefault() > 0)
                customerAgriculture.MilkProductionQty = customerAgricultureDto.MilkProductionQty;
            if (String.IsNullOrWhiteSpace(customerAgricultureDto.Comments)==false)
                customerAgriculture.Comments = customerAgricultureDto.Comments;
         


        }
    }
}
