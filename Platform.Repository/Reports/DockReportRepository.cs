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
    public class DockReportRepository
    {
        PlatformDBEntities _repository;
        public DockReportRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }


        public DockCollectionSummaryDTO DockCollectionSummaryByDate(DateTime collectionStartDate,DateTime collectionEndDate,
            int collectionStartShift,int collectionEndShift,int milkType)
        {
            DockCollectionSummaryDTO dockCollectionSummaryDTO = new DockCollectionSummaryDTO();
            dockCollectionSummaryDTO.CollectionFromDate = collectionStartDate;
            dockCollectionSummaryDTO.CollectionToDate = collectionEndDate;
            dockCollectionSummaryDTO.dockCollectionSummaryListDTO = new List<DockCollectionSummaryListDTO>();

            // Create a SQL command to execute the sproc 
            var cmd = _repository.Database.Connection.CreateCommand();
            cmd.CommandText = "[dbo].[DockCollectionSummaryByDate]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CollectionStartDate", SqlDbType.DateTime, 4));
            cmd.Parameters.Add(new SqlParameter("@CollectionEndDate", SqlDbType.DateTime, 4));
            cmd.Parameters.Add(new SqlParameter("@CollectionStartShift", SqlDbType.Int, 4));
            cmd.Parameters.Add(new SqlParameter("@CollectionEndShift", SqlDbType.Int, 4));
            cmd.Parameters.Add(new SqlParameter("@MilkType", SqlDbType.Int, 4));
            cmd.Parameters["@CollectionStartDate"].Value = collectionStartDate;
            cmd.Parameters["@CollectionEndDate"].Value = collectionEndDate;
            cmd.Parameters["@CollectionStartShift"].Value = collectionStartShift;
            cmd.Parameters["@CollectionEndShift"].Value = collectionEndShift;
            cmd.Parameters["@MilkType"].Value = milkType;

            try
            {
                // Run the sproc  
                _repository.Database.Connection.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                        dockCollectionSummaryDTO.dockCollectionSummaryListDTO.Add(
                            new DockCollectionSummaryListDTO()
                            {
                                CollectionDate = Convert.ToDateTime(reader["CollectionDate"]),
                                 AvgFAT =Convert.ToDecimal(reader["AvgFAT"]),
                                 AvgCLR= Convert.ToDecimal(reader["AvgCLR"]),
                                AvgRatePerUnit = Convert.ToDecimal(reader["AvgRatePerUnit"]),
                                RejectedQuantity = Convert.ToDecimal(reader["TotalRejectedQuantity"]),
                                MiilkType =((ReportMilkTypeEnum)Convert.ToInt32(reader["ProductId"])).ToString(),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                Commission = Convert.ToDecimal(reader["Commission"]),
                                Shift = ((ReportShiftEnum)Convert.ToInt32(reader["ShiftId"])).ToString(),
                                TotalQuantity = Convert.ToDecimal(reader["TotalQuantity"]),
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                TotalVLC = Convert.ToInt32(reader["VLCCount"])
                            });
                }
            }



            finally
            {
                _repository.Database.Connection.Close();
            }

            return dockCollectionSummaryDTO;
        }


        public DockCollectionSummaryByVLCDTO DockCollectionSummaryByVLC(int vlcId, DateTime collectionStartDate, DateTime collectionEndDate,
             int collectionStartShift, int collectionEndShift, int milkType)
        {

            DockCollectionSummaryByVLCDTO dockCollectionSummaryByVLCDTO = new DockCollectionSummaryByVLCDTO();
            dockCollectionSummaryByVLCDTO.CollectionFromDate = collectionStartDate;
            dockCollectionSummaryByVLCDTO.CollectionToDate = collectionEndDate;
            dockCollectionSummaryByVLCDTO.VLCId = vlcId;
           
                dockCollectionSummaryByVLCDTO.dockCollectionSummaryListByVLCDTO = new List<DockCollectionSummaryListByVLCDTO>();

                // Create a SQL command to execute the sproc 
                var cmd = _repository.Database.Connection.CreateCommand();
                cmd.CommandText = "[dbo].[DockCollectionSummaryByVLC]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CollectionStartDate", SqlDbType.DateTime, 4));
                cmd.Parameters.Add(new SqlParameter("@CollectionEndDate", SqlDbType.DateTime, 4));
            cmd.Parameters.Add(new SqlParameter("@CollectionStartShift", SqlDbType.Int, 4));
            cmd.Parameters.Add(new SqlParameter("@CollectionEndShift", SqlDbType.Int, 4));
            cmd.Parameters.Add(new SqlParameter("@MilkType", SqlDbType.Int, 4));

            cmd.Parameters.Add(new SqlParameter("@VLCId", SqlDbType.Int, 4));
            cmd.Parameters["@CollectionStartDate"].Value = collectionStartDate;
            cmd.Parameters["@CollectionEndDate"].Value = collectionEndDate;
            cmd.Parameters["@CollectionStartShift"].Value = collectionStartShift;
            cmd.Parameters["@CollectionEndShift"].Value = collectionEndShift;
            cmd.Parameters["@MilkType"].Value = milkType;
            cmd.Parameters["@VLCId"].Value = vlcId;
            try
            {
                // Run the sproc  
                _repository.Database.Connection.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                        dockCollectionSummaryByVLCDTO.dockCollectionSummaryListByVLCDTO.Add(
                            new DockCollectionSummaryListByVLCDTO()
                            {


                                CollectionDate = Convert.ToDateTime(reader["CollectionDate"]),
                                AvgFAT = Convert.ToDecimal(reader["AvgFAT"]),
                                AvgCLR = Convert.ToDecimal(reader["AvgCLR"]),
                                AvgRatePerUnit = Convert.ToDecimal(reader["AvgRatePerUnit"]),
                                RejectedQuantity = Convert.ToDecimal(reader["TotalRejectedQuantity"]),
                                MiilkType = ((ReportMilkTypeEnum)Convert.ToInt32(reader["ProductId"])).ToString(),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                Commission = Convert.ToDecimal(reader["Commission"]),
                                Shift = ((ReportShiftEnum)Convert.ToInt32(reader["ShiftId"])).ToString(),
                                TotalQuantity = Convert.ToDecimal(reader["TotalQuantity"]),
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                TotalCan = Convert.ToInt32(reader["TotalCan"]),
                                TotalRejectedCan=Convert.ToInt32(reader["TotalRejectedCan"])                               
                                
                            });
                }


            }
            finally
            {
                _repository.Database.Connection.Close();
            }

                return dockCollectionSummaryByVLCDTO;
            
        }

        public VLCExpenseSummaryDTO VLCExpenseSummary(DateTime startDate, DateTime endDate)
        {

            VLCExpenseSummaryDTO vLCExpenseSummaryDTO = new VLCExpenseSummaryDTO();
            vLCExpenseSummaryDTO.FromDate = startDate;
            vLCExpenseSummaryDTO.ToDate = endDate;

            vLCExpenseSummaryDTO.vlcExpenseSummaryDTOList = new List<VLCExpenseSummaryDetailDTO>();

            // Create a SQL command to execute the sproc 
            var cmd = _repository.Database.Connection.CreateCommand();
            cmd.CommandText = "[dbo].[VLCExpenseSummaryByVLC]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime, 4));
            cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime, 4));
            cmd.Parameters["@StartDate"].Value = startDate;
            cmd.Parameters["@EndDate"].Value = endDate;
            try
            {
                // Run the sproc  
                _repository.Database.Connection.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                        vLCExpenseSummaryDTO.vlcExpenseSummaryDTOList.Add(
                            new VLCExpenseSummaryDetailDTO()
                            {


                                ExpenseDate = Convert.ToDateTime(reader["ExpenseDate"]),
                                ExpenseReason = Convert.ToString((VLCExpenseEnum)reader["ExpenseReason"]),
                                VLCId = Convert.ToInt32(reader["VLCId"]),
                                VLCName = Convert.ToString(reader["VLCName"]),
                               CRAmount = Convert.ToDecimal(reader["PaymentCrAmount"]),
                               DRAmount= Convert.ToDecimal(reader["PaymentDrAmount"]),
                                Comments= Convert.ToString(reader["ExpenseComments"])


                            });
                }


            }
            finally
            {
                _repository.Database.Connection.Close();
            }

            return vLCExpenseSummaryDTO;

        }

        //public DockCollectionSummaryDetailByVLCDTO DockCollectionSummaryDetailByVLC(int VlcId, DateTime collectionStartDate, DateTime collectionEndDate)
        //{

        //    DockCollectionSummaryDetailByVLCDTO dockCollectionSummaryDetailByVLCDTO = new DockCollectionSummaryDetailByVLCDTO();
        //    dockCollectionSummaryDetailByVLCDTO.CollectionFromDate = collectionStartDate;
        //    dockCollectionSummaryDetailByVLCDTO.CollectionToDate = collectionEndDate;
        //    dockCollectionSummaryDetailByVLCDTO.VLCId = VlcId;

        //    dockCollectionSummaryDetailByVLCDTO.dockCollectionSummaryDetailByVLCListDTO = new List<DockCollectionSummaryDetailByVLCListDTO>();

        //    // Create a SQL command to execute the sproc 
        //    var cmd = _repository.Database.Connection.CreateCommand();
        //    cmd.CommandText = "[dbo].[DockCollectionSummaryDetailByVLC]";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add(new SqlParameter("@CollectionStartDate", SqlDbType.DateTime, 4));
        //    cmd.Parameters.Add(new SqlParameter("@CollectionEndDate", SqlDbType.DateTime, 4));

        //    cmd.Parameters.Add(new SqlParameter("@VLCId", SqlDbType.Int, 4));
        //    cmd.Parameters["@CollectionStartDate"].Value = collectionStartDate;
        //    cmd.Parameters["@CollectionEndDate"].Value = collectionEndDate;
        //    cmd.Parameters["@VLCId"].Value = VlcId;
        //    try
        //    {
        //        // Run the sproc  
        //        _repository.Database.Connection.Open();
        //        var reader = cmd.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //                dockCollectionSummaryDetailByVLCDTO.dockCollectionSummaryDetailByVLCListDTO.Add(
        //                    new DockCollectionSummaryDetailByVLCListDTO()
        //                    {
        //                        CollectionDate = Convert.ToDateTime(reader["CollectionDate"]),
        //                        MilkType =(ReportMilkTypeEnum) Convert.ToInt32(reader["ProductId"]) ,
        //                        TotalCan = Convert.ToInt32(reader["TotalCan"]),
        //                        Fat = Convert.ToDecimal(reader["Fat"]),
        //                        CLR = Convert.ToDecimal(reader["CLR"]),
        //                        TotalRejectedCan = Convert.ToInt32(reader["TotalRejectedCan"]),
        //                        RejectedQuantity = Convert.ToDecimal(reader["TotalRejectedQuantity"]),
        //                        //Amount = Convert.ToDecimal(reader["Amount"]),
        //                        //Commission = Convert.ToDecimal(reader["Commission"]),
        //                        Shift = Convert.ToInt32(reader["ShiftId"]) == 1 ? "Morning" : "Evening",
        //                        TotalQuantity = Convert.ToDecimal(reader["TotalQuantity"]),
        //                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
        //                        RatePerUnit = Convert.ToDecimal(reader["RatePerUnit"])
        //                    });
        //        }
        //    }





        //    finally
        //    {
        //        _repository.Database.Connection.Close();
        //    }

        //    return dockCollectionSummaryDetailByVLCDTO;

        //}

    }



}
