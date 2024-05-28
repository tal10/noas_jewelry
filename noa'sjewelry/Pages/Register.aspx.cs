using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // If the user already connected - regular user or admin
        if ((Session["isAdmin"] != null && (bool)Session["isAdmin"] == true) ||
            (Session["userName"] != null))
        {
            // Redirect to home page
            Response.Redirect("Default.aspx");
        }
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        // Take all user input
        string userName = txtUsername.Text.Trim();
        string password = txtPassword.Text;
        string confirmPassword = txtConfirmPassword.Text;
        int gender = rbGender.SelectedValue == "Male" ? 1 : 0;
        string firstName = txtFirstName.Text.Trim();
        string lastName = txtLastName.Text.Trim();
        string email = txtEmail.Text.Trim();
        string phoneNumber = txtPhoneNumber.Text.Trim();
        string birthDate = txtBirthdate.Text.Trim();

        // If at least one of the input is empty
        if (userName == "" || password == "" || confirmPassword == "" || firstName == "" || lastName == "" || email == "" || phoneNumber == "" || birthDate == "")
        {
            lblMessage.Text = "All fields must be filled";
            return;
        }

        // Validate password and confirm password are the same
        if (password != confirmPassword)
        {
            lblMessage.Text = "Passwords do not match.";
            return;
        }

        // Check if username already exists
        if (IsUsernameExists(userName))
        {
            lblMessage.Text = $"Username: {userName} already exists. Please choose a different username.";
            return;
        }

        // Creating connection to the database
        SqlConnection connection = DatabaseHelper.GetOpenConnection();

        string query = "INSERT INTO Users (UserName, [Password], Gender, FirstName, LastName, Email, PhoneNumber, BirthDate) " +
                       "VALUES (@Username, @Password, @Gender, @FirstName, @LastName, @Email, @PhoneNumber, @Birthdate)";

        // Set all query input - from the user inputs
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Username", userName);
        command.Parameters.AddWithValue("@Password", password);
        command.Parameters.AddWithValue("@Gender", gender);
        command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = firstName;
        command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = lastName;
        command.Parameters.AddWithValue("@Email", email);
        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
        command.Parameters.AddWithValue("@Birthdate", birthDate);

        // Execute the query - Insert new row into Users table
        int rowsAffected = command.ExecuteNonQuery();

        // Close the connection to the database
        DatabaseHelper.CloseConnection(connection);

        // If user name added successfully
        if (rowsAffected > 0)
        {
            lblMessage.Text = "Registration successful.";
            Session["userName"] = userName;
            Session["userFullName"] = $"{firstName} {lastName}";
        }
        else
        {
            lblMessage.Text = "Registration failed.";
        }
    }

    // Method to check if the username already exists
    private bool IsUsernameExists(string username)
    {
        // Connect to database
        SqlConnection connection = DatabaseHelper.GetOpenConnection();

        string query = "SELECT COUNT(*) FROM Users WHERE UserName = @Username";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Username", username);

        // Execute the query
        int count = (int)command.ExecuteScalar();

        DatabaseHelper.CloseConnection(connection);

        if (count > 0)
            return true;
        return false;
    }
}