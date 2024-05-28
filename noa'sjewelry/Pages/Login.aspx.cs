using System;
using System.Data.SqlClient;
using System.Web;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["isAdmin"] != null && (bool)Session["isAdmin"] == true) ||
            (Session["userName"] != null))
        {
            // Redirect to login page or unauthorized access page
            Response.Redirect("Default.aspx");
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        // Take user input
        string username = txtUsername.Text;
        string password = txtPassword.Text;

        // check if admin
        if (username == "admin" && password == "password")
        {
            Session["isAdmin"] = true;
            Response.Redirect("Default.aspx");
        }
        else
        {
            // Creating connection to the database
            SqlConnection connection = DatabaseHelper.GetOpenConnection();

            // SQL query to check if username and password exist in the database
            string query = "SELECT COUNT(*) FROM Users WHERE UserName = @UserName AND Password = @Password";

            SqlCommand command = new SqlCommand(query, connection);

            // Add parameters to the query
            command.Parameters.AddWithValue("@UserName", username);
            command.Parameters.AddWithValue("@Password", password);

            // Execute the query against the database
            int userCount = (int)command.ExecuteScalar();

            DatabaseHelper.CloseConnection(connection);

            if (userCount == 1)
            {
                Session["userName"] = username;
                Session["userFullName"] = DatabaseHelper.GetUserFullName(username);

                // Login successful, redirect to home page or dashboard
                Response.Redirect("Default.aspx");
            }
            else
            {
                // Login failed, display error message
                lblMessage.Text = "Invalid username or password. Please try again.";
            }
        }
    }
}