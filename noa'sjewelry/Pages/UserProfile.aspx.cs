using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["userName"] == null) && (Session["isAdmin"] == null))
        {
            // User is not logged in, redirect to login page
            Response.Redirect("Login.aspx");
        }

        lblGreeting.Text = Utils.GetGreeting(Session);

        if (!IsPostBack)
        {
            // Load user details
            LoadUserDetails();
        }
    }

    protected void LoadUserDetails()
    {
        // Assuming you have a session variable storing the current user's username
        string userName = Session["UserName"] as string;

        if (!string.IsNullOrEmpty(userName))
        {
            // Fetch user details from the database
            SqlConnection connection = DatabaseHelper.GetOpenConnection();

            string query = "SELECT FirstName, LastName, Email, PhoneNumber, BirthDate FROM Users WHERE UserName = @UserName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", userName);

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                txtUserName.Text = userName;
                txtFirstName.Text = reader["FirstName"].ToString();
                txtLastName.Text = reader["LastName"].ToString();
                txtEmail.Text = reader["Email"].ToString();
                txtPhoneNumber.Text = reader["PhoneNumber"].ToString();
                txtBirthDate.Text = ((DateTime)reader["BirthDate"]).ToString("yyyy-MM-dd");
            }
            reader.Close();
            DatabaseHelper.CloseConnection(connection);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        // Assuming you have a session variable storing the current user's username
        string userName = Session["UserName"] as string;

        if (!string.IsNullOrEmpty(userName))
        {
            // Update user details in the database
            SqlConnection connection = DatabaseHelper.GetOpenConnection();

            string query = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber, BirthDate = @BirthDate";
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                query += ", Password = @Password";
            }
            query += " WHERE UserName = @UserName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
            command.Parameters.AddWithValue("@LastName", txtLastName.Text);
            command.Parameters.AddWithValue("@Email", txtEmail.Text);
            command.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);
            command.Parameters.AddWithValue("@BirthDate", DateTime.Parse(txtBirthDate.Text));
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                command.Parameters.AddWithValue("@Password", txtPassword.Text);
            }
            command.Parameters.AddWithValue("@UserName", userName);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                // User details updated successfully
                lblMessage.Text = "user detailes updated successfully";
            }
            else
            {
                // Failed to update user details
                // You can display an error message
            }

            DatabaseHelper.CloseConnection(connection);
        }
    }

}