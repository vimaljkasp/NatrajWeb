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


    }

    public static class DbDataReaderExtension
    {
        public static string SafeGetString(this DbDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }


    }
}
