using System;
using System.Collections.Generic;
using System.Configuration;
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

        if (string.IsNullOrEmpty(jewelryType))
        {
            lblMessage.Text = "Jewelry not available";
            return;
        }

        // Construct SQL query to select jewelries of the specified type
        string query = "SELECT JewelryID, Name, Price, PictureUrl FROM Jewelries WHERE Type = @Type";

        SqlConnection connection = DatabaseHelper.GetOpenConnection();

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Type", jewelryType);

        SqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            lblMessage.Text = "Jewelry not available";
        }
        else
        {
            // Bind the data to the Repeater control
            rptJewelries.DataSource = reader;
            rptJewelries.DataBind();
        }
        reader.Close();
        DatabaseHelper.CloseConnection(connection);
    }

    protected void btnAddToCart_Click(object sender, EventArgs e)
    {
        Button btnAddToCart = (Button)sender;
        string jewelryId = btnAddToCart.CommandArgument;

        bool isAdded = AddJewelryToCart(jewelryId);

        if (isAdded)
            lblMessage.Text = "jewelery added to the cart successfully";
    }

    public bool AddJewelryToCart(string jewelryId)
    {
        SqlConnection connection = DatabaseHelper.GetOpenConnection();

        // Query the database to get the name and price of the jewelry item
        string query = "SELECT Name, Price FROM Jewelries WHERE JewelryID = @JewelryID";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@JewelryID", jewelryId);
        SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            string name = reader["Name"].ToString();
            decimal price = Convert.ToDecimal(reader["Price"]);

            reader.Close();
            DatabaseHelper.CloseConnection(connection);

            // Create a CartItem object
            CartItem item = new CartItem
            {
                JewelryId = jewelryId,
                Name = name,
                Price = price
            };

            // Retrieve cart from session or create a new cart
            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

            // Add item to cart
            cart.Add(item);

            // Update session
            Session["Cart"] = cart;

            return true;
        }
        return false;
    }
}