using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if isAdmin session variable exists
        if (Session["isAdmin"] == null || !(bool)Session["isAdmin"])
        {
            // Redirect to login page or unauthorized access page
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            // Load user details
            LoadUserDetails();
        }
    }

    public void LoadUserDetails()
    {
        string query = "SELECT UserName, FirstName, LastName, Email, PhoneNumber, BirthDate FROM Users";

        SqlConnection connection = DatabaseHelper.GetOpenConnection();
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataTable dtUsers = new DataTable();
        adapter.Fill(dtUsers);

        if (dtUsers.Rows.Count > 0)
        {
            GridViewUsers.DataSource = dtUsers;
            GridViewUsers.DataBind();
        }

        DatabaseHelper.CloseConnection(connection);
    }

    protected void GridViewUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string usernameToDelete = e.CommandArgument.ToString();
            DeleteUser(usernameToDelete);
            LoadUserDetails();
        }
    }

    protected void GridViewUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // This method can be left empty since we are handling deletion in RowCommand
    }

    protected void DeleteUser(string username)
    {
        string query = "DELETE FROM Users WHERE UserName = @UserName";

        SqlConnection connection = DatabaseHelper.GetOpenConnection();
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@UserName", username);
        command.ExecuteNonQuery();
        DatabaseHelper.CloseConnection(connection);
    }
}