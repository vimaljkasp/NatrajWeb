using Platform.DTO;
using Platform.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service
{
    public class CustomerBankConvertor
    {
        public static CustomerBankDTO ConvertTocustomerBankDto(CustomerBank customerBank)
        {
            CustomerBankDTO customerBankDto = new CustomerBankDTO();
            if (customerBank != null)
            {
                customerBankDto.CustomerBankId = customerBank.CustomerBankId;
                customerBankDto.CustomerId = customerBank.CustomerId.GetValueOrDefault();
                customerBankDto.IFSCCode = customerBank.IFSCCode;
                customerBankDto.AccountNo = customerBank.AccountNo;
                customerBankDto.Branch = customerBank.Branch;
                customerBankDto.BankName = customerBank.BankName;
                customerBankDto.AccountHolderName = customerBank.AccountHolderName;
                customerBankDto.UPIId = customerBank.UPIId;
                customerBankDto.CreatedBy = customerBank.CreatedBy;
                customerBankDto.CreatedDate = customerBank.CreatedDate;
                customerBankDto.IsDeleted = customerBank.IsDeleted;
                customerBankDto.ModifiedBy = customerBank.ModifiedBy;
                customerBankDto.ModifiedDate = customerBank.ModifiedDate;
            }
            return customerBankDto;

        }

        public static void ConvertToCustomerBankEntity(ref CustomerBank customerBank, CustomerBankDTO customerBankDto, bool isUpdate)
        {
            
            customerBank.CustomerId = customerBankDto.CustomerId;
            if (String.IsNullOrWhiteSpace(customerBankDto.IFSCCode) == false)
                customerBank.IFSCCode = customerBankDto.IFSCCode;
            if (String.IsNullOrWhiteSpace(customerBankDto.AccountNo) == false)
                customerBank.AccountNo = customerBankDto.AccountNo;
            if (String.IsNullOrWhiteSpace(customerBankDto.Branch) == false)
              customerBank.Branch = customerBankDto.Branch;
            if (String.IsNullOrWhiteSpace(customerBankDto.BankName) == false)
              customerBank.BankName = customerBankDto.BankName;
            if (String.IsNullOrWhiteSpace(customerBankDto.AccountHolderName) == false)
                customerBank.AccountHolderName = customerBankDto.AccountHolderName;
            if (String.IsNullOrWhiteSpace(customerBankDto.UPIId) == false)
                customerBank.UPIId = customerBankDto.UPIId;
       


        }
    }
}
