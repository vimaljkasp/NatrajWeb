using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Platform.DTO;
using Platform.Sql;


namespace Platform.Repository
{
    public class VLCReportRepository
    {
        PlatformDBEntities _repository;
        public VLCReportRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }


        public VLCCollectionSummaryDTO GetCollectionSummaryReportByVLC(int vlcId)
        {
            VLCCollectionSummaryDTO vLCCollectionSummaryDTO = new VLCCollectionSummaryDTO();
            vLCCollectionSummaryDTO.VLCId = vlcId;
            vLCCollectionSummaryDTO.vLCCollectionSummaryDtlDTOList = new List<VLCCollectionSummaryDtlDTO>();

            // Create a SQL command to execute the sproc 
            var cmd = _repository.Database.Connection.CreateCommand();
            cmd.CommandText = "[dbo].[CollectionSummaryByVLC]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@VLCId", SqlDbType.Int, 4));
            cmd.Parameters["@VLCId"].Value = vlcId;
            try
            {
                // Run the sproc  
                _repository.Database.Connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                    vLCCollectionSummaryDTO.vLCCollectionSummaryDtlDTOList.Add(
                        new VLCCollectionSummaryDtlDTO()
                        {
                            CollectionDate = Convert.ToDateTime(reader["CollectionDate"]),
                            Shift = Convert.ToInt32(reader["ShiftId"]) == 1 ? "Morning" : "Evening",
                            TotalQuantity = Convert.ToDecimal(reader["TotalQuantity"]),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                            TotalCustomer = Convert.ToInt32(reader["CustomerCount"])
                        });
            }

            finally
            {
                _repository.Database.Connection.Close();
            }

            return vLCCollectionSummaryDTO;
        }


        public CustomerCollectionSummaryDTO GetCollectionSummaryReportByCustomer(int customerId)
        {

            CustomerCollectionSummaryDTO customerCollectionSummaryDTO = new CustomerCollectionSummaryDTO();
            customerCollectionSummaryDTO.CustomerId = customerId;
            customerCollectionSummaryDTO.customerCollectionSummaryDtlDTOList = new List<CustomerCollectionSummaryDtlDTO>();
            // Create a SQL command to execute the sproc 
            var cmd = _repository.Database.Connection.CreateCommand();
            cmd.CommandText = "[dbo].[CollectionSummaryByCustomer]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CustomerId", SqlDbType.Int, 4));
            cmd.Parameters["@CustomerId"].Value = customerId;
            try
            {
                _repository.Database.Connection.Open();

                // Run the sproc  
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                    customerCollectionSummaryDTO.customerCollectionSummaryDtlDTOList.Add(
                        new CustomerCollectionSummaryDtlDTO()
                        {
                            CollectionDate = Convert.ToDateTime(reader["CollectionDate"]),
                            Shift = Convert.ToInt32(reader["ShiftId"]) == 1 ? "Morning" : "Evening",
                            TotalQuantity = Convert.ToDecimal(reader["TotalQuantity"]),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),

                        });
            }

            finally
            {
                _repository.Database.Connection.Close();
            }

            return customerCollectionSummaryDTO;
        }


        public VLCPaymentStatementDTO VLCPaymentSummaryByDate(int vlcId, DateTime startDate, DateTime endDate)
        {

            VLCPaymentStatementDTO vLCPaymentStatementDTO = new VLCPaymentStatementDTO();
            vLCPaymentStatementDTO.PaymentStartDate = startDate;
            vLCPaymentStatementDTO.PaymentEndDate = endDate;
            vLCPaymentStatementDTO.VLCId = vlcId;

            vLCPaymentStatementDTO.vlcPaymentStatmentListDTO = new List<VLCPaymentStatmentListDTO>();

            // Create a SQL command to execute the sproc 
            var cmd = _repository.Database.Connection.CreateCommand();
            cmd.CommandText = "[dbo].[VLCPaymentStatmentSummaryByDate]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PaymentFromDate", SqlDbType.DateTime, 4));
            cmd.Parameters.Add(new SqlParameter("@PaymentToDate", SqlDbType.DateTime, 4));

            cmd.Parameters.Add(new SqlParameter("@VLCId", SqlDbType.Int, 4));
            cmd.Parameters["@PaymentFromDate"].Value = startDate;
            cmd.Parameters["@PaymentToDate"].Value = endDate;
            cmd.Parameters["@VLCId"].Value = vlcId;
            try
            {
                // Run the sproc  
                _repository.Database.Connection.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                        vLCPaymentStatementDTO.vlcPaymentStatmentListDTO.Add(
                            new VLCPaymentStatmentListDTO()
                            {
                                PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                                PaymentCRAmoumt = Convert.ToDecimal(reader["PaymentCRAmount"]),
                                PaymentDRAmoumt = Convert.ToDecimal(reader["PaymentDRAmount"]),
                                PaymentReceivedBy = Convert.ToString(reader["PaymentReceivedBy"]),
                                PaymentMode =(PaymentModeEnum) Convert.ToInt32(reader["PaymentMode"]),
                                PaymentComments=Convert.ToString(reader["PaymentComments"])
                               
                            });
                }


            }
            finally
            {
                _repository.Database.Connection.Close();
            }

            return vLCPaymentStatementDTO;

        }
    }

     

    
    
}
