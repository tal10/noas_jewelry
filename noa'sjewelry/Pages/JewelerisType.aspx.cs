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
        lblGreeting.Text = Utils.GetGreeting(Session);

        // האם מדובר בטעינה הראשונה של העמוד
        if (!IsPostBack)
        {
            // טעינת התכשיטים 
            LoadJewelries();
        }
    }

    // פעולה לטעינת התכשיטים בהתאם לסוג התכשיט
    private void LoadJewelries()
    {
        // קבלת הפרמטת type
        // שהוא בעצם סוג התכשיט
        string jewelryType = Request.QueryString["type"];

        // אם הסוג תכשיט לא קיים
        if (string.IsNullOrEmpty(jewelryType))
        {
            // הצגת הודעת שגיאה
            lblMessage.Text = "Jewelry not available";
            return;
        }

        // Construct SQL query to select jewelries of the specified type
        string query = "SELECT JewelryID, Name, Price, PictureUrl FROM Jewelries WHERE Type = @Type";

        // פתיחת חיבור מול המסד נתונים  
        SqlConnection connection = DatabaseHelper.GetOpenConnection();

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Type", jewelryType);

        // הרצת השאילתה המחזירה את כל התכשיטים של סוג מסוים
        SqlDataReader reader = command.ExecuteReader();

        // אם לא קיימים נתונים
        if (!reader.HasRows)
        {
            lblMessage.Text = "Jewelry not available";
        }
        else
        {
            // לוקחים את הנתונים שחזרו ממסד הנתונים ומכניסים אותם לתוך הריפיטר
            rptJewelries.DataSource = reader;
            rptJewelries.DataBind();
        }

        reader.Close();
        DatabaseHelper.CloseConnection(connection);
    }

    public bool IsUserLoggedIn()
    {
        return ((Session["userName"] != null) ||
                (Session["isAdmin"] != null && (bool)Session["isAdmin"] == true));
    }

    protected void btnAddToCart_Click(object sender, EventArgs e)
    {
        Button btnAddToCart = (Button)sender;
        string jewelryId = btnAddToCart.CommandArgument;
        string userName = Session["userName"].ToString();

        bool isAdded = AddJewelryToCart(jewelryId, userName);

        if (isAdded)
            lblMessage.Text = "jewelery added to the cart successfully";
    }

    public bool AddJewelryToCart(string jewelryId, string userName)
    {
        // פתיחת חיבור מול המסד נתונים
        SqlConnection connection = DatabaseHelper.GetOpenConnection();

        // Query the database to get the name and price of the jewelry item
        string query = "SELECT Count(*) FROM Jewelries WHERE JewelryID = @JewelryID";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@JewelryID", jewelryId);
        bool isJewelryExist = (int)command.ExecuteScalar() > 0;

        if (isJewelryExist == false)
            return false;

        // Check if the item already exists in the cart
        string checkQuery = "SELECT Quantity FROM CartItems WHERE UserName = @UserName AND JewelryID = @JewelryID";
        SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
        checkCommand.Parameters.AddWithValue("@UserName", userName);
        checkCommand.Parameters.AddWithValue("@JewelryID", jewelryId);

        SqlDataReader reader = checkCommand.ExecuteReader();
        if (reader.Read())
        {
            // If item exists, update the quantity
            int quantity = Convert.ToInt32(reader["Quantity"]);
            reader.Close();

            string updateQuery = "UPDATE CartItems SET Quantity = @Quantity WHERE UserName = @UserName AND JewelryID = @JewelryID";
            SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
            updateCommand.Parameters.AddWithValue("@Quantity", quantity + 1);
            updateCommand.Parameters.AddWithValue("@UserName", userName);
            updateCommand.Parameters.AddWithValue("@JewelryID", jewelryId);

            int rowsAffected = updateCommand.ExecuteNonQuery();
            DatabaseHelper.CloseConnection(connection);
            return rowsAffected > 0;
        }
        else
        {
            // If item does not exist, insert a new row
            reader.Close();

            string insertQuery = "INSERT INTO CartItems (UserName, JewelryID, Quantity) VALUES (@UserName, @JewelryID, @Quantity)";
            SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
            insertCommand.Parameters.AddWithValue("@UserName", userName);
            insertCommand.Parameters.AddWithValue("@JewelryID", jewelryId);
            insertCommand.Parameters.AddWithValue("@Quantity", 1);

            int rowsAffected = insertCommand.ExecuteNonQuery();
            DatabaseHelper.CloseConnection(connection);
            return rowsAffected > 0;
        }

        /*
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
        }
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
        */
    }
}