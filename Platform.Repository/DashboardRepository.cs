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
    public class DashboardRepository
    {
        PlatformDBEntities _repository;
        public DashboardRepository(PlatformDBEntities repository)
        {
            _repository = repository;
        }


        public Int32 NextNumberGenerator(string enitityCode)
        {
            int nextNumber = 0;

            // Create a SQL command to execute the sproc 
            var cmd = _repository.Database.Connection.CreateCommand();
            cmd.CommandText = "[dbo].[GetNextEntityNumber]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@EntityName", SqlDbType.NVarChar, 50));
            cmd.Parameters.Add(new SqlParameter("@NextNumber", SqlDbType.Int, 4));
            cmd.Parameters["@NextNumber"].Direction = ParameterDirection.Output;
            cmd.Parameters["@NextNumber"].Value = nextNumber;
            cmd.Parameters["@EntityName"].Value = enitityCode;
            try
            {
                _repository.Database.Connection.Open();

                // Run the sproc  
                var reader = cmd.ExecuteReader();
                while (reader.Read())

                { }
                nextNumber = (int)cmd.Parameters[1].Value;
            }

            finally
            {
                _repository.Database.Connection.Close();
            }

            return nextNumber;
        }

        public string GenerateVLCCode(string VillageName)
        {
            string VLCCode= string.Empty;
            // Create a SQL command to execute the sproc 
            var cmd = _repository.Database.Connection.CreateCommand();
            cmd.CommandText = "[dbo].[GenerateVLCCode]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Village", SqlDbType.NVarChar, 50));
            cmd.Parameters.Add(new SqlParameter("@VLCCode", SqlDbType.NVarChar, 50));
            cmd.Parameters["@VLCCode"].Direction = ParameterDirection.Output;
            cmd.Parameters["@Village"].Value = VillageName;            
            try
            {
                _repository.Database.Connection.Open();
                // Run the sproc  
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                { }
                VLCCode =Convert.ToString(cmd.Parameters[1].Value);
            }
            finally
            {
                _repository.Database.Connection.Close();
            }
            return VLCCode;
        }

        public List<TopTenVLCWalletBalance> GetTopTenVLCWalletBalance()
        {
            List<TopTenVLCWalletBalance> lstTopTenVLCWalletBalances = new List<TopTenVLCWalletBalance>();


            // Create a SQL command to execute the sproc 
            var cmd = _repository.Database.Connection.CreateCommand();
            cmd.CommandText = "[dbo].[GetTopTenVLCWalletBalance]";
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                _repository.Database.Connection.Open();

                // Run the sproc  
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string VLCName = reader["VLCName"].ToString();
                    string WalletBalance = reader["WalletBalance"].ToString();
                    string AmountDueDate = reader["AmountDueDate"].ToString();
                    TopTenVLCWalletBalance topTenVLCWalletBalance = new TopTenVLCWalletBalance();
                    topTenVLCWalletBalance.AmountDueDate = Convert.ToDateTime(AmountDueDate);
                    topTenVLCWalletBalance.VLCName = VLCName;
                    topTenVLCWalletBalance.WalletBalance = WalletBalance;
                    lstTopTenVLCWalletBalances.Add(topTenVLCWalletBalance);
                }

            }

            finally
            {
                _repository.Database.Connection.Close();
            }

            return lstTopTenVLCWalletBalances;

        }

        public List<TopTenDockCollectionDateWise> GetTopTenDockCollectionDateWise()
        {

            List<TopTenDockCollectionDateWise> lstTopTenDockCollectionDateWise = new List<TopTenDockCollectionDateWise>();

            // Create a SQL command to execute the sproc 
            var cmd = _repository.Database.Connection.CreateCommand();
            cmd.CommandText = "[dbo].[GetTopTenDockCollectionDateWise]";
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                _repository.Database.Connection.Open();

                // Run the sproc  
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string Amount = reader["Amount"].ToString();
                    string CollectionDateTime = reader["CollectionDateTime"].ToString();
                    string Comments = reader["Comments"].ToString();
                    string Commission = reader["Commission"].ToString();
                    string ReceiverName = reader["ReceiverName"].ToString();
                    string RejectedQuantity = reader["RejectedQuantity"].ToString();
                    string ShiftId = reader["ShiftId"].ToString();
                    string TotalAmount = reader["TotalAmount"].ToString();
                    string TotalCan = reader["TotalCan"].ToString();
                    string TotalQuantity = reader["TotalQuantity"].ToString();
                    string TotalRejectedCan = reader["TotalRejectedCan"].ToString();


                    TopTenDockCollectionDateWise topTenDockCollectionDateWise = new TopTenDockCollectionDateWise();
                    topTenDockCollectionDateWise.Amount = Convert.ToDecimal(Amount);
                    topTenDockCollectionDateWise.CollectionDateTime = Convert.ToDateTime(CollectionDateTime);
                    topTenDockCollectionDateWise.Comments = Comments;
                    topTenDockCollectionDateWise.Commission = Convert.ToDecimal(Commission);
                    topTenDockCollectionDateWise.ReceiverName = ReceiverName;
                    topTenDockCollectionDateWise.RejectedQuantity = Convert.ToDecimal(RejectedQuantity);
                    ShiftEnum shiftEnum;
                    Enum.TryParse(ShiftId, out shiftEnum);
                    topTenDockCollectionDateWise.ShiftId = shiftEnum;
                    topTenDockCollectionDateWise.TotalAmount = Convert.ToDecimal(TotalAmount);
                    topTenDockCollectionDateWise.TotalCan = Convert.ToInt16(TotalCan);
                    topTenDockCollectionDateWise.TotalQuantity = Convert.ToDecimal(TotalQuantity);
                    topTenDockCollectionDateWise.TotalRejectedCan = Convert.ToInt16(TotalRejectedCan);

                    lstTopTenDockCollectionDateWise.Add(topTenDockCollectionDateWise);
                }

            }

            finally
            {
                _repository.Database.Connection.Close();
            }

            return lstTopTenDockCollectionDateWise;
        }

        public List<TopDCOrderDisplay> GetTopTenOrders()
        {

            List<TopDCOrderDisplay> lstTopDCOrderDisplay = new List<TopDCOrderDisplay>();

            // Create a SQL command to execute the sproc 
            var cmd = _repository.Database.Connection.CreateCommand();
            cmd.CommandText = "[dbo].[GetTopTenOrders]";
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                _repository.Database.Connection.Open();

                // Run the sproc  
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string DCName = reader["DCName"].ToString();
                    string DCOrderNumber = reader["DCOrderNumber"].ToString();
                    string DeliveryExpectedDate = reader["DeliveryExpectedDate"].ToString();
                    string OrderComments = reader["OrderComments"].ToString();
                    string OrderDate = reader["OrderDate"].ToString();
                    string OrderTotalPrice = reader["OrderTotalPrice"].ToString();

                    //string ShiftId = reader["ShiftId"].ToString();
                    //string TotalAmount = reader["TotalAmount"].ToString();
                    //string TotalCan = reader["TotalCan"].ToString();
                    //string TotalQuantity = reader["TotalQuantity"].ToString();
                    //string TotalRejectedCan = reader["TotalRejectedCan"].ToString();


                    TopDCOrderDisplay topDCOrderDisplay = new TopDCOrderDisplay();
                    topDCOrderDisplay.DCName = DCName;
                    topDCOrderDisplay.DCOrderNumber = DCOrderNumber;
                    topDCOrderDisplay.DeliveryExpectedDate = Convert.ToDateTime(DeliveryExpectedDate);
                    topDCOrderDisplay.OrderComments = OrderComments;
                    topDCOrderDisplay.OrderDate = Convert.ToDateTime(OrderDate);
                    topDCOrderDisplay.OrderTotalPrice = OrderTotalPrice;


                    lstTopDCOrderDisplay.Add(topDCOrderDisplay);
                }

            }

            finally
            {
                _repository.Database.Connection.Close();
            }

            return lstTopDCOrderDisplay;
        }

        public Dictionary<string, decimal> GetDashboardDetails()
        {
            Dictionary<string, decimal> dict = new Dictionary<string, decimal>();
            
            // Create a SQL command to execute the sproc 
            var cmd = _repository.Database.Connection.CreateCommand();
            cmd.CommandText = "[dbo].[GetDashboardDetails]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TotalCustomers", SqlDbType.Int, 4));
            cmd.Parameters["@TotalCustomers"].Direction = ParameterDirection.Output;

            cmd.Parameters.Add(new SqlParameter("@TotalMilkCollectionLtr", SqlDbType.Decimal, 4));
            cmd.Parameters["@TotalMilkCollectionLtr"].Direction = ParameterDirection.Output;

            cmd.Parameters.Add(new SqlParameter("@TotalVLC", SqlDbType.Int, 4));
            cmd.Parameters["@TotalVLC"].Direction = ParameterDirection.Output;

            cmd.Parameters.Add(new SqlParameter("@TotalOrders", SqlDbType.Int, 4));
            cmd.Parameters["@TotalOrders"].Direction = ParameterDirection.Output;

            try
            {
                _repository.Database.Connection.Open();

                // Run the sproc  
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                }
                int TotalCustomers = (int)cmd.Parameters["@TotalCustomers"].Value;
                decimal TotalMilkCollectionLtr = Convert.ToDecimal(cmd.Parameters["@TotalMilkCollectionLtr"].Value);
                int TotalVLC = (int)cmd.Parameters["@TotalVLC"].Value;
                int TotalOrders = (int)cmd.Parameters["@TotalOrders"].Value;

                dict.Add("TCUST", TotalCustomers);
                dict.Add("TVLC", TotalVLC);
                dict.Add("TMILK", TotalMilkCollectionLtr);
                dict.Add("TORD", TotalOrders);
            }

            finally
            {
                _repository.Database.Connection.Close();
            }

            return dict;
        }
    }


}
