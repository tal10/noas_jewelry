using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JewelerisType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadJewelries();
        }
    }

    private void LoadJewelries()
    {
        // Retrieve jewelry type from query string
        string jewelryType = Request.QueryString["type"];

        // Construct SQL query to select jewelries of the specified type
        string query = "SELECT JewelryID, Name, Price, PictureUrl FROM Jewelries WHERE Type = @Type";

        SqlConnection connection = DatabaseHelper.GetOpenConnection();


        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Type", jewelryType);

        SqlDataReader reader = command.ExecuteReader();

        // Bind the data to the Repeater control
        rptJewelries.DataSource = reader;
        rptJewelries.DataBind();

        reader.Close();
        DatabaseHelper.CloseConnection(connection);
    }
}