using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DatabaseHelper
/// </summary>
public static class DatabaseHelper
{
    private static readonly string ConnectionString;

    static DatabaseHelper()
    {
        string path = HttpContext.Current.Server.MapPath("~/App_Data/");
        path += "Database.mdf";
        ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=" + path + ";Integrated Security=True;";
    }

    public static SqlConnection GetOpenConnection()
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();
        return connection;
    }

    public static void CloseConnection(SqlConnection connection)
    {
        if (connection != null && connection.State == ConnectionState.Open)
        {
            connection.Close();
        }
    }

    public static string GetUserFullName(string username)
    {
        // Get connection to database
        SqlConnection connection = GetOpenConnection();

        // SQL query to get the user first name and last name
        string query = "SELECT FirstName, LastName FROM Users WHERE UserName = @UserName";

        SqlCommand command = new SqlCommand(query, connection);

        // Add parameters to the query
        command.Parameters.AddWithValue("@UserName", username);

        // Execute the quert
        SqlDataReader reader = command.ExecuteReader();

        string fullName = null;
        // if results were found
        if (reader.Read())
        {
            // Retrieve user's first and last name
            string firstName = reader["FirstName"].ToString();
            string lastName = reader["LastName"].ToString();

            // Close the reader
            reader.Close();

            fullName = $"{firstName} {lastName}";
        }

        // Close connection to database
        CloseConnection(connection);

        // return the full name
        return fullName;
    }
}