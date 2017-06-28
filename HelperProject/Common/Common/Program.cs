using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Common
{
    public class Program
    {
        void Main(string[] args)
        {
            SelectWithAdoNET();
        }

        void ExecuteProcedureWithAdoNET()
        {
            string connectionString = "";
            string procedureName =
                @"dbo.DoSomething()";
            int paramValue = 5;
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = connection.CreateCommand();

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = procedureName;
                SqlParameter sqlp = new SqlParameter();
                sqlp.ParameterName = "@ValidPassword";
                sqlp.SqlDbType = SqlDbType.Int;
                sqlp.Direction = ParameterDirection.Output;
                sqlp.Value = 0;
                command.Parameters.Add(sqlp);
                command.Parameters.Add("@sLogonId", SqlDbType.VarChar).Value = "someone";

                command.ExecuteNonQuery();
                int returnvalue;
                int.TryParse(command.Parameters["@ValidPassword"].Value.ToString(), out returnvalue);
                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.

            }
        }
        void SelectWithAdoNET()
        {
            string connectionString = "";
            string queryString =
                @" SELECT 
                  *
            FROM [dbo].[ISIMS_CLAIMSUMMARY]
            where CLAIMID = @claimId"
                + "ORDER BY UnitPrice DESC;";
            int paramValue = 5;
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@claimId", paramValue);

                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        reader.GetValue<string>("claimid");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }
        }
    }
}
